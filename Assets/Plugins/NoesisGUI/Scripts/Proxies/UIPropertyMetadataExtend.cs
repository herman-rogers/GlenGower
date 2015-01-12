using System;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Noesis
{

    public partial class UIPropertyMetadata
    {
        public UIPropertyMetadata(object defaultValue)
            : this(Create(defaultValue, null), true)
        {
        }

        public UIPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback)
            : this(Create(defaultValue, propertyChangedCallback), true)
        {
        }

        private static IntPtr Create(object defaultValue, PropertyChangedCallback propertyChangedCallback)
        {
            IntPtr propertyMetadataPtr = IntPtr.Zero;
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback =
                propertyChangedCallback == null ?
                    (DelegateInvokePropertyChangedCallback)null : InvokePropertyChangedCallback;

            if (defaultValue != null && defaultValue.GetType().GetTypeInfo().IsEnum)
            {
                defaultValue = defaultValue.ToString();
            }

            if (defaultValue == null)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_BaseComponent_(IntPtr.Zero,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is bool)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_Bool_((bool)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is float)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_Float_((float)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is int)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_Int_((int)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is uint)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_UInt_((uint)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is short)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_Short_((short)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is ushort)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_UShort_((ushort)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is string)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_String_((string)defaultValue,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Color)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_Color_(
                    (Noesis.Color)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Point)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_Point_(
                    (Noesis.Point)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Rect)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_Rect_(
                    (Noesis.Rect)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Size)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_Size_(
                    (Noesis.Size)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Thickness)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_Thickness_(
                    (Noesis.Thickness)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.CornerRadius)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_CornerRadius_(
                    (Noesis.CornerRadius)defaultValue, invokePropertyChangedCallback);
            }
            else if (defaultValue is Type)
            {
                IntPtr defPtr = Noesis.Extend.GetResourceKeyType(defaultValue as Type);
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_BaseComponent_(defPtr,
                    invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.BaseComponent)
            {
                propertyMetadataPtr = Noesis_CreateUIPropertyMetadata_BaseComponent_(
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

        #region Imports
        private static IntPtr Noesis_CreateUIPropertyMetadata_Bool_(bool defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_Bool(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_Float_(float defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_Float(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_Int_(int defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_Int(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_UInt_(uint defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_UInt(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_Short_(short defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_Short(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_UShort_(ushort defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_UShort(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_String_(string defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_String(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_Color_(Noesis.Color defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_Color(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_Point_(Noesis.Point defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_Point(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_Rect_(Noesis.Rect defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_Rect(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_Size_(Noesis.Size defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_Size(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_Thickness_(Noesis.Thickness defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_Thickness(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_CornerRadius_(Noesis.CornerRadius defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_CornerRadius(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateUIPropertyMetadata_BaseComponent_(IntPtr defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateUIPropertyMetadata_BaseComponent(defaultValue,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

    #if UNITY_EDITOR

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public new static void RegisterFunctions(Library lib)
        {
            // create UIPropertyMetadata 
            _CreateUIPropertyMetadata_Bool = lib.Find<CreateUIPropertyMetadataDelegate_Bool>(
                "Noesis_CreateUIPropertyMetadata_Bool");
            _CreateUIPropertyMetadata_Float = lib.Find<CreateUIPropertyMetadataDelegate_Float>(
                "Noesis_CreateUIPropertyMetadata_Float");
            _CreateUIPropertyMetadata_Int = lib.Find<CreateUIPropertyMetadataDelegate_Int>(
                "Noesis_CreateUIPropertyMetadata_Int");
            _CreateUIPropertyMetadata_UInt = lib.Find<CreateUIPropertyMetadataDelegate_UInt>(
                "Noesis_CreateUIPropertyMetadata_UInt");
            _CreateUIPropertyMetadata_Short = lib.Find<CreateUIPropertyMetadataDelegate_Short>(
                "Noesis_CreateUIPropertyMetadata_Short");
            _CreateUIPropertyMetadata_UShort = lib.Find<CreateUIPropertyMetadataDelegate_UShort>(
                "Noesis_CreateUIPropertyMetadata_UShort");
            _CreateUIPropertyMetadata_String = lib.Find<CreateUIPropertyMetadataDelegate_String>(
                "Noesis_CreateUIPropertyMetadata_String");
            _CreateUIPropertyMetadata_Color = lib.Find<CreateUIPropertyMetadataDelegate_Color>(
                "Noesis_CreateUIPropertyMetadata_Color");
            _CreateUIPropertyMetadata_Point = lib.Find<CreateUIPropertyMetadataDelegate_Point>(
                "Noesis_CreateUIPropertyMetadata_Point");
            _CreateUIPropertyMetadata_Rect = lib.Find<CreateUIPropertyMetadataDelegate_Rect>(
                "Noesis_CreateUIPropertyMetadata_Rect");
            _CreateUIPropertyMetadata_Size = lib.Find<CreateUIPropertyMetadataDelegate_Size>(
                "Noesis_CreateUIPropertyMetadata_Size");
            _CreateUIPropertyMetadata_Thickness = lib.Find<CreateUIPropertyMetadataDelegate_Thickness>(
                "Noesis_CreateUIPropertyMetadata_Thickness");
            _CreateUIPropertyMetadata_CornerRadius = lib.Find<CreateUIPropertyMetadataDelegate_CornerRadius>(
                "Noesis_CreateUIPropertyMetadata_CornerRadius");
            _CreateUIPropertyMetadata_BaseComponent = lib.Find<CreateUIPropertyMetadataDelegate_BaseComponent>(
                "Noesis_CreateUIPropertyMetadata_BaseComponent");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public new static void UnregisterFunctions()
        {
            // create UIPropertyMetadata 
            _CreateUIPropertyMetadata_Bool = null;
            _CreateUIPropertyMetadata_Float = null;
            _CreateUIPropertyMetadata_Int = null;
            _CreateUIPropertyMetadata_UInt = null;
            _CreateUIPropertyMetadata_Short = null;
            _CreateUIPropertyMetadata_UShort = null;
            _CreateUIPropertyMetadata_String = null;
            _CreateUIPropertyMetadata_Color = null;
            _CreateUIPropertyMetadata_Point = null;
            _CreateUIPropertyMetadata_Rect = null;
            _CreateUIPropertyMetadata_Size = null;
            _CreateUIPropertyMetadata_Thickness = null;
            _CreateUIPropertyMetadata_CornerRadius = null;
            _CreateUIPropertyMetadata_BaseComponent = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_Bool(
            [MarshalAs(UnmanagedType.U1)] bool defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_Bool _CreateUIPropertyMetadata_Bool;
        private static IntPtr Noesis_CreateUIPropertyMetadata_Bool(bool defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_Bool(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_Float(float defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_Float _CreateUIPropertyMetadata_Float;
        private static IntPtr Noesis_CreateUIPropertyMetadata_Float(float defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_Float(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_Int(int defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_Int _CreateUIPropertyMetadata_Int;
        private static IntPtr Noesis_CreateUIPropertyMetadata_Int(int defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_Int(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_UInt(uint defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_UInt _CreateUIPropertyMetadata_UInt;
        private static IntPtr Noesis_CreateUIPropertyMetadata_UInt(uint defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_UInt(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_Short(short defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_Short _CreateUIPropertyMetadata_Short;
        private static IntPtr Noesis_CreateUIPropertyMetadata_Short(short defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_Short(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_UShort(ushort defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_UShort _CreateUIPropertyMetadata_UShort;
        private static IntPtr Noesis_CreateUIPropertyMetadata_UShort(ushort defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_UShort(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_String(
            [MarshalAs(UnmanagedType.LPStr)] string defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_String _CreateUIPropertyMetadata_String;
        private static IntPtr Noesis_CreateUIPropertyMetadata_String(string defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_String(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_Color(Noesis.Color defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_Color _CreateUIPropertyMetadata_Color;
        private static IntPtr Noesis_CreateUIPropertyMetadata_Color(Noesis.Color defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_Color(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_Point(Noesis.Point defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_Point _CreateUIPropertyMetadata_Point;
        private static IntPtr Noesis_CreateUIPropertyMetadata_Point(Noesis.Point defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_Point(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_Rect(Noesis.Rect defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_Rect _CreateUIPropertyMetadata_Rect;
        private static IntPtr Noesis_CreateUIPropertyMetadata_Rect(Noesis.Rect defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_Rect(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_Size(Noesis.Size defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_Size _CreateUIPropertyMetadata_Size;
        private static IntPtr Noesis_CreateUIPropertyMetadata_Size(Noesis.Size defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_Size(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_Thickness(Noesis.Thickness defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_Thickness _CreateUIPropertyMetadata_Thickness;
        private static IntPtr Noesis_CreateUIPropertyMetadata_Thickness(Noesis.Thickness defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_Thickness(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_CornerRadius(Noesis.CornerRadius defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_CornerRadius _CreateUIPropertyMetadata_CornerRadius;
        private static IntPtr Noesis_CreateUIPropertyMetadata_CornerRadius(Noesis.CornerRadius defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_CornerRadius(defaultValue, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateUIPropertyMetadataDelegate_BaseComponent(IntPtr defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateUIPropertyMetadataDelegate_BaseComponent _CreateUIPropertyMetadata_BaseComponent;
        private static IntPtr Noesis_CreateUIPropertyMetadata_BaseComponent(IntPtr defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateUIPropertyMetadata_BaseComponent(defaultValue, invokePropertyChangedCallback);
        }

    #else

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_Bool")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_Bool")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_Bool(
            [MarshalAs(UnmanagedType.U1)] bool defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_Float")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_Float")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_Float(float defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_Int")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_Int")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_Int(int defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_UInt")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_UInt")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_UInt(uint defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_Short")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_Short")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_Short(short defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_UShort")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_UShort")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_UShort(ushort defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_String")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_String")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_String(
            [MarshalAs(UnmanagedType.LPStr)] string defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_Color")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_Color")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_Color(
            Noesis.Color defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_Point")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_Point")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_Point(
            Noesis.Point defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_Rect")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_Rect")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_Rect(
            Noesis.Rect defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_Size")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_Size")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_Size(
            Noesis.Size defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_Thickness")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_Thickness")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_Thickness(
            Noesis.Thickness defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_CornerRadius")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_CornerRadius")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_CornerRadius(
            Noesis.CornerRadius defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateUIPropertyMetadata_BaseComponent")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateUIPropertyMetadata_BaseComponent")]
        #endif
        private static extern IntPtr Noesis_CreateUIPropertyMetadata_BaseComponent(IntPtr defaultValue,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

    #endif
        #endregion
    }

}
