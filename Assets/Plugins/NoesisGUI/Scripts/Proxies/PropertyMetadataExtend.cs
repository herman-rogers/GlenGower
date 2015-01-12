using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Noesis
{
    public delegate void PropertyChangedCallback(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    );

    public partial class PropertyMetadata
    {
        public PropertyMetadata(object defaultValue)
            : this(Create(defaultValue, null), true)
        {
        }

        public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback)
            : this(Create(defaultValue, propertyChangedCallback), true)
        {
        }

        private static IntPtr Create(object defaultValue, PropertyChangedCallback propertyChangedCallback)
        {
            IntPtr propertyMetadataPtr = IntPtr.Zero;
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback =
                propertyChangedCallback == null ?
                    (DelegateInvokePropertyChangedCallback)null : _invokePropertyChanged;

            if (defaultValue != null && defaultValue.GetType().GetTypeInfo().IsEnum)
            {
                defaultValue = defaultValue.ToString();
            }

            if (defaultValue == null)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_BaseComponent_(IntPtr.Zero,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is bool)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_Bool_((bool)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is float)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_Float_((float)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is int)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_Int_((int)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is uint)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_UInt_((uint)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is short)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_Short_((short)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is ushort)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_UShort_((ushort)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is string)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_String_((string)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Color)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_Color_(
                    (Noesis.Color)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Point)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_Point_(
                    (Noesis.Point)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Rect)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_Rect_(
                    (Noesis.Rect)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Size)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_Size_(
                    (Noesis.Size)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Thickness)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_Thickness_(
                    (Noesis.Thickness)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.CornerRadius)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_CornerRadius_(
                    (Noesis.CornerRadius)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Type)
            {
                IntPtr defPtr = Noesis.Extend.GetResourceKeyType(defaultValue as Type);
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_BaseComponent_(defPtr,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.BaseComponent)
            {
                propertyMetadataPtr = Noesis_CreatePropertyMetadata_BaseComponent_(
                    Noesis.BaseComponent.getCPtr((Noesis.BaseComponent)defaultValue).Handle,
                    invokePropertyChangedCallback);
            }

            if (propertyMetadataPtr == IntPtr.Zero)
            {
                throw new System.Exception("Default value type not supported");
            }

            // Register property changed callback
            if (propertyChangedCallback != null)
            {
                _PropertyChangedCallback.Add(propertyMetadataPtr, propertyChangedCallback);
            }

            return propertyMetadataPtr;
        }

        internal delegate void DelegateInvokePropertyChangedCallback(IntPtr cPtr, IntPtr d, IntPtr e);
        private static DelegateInvokePropertyChangedCallback _invokePropertyChanged = InvokePropertyChangedCallback;

        [MonoPInvokeCallback(typeof(DelegateInvokePropertyChangedCallback))]
        protected static void InvokePropertyChangedCallback(IntPtr cPtr, IntPtr d, IntPtr e)
        {
            if (!_PropertyChangedCallback.ContainsKey(cPtr))
            {
                throw new System.Exception("PropertyChangedCallback not found");
            }

            // Invoke callback
            _PropertyChangedCallback[cPtr](
                Noesis.Extend.Unbox(Noesis.Extend.GetProxy(d, false)) as Noesis.DependencyObject,
                new DependencyPropertyChangedEventArgs(e, false));
        }

        static protected Dictionary<IntPtr, PropertyChangedCallback> _PropertyChangedCallback =
            new Dictionary<IntPtr, PropertyChangedCallback>();

        internal static void ClearCallbacks()
        {
            _PropertyChangedCallback.Clear();
        }

        #region Imports
        private static IntPtr Noesis_CreatePropertyMetadata_Bool_(bool defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_Bool(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_Float_(float defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_Float(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_Int_(int defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_Int(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_UInt_(uint defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_UInt(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_Short_(short defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_Short(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_UShort_(ushort defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_UShort(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_String_(string defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_String(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_Color_(Noesis.Color defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_Color(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_Point_(Noesis.Point defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_Point(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_Rect_(Noesis.Rect defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_Rect(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_Size_(Noesis.Size defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_Size(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_Thickness_(Noesis.Thickness defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_Thickness(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_CornerRadius_(Noesis.CornerRadius defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_CornerRadius(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreatePropertyMetadata_BaseComponent_(IntPtr defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreatePropertyMetadata_BaseComponent(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

    #if UNITY_EDITOR

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void RegisterFunctions(Library lib)
        {
            _CreatePropertyMetadata_Bool = lib.Find<CreatePropertyMetadataDelegate_Bool>(
                "Noesis_CreatePropertyMetadata_Bool");
            _CreatePropertyMetadata_Float = lib.Find<CreatePropertyMetadataDelegate_Float>(
                "Noesis_CreatePropertyMetadata_Float");
            _CreatePropertyMetadata_Int = lib.Find<CreatePropertyMetadataDelegate_Int>(
                "Noesis_CreatePropertyMetadata_Int");
            _CreatePropertyMetadata_UInt = lib.Find<CreatePropertyMetadataDelegate_UInt>(
                "Noesis_CreatePropertyMetadata_UInt");
            _CreatePropertyMetadata_Short = lib.Find<CreatePropertyMetadataDelegate_Short>(
                "Noesis_CreatePropertyMetadata_Short");
            _CreatePropertyMetadata_UShort = lib.Find<CreatePropertyMetadataDelegate_UShort>(
                "Noesis_CreatePropertyMetadata_UShort");
            _CreatePropertyMetadata_String = lib.Find<CreatePropertyMetadataDelegate_String>(
                "Noesis_CreatePropertyMetadata_String");
            _CreatePropertyMetadata_Color = lib.Find<CreatePropertyMetadataDelegate_Color>(
                "Noesis_CreatePropertyMetadata_Color");
            _CreatePropertyMetadata_Point = lib.Find<CreatePropertyMetadataDelegate_Point>(
                "Noesis_CreatePropertyMetadata_Point");
            _CreatePropertyMetadata_Rect = lib.Find<CreatePropertyMetadataDelegate_Rect>(
                "Noesis_CreatePropertyMetadata_Rect");
            _CreatePropertyMetadata_Size = lib.Find<CreatePropertyMetadataDelegate_Size>(
                "Noesis_CreatePropertyMetadata_Size");
            _CreatePropertyMetadata_Thickness = lib.Find<CreatePropertyMetadataDelegate_Thickness>(
                "Noesis_CreatePropertyMetadata_Thickness");
            _CreatePropertyMetadata_CornerRadius = lib.Find<CreatePropertyMetadataDelegate_CornerRadius>(
                "Noesis_CreatePropertyMetadata_CornerRadius");
            _CreatePropertyMetadata_BaseComponent = lib.Find<CreatePropertyMetadataDelegate_BaseComponent>(
                "Noesis_CreatePropertyMetadata_BaseComponent");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void UnregisterFunctions()
        {
            _CreatePropertyMetadata_Bool = null;
            _CreatePropertyMetadata_Float = null;
            _CreatePropertyMetadata_Int = null;
            _CreatePropertyMetadata_UInt = null;
            _CreatePropertyMetadata_Short = null;
            _CreatePropertyMetadata_UShort = null;
            _CreatePropertyMetadata_String = null;
            _CreatePropertyMetadata_Color = null;
            _CreatePropertyMetadata_Point = null;
            _CreatePropertyMetadata_Rect = null;
            _CreatePropertyMetadata_Size = null;
            _CreatePropertyMetadata_Thickness = null;
            _CreatePropertyMetadata_CornerRadius = null;
            _CreatePropertyMetadata_BaseComponent = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_Bool(bool defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_Bool _CreatePropertyMetadata_Bool;
        private static IntPtr Noesis_CreatePropertyMetadata_Bool(bool defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_Bool(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_Float(float defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_Float _CreatePropertyMetadata_Float;
        private static IntPtr Noesis_CreatePropertyMetadata_Float(float defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_Float(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_Int(int defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_Int _CreatePropertyMetadata_Int;
        private static IntPtr Noesis_CreatePropertyMetadata_Int(int defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_Int(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_UInt(uint defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_UInt _CreatePropertyMetadata_UInt;
        private static IntPtr Noesis_CreatePropertyMetadata_UInt(uint defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_UInt(defaultValue,invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_Short(short defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_Short _CreatePropertyMetadata_Short;
        private static IntPtr Noesis_CreatePropertyMetadata_Short(short defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_Short(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_UShort(ushort defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_UShort _CreatePropertyMetadata_UShort;
        private static IntPtr Noesis_CreatePropertyMetadata_UShort(ushort defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_UShort(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_String(
            [MarshalAs(UnmanagedType.LPStr)] string defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_String _CreatePropertyMetadata_String;
        private static IntPtr Noesis_CreatePropertyMetadata_String(string defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_String(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_Color(Noesis.Color defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_Color _CreatePropertyMetadata_Color;
        private static IntPtr Noesis_CreatePropertyMetadata_Color(Noesis.Color defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_Color(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_Point(Noesis.Point defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_Point _CreatePropertyMetadata_Point;
        private static IntPtr Noesis_CreatePropertyMetadata_Point(Noesis.Point defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_Point(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_Rect(Noesis.Rect defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_Rect _CreatePropertyMetadata_Rect;
        private static IntPtr Noesis_CreatePropertyMetadata_Rect(Noesis.Rect defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_Rect(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_Size(Noesis.Size defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_Size _CreatePropertyMetadata_Size;
        private static IntPtr Noesis_CreatePropertyMetadata_Size(Noesis.Size defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_Size(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_Thickness(Noesis.Thickness defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_Thickness _CreatePropertyMetadata_Thickness;
        private static IntPtr Noesis_CreatePropertyMetadata_Thickness(Noesis.Thickness defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_Thickness(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_CornerRadius(Noesis.CornerRadius defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_CornerRadius _CreatePropertyMetadata_CornerRadius;
        private static IntPtr Noesis_CreatePropertyMetadata_CornerRadius(Noesis.CornerRadius defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_CornerRadius(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreatePropertyMetadataDelegate_BaseComponent(IntPtr defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreatePropertyMetadataDelegate_BaseComponent _CreatePropertyMetadata_BaseComponent;
        private static IntPtr Noesis_CreatePropertyMetadata_BaseComponent(IntPtr defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreatePropertyMetadata_BaseComponent(defaultValue, invokePropertyChangedCallback);
        }

    #else

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_Bool")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_Bool")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_Bool(
            [MarshalAs(UnmanagedType.U1)] bool defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_Float")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_Float")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_Float(float defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_Int")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_Int")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_Int(int defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_UInt")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_UInt")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_UInt(uint defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_Short")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_Short")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_Short(short defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_UShort")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_UShort")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_UShort(ushort defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_String")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_String")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_String(
            [MarshalAs(UnmanagedType.LPStr)] string defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_Color")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_Color")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_Color(
            Noesis.Color defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_Point")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_Point")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_Point(
            Noesis.Point defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_Rect")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_Rect")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_Rect(
            Noesis.Rect defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_Size")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_Size")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_Size(
            Noesis.Size defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_Thickness")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_Thickness")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_Thickness(
            Noesis.Thickness defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_CornerRadius")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_CornerRadius")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_CornerRadius(
            Noesis.CornerRadius defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreatePropertyMetadata_BaseComponent")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreatePropertyMetadata_BaseComponent")]
        #endif
        private static extern IntPtr Noesis_CreatePropertyMetadata_BaseComponent(IntPtr defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

    #endif
        #endregion
    }

}
