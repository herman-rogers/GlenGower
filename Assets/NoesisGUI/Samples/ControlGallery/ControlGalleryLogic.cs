using UnityEngine;
using System.Collections;

public class ControlGalleryLogic : MonoBehaviour
{
    Noesis.FrameworkElement _root;

    Noesis.ResourceDictionary _noesisStyleResources;
    Noesis.ResourceDictionary _simpleStyleResources;
    Noesis.ResourceDictionary _windowsStyleResources;
    Noesis.ComboBox _styleSelector;
    Noesis.Panel _container;
    Noesis.TreeView _samples;

    Noesis.Border _container1;
    Noesis.Border _container2;
    Noesis.Border _descHost1;
    Noesis.Border _descHost2;

    Noesis.Storyboard _showContainer1;
    Noesis.Storyboard _showContainer2;

    enum VisibleContainer
    {
        Container1,
        Container2
    }
    VisibleContainer _visibleContainer = VisibleContainer.Container1;

    string _lastSample;

    // Use this for initialization
    void Start ()
    {
        NoesisGUIPanel gui = GetComponent<NoesisGUIPanel>();

        _root = gui.GetContent();

        _noesisStyleResources = (Noesis.ResourceDictionary)NoesisGUISystem.LoadXaml("Assets/NoesisGUI/Themes/NoesisStyle.xaml");
        _simpleStyleResources = (Noesis.ResourceDictionary)NoesisGUISystem.LoadXaml("Assets/NoesisGUI/Themes/SimpleStyle.xaml");
        _windowsStyleResources = (Noesis.ResourceDictionary)NoesisGUISystem.LoadXaml("Assets/NoesisGUI/Themes/WindowsStyle.xaml");

        _styleSelector = (Noesis.ComboBox)_root.FindName("StyleSelector");
        _styleSelector.SelectionChanged += OnStyleSelectionChanged;

        _samples = (Noesis.TreeView)_root.FindName("Samples");
        _samples.SelectedItemChanged += OnSamplesSelectionChanged;

        _container = (Noesis.Panel)_root.FindName("Container");

        _container1 = (Noesis.Border)_root.FindName("Container1");
        _container2 = (Noesis.Border)_root.FindName("Container2");
        _descHost1 = (Noesis.Border)_root.FindName("DescriptionHost1");
        _descHost2 = (Noesis.Border)_root.FindName("DescriptionHost2");

        _showContainer1 = (Noesis.Storyboard)_root.FindResource("ShowContainer1");
        _showContainer1.Completed += OnShowContainer1Completed;

        _showContainer2 = (Noesis.Storyboard)_root.FindResource("ShowContainer2");
        _showContainer2.Completed += OnShowContainer2Completed;

        // initially load Button sample
        Noesis.UIElement sample, desc;
        LoadSample("Button.xaml", out sample, out desc);

        _container1.Child = sample;
        _container1.Visibility = Noesis.Visibility.Visible;

        _descHost1.Child = desc;
        _descHost1.Visibility = Noesis.Visibility.Visible;
    }

    void OnStyleSelectionChanged(object sender, Noesis.SelectionChangedEventArgs args)
    {
        switch (_styleSelector.SelectedIndex)
        {
            case 0:
            {
                _container.Resources = _noesisStyleResources;
                break;
            }
            case 1:
            {
                _container.Resources = _simpleStyleResources;
                break;
            }
            case 2:
            {
                _container.Resources = _windowsStyleResources;
                break;
            }
        }

        args.handled = true;
    }

    void OnSamplesSelectionChanged(object oldValue, object newValue)
    {
        Noesis.TreeViewItem tvi = newValue as Noesis.TreeViewItem;
        if (tvi != null && !tvi.HasItems)
        {
            string sampleName = tvi.Tag as string;
            if (_lastSample != sampleName)
            {
                LoadSample(sampleName);
                _lastSample = sampleName;
            }
        }
    }

    void OnShowContainer1Completed(object sender, Noesis.TimelineEventArgs args)
    {
        _container2.Child = null;
        _samples.IsEnabled = true;
    }

    void OnShowContainer2Completed(object sender, Noesis.TimelineEventArgs args)
    {
        _container1.Child = null;
        _samples.IsEnabled = true;
    }

    void LoadSample(string sampleName)
    {
        Noesis.UIElement sample, desc;
        LoadSample(sampleName, out sample, out desc);

        _samples.IsEnabled = false;

        if (_visibleContainer == VisibleContainer.Container1)
        {
            _container2.Child = sample;
            _descHost2.Child = desc;
            _showContainer2.Begin(_root);
            _visibleContainer = VisibleContainer.Container2;
        }
        else
        {
            _container1.Child = sample;
            _descHost1.Child = desc;
            _showContainer1.Begin(_root);
            _visibleContainer = VisibleContainer.Container1;
        }
    }

    void LoadSample(string sampleName, out Noesis.UIElement sample, out Noesis.UIElement desc)
    {
        sample = (Noesis.UIElement)NoesisGUISystem.LoadXaml("Assets/NoesisGUI/Samples/ControlGallery/Samples/" + sampleName);
        desc = (Noesis.UIElement)NoesisGUISystem.LoadXaml("Assets/NoesisGUI/Samples/ControlGallery/Desc/" + sampleName);
    }
}
