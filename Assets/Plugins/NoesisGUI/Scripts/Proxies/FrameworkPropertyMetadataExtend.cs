using System;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Noesis
{

    public partial class FrameworkPropertyMetadata
    {
        public FrameworkPropertyMetadata(object defaultValue)
            : this(Create(defaultValue, FrameworkOptions.None, null), true)
        {
        }

        public FrameworkPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback)
            : this(Create(defaultValue, FrameworkOptions.None, propertyChangedCallback), true)
        {
        }

        public FrameworkPropertyMetadata(object defaultValue, FrameworkOptions options)
            : this(Create(defaultValue, options, null), true)
        {
        }

        public FrameworkPropertyMetadata(object defaultValue, FrameworkOptions options, PropertyChangedCallback propertyChangedCallback)
            : this(Create(defaultValue, options, propertyChangedCallback), true)
        {
        }

        private static IntPtr Create(object defaultValue, FrameworkOptions options,
            PropertyChangedCallback propertyChangedCallback)
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
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_BaseComponent_(IntPtr.Zero,
                    (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is bool)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_Bool_(
                    (bool)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is float)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_Float_(
                    (float)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is int)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_Int_(
                    (int)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is uint)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_UInt_(
                    (uint)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is short)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_Short_(
                    (short)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is ushort)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_UShort_(
                    (ushort)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is string)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_String_(
                    (string)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Color)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_Color_(
                    (Noesis.Color)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Point)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_Point_(
                    (Noesis.Point)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Rect)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_Rect_(
                    (Noesis.Rect)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Size)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_Size_(
                    (Noesis.Size)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.Thickness)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_Thickness_(
                    (Noesis.Thickness)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.CornerRadius)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_CornerRadius_(
                    (Noesis.CornerRadius)defaultValue, (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is Type)
            {
                IntPtr defPtr = Noesis.Extend.GetResourceKeyType(defaultValue as Type);
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_BaseComponent_(defPtr,
                    (int)options, invokePropertyChangedCallback);
            }
            else if (defaultValue is Noesis.BaseComponent)
            {
                propertyMetadataPtr = Noesis_CreateFrameworkPropertyMetadata_BaseComponent_(
                    Noesis.BaseComponent.getCPtr((Noesis.BaseComponent)defaultValue).Handle,
                    (int)options, invokePropertyChangedCallback);
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
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Bool_(bool defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_Bool(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Float_(float defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_Float(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Int_(int defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_Int(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_UInt_(uint defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_UInt(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Short_(short defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_Short(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_UShort_(ushort defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_UShort(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_String_(string defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_String(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Color_(Noesis.Color defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_Color(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Point_(Noesis.Point defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_Point(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Rect_(Noesis.Rect defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_Rect(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Size_(Noesis.Size defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_Size(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Thickness_(Noesis.Thickness defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_Thickness(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_CornerRadius_(Noesis.CornerRadius defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_CornerRadius(defaultValue, options,
                invokePropertyChangedCallback);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_BaseComponent_(IntPtr defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata_BaseComponent(defaultValue, options,
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
            // create FrameworkPropertyMetadata 
            _CreateFrameworkPropertyMetadata_Bool = lib.Find<CreateFrameworkPropertyMetadataDelegate_Bool>(
                "Noesis_CreateFrameworkPropertyMetadata_Bool");
            _CreateFrameworkPropertyMetadata_Float = lib.Find<CreateFrameworkPropertyMetadataDelegate_Float>(
                "Noesis_CreateFrameworkPropertyMetadata_Float");
            _CreateFrameworkPropertyMetadata_Int = lib.Find<CreateFrameworkPropertyMetadataDelegate_Int>(
                "Noesis_CreateFrameworkPropertyMetadata_Int");
            _CreateFrameworkPropertyMetadata_UInt = lib.Find<CreateFrameworkPropertyMetadataDelegate_UInt>(
                "Noesis_CreateFrameworkPropertyMetadata_UInt");
            _CreateFrameworkPropertyMetadata_Short = lib.Find<CreateFrameworkPropertyMetadataDelegate_Short>(
                "Noesis_CreateFrameworkPropertyMetadata_Short");
            _CreateFrameworkPropertyMetadata_UShort = lib.Find<CreateFrameworkPropertyMetadataDelegate_UShort>(
                "Noesis_CreateFrameworkPropertyMetadata_UShort");
            _CreateFrameworkPropertyMetadata_String = lib.Find<CreateFrameworkPropertyMetadataDelegate_String>(
                "Noesis_CreateFrameworkPropertyMetadata_String");
            _CreateFrameworkPropertyMetadata_Color = lib.Find<CreateFrameworkPropertyMetadataDelegate_Color>(
                "Noesis_CreateFrameworkPropertyMetadata_Color");
            _CreateFrameworkPropertyMetadata_Point = lib.Find<CreateFrameworkPropertyMetadataDelegate_Point>(
                "Noesis_CreateFrameworkPropertyMetadata_Point");
            _CreateFrameworkPropertyMetadata_Rect = lib.Find<CreateFrameworkPropertyMetadataDelegate_Rect>(
                "Noesis_CreateFrameworkPropertyMetadata_Rect");
            _CreateFrameworkPropertyMetadata_Size = lib.Find<CreateFrameworkPropertyMetadataDelegate_Size>(
                "Noesis_CreateFrameworkPropertyMetadata_Size");
            _CreateFrameworkPropertyMetadata_Thickness = lib.Find<CreateFrameworkPropertyMetadataDelegate_Thickness>(
                "Noesis_CreateFrameworkPropertyMetadata_Thickness");
            _CreateFrameworkPropertyMetadata_CornerRadius = lib.Find<CreateFrameworkPropertyMetadataDelegate_CornerRadius>(
                "Noesis_CreateFrameworkPropertyMetadata_CornerRadius");
            _CreateFrameworkPropertyMetadata_BaseComponent = lib.Find<CreateFrameworkPropertyMetadataDelegate_BaseComponent>(
                "Noesis_CreateFrameworkPropertyMetadata_BaseComponent");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public new static void UnregisterFunctions()
        {
            // create FrameworkPropertyMetadata 
            _CreateFrameworkPropertyMetadata_Bool = null;
            _CreateFrameworkPropertyMetadata_Float = null;
            _CreateFrameworkPropertyMetadata_Int = null;
            _CreateFrameworkPropertyMetadata_UInt = null;
            _CreateFrameworkPropertyMetadata_Short = null;
            _CreateFrameworkPropertyMetadata_UShort = null;
            _CreateFrameworkPropertyMetadata_String = null;
            _CreateFrameworkPropertyMetadata_Color = null;
            _CreateFrameworkPropertyMetadata_Point = null;
            _CreateFrameworkPropertyMetadata_Rect = null;
            _CreateFrameworkPropertyMetadata_Size = null;
            _CreateFrameworkPropertyMetadata_Thickness = null;
            _CreateFrameworkPropertyMetadata_CornerRadius = null;
            _CreateFrameworkPropertyMetadata_BaseComponent = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_Bool(
            [MarshalAs(UnmanagedType.U1)] bool defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_Bool _CreateFrameworkPropertyMetadata_Bool;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Bool(bool defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_Bool(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_Float(float defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_Float _CreateFrameworkPropertyMetadata_Float;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Float(float defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_Float(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_Int(int defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_Int _CreateFrameworkPropertyMetadata_Int;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Int(int defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_Int(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_UInt(uint defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_UInt _CreateFrameworkPropertyMetadata_UInt;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_UInt(uint defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_UInt(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_Short(short defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_Short _CreateFrameworkPropertyMetadata_Short;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Short(short defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_Short(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_UShort(ushort defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_UShort _CreateFrameworkPropertyMetadata_UShort;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_UShort(ushort defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_UShort(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_String(
            [MarshalAs(UnmanagedType.LPStr)] string defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_String _CreateFrameworkPropertyMetadata_String;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_String(string defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_String(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_Color(Noesis.Color defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_Color _CreateFrameworkPropertyMetadata_Color;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Color(Noesis.Color defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_Color(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_Point(Noesis.Point defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_Point _CreateFrameworkPropertyMetadata_Point;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Point(Noesis.Point defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_Point(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_Rect(Noesis.Rect defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_Rect _CreateFrameworkPropertyMetadata_Rect;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Rect(Noesis.Rect defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_Rect(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_Size(Noesis.Size defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_Size _CreateFrameworkPropertyMetadata_Size;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Size(Noesis.Size defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_Size(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_Thickness(Noesis.Thickness defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_Thickness _CreateFrameworkPropertyMetadata_Thickness;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_Thickness(Noesis.Thickness defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_Thickness(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_CornerRadius(Noesis.CornerRadius defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_CornerRadius _CreateFrameworkPropertyMetadata_CornerRadius;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_CornerRadius(Noesis.CornerRadius defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_CornerRadius(defaultValue, options, invokePropertyChangedCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateFrameworkPropertyMetadataDelegate_BaseComponent(IntPtr defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        static CreateFrameworkPropertyMetadataDelegate_BaseComponent _CreateFrameworkPropertyMetadata_BaseComponent;
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_BaseComponent(IntPtr defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            return _CreateFrameworkPropertyMetadata_BaseComponent(defaultValue, options, invokePropertyChangedCallback);
        }

    #else

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_Bool")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_Bool")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_Bool(
            [MarshalAs(UnmanagedType.U1)] bool defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_Float")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_Float")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_Float(float defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_Int")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_Int")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_Int(int defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_UInt")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_UInt")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_UInt(uint defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_Short")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_Short")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_Short(short defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_UShort")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_UShort")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_UShort(ushort defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_String")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_String")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_String(
            [MarshalAs(UnmanagedType.LPStr)] string defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_Color")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_Color")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_Color(
            Noesis.Color defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_Point")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_Point")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_Point(
            Noesis.Point defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_Rect")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_Rect")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_Rect(
            Noesis.Rect defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_Size")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_Size")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_Size(
            Noesis.Size defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_Thickness")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_Thickness")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_Thickness(
            Noesis.Thickness defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_CornerRadius")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_CornerRadius")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_CornerRadius(
            Noesis.CornerRadius defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateFrameworkPropertyMetadata_BaseComponent")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateFrameworkPropertyMetadata_BaseComponent")]
        #endif
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata_BaseComponent(
            IntPtr defaultValue, int options,
            DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

    #endif
        #endregion
    }

}
