using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    #if UNITY_EDITOR

    public static class _Extend
    {
        public static void Initialized(bool init)
        {
            Extend.Initialized = init;
        }

        public static void RegisterFunctions(Library lib)
        {
            Extend.RegisterFunctions(lib);
        }

        public static void UnregisterFunctions()
        {
            Extend.UnregisterFunctions();
        }

        public static void RegisterCallbacks()
        {
            Extend.RegisterCallbacks();
        }

        public static void UnregisterCallbacks()
        {
            Extend.UnregisterCallbacks();
        }

        public static void RegisterNativeTypes()
        {
            Extend.RegisterNativeTypes();
        }

        public static void ResetDependencyProperties()
        {
            Extend.ResetDependencyProperties();
        }
    }

    #endif

    internal partial class Extend
    {
        private static IntPtr Noesis_InstantiateExtend_(IntPtr unityType)
        {
            IntPtr result = Noesis_InstantiateExtend(unityType);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static void Noesis_LaunchPropertyChangedEvent_(IntPtr unityType, IntPtr cPtr, IntPtr propertyName)
        {
            Noesis_LaunchPropertyChangedEvent(unityType, cPtr, propertyName);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static void Noesis_LaunchCollectionChangedEvent_(IntPtr unityType, IntPtr cPtr, int action,
            IntPtr newItem, IntPtr oldItem, int newIndex, int oldIndex)
        {
            Noesis_LaunchCollectionChangedEvent(unityType, cPtr, action, newItem, oldItem, newIndex, oldIndex);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

        private static IntPtr Noesis_GetResourceKeyType_(IntPtr nativeType)
        {
            IntPtr result = Noesis_GetResourceKeyType(nativeType);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
            return result;
        }

        private static void Noesis_RegisterReflectionCallbacks_(
            Callback_RegisterType callback_RegisterType,
            Callback_DependencyPropertyChanged callback_DependencyPropertyChanged,
            Callback_OnPostInit callback_OnPostInit,
            Callback_CommandCanExecute callback_CommandCanExecute,
            Callback_CommandExecute callback_CommandExecute,
            Callback_ConverterConvert callback_ConverterConvert,
            Callback_ConverterConvertBack callback_ConverterConvertBack,
            Callback_ListCount callback_ListCount,
            Callback_ListGet callback_ListGet,
            Callback_ListSet callback_ListSet,
            Callback_ListAdd callback_ListAdd,
            Callback_ListClear callback_ListClear,
            Callback_ListContains callback_ListContains,
            Callback_ListIndexOf callback_ListIndexOf,
            Callback_ListInsert callback_ListInsert,
            Callback_ListRemove callback_ListRemove,
            Callback_ListRemoveAt callback_ListRemoveAt,
            Callback_DictionaryCount callback_DictionaryCount,
            Callback_DictionaryContains callback_DictionaryContains,
            Callback_DictionaryFind callback_DictionaryFind,
            Callback_DictionaryAdd callback_DictionaryAdd,
            Callback_DictionaryRemove callback_DictionaryRemove,
            Callback_DictionaryClear callback_DictionaryClear,
            Callback_GetName callback_GetName,
            Callback_GetBaseType callback_GetBaseType,
            Callback_GetPropertiesCount callback_GetPropertiesCount,
            Callback_GetPropertyIndex callback_GetPropertyIndex,
            Callback_GetPropertyType callback_GetPropertyType,
            Callback_GetPropertyInfo callback_GetPropertyInfo,
            Callback_GetPropertyValue_Bool callback_GetPropertyValue_Bool,
            Callback_GetPropertyValue_Float callback_GetPropertyValue_Float,
            Callback_GetPropertyValue_Int callback_GetPropertyValue_Int,
            Callback_GetPropertyValue_UInt callback_GetPropertyValue_UInt,
            Callback_GetPropertyValue_Short callback_GetPropertyValue_Short,
            Callback_GetPropertyValue_UShort callback_GetPropertyValue_UShort,
            Callback_GetPropertyValue_String callback_GetPropertyValue_String,
            Callback_GetPropertyValue_Color callback_GetPropertyValue_Color,
            Callback_GetPropertyValue_Point callback_GetPropertyValue_Point,
            Callback_GetPropertyValue_Rect callback_GetPropertyValue_Rect,
            Callback_GetPropertyValue_Size callback_GetPropertyValue_Size,
            Callback_GetPropertyValue_Thickness callback_GetPropertyValue_Thickness,
            Callback_GetPropertyValue_CornerRadius callback_GetPropertyValue_CornerRadius,
            Callback_GetPropertyValue_BaseComponent callback_GetPropertyValue_BaseComponent,
            Callback_SetPropertyValue_Bool callback_SetPropertyValue_Bool,
            Callback_SetPropertyValue_Float callback_SetPropertyValue_Float,
            Callback_SetPropertyValue_Int callback_SetPropertyValue_Int,
            Callback_SetPropertyValue_UInt callback_SetPropertyValue_UInt,
            Callback_SetPropertyValue_Short callback_SetPropertyValue_Short,
            Callback_SetPropertyValue_UShort callback_SetPropertyValue_UShort,
            Callback_SetPropertyValue_String callback_SetPropertyValue_String,
            Callback_SetPropertyValue_Color callback_SetPropertyValue_Color,
            Callback_SetPropertyValue_Point callback_SetPropertyValue_Point,
            Callback_SetPropertyValue_Rect callback_SetPropertyValue_Rect,
            Callback_SetPropertyValue_Size callback_SetPropertyValue_Size,
            Callback_SetPropertyValue_Thickness callback_SetPropertyValue_Thickness,
            Callback_SetPropertyValue_CornerRadius callback_SetPropertyValue_CornerRadius,
            Callback_SetPropertyValue_BaseComponent callback_SetPropertyValue_BaseComponent,
            Callback_CreateInstance callback_CreateInstance,
            Callback_DeleteInstance callback_DeleteInstance,
            Callback_GrabInstance callback_GrabInstance)
        {
            Noesis_RegisterReflectionCallbacks(
                callback_RegisterType,
                callback_DependencyPropertyChanged,
                callback_OnPostInit,
                callback_CommandCanExecute,
                callback_CommandExecute,
                callback_ConverterConvert,
                callback_ConverterConvertBack,
                callback_ListCount,
                callback_ListGet,
                callback_ListSet,
                callback_ListAdd,
                callback_ListClear,
                callback_ListContains,
                callback_ListIndexOf,
                callback_ListInsert,
                callback_ListRemove,
                callback_ListRemoveAt,
                callback_DictionaryCount,
                callback_DictionaryContains,
                callback_DictionaryFind,
                callback_DictionaryAdd,
                callback_DictionaryRemove,
                callback_DictionaryClear,
                callback_GetName,
                callback_GetBaseType,
                callback_GetPropertiesCount,
                callback_GetPropertyIndex,
                callback_GetPropertyType,
                callback_GetPropertyInfo,
                callback_GetPropertyValue_Bool,
                callback_GetPropertyValue_Float,
                callback_GetPropertyValue_Int,
                callback_GetPropertyValue_UInt,
                callback_GetPropertyValue_Short,
                callback_GetPropertyValue_UShort,
                callback_GetPropertyValue_String,
                callback_GetPropertyValue_Color,
                callback_GetPropertyValue_Point,
                callback_GetPropertyValue_Rect,
                callback_GetPropertyValue_Size,
                callback_GetPropertyValue_Thickness,
                callback_GetPropertyValue_CornerRadius,
                callback_GetPropertyValue_BaseComponent,
                callback_SetPropertyValue_Bool,
                callback_SetPropertyValue_Float,
                callback_SetPropertyValue_Int,
                callback_SetPropertyValue_UInt,
                callback_SetPropertyValue_Short,
                callback_SetPropertyValue_UShort,
                callback_SetPropertyValue_String,
                callback_SetPropertyValue_Color,
                callback_SetPropertyValue_Point,
                callback_SetPropertyValue_Rect,
                callback_SetPropertyValue_Size,
                callback_SetPropertyValue_Thickness,
                callback_SetPropertyValue_CornerRadius,
                callback_SetPropertyValue_BaseComponent,
                callback_CreateInstance,
                callback_DeleteInstance,
                callback_GrabInstance);
            #if UNITY_EDITOR || NOESIS_API
            Error.Check();
            #endif
        }

    #if UNITY_EDITOR

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void RegisterFunctions(Library lib)
        {
            _instantiateExtend = lib.Find<InstantiateExtendDelegate>("Noesis_InstantiateExtend");
            _launchPropertyChangedEvent = lib.Find<LaunchPropertyChangedEventDelegate>("Noesis_LaunchPropertyChangedEvent");
            _launchCollectionChangedEvent = lib.Find<LaunchCollectionChangedEventDelegate>("Noesis_LaunchCollectionChangedEvent");
            _getResourceKeyType = lib.Find<GetResourceKeyTypeDelegate>("Noesis_GetResourceKeyType");
            _registerReflectionCallbacks = lib.Find<RegisterReflectionCallbacksDelegate>("Noesis_RegisterReflectionCallbacks");

            DependencyObject.RegisterFunctions(lib);
            DependencyProperty.RegisterFunctions(lib);
            PropertyMetadata.RegisterFunctions(lib);
            UIPropertyMetadata.RegisterFunctions(lib);
            FrameworkPropertyMetadata.RegisterFunctions(lib);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void UnregisterFunctions()
        {
            _instantiateExtend = null;
            _launchPropertyChangedEvent = null;
            _launchCollectionChangedEvent = null;
            _getResourceKeyType = null;
            _registerReflectionCallbacks = null;

            DependencyObject.UnregisterFunctions();
            DependencyProperty.UnregisterFunctions();
            PropertyMetadata.UnregisterFunctions();
            UIPropertyMetadata.UnregisterFunctions();
            FrameworkPropertyMetadata.UnregisterFunctions();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr InstantiateExtendDelegate(IntPtr unityType);
        static InstantiateExtendDelegate _instantiateExtend;
        private static IntPtr Noesis_InstantiateExtend(IntPtr unityType)
        {
            return _instantiateExtend(unityType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void LaunchPropertyChangedEventDelegate(IntPtr unityType, IntPtr cPtr, IntPtr propertyName);
        static LaunchPropertyChangedEventDelegate _launchPropertyChangedEvent;
        private static void Noesis_LaunchPropertyChangedEvent(IntPtr unityType, IntPtr cPtr, IntPtr propertyName)
        {
            _launchPropertyChangedEvent(unityType, cPtr, propertyName);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void LaunchCollectionChangedEventDelegate(IntPtr unityType, IntPtr cPtr, int action,
            IntPtr newItem, IntPtr oldItem, int newIndex, int oldIndex);
        static LaunchCollectionChangedEventDelegate _launchCollectionChangedEvent;
        private static void Noesis_LaunchCollectionChangedEvent(IntPtr unityType, IntPtr cPtr, int action,
            IntPtr newItem, IntPtr oldItem, int newIndex, int oldIndex)
        {
            _launchCollectionChangedEvent(unityType, cPtr, action, newItem, oldItem, newIndex, oldIndex);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr GetResourceKeyTypeDelegate(IntPtr nativeType);
        static GetResourceKeyTypeDelegate _getResourceKeyType;
        private static IntPtr Noesis_GetResourceKeyType(IntPtr nativeType)
        {
            return _getResourceKeyType(nativeType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void RegisterReflectionCallbacksDelegate(
            Callback_RegisterType callback_RegisterType,
            Callback_DependencyPropertyChanged callback_DependencyPropertyChanged,
            Callback_OnPostInit callback_OnPostInit,
            Callback_CommandCanExecute callback_CommandCanExecute,
            Callback_CommandExecute callback_CommandExecute,
            Callback_ConverterConvert callback_ConverterConvert,
            Callback_ConverterConvertBack callback_ConverterConvertBack,
            Callback_ListCount callback_ListCount,
            Callback_ListGet callback_ListGet,
            Callback_ListSet callback_ListSet,
            Callback_ListAdd callback_ListAdd,
            Callback_ListClear callback_ListClear,
            Callback_ListContains callback_ListContains,
            Callback_ListIndexOf callback_ListIndexOf,
            Callback_ListInsert callback_ListInsert,
            Callback_ListRemove callback_ListRemove,
            Callback_ListRemoveAt callback_ListRemoveAt,
            Callback_DictionaryCount callback_DictionaryCount,
            Callback_DictionaryContains callback_DictionaryContains,
            Callback_DictionaryFind callback_DictionaryFind,
            Callback_DictionaryAdd callback_DictionaryAdd,
            Callback_DictionaryRemove callback_DictionaryRemove,
            Callback_DictionaryClear callback_DictionaryClear, 
            Callback_GetName callback_GetName,
            Callback_GetBaseType callback_GetBaseType,
            Callback_GetPropertiesCount callback_GetPropertiesCount,
            Callback_GetPropertyIndex callback_GetPropertyIndex,
            Callback_GetPropertyType callback_GetPropertyType,
            Callback_GetPropertyInfo callback_GetPropertyInfo,
            Callback_GetPropertyValue_Bool callback_GetPropertyValue_Bool,
            Callback_GetPropertyValue_Float callback_GetPropertyValue_Float,
            Callback_GetPropertyValue_Int callback_GetPropertyValue_Int,
            Callback_GetPropertyValue_UInt callback_GetPropertyValue_UInt,
            Callback_GetPropertyValue_Short callback_GetPropertyValue_Short,
            Callback_GetPropertyValue_UShort callback_GetPropertyValue_UShort,
            Callback_GetPropertyValue_String callback_GetPropertyValue_String,
            Callback_GetPropertyValue_Color callback_GetPropertyValue_Color,
            Callback_GetPropertyValue_Point callback_GetPropertyValue_Point,
            Callback_GetPropertyValue_Rect callback_GetPropertyValue_Rect,
            Callback_GetPropertyValue_Size callback_GetPropertyValue_Size,
            Callback_GetPropertyValue_Thickness callback_GetPropertyValue_Thickness,
            Callback_GetPropertyValue_CornerRadius callback_GetPropertyValue_CornerRadius,
            Callback_GetPropertyValue_BaseComponent callback_GetPropertyValue_BaseComponent,
            Callback_SetPropertyValue_Bool callback_SetPropertyValue_Bool,
            Callback_SetPropertyValue_Float callback_SetPropertyValue_Float,
            Callback_SetPropertyValue_Int callback_SetPropertyValue_Int,
            Callback_SetPropertyValue_UInt callback_SetPropertyValue_UInt,
            Callback_SetPropertyValue_Short callback_SetPropertyValue_Short,
            Callback_SetPropertyValue_UShort callback_SetPropertyValue_UShort,
            Callback_SetPropertyValue_String callback_SetPropertyValue_String,
            Callback_SetPropertyValue_Color callback_SetPropertyValue_Color,
            Callback_SetPropertyValue_Point callback_SetPropertyValue_Point,
            Callback_SetPropertyValue_Rect callback_SetPropertyValue_Rect,
            Callback_SetPropertyValue_Size callback_SetPropertyValue_Size,
            Callback_SetPropertyValue_Thickness callback_SetPropertyValue_Thickness,
            Callback_SetPropertyValue_CornerRadius callback_SetPropertyValue_CornerRadius,
            Callback_SetPropertyValue_BaseComponent callback_SetPropertyValue_BaseComponent,
            Callback_CreateInstance callback_CreateInstance,
            Callback_DeleteInstance callback_DeleteInstance,
            Callback_GrabInstance callback_GrabInstance);
        
        static RegisterReflectionCallbacksDelegate _registerReflectionCallbacks;
        private static void Noesis_RegisterReflectionCallbacks(
            Callback_RegisterType callback_RegisterType,
            Callback_DependencyPropertyChanged callback_DependencyPropertyChanged,
            Callback_OnPostInit callback_OnPostInit,
            Callback_CommandCanExecute callback_CommandCanExecute,
            Callback_CommandExecute callback_CommandExecute,
            Callback_ConverterConvert callback_ConverterConvert,
            Callback_ConverterConvertBack callback_ConverterConvertBack,
            Callback_ListCount callback_ListCount,
            Callback_ListGet callback_ListGet,
            Callback_ListSet callback_ListSet,
            Callback_ListAdd callback_ListAdd,
            Callback_ListClear callback_ListClear,
            Callback_ListContains callback_ListContains,
            Callback_ListIndexOf callback_ListIndexOf,
            Callback_ListInsert callback_ListInsert,
            Callback_ListRemove callback_ListRemove,
            Callback_ListRemoveAt callback_ListRemoveAt,
            Callback_DictionaryCount callback_DictionaryCount,
            Callback_DictionaryContains callback_DictionaryContains,
            Callback_DictionaryFind callback_DictionaryFind,
            Callback_DictionaryAdd callback_DictionaryAdd,
            Callback_DictionaryRemove callback_DictionaryRemove,
            Callback_DictionaryClear callback_DictionaryClear,
            Callback_GetName callback_GetName,
            Callback_GetBaseType callback_GetBaseType,
            Callback_GetPropertiesCount callback_GetPropertiesCount,
            Callback_GetPropertyIndex callback_GetPropertyIndex,
            Callback_GetPropertyType callback_GetPropertyType,
            Callback_GetPropertyInfo callback_GetPropertyInfo,
            Callback_GetPropertyValue_Bool callback_GetPropertyValue_Bool,
            Callback_GetPropertyValue_Float callback_GetPropertyValue_Float,
            Callback_GetPropertyValue_Int callback_GetPropertyValue_Int,
            Callback_GetPropertyValue_UInt callback_GetPropertyValue_UInt,
            Callback_GetPropertyValue_Short callback_GetPropertyValue_Short,
            Callback_GetPropertyValue_UShort callback_GetPropertyValue_UShort,
            Callback_GetPropertyValue_String callback_GetPropertyValue_String,
            Callback_GetPropertyValue_Color callback_GetPropertyValue_Color,
            Callback_GetPropertyValue_Point callback_GetPropertyValue_Point,
            Callback_GetPropertyValue_Rect callback_GetPropertyValue_Rect,
            Callback_GetPropertyValue_Size callback_GetPropertyValue_Size,
            Callback_GetPropertyValue_Thickness callback_GetPropertyValue_Thickness,
            Callback_GetPropertyValue_CornerRadius callback_GetPropertyValue_CornerRadius,
            Callback_GetPropertyValue_BaseComponent callback_GetPropertyValue_BaseComponent,
            Callback_SetPropertyValue_Bool callback_SetPropertyValue_Bool,
            Callback_SetPropertyValue_Float callback_SetPropertyValue_Float,
            Callback_SetPropertyValue_Int callback_SetPropertyValue_Int,
            Callback_SetPropertyValue_UInt callback_SetPropertyValue_UInt,
            Callback_SetPropertyValue_Short callback_SetPropertyValue_Short,
            Callback_SetPropertyValue_UShort callback_SetPropertyValue_UShort,
            Callback_SetPropertyValue_String callback_SetPropertyValue_String,
            Callback_SetPropertyValue_Color callback_SetPropertyValue_Color,
            Callback_SetPropertyValue_Point callback_SetPropertyValue_Point,
            Callback_SetPropertyValue_Rect callback_SetPropertyValue_Rect,
            Callback_SetPropertyValue_Size callback_SetPropertyValue_Size,
            Callback_SetPropertyValue_Thickness callback_SetPropertyValue_Thickness,
            Callback_SetPropertyValue_CornerRadius callback_SetPropertyValue_CornerRadius,
            Callback_SetPropertyValue_BaseComponent callback_SetPropertyValue_BaseComponent,
            Callback_CreateInstance callback_CreateInstance,
            Callback_DeleteInstance callback_DeleteInstance,
            Callback_GrabInstance callback_GrabInstance)
        {
            _registerReflectionCallbacks(
                callback_RegisterType,
                callback_DependencyPropertyChanged,
                callback_OnPostInit,
                callback_CommandCanExecute,
                callback_CommandExecute,
                callback_ConverterConvert,
                callback_ConverterConvertBack,
                callback_ListCount,
                callback_ListGet,
                callback_ListSet,
                callback_ListAdd,
                callback_ListClear,
                callback_ListContains,
                callback_ListIndexOf,
                callback_ListInsert,
                callback_ListRemove,
                callback_ListRemoveAt,
                callback_DictionaryCount,
                callback_DictionaryContains,
                callback_DictionaryFind,
                callback_DictionaryAdd,
                callback_DictionaryRemove,
                callback_DictionaryClear, 
                callback_GetName,
                callback_GetBaseType,
                callback_GetPropertiesCount,
                callback_GetPropertyIndex,
                callback_GetPropertyType,
                callback_GetPropertyInfo,
                callback_GetPropertyValue_Bool,
                callback_GetPropertyValue_Float,
                callback_GetPropertyValue_Int,
                callback_GetPropertyValue_UInt,
                callback_GetPropertyValue_Short,
                callback_GetPropertyValue_UShort,
                callback_GetPropertyValue_String,
                callback_GetPropertyValue_Color,
                callback_GetPropertyValue_Point,
                callback_GetPropertyValue_Rect,
                callback_GetPropertyValue_Size,
                callback_GetPropertyValue_Thickness,
                callback_GetPropertyValue_CornerRadius,
                callback_GetPropertyValue_BaseComponent,
                callback_SetPropertyValue_Bool,
                callback_SetPropertyValue_Float,
                callback_SetPropertyValue_Int,
                callback_SetPropertyValue_UInt,
                callback_SetPropertyValue_Short,
                callback_SetPropertyValue_UShort,
                callback_SetPropertyValue_String,
                callback_SetPropertyValue_Color,
                callback_SetPropertyValue_Point,
                callback_SetPropertyValue_Rect,
                callback_SetPropertyValue_Size,
                callback_SetPropertyValue_Thickness,
                callback_SetPropertyValue_CornerRadius,
                callback_SetPropertyValue_BaseComponent,
                callback_CreateInstance,
                callback_DeleteInstance,
                callback_GrabInstance);
        }

    #else

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_InstantiateExtend")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_InstantiateExtend")]
        #endif
        private static extern IntPtr Noesis_InstantiateExtend(IntPtr unityType);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_LaunchPropertyChangedEvent")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_LaunchPropertyChangedEvent")]
        #endif
        private static extern void Noesis_LaunchPropertyChangedEvent(IntPtr unityType, IntPtr cPtr, IntPtr propertyName);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_LaunchCollectionChangedEvent")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_LaunchCollectionChangedEvent")]
        #endif
        private static extern void Noesis_LaunchCollectionChangedEvent(IntPtr unityType, IntPtr cPtr,
            int action, IntPtr newItem, IntPtr oldItem, int newIndex, int oldIndex);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_GetResourceKeyType")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_GetResourceKeyType")]
        #endif
        private static extern IntPtr Noesis_GetResourceKeyType(IntPtr unityType);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_RegisterReflectionCallbacks")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_RegisterReflectionCallbacks")]
        #endif
        static extern void Noesis_RegisterReflectionCallbacks(
            Callback_RegisterType callback_RegisterType,
            Callback_DependencyPropertyChanged callback_DependencyPropertyChanged,
            Callback_OnPostInit callback_OnPostInit,
            Callback_CommandCanExecute callback_CommandCanExecute,
            Callback_CommandExecute callback_CommandExecute,
            Callback_ConverterConvert callback_ConverterConvert,
            Callback_ConverterConvertBack callback_ConverterConvertBack,
            Callback_ListCount callback_ListCount,
            Callback_ListGet callback_ListGet,
            Callback_ListSet callback_ListSet,
            Callback_ListAdd callback_ListAdd,
            Callback_ListClear callback_ListClear,
            Callback_ListContains callback_ListContains,
            Callback_ListIndexOf callback_ListIndexOf,
            Callback_ListInsert callback_ListInsert,
            Callback_ListRemove callback_ListRemove,
            Callback_ListRemoveAt callback_ListRemoveAt,
            Callback_DictionaryCount callback_DictionaryCount,
            Callback_DictionaryContains callback_DictionaryContains,
            Callback_DictionaryFind callback_DictionaryFind,
            Callback_DictionaryAdd callback_DictionaryAdd,
            Callback_DictionaryRemove callback_DictionaryRemove,
            Callback_DictionaryClear callback_DictionaryClear,
            Callback_GetName callback_GetName,
            Callback_GetBaseType callback_GetBaseType,
            Callback_GetPropertiesCount callback_GetPropertiesCount,
            Callback_GetPropertyIndex callback_GetPropertyIndex,
            Callback_GetPropertyType callback_GetPropertyType,
            Callback_GetPropertyInfo callback_GetPropertyInfo,
            Callback_GetPropertyValue_Bool callback_GetPropertyValue_Bool,
            Callback_GetPropertyValue_Float callback_GetPropertyValue_Float,
            Callback_GetPropertyValue_Int callback_GetPropertyValue_Int,
            Callback_GetPropertyValue_UInt callback_GetPropertyValue_UInt,
            Callback_GetPropertyValue_Short callback_GetPropertyValue_Short,
            Callback_GetPropertyValue_UShort callback_GetPropertyValue_UShort,
            Callback_GetPropertyValue_String callback_GetPropertyValue_String,
            Callback_GetPropertyValue_Color callback_GetPropertyValue_Color,
            Callback_GetPropertyValue_Point callback_GetPropertyValue_Point,
            Callback_GetPropertyValue_Rect callback_GetPropertyValue_Rect,
            Callback_GetPropertyValue_Size callback_GetPropertyValue_Size,
            Callback_GetPropertyValue_Thickness callback_GetPropertyValue_Thickness,
            Callback_GetPropertyValue_CornerRadius callback_GetPropertyValue_CornerRadius,
            Callback_GetPropertyValue_BaseComponent callback_GetPropertyValue_BaseComponent,
            Callback_SetPropertyValue_Bool callback_SetPropertyValue_Bool,
            Callback_SetPropertyValue_Float callback_SetPropertyValue_Float,
            Callback_SetPropertyValue_Int callback_SetPropertyValue_Int,
            Callback_SetPropertyValue_UInt callback_SetPropertyValue_UInt,
            Callback_SetPropertyValue_Short callback_SetPropertyValue_Short,
            Callback_SetPropertyValue_UShort callback_SetPropertyValue_UShort,
            Callback_SetPropertyValue_String callback_SetPropertyValue_String,
            Callback_SetPropertyValue_Color callback_SetPropertyValue_Color,
            Callback_SetPropertyValue_Point callback_SetPropertyValue_Point,
            Callback_SetPropertyValue_Rect callback_SetPropertyValue_Rect,
            Callback_SetPropertyValue_Size callback_SetPropertyValue_Size,
            Callback_SetPropertyValue_Thickness callback_SetPropertyValue_Thickness,
            Callback_SetPropertyValue_CornerRadius callback_SetPropertyValue_CornerRadius,
            Callback_SetPropertyValue_BaseComponent callback_SetPropertyValue_BaseComponent,
            Callback_CreateInstance callback_CreateInstance,
            Callback_DeleteInstance callback_DeleteInstance,
            Callback_GrabInstance callback_GrabInstance);

    #endif

    }
}

