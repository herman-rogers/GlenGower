using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Noesis.Samples
{
    public class UnityObject : NotifyPropertyChangedBase
    {
        // Type property
        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        // Color property
        public Noesis.SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged("Color");
                }
            }
        }

        public string Scale
        {
            get { return _scale; }
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    OnPropertyChanged("Scale");
                }
            }
        }

        public string Pos
        {
            get { return _pos; }
            set
            {
                if (_pos != value)
                {
                    _pos = value;
                    OnPropertyChanged("Pos");
                }
            }
        }

        public GameObject MyObject
        {
            set { _object = value; }
            get { return _object; }
        }

        public static void Register()
        {
        }

        // X scale
        //@{
        public void SetScaleX(float val)
        {
            _scaleX = val;
            UpdateScale();
        }
        public float GetScaleX()
        {
            return _scaleX;
        }
        //@}

        // Y scale
        //@{
        public void SetScaleY(float val)
        {
            _scaleY = val;
            UpdateScale();
        }
        public float GetScaleY()
        {
            return _scaleY;
        }
        //@}

        // Z scale
        //@{
        public void SetScaleZ(float val)
        {
            _scaleZ = val;
            UpdateScale();
        }
        public float GetScaleZ()
        {
            return _scaleZ;
        }
        //@}

        // X position
        //@{
        public void SetPosX(float val)
        {
            _posX = val;
            UpdatePos();
        }
        public float GetPosX()
        {
            return _posX;
        }
        //@}

        // Y position
        //@{
        public void SetPosY(float val)
        {
            _posY = val;
            UpdatePos();
        }
        public float GetPosY()
        {
            return _posY;
        }
        //@}

        // Z position
        //@{
        public void SetPosZ(float val)
        {
            _posZ = val;
            UpdatePos();
        }

        public float GetPosZ()
        {
            return _posZ;
        }
        //@}

        private void UpdatePos()
        {
            Pos = String.Format("Position: (X:{0:0.00}, Y:{1:0.00}, Z:{2:0.00})",
                _posX, _posY, _posZ);
        }

        private void UpdateScale()
        {
            Scale = String.Format("Scale: (X:{0:0.00}, Y:{1:0.00}, Z:{2:0.00})",
                _scaleX, _scaleY, _scaleZ);
        }

        // Type
        private string _type;

        // Color
        private Noesis.SolidColorBrush _color;

        // Scale string
        private string _scale;

        // Position string
        private string _pos;

        // Scale
        private float _scaleX;
        private float _scaleY;
        private float _scaleZ;

        // Position
        private float _posX;
        private float _posY;
        private float _posZ;

        private GameObject _object;
    }
}

////////////////////////////////////////////////////////////////////////////////////////////
public class PrimitivesController : MonoBehaviour 
{
    Noesis.FrameworkElement _root;

    Noesis.UserControls.ColorPicker _colorPicker;

    Noesis.Slider _x;
    Noesis.Slider _y;
    Noesis.Slider _z;

    Noesis.Slider _scaleX;
    Noesis.Slider _scaleY;
    Noesis.Slider _scaleZ;

    Noesis.Button _typeSphere;
    Noesis.Button _typeCapsule;
    Noesis.Button _typeCylinder;
    Noesis.Button _typeCube;
    Noesis.Button _typePlane;
    
    GameObject _dirLight;
    
    GameObject _selectedObject;

    Noesis.Slider _sunDir;

    Noesis.Border _updateGB;
    Noesis.TextBlock _updateGBHeader;

    Noesis.TextBlock _selectedLabel;
    Noesis.TranslateTransform _selectedLabelTrans;

    System.Collections.ObjectModel.ObservableCollection<Noesis.Samples.UnityObject> _unityObjects;
    System.Collections.Generic.Dictionary<GameObject, Noesis.Samples.UnityObject> _objs;

    Noesis.ListBox _listBox;

    ////////////////////////////////////////////////////////////////////////////////////////////
    // Use this for initialization
    void Start () 
    {
        // Access to the NoesisGUIPanel component
        NoesisGUIPanel noesisGUI = GetComponent<NoesisGUIPanel>();

        // Obtain the root of the loaded UI resource, in this case it is a Grid element
        _root = noesisGUI.GetContent();

        _sunDir = (Noesis.Slider)_root.FindName("sliderSun");
        _sunDir.ValueChanged += this.OnSunDirChanged;

        _colorPicker = (Noesis.UserControls.ColorPicker)_root.FindName("ColorPicker");
        _colorPicker.ColorChanged += this.OnColorPickerChanged;

        _x = (Noesis.Slider)_root.FindName("Xval");
        _x.ValueChanged += this.OnXPosChanged;
        _y = (Noesis.Slider)_root.FindName("Yval");
        _y.ValueChanged += this.OnYPosChanged;
        _z = (Noesis.Slider)_root.FindName("Zval");
        _z.ValueChanged += this.OnZPosChanged;

        _scaleX = (Noesis.Slider)_root.FindName("ScaleXval");
        _scaleX.ValueChanged += this.OnScaleXChanged;
        _scaleY = (Noesis.Slider)_root.FindName("ScaleYval");
        _scaleY.ValueChanged += this.OnScaleYChanged;
        _scaleZ = (Noesis.Slider)_root.FindName("ScaleZval");
        _scaleZ.ValueChanged += this.OnScaleZChanged;

        _typeSphere = (Noesis.Button)_root.FindName("TypeSphere");
        _typeSphere.Click += this.OnCreateSphere;
        _typeCapsule = (Noesis.Button)_root.FindName("TypeCapsule");
        _typeCapsule.Click += this.OnCreateCapsule;
        _typeCylinder = (Noesis.Button)_root.FindName("TypeCylinder");
        _typeCylinder.Click += this.OnCreateCylinder;
        _typeCube = (Noesis.Button)_root.FindName("TypeCube");
        _typeCube.Click += this.OnCreateCube;
        _typePlane = (Noesis.Button)_root.FindName("TypePlane");
        _typePlane.Click += this.OnCreatePlane;

        _updateGB = (Noesis.Border)_root.FindName("UpdateGB");
        _updateGB.IsEnabled = false;
        _updateGBHeader = (Noesis.TextBlock)_root.FindName("UpdateGBHeader");

        _selectedLabel = (Noesis.TextBlock)_root.FindName("SelectedLbl");
        _selectedLabel.Visibility = Noesis.Visibility.Hidden;

        _selectedLabelTrans = (Noesis.TranslateTransform)_root.FindName("selectLblPos");
        
        _objs = new System.Collections.Generic.Dictionary<GameObject, Noesis.Samples.UnityObject>();
        
        _dirLight = GameObject.Find("Key light");
        _selectedObject = null;

        _listBox = (Noesis.ListBox)_root.FindName("MainLB");

        _unityObjects = new System.Collections.ObjectModel.ObservableCollection<Noesis.Samples.UnityObject>();
        _listBox.ItemsSource = _unityObjects;
        _listBox.SelectionMode = Noesis.SelectionMode.Single;
        _listBox.SelectionChanged += this.OnSelectionChanged;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100000.0f))
            {
                _selectedObject = hit.collider.gameObject;
                _updateGB.IsEnabled = true;
                _updateGBHeader.Text = _selectedObject.name;
                _selectedLabel.Visibility = Noesis.Visibility.Visible;
                UpdateLabelTranslation(_selectedObject.transform.position);
                FillDataFromSelObj();

                Noesis.Samples.UnityObject obj = _objs[_selectedObject];
                _listBox.SelectedItem = obj;
            }
            else
            {
                _selectedObject = null;
                _updateGB.IsEnabled = false;
                _selectedLabel.Visibility = Noesis.Visibility.Hidden;
                _listBox.SelectedItem = null;
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnSunDirChanged(float oldValue, float newValue)
    {
        _dirLight.transform.localEulerAngles = new Vector3(50, -newValue, 0);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnColorPickerChanged(object sender, Noesis.EventArgs e)
    {
        Noesis.Color color = _colorPicker.Color.Color;
        _selectedObject.GetComponent<Renderer>().material.SetColor("_Color", new UnityEngine.Color(
            color.R, color.G, color.B, color.A));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnXPosChanged(float oldValue, float newValue)
    {
        if (_selectedObject != null)
        {
            Noesis.Samples.UnityObject obj = _objs[_selectedObject];
            obj.SetPosX(newValue);

            Vector3 newPos = new Vector3(newValue, obj.GetPosY(), obj.GetPosZ());
            _selectedObject.transform.position = newPos;
            UpdateLabelTranslation(newPos);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnYPosChanged(float oldValue, float newValue)
    {
        if (_selectedObject != null)
        {
            Noesis.Samples.UnityObject obj = _objs[_selectedObject];
            obj.SetPosY(newValue);

            Vector3 newPos = new Vector3(obj.GetPosX(), newValue, obj.GetPosZ());
            _selectedObject.transform.position = newPos;
            UpdateLabelTranslation(newPos);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnZPosChanged(float oldValue, float newValue)
    {
        if (_selectedObject != null)
        {
            Noesis.Samples.UnityObject obj = _objs[_selectedObject];
            obj.SetPosZ(newValue);

            Vector3 newPos = new Vector3(obj.GetPosX(), obj.GetPosY(), newValue);
            _selectedObject.transform.position = newPos;
            UpdateLabelTranslation(newPos);
        }
    }

    void UpdateLabelTranslation(Vector3 pos)
    {
        Vector3 scPos = Camera.main.WorldToScreenPoint(pos);
        _selectedLabelTrans.X = scPos.x - 28;
        _selectedLabelTrans.Y = Camera.main.pixelHeight - scPos.y - 7;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnScaleXChanged(float oldValue, float newValue)
    {
        if (_selectedObject != null)
        {
            Noesis.Samples.UnityObject obj = _objs[_selectedObject];
            obj.SetScaleX(newValue);

            _selectedObject.transform.localScale = new Vector3(
                newValue, obj.GetScaleY(), obj.GetScaleZ());
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnScaleYChanged(float oldValue, float newValue)
    {
        if (_selectedObject != null)
        {
            Noesis.Samples.UnityObject obj = _objs[_selectedObject];
            obj.SetScaleY(newValue);

            _selectedObject.transform.localScale = new Vector3(
                obj.GetScaleX(), newValue, obj.GetScaleZ());
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnScaleZChanged(float oldValue, float newValue)
    {
        if (_selectedObject != null)
        {
            Noesis.Samples.UnityObject obj = _objs[_selectedObject];
            obj.SetScaleZ(newValue);

            _selectedObject.transform.localScale = new Vector3(
                obj.GetScaleX(), obj.GetScaleY(), newValue);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnCreateSphere(object sender, Noesis.RoutedEventArgs e)
    {    
        CreatePrimitiveObject(PrimitiveType.Sphere);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnCreateCapsule(object sender, Noesis.RoutedEventArgs e)
    {    
        CreatePrimitiveObject(PrimitiveType.Capsule);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnCreateCylinder(object sender, Noesis.RoutedEventArgs e)
    {    
        CreatePrimitiveObject(PrimitiveType.Cylinder);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnCreateCube(object sender, Noesis.RoutedEventArgs e)
    {    
        CreatePrimitiveObject(PrimitiveType.Cube);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnCreatePlane(object sender, Noesis.RoutedEventArgs e)
    {    
        CreatePrimitiveObject(PrimitiveType.Plane);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void CreatePrimitiveObject(PrimitiveType primitiveType)
    {
        GameObject obj = GameObject.CreatePrimitive(primitiveType);
        obj.transform.position = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(30, 30, 30);

        obj.GetComponent<Renderer>().material = (Material)Resources.Load("PrimitivesMaterial");
        obj.GetComponent<Renderer>().material.SetColor("_Color", UnityEngine.Color.white);

        Noesis.Samples.UnityObject myObj = new Noesis.Samples.UnityObject();

        myObj.Color = new Noesis.SolidColorBrush(new Noesis.Color(255, 255, 255, 255));
        
        myObj.SetScaleX(30);
        myObj.SetScaleY(30);
        myObj.SetScaleZ(30);
        
        myObj.SetPosX(0);
        myObj.SetPosY(0);
        myObj.SetPosZ(0);
        
        myObj.Type = obj.name;
        
        myObj.MyObject = obj;

        _objs.Add(obj, myObj);
        
        _unityObjects.Add(myObj);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    void FillDataFromSelObj()
    {
        _colorPicker.Color = _objs[_selectedObject].Color;

        _x.Value = _selectedObject.transform.position.x;
        _y.Value = _selectedObject.transform.position.y;
        _z.Value = _selectedObject.transform.position.z;

        _scaleX.Value = _selectedObject.transform.localScale.x;
        _scaleY.Value = _selectedObject.transform.localScale.y;
        _scaleZ.Value = _selectedObject.transform.localScale.z;
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnSelectionChanged(object sender, Noesis.SelectionChangedEventArgs args)
    {
        int idxSel = _listBox.SelectedIndex;
        if (idxSel < 0)
        {
            _selectedObject = null;
            _updateGB.IsEnabled = false;
            _selectedLabel.Visibility = Noesis.Visibility.Hidden;
        }
        else
        {
            _selectedObject = ((Noesis.Samples.UnityObject)_listBox.SelectedItem).MyObject;
    
            this.FillDataFromSelObj();

            _updateGB.IsEnabled = true;
            _updateGBHeader.Text = _selectedObject.name;
            _selectedLabel.Visibility = Noesis.Visibility.Visible;
        }
    }
}
