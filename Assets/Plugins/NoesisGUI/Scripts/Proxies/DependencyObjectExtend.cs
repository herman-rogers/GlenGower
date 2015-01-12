using System;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Noesis
{

    public partial class DependencyObject
    {
        public T GetValue<T>(DependencyProperty dependencyProperty)
        {
            if (dependencyProperty == null)
            {
                throw new Exception("Can't get value, DependencyProperty is null");
            }

            T retVal = default(T);

            if (typeof(T) == typeof(bool))
            {
                retVal = (T)(object)Noesis_DependencyGet_Bool_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
            }
            else if (typeof(T) == typeof(float))
            {
                retVal = (T)(object)Noesis_DependencyGet_Float_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
            }
            else if (typeof(T) == typeof(int))
            {
                retVal = (T)(object)Noesis_DependencyGet_Int_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
            }
            else if (typeof(T) == typeof(uint))
            {
                retVal = (T)(object)Noesis_DependencyGet_UInt_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
            }
            else if (typeof(T) == typeof(short))
            {
                retVal = (T)(object)Noesis_DependencyGet_Short_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
            }
            else if (typeof(T) == typeof(ushort))
            {
                retVal = (T)(object)Noesis_DependencyGet_UShort_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
            }
            else if (typeof(T).GetTypeInfo().IsEnum)
            {
                IntPtr valPtr = Noesis_DependencyGet_String_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
                retVal = (T)Noesis.Extend.ParseEnum(typeof(T), Marshal.PtrToStringAnsi(valPtr),
                    GetType().Name, dependencyProperty.GetName());
            }
            else if (typeof(T) == typeof(string))
            {
                IntPtr valPtr = Noesis_DependencyGet_String_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
                retVal = (T)(object)Marshal.PtrToStringAnsi(valPtr);
            }
            else if (typeof(T) == typeof(Noesis.Color))
            {
                IntPtr valPtr = Noesis_DependencyGet_Color_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
                retVal = Marshal.PtrToStructure<T>(valPtr);
            }
            else if (typeof(T) == typeof(Noesis.Point))
            {
                IntPtr valPtr = Noesis_DependencyGet_Point_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
                retVal = Marshal.PtrToStructure<T>(valPtr);
            }
            else if (typeof(T) == typeof(Noesis.Rect))
            {
                IntPtr valPtr = Noesis_DependencyGet_Rect_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
                retVal = Marshal.PtrToStructure<T>(valPtr);
            }
            else if (typeof(T) == typeof(Noesis.Size))
            {
                IntPtr valPtr = Noesis_DependencyGet_Size_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
                retVal = Marshal.PtrToStructure<T>(valPtr);
            }
            else if (typeof(T) == typeof(Noesis.Thickness))
            {
                IntPtr valPtr = Noesis_DependencyGet_Thickness_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
                retVal = Marshal.PtrToStructure<T>(valPtr);
            }
            else if (typeof(T) == typeof(CornerRadius))
            {
                IntPtr valPtr = Noesis_DependencyGet_CornerRadius_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);
                retVal = Marshal.PtrToStructure<T>(valPtr);
            }
            else if (typeof(T) == typeof(BaseComponent) ||
                typeof(T).GetTypeInfo().IsSubclassOf(typeof(BaseComponent)))
            {
                IntPtr cPtr = Noesis_DependencyGet_BaseComponent_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle);

                return (T)(object)Noesis.Extend.GetProxy(cPtr, false);
            }

            return retVal;
        }

        public void SetValue<T>(DependencyProperty dependencyProperty, T val)
        {
            if (dependencyProperty == null)
            {
                throw new Exception("Can't get value, DependencyProperty is null");
            }

            if (typeof(T) == typeof(bool))
            {
                Noesis_DependencySet_Bool_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (bool)(object)val);
            }
            else if (typeof(T) == typeof(float))
            {
                Noesis_DependencySet_Float_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (float)(object)val);
            }
            else if (typeof(T) == typeof(int))
            {
                Noesis_DependencySet_Int_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (int)(object)val);
            }
            else if (typeof(T) == typeof(uint))
            {
                Noesis_DependencySet_UInt_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (uint)(object)val);
            }
            else if (typeof(T) == typeof(short))
            {
                Noesis_DependencySet_Short_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (short)(object)val);
            }
            else if (typeof(T) == typeof(ushort))
            {
                Noesis_DependencySet_UShort_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (ushort)(object)val);
            }
            else if (typeof(T).GetTypeInfo().IsEnum)
            {
                Noesis_DependencySet_String_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (string)(object)val);
            }
            else if (typeof(T) == typeof(string))
            {
                Noesis_DependencySet_String_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (string)(object)val);
            }
            else if (typeof(T) == typeof(Noesis.Color))
            {
                Noesis_DependencySet_Color_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (Noesis.Color)(object)val);
            }
            else if (typeof(T) == typeof(Noesis.Point))
            {
                Noesis_DependencySet_Point_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (Noesis.Point)(object)val);
            }
            else if (typeof(T) == typeof(Noesis.Rect))
            {
                Noesis_DependencySet_Rect_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (Noesis.Rect)(object)val);
            }
            else if (typeof(T) == typeof(Noesis.Size))
            {
                Noesis_DependencySet_Size_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (Noesis.Size)(object)val);
            }
            else if (typeof(T) == typeof(Noesis.Thickness))
            {
                Noesis_DependencySet_Thickness_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (Noesis.Thickness)(object)val);
            }
            else if (typeof(T) == typeof(CornerRadius))
            {
                Noesis_DependencySet_CornerRadius_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    (Noesis.CornerRadius)(object)val);
            }
            else if (typeof(T) == typeof(BaseComponent) || typeof(T).GetTypeInfo().IsSubclassOf(typeof(BaseComponent)))
            {
                IntPtr valPtr = BaseComponent.getCPtr((BaseComponent)(object)val).Handle;
                Noesis_DependencySet_BaseComponent_(swigCPtr.Handle,
                    DependencyProperty.getCPtr(dependencyProperty).Handle,
                    valPtr);
            }
        }

        #region Imports
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void CheckProperty(IntPtr dependencyObject, IntPtr dependencyProperty, string msg)
        {
            if (dependencyObject == IntPtr.Zero)
            {
                throw new Exception("Can't " + msg + " value, DependencyObject is null");
            }

            if (dependencyProperty == IntPtr.Zero)
            {
                throw new Exception("Can't " + msg + " value, DependencyProperty is null");
            }
        }

        private static bool Noesis_DependencyGet_Bool_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            bool result = Noesis_DependencyGet_Bool(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static float Noesis_DependencyGet_Float_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            float result = Noesis_DependencyGet_Float(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static int Noesis_DependencyGet_Int_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            int result = Noesis_DependencyGet_Int(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static uint Noesis_DependencyGet_UInt_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            uint result = Noesis_DependencyGet_UInt(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static short Noesis_DependencyGet_Short_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            short result = Noesis_DependencyGet_Short(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static ushort Noesis_DependencyGet_UShort_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            ushort result = Noesis_DependencyGet_UShort(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_DependencyGet_String_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            IntPtr result = Noesis_DependencyGet_String(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_DependencyGet_Color_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            IntPtr result = Noesis_DependencyGet_Color(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_DependencyGet_Point_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            IntPtr result = Noesis_DependencyGet_Point(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_DependencyGet_Rect_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            IntPtr result = Noesis_DependencyGet_Rect(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_DependencyGet_Size_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            IntPtr result = Noesis_DependencyGet_Size(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_DependencyGet_Thickness_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            IntPtr result = Noesis_DependencyGet_Thickness(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_DependencyGet_CornerRadius_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            IntPtr result = Noesis_DependencyGet_CornerRadius(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static IntPtr Noesis_DependencyGet_BaseComponent_(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            CheckProperty(dependencyObject, dependencyProperty, "get");
            IntPtr result = Noesis_DependencyGet_BaseComponent(dependencyObject, dependencyProperty);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static void Noesis_DependencySet_Bool_(IntPtr dependencyObject, IntPtr dependencyProperty, bool val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_Bool(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_Float_(IntPtr dependencyObject, IntPtr dependencyProperty, float val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_Float(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_Int_(IntPtr dependencyObject, IntPtr dependencyProperty, int val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_Int(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_UInt_(IntPtr dependencyObject, IntPtr dependencyProperty, uint val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_UInt(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_Short_(IntPtr dependencyObject, IntPtr dependencyProperty, short val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_Short(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_UShort_(IntPtr dependencyObject, IntPtr dependencyProperty, ushort val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_UShort(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_String_(IntPtr dependencyObject, IntPtr dependencyProperty, string val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_String(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_Color_(IntPtr dependencyObject, IntPtr dependencyProperty,
            Color val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_Color(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_Point_(IntPtr dependencyObject, IntPtr dependencyProperty,
            Point val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_Point(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_Rect_(IntPtr dependencyObject, IntPtr dependencyProperty,
            Rect val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_Rect(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_Size_(IntPtr dependencyObject, IntPtr dependencyProperty,
            Size val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_Size(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_Thickness_(IntPtr dependencyObject, IntPtr dependencyProperty,
            Thickness val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_Thickness(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_CornerRadius_(IntPtr dependencyObject, IntPtr dependencyProperty,
            CornerRadius val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_CornerRadius(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_DependencySet_BaseComponent_(IntPtr dependencyObject, IntPtr dependencyProperty,
            IntPtr val)
        {
            CheckProperty(dependencyObject, dependencyProperty, "set");
            Noesis_DependencySet_BaseComponent(dependencyObject, dependencyProperty, val);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

    #if UNITY_EDITOR

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void RegisterFunctions(Library lib)
        {
            // DependencyObject Get/Set
            _DependencyGet_Bool = lib.Find<DependencyGet_BoolDelegate>(
                "Noesis_DependencyGet_Bool");
            _DependencyGet_Float = lib.Find<DependencyGet_FloatDelegate>(
                "Noesis_DependencyGet_Float");
            _DependencyGet_Int = lib.Find<DependencyGet_IntDelegate>(
                "Noesis_DependencyGet_Int");
            _DependencyGet_UInt = lib.Find<DependencyGet_UIntDelegate>(
                "Noesis_DependencyGet_UInt");
            _DependencyGet_Short = lib.Find<DependencyGet_ShortDelegate>(
                "Noesis_DependencyGet_Short");
            _DependencyGet_UShort = lib.Find<DependencyGet_UShortDelegate>(
                "Noesis_DependencyGet_UShort");
            _DependencyGet_String = lib.Find<DependencyGet_StringDelegate>(
                "Noesis_DependencyGet_String");
            _DependencyGet_Color = lib.Find<DependencyGet_ColorDelegate>(
                "Noesis_DependencyGet_Color");
            _DependencyGet_Point = lib.Find<DependencyGet_PointDelegate>(
                "Noesis_DependencyGet_Point");
            _DependencyGet_Rect = lib.Find<DependencyGet_RectDelegate>(
                "Noesis_DependencyGet_Rect");
            _DependencyGet_Size = lib.Find<DependencyGet_SizeDelegate>(
                "Noesis_DependencyGet_Size");
            _DependencyGet_Thickness = lib.Find<DependencyGet_ThicknessDelegate>(
                "Noesis_DependencyGet_Thickness");
            _DependencyGet_CornerRadius = lib.Find<DependencyGet_CornerRadiusDelegate>(
                "Noesis_DependencyGet_CornerRadius");
            _DependencyGet_BaseComponent = lib.Find<DependencyGet_BaseComponentDelegate>(
                "Noesis_DependencyGet_BaseComponent");

            _DependencySet_Bool = lib.Find<DependencySet_BoolDelegate>(
                "Noesis_DependencySet_Bool");
            _DependencySet_Float = lib.Find<DependencySet_FloatDelegate>(
                "Noesis_DependencySet_Float");
            _DependencySet_Int = lib.Find<DependencySet_IntDelegate>(
                "Noesis_DependencySet_Int");
            _DependencySet_UInt = lib.Find<DependencySet_UIntDelegate>(
                "Noesis_DependencySet_UInt");
            _DependencySet_Short = lib.Find<DependencySet_ShortDelegate>(
                "Noesis_DependencySet_Short");
            _DependencySet_UShort = lib.Find<DependencySet_UShortDelegate>(
                "Noesis_DependencySet_UShort");
            _DependencySet_String = lib.Find<DependencySet_StringDelegate>(
                "Noesis_DependencySet_String");
            _DependencySet_Color = lib.Find<DependencySet_ColorDelegate>(
                "Noesis_DependencySet_Color");
            _DependencySet_Point = lib.Find<DependencySet_PointDelegate>(
                "Noesis_DependencySet_Point");
            _DependencySet_Rect = lib.Find<DependencySet_RectDelegate>(
                "Noesis_DependencySet_Rect");
            _DependencySet_Size = lib.Find<DependencySet_SizeDelegate>(
                "Noesis_DependencySet_Size");
            _DependencySet_Thickness = lib.Find<DependencySet_ThicknessDelegate>(
                "Noesis_DependencySet_Thickness");
            _DependencySet_CornerRadius = lib.Find<DependencySet_CornerRadiusDelegate>(
                "Noesis_DependencySet_CornerRadius");
            _DependencySet_BaseComponent = lib.Find<DependencySet_BaseComponentDelegate>(
                "Noesis_DependencySet_BaseComponent");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void UnregisterFunctions()
        {
            // DependencyObject Get/Set
            _DependencyGet_Bool = null;
            _DependencyGet_Float = null;
            _DependencyGet_Int = null;
            _DependencyGet_UInt = null;
            _DependencyGet_Short = null;
            _DependencyGet_UShort = null;
            _DependencyGet_String = null;
            _DependencyGet_Color = null;
            _DependencyGet_Point = null;
            _DependencyGet_Rect = null;
            _DependencyGet_Size = null;
            _DependencyGet_Thickness = null;
            _DependencyGet_CornerRadius = null;
            _DependencyGet_BaseComponent = null;

            _DependencySet_Bool = null;
            _DependencySet_Float = null;
            _DependencySet_Int = null;
            _DependencySet_UInt = null;
            _DependencySet_Short = null;
            _DependencySet_UShort = null;
            _DependencySet_String = null;
            _DependencySet_Color = null;
            _DependencySet_Point = null;
            _DependencySet_Rect = null;
            _DependencySet_Size = null;
            _DependencySet_Thickness = null;
            _DependencySet_CornerRadius = null;
            _DependencySet_BaseComponent = null;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        [return: MarshalAs(UnmanagedType.U1)]
        delegate bool DependencyGet_BoolDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_BoolDelegate _DependencyGet_Bool;
        private static bool Noesis_DependencyGet_Bool(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_Bool(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate float DependencyGet_FloatDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_FloatDelegate _DependencyGet_Float;
        private static float Noesis_DependencyGet_Float(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_Float(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate int DependencyGet_IntDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_IntDelegate _DependencyGet_Int;
        private static int Noesis_DependencyGet_Int(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_Int(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate uint DependencyGet_UIntDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_UIntDelegate _DependencyGet_UInt;
        private static uint Noesis_DependencyGet_UInt(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_UInt(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate short DependencyGet_ShortDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_ShortDelegate _DependencyGet_Short;
        private static short Noesis_DependencyGet_Short(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_Short(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate ushort DependencyGet_UShortDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_UShortDelegate _DependencyGet_UShort;
        private static ushort Noesis_DependencyGet_UShort(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_UShort(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr DependencyGet_StringDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_StringDelegate _DependencyGet_String;
        private static IntPtr Noesis_DependencyGet_String(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_String(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr DependencyGet_ColorDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_ColorDelegate _DependencyGet_Color;
        private static IntPtr Noesis_DependencyGet_Color(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_Color(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr DependencyGet_PointDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_PointDelegate _DependencyGet_Point;
        private static IntPtr Noesis_DependencyGet_Point(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_Point(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr DependencyGet_RectDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_RectDelegate _DependencyGet_Rect;
        private static IntPtr Noesis_DependencyGet_Rect(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_Rect(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr DependencyGet_SizeDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_SizeDelegate _DependencyGet_Size;
        private static IntPtr Noesis_DependencyGet_Size(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_Size(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr DependencyGet_ThicknessDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_ThicknessDelegate _DependencyGet_Thickness;
        private static IntPtr Noesis_DependencyGet_Thickness(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_Thickness(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr DependencyGet_CornerRadiusDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_CornerRadiusDelegate _DependencyGet_CornerRadius;
        private static IntPtr Noesis_DependencyGet_CornerRadius(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_CornerRadius(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr DependencyGet_BaseComponentDelegate(IntPtr dependencyObject, IntPtr dependencyProperty);
        static DependencyGet_BaseComponentDelegate _DependencyGet_BaseComponent;
        private static IntPtr Noesis_DependencyGet_BaseComponent(IntPtr dependencyObject, IntPtr dependencyProperty)
        {
            return _DependencyGet_BaseComponent(dependencyObject, dependencyProperty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_BoolDelegate(IntPtr dependencyObject, IntPtr dependencyProperty,
            [MarshalAs(UnmanagedType.U1)] bool val);
        static DependencySet_BoolDelegate _DependencySet_Bool;
        private static void Noesis_DependencySet_Bool(IntPtr dependencyObject, IntPtr dependencyProperty, bool val)
        {
            _DependencySet_Bool(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_FloatDelegate(IntPtr dependencyObject, IntPtr dependencyProperty, float val);
        static DependencySet_FloatDelegate _DependencySet_Float;
        private static void Noesis_DependencySet_Float(IntPtr dependencyObject, IntPtr dependencyProperty, float val)
        {
            _DependencySet_Float(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_IntDelegate(IntPtr dependencyObject, IntPtr dependencyProperty, int val);
        static DependencySet_IntDelegate _DependencySet_Int;
        private static void Noesis_DependencySet_Int(IntPtr dependencyObject, IntPtr dependencyProperty, int val)
        {
            _DependencySet_Int(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_UIntDelegate(IntPtr dependencyObject, IntPtr dependencyProperty, uint val);
        static DependencySet_UIntDelegate _DependencySet_UInt;
        private static void Noesis_DependencySet_UInt(IntPtr dependencyObject, IntPtr dependencyProperty, uint val)
        {
            _DependencySet_UInt(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_ShortDelegate(IntPtr dependencyObject, IntPtr dependencyProperty, short val);
        static DependencySet_ShortDelegate _DependencySet_Short;
        private static void Noesis_DependencySet_Short(IntPtr dependencyObject, IntPtr dependencyProperty, short val)
        {
            _DependencySet_Short(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_UShortDelegate(IntPtr dependencyObject, IntPtr dependencyProperty, ushort val);
        static DependencySet_UShortDelegate _DependencySet_UShort;
        private static void Noesis_DependencySet_UShort(IntPtr dependencyObject, IntPtr dependencyProperty, ushort val)
        {
            _DependencySet_UShort(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_StringDelegate(IntPtr dependencyObject, IntPtr dependencyProperty, string val);
        static DependencySet_StringDelegate _DependencySet_String;
        private static void Noesis_DependencySet_String(IntPtr dependencyObject, IntPtr dependencyProperty, string val)
        {
            _DependencySet_String(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_ColorDelegate(IntPtr dependencyObject, IntPtr dependencyProperty,
            Color val);
        static DependencySet_ColorDelegate _DependencySet_Color;
        private static void Noesis_DependencySet_Color(IntPtr dependencyObject, IntPtr dependencyProperty,
            Color val)
        {
            _DependencySet_Color(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_PointDelegate(IntPtr dependencyObject, IntPtr dependencyProperty,
            Point val);
        static DependencySet_PointDelegate _DependencySet_Point;
        private static void Noesis_DependencySet_Point(IntPtr dependencyObject, IntPtr dependencyProperty,
            Point val)
        {
            _DependencySet_Point(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_RectDelegate(IntPtr dependencyObject, IntPtr dependencyProperty,
            Rect val);
        static DependencySet_RectDelegate _DependencySet_Rect;
        private static void Noesis_DependencySet_Rect(IntPtr dependencyObject, IntPtr dependencyProperty,
            Rect val)
        {
            _DependencySet_Rect(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_SizeDelegate(IntPtr dependencyObject, IntPtr dependencyProperty,
            Size val);
        static DependencySet_SizeDelegate _DependencySet_Size;
        private static void Noesis_DependencySet_Size(IntPtr dependencyObject, IntPtr dependencyProperty,
            Size val)
        {
            _DependencySet_Size(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_ThicknessDelegate(IntPtr dependencyObject, IntPtr dependencyProperty,
            Thickness val);
        static DependencySet_ThicknessDelegate _DependencySet_Thickness;
        private static void Noesis_DependencySet_Thickness(IntPtr dependencyObject, IntPtr dependencyProperty,
            Thickness val)
        {
            _DependencySet_Thickness(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_CornerRadiusDelegate(IntPtr dependencyObject, IntPtr dependencyProperty,
            CornerRadius val);
        static DependencySet_CornerRadiusDelegate _DependencySet_CornerRadius;
        private static void Noesis_DependencySet_CornerRadius(IntPtr dependencyObject, IntPtr dependencyProperty,
            CornerRadius val)
        {
            _DependencySet_CornerRadius(dependencyObject, dependencyProperty, val);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DependencySet_BaseComponentDelegate(IntPtr dependencyObject, IntPtr dependencyProperty, IntPtr val);
        static DependencySet_BaseComponentDelegate _DependencySet_BaseComponent;
        private static void Noesis_DependencySet_BaseComponent(IntPtr dependencyObject, IntPtr dependencyProperty,
            IntPtr val)
        {
            _DependencySet_BaseComponent(dependencyObject, dependencyProperty, val);
        }

    #else

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_Bool")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_Bool")]
        #endif
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool Noesis_DependencyGet_Bool(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_Float")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_Float")]
        #endif
        private static extern float Noesis_DependencyGet_Float(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_Int")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_Int")]
        #endif
        private static extern int Noesis_DependencyGet_Int(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_UInt")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_UInt")]
        #endif
        private static extern uint Noesis_DependencyGet_UInt(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_Short")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_Short")]
        #endif
        private static extern short Noesis_DependencyGet_Short(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_UShort")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_UShort")]
        #endif
        private static extern ushort Noesis_DependencyGet_UShort(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_String")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_String")]
        #endif
        private static extern IntPtr Noesis_DependencyGet_String(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_Color")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_Color")]
        #endif
        private static extern IntPtr Noesis_DependencyGet_Color(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_Point")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_Point")]
        #endif
        private static extern IntPtr Noesis_DependencyGet_Point(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_Rect")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_Rect")]
        #endif
        private static extern IntPtr Noesis_DependencyGet_Rect(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_Size")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_Size")]
        #endif
        private static extern IntPtr Noesis_DependencyGet_Size(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_Thickness")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_Thickness")]
        #endif
        private static extern IntPtr Noesis_DependencyGet_Thickness(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_CornerRadius")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_CornerRadius")]
        #endif
        private static extern IntPtr Noesis_DependencyGet_CornerRadius(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencyGet_BaseComponent")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencyGet_BaseComponent")]
        #endif
        private static extern IntPtr Noesis_DependencyGet_BaseComponent(IntPtr dependencyObject,
            IntPtr dependencyProperty);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_Bool")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_Bool")]
        #endif
        private static extern void Noesis_DependencySet_Bool(IntPtr dependencyObject,
            IntPtr dependencyProperty, [MarshalAs(UnmanagedType.U1)] bool val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_Float")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_Float")]
        #endif
        private static extern void Noesis_DependencySet_Float(IntPtr dependencyObject,
            IntPtr dependencyProperty, float val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_Int")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_Int")]
        #endif
        private static extern void Noesis_DependencySet_Int(IntPtr dependencyObject,
            IntPtr dependencyProperty, int val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_UInt")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_UInt")]
        #endif
        private static extern void Noesis_DependencySet_UInt(IntPtr dependencyObject,
            IntPtr dependencyProperty, uint val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_Short")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_Short")]
        #endif
        private static extern void Noesis_DependencySet_Short(IntPtr dependencyObject,
            IntPtr dependencyProperty, short val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_UShort")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_UShort")]
        #endif
        private static extern void Noesis_DependencySet_UShort(IntPtr dependencyObject,
            IntPtr dependencyProperty, ushort val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_String")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_String")]
        #endif
        private static extern void Noesis_DependencySet_String(IntPtr dependencyObject,
            IntPtr dependencyProperty, string val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_Color")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_Color")]
        #endif
        private static extern void Noesis_DependencySet_Color(IntPtr dependencyObject,
            IntPtr dependencyProperty, Color val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_Point")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_Point")]
        #endif
        private static extern void Noesis_DependencySet_Point(IntPtr dependencyObject,
            IntPtr dependencyProperty, Point val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_Rect")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_Rect")]
        #endif
        private static extern void Noesis_DependencySet_Rect(IntPtr dependencyObject,
            IntPtr dependencyProperty, Rect val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_Size")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_Size")]
        #endif
        private static extern void Noesis_DependencySet_Size(IntPtr dependencyObject,
            IntPtr dependencyProperty, Size val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_Thickness")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_Thickness")]
        #endif
        private static extern void Noesis_DependencySet_Thickness(IntPtr dependencyObject,
            IntPtr dependencyProperty, Thickness val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_CornerRadius")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_CornerRadius")]
        #endif
        private static extern void Noesis_DependencySet_CornerRadius(IntPtr dependencyObject,
            IntPtr dependencyProperty, CornerRadius val);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_DependencySet_BaseComponent")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_DependencySet_BaseComponent")]
        #endif
        private static extern void Noesis_DependencySet_BaseComponent(IntPtr dependencyObject,
            IntPtr dependencyProperty, IntPtr val);

    #endif
        #endregion
    }

}
