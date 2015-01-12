using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;


namespace Noesis.UserControls
{

////////////////////////////////////////////////////////////////////////////////////////////////////
[Noesis.UserControlSource("Assets/NoesisGUI/UserControls/HSVControl/HSVControl.xaml")]
public class HSVControl : Noesis.UserControl
{
    // Dependency properties
    //@{
    public static Noesis.DependencyProperty AlphaProperty = Noesis.DependencyProperty.Register("Alpha",
        typeof(float), typeof(HSVControl),
        new Noesis.PropertyMetadata(1.0f));
    public static Noesis.DependencyProperty HueProperty = Noesis.DependencyProperty.Register("Hue",
        typeof(float), typeof(HSVControl),
        new Noesis.PropertyMetadata(180.0f, new Noesis.PropertyChangedCallback(OnHueChanged)));
    public static Noesis.DependencyProperty SaturationProperty = Noesis.DependencyProperty.Register("Saturation",
        typeof(float), typeof(HSVControl),
        new Noesis.PropertyMetadata(0.0f, new Noesis.PropertyChangedCallback(OnSaturationChanged)));
    public static Noesis.DependencyProperty VProperty = Noesis.DependencyProperty.Register("V",
        typeof(float), typeof(HSVControl),
        new Noesis.PropertyMetadata(0.0f, new Noesis.PropertyChangedCallback(OnVChanged)));
    //@}

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public float Alpha
    {
        get { return GetValue<float>(AlphaProperty); }
        set { SetValue<float>(AlphaProperty, value); }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public float Hue
    {
        get { return GetValue<float>(HueProperty); }
        set { SetValue<float>(HueProperty, value); }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public float Saturation
    {
        get { return GetValue<float>(SaturationProperty); }
        set { SetValue<float>(SaturationProperty, value); }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public float V
    {
        get { return GetValue<float>(VProperty); }
        set { SetValue<float>(VProperty, value); }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public delegate void HSVChangedEventHandler();

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public event HSVChangedEventHandler HSVChanged;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    protected virtual void OnHSVChanged()
    {
        HSVChangedEventHandler handler = HSVChanged;
        if (handler != null)
        {
            handler();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public HSVControl()
    {
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public void OnPostInit()
    {
        // Initialize the user control here

        // User control elemetns
        this.colorSpectrum = (Noesis.GradientStop)FindName("ColorSpectrum");
        this.thumbTranslate = (Noesis.TranslateTransform)FindName("ThumbTransform");
        this.spectrum = (Noesis.Slider)FindName("Spectrum");
        this.svGrid = (Noesis.FrameworkElement)FindName("SVGrid");

        // Register events
        this.svGrid.MouseLeftButtonDown += this.OnMouseLeftButtonDown;
        this.svGrid.MouseLeftButtonUp += this.OnMouseLeftButtonUp;
        this.svGrid.MouseMove += this.OnMouseMove;
        this.svGrid.SizeChanged += this.OnSizeChanged;
        this.spectrum.ValueChanged += this.OnSliderValueChange;

        // Initialize the data members
        this.movingSV = false;
        this.updatingSlider = false;
        this.isSettingRGBA = false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private static void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        HSVControl hsvControl = d as HSVControl;
        if (hsvControl != null)
        {
            float newValue = hsvControl.GetValue<float>(e.prop);
            hsvControl.colorSpectrum.Color = HSVToColor(newValue, 1, 1);

            if (!hsvControl.updatingSlider)
            {
                hsvControl.spectrum.Value = newValue;
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private static void OnSaturationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        HSVControl hsvControl = d as HSVControl;
        if (hsvControl != null)
        {
            if (!hsvControl.movingSV)
            {
                float saturation = hsvControl.GetValue<float>(e.prop);
                Noesis.Size size = hsvControl.RenderSize;

                hsvControl.SetThumbPosition(
                    CalculateThumbPosition(saturation, hsvControl.V, size));
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private static void OnVChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        HSVControl hsvControl = d as HSVControl;
        if (hsvControl != null)
        {
            if (!hsvControl.movingSV)
            {
                float v = hsvControl.GetValue<float>(e.prop);
                Noesis.Size size = hsvControl.RenderSize;

                hsvControl.SetThumbPosition(
                    CalculateThumbPosition(hsvControl.Saturation, v, size));
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private static Noesis.Point CalculateThumbPosition(float saturation, float v, Noesis.Size size)
    {
        return new Noesis.Point(v * size.Width, size.Height - (saturation * size.Height));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private static void ColorToHSV(Noesis.Color color, ref float h, ref float s, ref float v)
    {
        float red = color.R;
        float green = color.G;
        float blue  = color.B;
        float min = Math.Min(red, Math.Min(green, blue));
        float max = Math.Max(red, Math.Max(green, blue));

        v = max;
        float delta = max - min;

        if (v == 0)
        {
            s = 0;
        }
        else
        {
            s = delta / max;
        }

        if (s == 0)
        {
            h = 0;
        }
        else
        {
            if (red == max)
            {
                h = (green - blue) / delta;
            }
            else if (green == max)
            {
                h = 2 + (blue - red) / delta;
            }
            else // blue == max
            {
                h = 4 + (red - green) / delta;
            }
        }

        h *= 60;

        if (h < 0)
        {
            h += 360;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private static Noesis.Color HSVToColor(float hue, float saturation, float value)
    {
        return HSVToColor(hue, saturation, value, 1.0f);
    }

    private static Noesis.Color HSVToColor(float hue, float saturation, float value, float alpha)
    {
        float chroma = value * saturation;

        if (hue == 360)
        {
            hue = 0;
        }

        float hueTag = hue / 60;
        float x = chroma * (1 - Math.Abs(hueTag % 2.0f - 1));
        float m = value - chroma;
        switch ((int)hueTag)
        {
            case 0:
            {
                return new Noesis.Color(
                    (byte)((chroma + m) * 255 + 0.5), 
                    (byte)((x + m) * 255 + 0.5), 
                    (byte)(m * 255 + 0.5),
                    (byte)(alpha * 255));
            }
            case 1:
            {
                return new Noesis.Color(
                    (byte)((x + m) * 255 + 0.5),
                    (byte)((chroma + m) * 255 + 0.5),
                    (byte)(m * 255 + 0.5),
                    (byte)(alpha * 255));
            }
            case 2:
            {
                return new Noesis.Color(
                    (byte)(m * 255 + 0.5),
                    (byte)((chroma + m) * 255 + 0.5),
                    (byte)((x + m) * 255 + 0.5),
                    (byte)(alpha * 255));
            }
            case 3:
            {
                return new Noesis.Color(
                    (byte)(m * 255 + 0.5),
                    (byte)((x + m) * 255 + 0.5),
                    (byte)((chroma + m) * 255 + 0.5),
                    (byte)(alpha * 255));
            }
            case 4:
            {
                return new Noesis.Color(
                    (byte)((x + m) * 255 + 0.5),
                    (byte)(m * 255 + 0.5),
                    (byte)((chroma + m) * 255 + 0.5),
                    (byte)(alpha * 255));
            }
            default:
            {
                return new Noesis.Color(
                    (byte)((chroma + m) * 255 + 0.5),
                    (byte)(m * 255 + 0.5),
                    (byte)((x + m) * 255 + 0.5),
                    (byte)(alpha * 255));
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetRGBA(Noesis.Color color)
    {
        isSettingRGBA = true;

        float h = 0, s = 0, v = 0;
        ColorToHSV(color, ref h, ref s, ref v);

        Hue = h;
        Saturation = s;
        V = v;
        Alpha = color.A;

        OnHSVChanged();

        isSettingRGBA = false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnMouseLeftButtonDown(object sender, Noesis.MouseButtonEventArgs args)
    {
        Focus();
        svGrid.CaptureMouse();

        movingSV = true;

        Noesis.Point ctrlPos = svGrid.PointFromScreen(args.position);

        this.SetThumbPosition(ctrlPos);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnMouseLeftButtonUp(object sender, Noesis.MouseButtonEventArgs args)
    {
        movingSV = false;

        svGrid.ReleaseMouseCapture();
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnMouseMove(object sender, Noesis.MouseEventArgs args)
    {
        if (movingSV)
        {
            Noesis.Point ctrlPos = svGrid.PointFromScreen(args.position);

            this.SetThumbPosition(ctrlPos);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnSizeChanged(object sender, Noesis.SizeChangedEventArgs args)
    {
        if (!movingSV)
        {
            Noesis.Size size = args.sizeChangedInfo.newSize;
            this.SetThumbPosition(new Noesis.Point(this.V * size.Width, this.Saturation * size.Height));
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnSliderValueChange(float oldValue, float newValue)
    {
        this.updatingSlider = true;
        Hue = newValue;

        if (!isSettingRGBA)
        {
            OnHSVChanged();
        }

        updatingSlider = false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void SetThumbPosition(Noesis.Point pos)
    {
        Noesis.Size size = svGrid.RenderSize;

        float xPos = Clamp<float>(pos.X, 0.0f, size.Width);
        float yPos = Clamp<float>(pos.Y, 0.0f, size.Height);

        float halfWidth = size.Width * 0.5f;
        float halfHeight = size.Height * 0.5f;

        thumbTranslate.X = xPos - halfWidth;
        thumbTranslate.Y = yPos - halfHeight;

        if (movingSV)
        {
            V = xPos / size.Width;
            Saturation = (size.Height - yPos) / size.Height;
            OnHSVChanged();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public Noesis.Color GetRGBA()
    {
        return HSVToColor(Hue, Saturation, V, Alpha);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
    {
        if (val.CompareTo(min) < 0)
        {
            return min;
        }
        else if(val.CompareTo(max) > 0)
        {
            return max;
        }
        else
        {
            return val;
        }
    }

    private Noesis.GradientStop colorSpectrum;
    private Noesis.TranslateTransform thumbTranslate;
    private Noesis.Slider spectrum;
    private Noesis.FrameworkElement svGrid;
    private bool movingSV = false;
    private bool updatingSlider = false;
    private bool isSettingRGBA = false;
}

}
