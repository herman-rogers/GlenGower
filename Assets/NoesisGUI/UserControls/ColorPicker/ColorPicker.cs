using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Noesis.UserControls
{

////////////////////////////////////////////////////////////////////////////////////////////////////
[Noesis.UserControlSource("Assets/NoesisGUI/UserControls/ColorPicker/ColorPicker.xaml")]
public class ColorPicker : Noesis.UserControl
{
    // Dependency properties
    //@{
    public static Noesis.DependencyProperty ColorProperty = Noesis.DependencyProperty.Register("Color",
        typeof(Noesis.SolidColorBrush), typeof(ColorPicker),
        new Noesis.PropertyMetadata(null, new Noesis.PropertyChangedCallback(OnColorChanged)));
    //@}

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public Noesis.SolidColorBrush Color
    {
        get { return GetValue<Noesis.SolidColorBrush>(ColorProperty); }
        set { SetValue<Noesis.SolidColorBrush>(ColorProperty, value); }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public delegate void ColorChangedEventHandler(Noesis.BaseComponent sender, Noesis.EventArgs e);

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public event ColorChangedEventHandler ColorChanged;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    protected virtual void OnColorChanged(Noesis.EventArgs e)
    {
        ColorChangedEventHandler handler = ColorChanged;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public void OnPostInit()
    {
        // Initialize the user control here

        // User control elemetns
        _r = (Noesis.Slider)FindName("R");
        _g = (Noesis.Slider)FindName("G");
        _b = (Noesis.Slider)FindName("B");
        _a = (Noesis.Slider)FindName("A");
        _hsv = (Noesis.UserControls.HSVControl)FindName("HSVControl");

        // Register events
        _r.ValueChanged += this.OnSliderValueChanged;
        _g.ValueChanged += this.OnSliderValueChanged;
        _b.ValueChanged += this.OnSliderValueChanged;
        _a.ValueChanged += this.OnSliderValueChanged;
        _hsv.HSVChanged += this.OnHSVChanged;

        // Initialize the data members
        _isUpdatingColor = false;
        _isUpdatingSliders = false;
        _changingHSV = false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ColorPicker colorPicker = d as ColorPicker;
        if (colorPicker != null)
        {
            Noesis.SolidColorBrush color = colorPicker.GetValue<Noesis.SolidColorBrush>(e.prop);
            colorPicker.OnColorChanged(color);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnColorChanged(Noesis.SolidColorBrush color)
    {
        if (!_isUpdatingColor)
        {
            UpdateSliders(color.Color);
        }

        if (!_changingHSV)
        {
            _hsv.SetRGBA(color.Color);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnSliderValueChanged(float oldValue, float newValue)
    {
        if (!_isUpdatingSliders)
        {
            if (this.Color == null || this.Color.IsFrozen())
            {
                _isUpdatingColor = true;
                this.Color = new Noesis.SolidColorBrush();
                _isUpdatingColor = false;
            }

            UpdateColor(_r.Value, _g.Value, _b.Value, _a.Value);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateColor(float r, float g, float b, float a)
    {
        _isUpdatingColor = true;
        UpdateColor(new Noesis.Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f));

        if (!_changingHSV)
        {
            _hsv.SetRGBA(this.Color.Color);
        }

        _isUpdatingColor = false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateSliders(Noesis.Color color)
    {
        _isUpdatingSliders = true;
        _r.Value = (float)color.Ri;
        _g.Value = (float)color.Gi;
        _b.Value = (float)color.Bi;
        _a.Value = (float)color.Ai;
        _isUpdatingSliders = false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnHSVChanged()
    {
        _changingHSV = true;
        Noesis.Color color = _hsv.GetRGBA();
        UpdateColor(color);
        UpdateSliders(color);
        _changingHSV = false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateColor(Noesis.Color color)
    {
        this.Color.Color = color;
        OnColorChanged(EventArgs.Empty);
    }

    Noesis.Slider _r;
    Noesis.Slider _g;
    Noesis.Slider _b;
    Noesis.Slider _a;

    Noesis.UserControls.HSVControl _hsv;

    bool _isUpdatingColor;
    bool _isUpdatingSliders;
    bool _changingHSV;
}

}

