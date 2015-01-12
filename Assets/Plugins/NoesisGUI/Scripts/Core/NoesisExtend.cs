using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Globalization;
using System.Linq;

namespace Noesis
{
    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Specifies the source XAML file of a UserControl
    ////////////////////////////////////////////////////////////////////////////////////////////////
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class UserControlSource : System.Attribute
    {
        internal string source = "";

        public UserControlSource(string xamlSource)
        {
            this.source = xamlSource;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Manages Noesis Extensibility
    ////////////////////////////////////////////////////////////////////////////////////////////////
    internal partial class Extend
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool Initialized { get; internal set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void RegisterCallbacks()
        {
            // register callbacks
            Noesis_RegisterReflectionCallbacks_(

                _registerType,

                _dependencyPropertyChanged,

                _onPostInit,

                _commandCanExecute,
                _commandExecute,

                _converterConvert,
                _converterConvertBack,

                _listCount,
                _listGet,
                _listSet,
                _listAdd,
                _listClear,
                _listContains,
                _listIndexOf,
                _listInsert,
                _listRemove,
                _listRemoveAt,

                _dictionaryCount,
                _dictionaryContains,
                _dictionaryFind,
                _dictionaryAdd,
                _dictionaryRemove,
                _dictionaryClear,

                _getName,
                _getBaseType,
                _getPropertiesCount,
                _getPropertyIndex,
                _getPropertyType,
                _getPropertyInfo,

                _getPropertyValue_Bool,
                _getPropertyValue_Float,
                _getPropertyValue_Int,
                _getPropertyValue_UInt,
                _getPropertyValue_Short,
                _getPropertyValue_UShort,
                _getPropertyValue_String,
                _getPropertyValue_Color,
                _getPropertyValue_Point,
                _getPropertyValue_Rect,
                _getPropertyValue_Size,
                _getPropertyValue_Thickness,
                _getPropertyValue_CornerRadius,
                _getPropertyValue_BaseComponent,

                _setPropertyValue_Bool,
                _setPropertyValue_Float,
                _setPropertyValue_Int,
                _setPropertyValue_UInt,
                _setPropertyValue_Short,
                _setPropertyValue_UShort,
                _setPropertyValue_String,
                _setPropertyValue_Color,
                _setPropertyValue_Point,
                _setPropertyValue_Rect,
                _setPropertyValue_Size,
                _setPropertyValue_Thickness,
                _setPropertyValue_CornerRadius,
                _setPropertyValue_BaseComponent,

                _createInstance,
                _deleteInstance,
                _grabInstance);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void UnregisterCallbacks()
        {
            // unregister callbacks
            Noesis_RegisterReflectionCallbacks_(
                null,
                null,
                null,
                null, null,
                null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null,
                null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null);

            _nativeTypes.Clear();
            _managedTypes.Clear();
            _types.Clear();
            _extends.Clear();
            _extendPtrs.Clear();
            _weakExtends.Clear();
            PropertyMetadata.ClearCallbacks();

            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public enum NativeTypeKind
        {
            Basic,
            Boxed,
            Component,
            Extended
        }

        public delegate Noesis.BaseComponent ComponentCreatorDelegate(IntPtr cPtr, bool ownMemory);

        public class NativeTypeInfo
        {
            public NativeTypeKind Kind { get; private set; }
            public System.Type Type { get; private set; }
            public ComponentCreatorDelegate Creator { get; private set; }

            public NativeTypeInfo(NativeTypeKind kind, System.Type type, ComponentCreatorDelegate creator)
            {
                Kind = kind;
                Type = type;
                Creator = creator;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        static Dictionary<IntPtr, NativeTypeInfo> _nativeTypes = new Dictionary<IntPtr, NativeTypeInfo>(320);
        static Dictionary<Type, IntPtr> _managedTypes = new Dictionary<Type, IntPtr>(320);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void AddNativeType(IntPtr nativeType, NativeTypeInfo info)
        {
            _nativeTypes[nativeType] = info;
            _managedTypes[info.Type] = nativeType;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static IntPtr GetNativeType(Type type)
        {
            IntPtr nativeType;
            if (!_managedTypes.TryGetValue(type, out nativeType))
            {
                throw new InvalidOperationException("Native type is not registered");
            }
            return nativeType;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static NativeTypeInfo GetNativeTypeInfo(IntPtr nativeType)
        {
            NativeTypeInfo info;
            if (!_nativeTypes.TryGetValue(nativeType, out info))
            {
                throw new InvalidOperationException("Native type is not registered");
            }
            return info;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static object GetProxy(IntPtr cPtr, bool ownMemory)
        {
            if (cPtr != IntPtr.Zero)
            {
                IntPtr nativeType = Noesis.BaseComponent.GetDynamicType(cPtr);
                NativeTypeInfo info = GetNativeTypeInfo(nativeType);

                switch (info.Kind)
                {
                    default:
                    case NativeTypeKind.Basic:
                    case NativeTypeKind.Boxed:
                        return new Noesis.BaseComponent(cPtr, ownMemory);

                    case NativeTypeKind.Component:
                        return GetProxyInstance(cPtr, ownMemory, info);

                    case NativeTypeKind.Extended:
                        return GetExtendInstance(cPtr);
                }
            }
            return null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static object Box(object val)
        {
            if (val is bool)
            {
                return AddProxy(NoesisGUI_.Box_Bool((bool)val));
            }
            else if (val is float)
            {
                return AddProxy(NoesisGUI_.Box_Float((float)val));
            }
            else if (val is int)
            {
                return AddProxy(NoesisGUI_.Box_Int((int)val));
            }
            else if (val is uint)
            {
                return AddProxy(NoesisGUI_.Box_UInt((uint)val));
            }
            else if (val is short)
            {
                return AddProxy(NoesisGUI_.Box_Short((short)val));
            }
            else if (val is ushort)
            {
                return AddProxy(NoesisGUI_.Box_UShort((ushort)val));
            }
            else if (val is string)
            {
                return AddProxy(NoesisGUI_.Box_String((string)val));
            }
            else if (val is Noesis.Color)
            {
                return AddProxy(NoesisGUI_.Box_Color((Noesis.Color)val));
            }
            else if (val is Noesis.Point)
            {
                return AddProxy(NoesisGUI_.Box_Point((Noesis.Point)val));
            }
            else if (val is Noesis.Rect)
            {
                return AddProxy(NoesisGUI_.Box_Rect((Noesis.Rect)val));
            }
            else if (val is Noesis.Size)
            {
                return AddProxy(NoesisGUI_.Box_Size((Noesis.Size)val));
            }
            else if (val is Noesis.Thickness)
            {
                return AddProxy(NoesisGUI_.Box_Thickness((Noesis.Thickness)val));
            }
            else if (val is Noesis.CornerRadius)
            {
                return AddProxy(NoesisGUI_.Box_CornerRadius((Noesis.CornerRadius)val));
            }

            return val;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static object Unbox(object obj)
        {
            Noesis.BaseComponent val = obj as Noesis.BaseComponent;
            if (val != null)
            {
                IntPtr cPtr = Noesis.BaseComponent.getCPtr(val).Handle;
                IntPtr nativeType = Noesis.BaseComponent.GetDynamicType(cPtr);
                NativeTypeInfo info = GetNativeTypeInfo(nativeType);

                if (info.Kind == NativeTypeKind.Boxed)
                {
                    if (info.Type == typeof(bool))
                    {
                        return NoesisGUI_.Unbox_Bool(val);
                    }
                    else if (info.Type == typeof(float))
                    {
                        return NoesisGUI_.Unbox_Float(val);
                    }
                    else if (info.Type == typeof(int))
                    {
                        return NoesisGUI_.Unbox_Int(val);
                    }
                    else if (info.Type == typeof(uint))
                    {
                        return NoesisGUI_.Unbox_UInt(val);
                    }
                    else if (info.Type == typeof(short))
                    {
                        return NoesisGUI_.Unbox_Short(val);
                    }
                    else if (info.Type == typeof(ushort))
                    {
                        return NoesisGUI_.Unbox_UShort(val);
                    }
                    else if (info.Type == typeof(string))
                    {
                        return NoesisGUI_.Unbox_String(val);
                    }
                    else if (info.Type == typeof(Noesis.Color))
                    {
                        return NoesisGUI_.Unbox_Color(val);
                    }
                    else if (info.Type == typeof(Noesis.Point))
                    {
                        return NoesisGUI_.Unbox_Point(val);
                    }
                    else if (info.Type == typeof(Noesis.Rect))
                    {
                        return NoesisGUI_.Unbox_Rect(val);
                    }
                    else if (info.Type == typeof(Noesis.Size))
                    {
                        return NoesisGUI_.Unbox_Size(val);
                    }
                    else if (info.Type == typeof(Noesis.Thickness))
                    {
                        return NoesisGUI_.Unbox_Thickness(val);
                    }
                    else if (info.Type == typeof(Noesis.CornerRadius))
                    {
                        return NoesisGUI_.Unbox_CornerRadius(val);
                    }
                }
            }
            return obj;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void RegisterNativeTypes()
        {
            AddNativeType(NoesisGUI_.GetType_Bool(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(bool), null));
            AddNativeType(NoesisGUI_.GetType_Float(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(float), null));
            AddNativeType(NoesisGUI_.GetType_Int(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(int), null));
            AddNativeType(NoesisGUI_.GetType_UInt(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(uint), null));
            AddNativeType(NoesisGUI_.GetType_Short(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(short), null));
            AddNativeType(NoesisGUI_.GetType_UShort(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(ushort), null));
            AddNativeType(NoesisGUI_.GetType_String(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(string), null));
            AddNativeType(NoesisGUI_.GetType_Color(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(Noesis.Color), null));
            AddNativeType(NoesisGUI_.GetType_Point(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(Noesis.Point), null));
            AddNativeType(NoesisGUI_.GetType_Rect(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(Noesis.Rect), null));
            AddNativeType(NoesisGUI_.GetType_Size(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(Noesis.Size), null));
            AddNativeType(NoesisGUI_.GetType_Thickness(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(Noesis.Thickness), null));
            AddNativeType(NoesisGUI_.GetType_CornerRadius(), new NativeTypeInfo(NativeTypeKind.Basic, typeof(Noesis.CornerRadius), null));

            AddNativeType(NoesisGUI_.GetType_Boxed_Bool(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(bool), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_Float(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(float), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_Int(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(int), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_UInt(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(uint), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_Short(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(short), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_UShort(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(ushort), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_String(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(string), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_Color(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(Noesis.Color), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_Point(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(Noesis.Point), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_Rect(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(Noesis.Rect), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_Size(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(Noesis.Size), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_Thickness(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(Noesis.Thickness), null));
            AddNativeType(NoesisGUI_.GetType_Boxed_CornerRadius(), new NativeTypeInfo(NativeTypeKind.Boxed, typeof(Noesis.CornerRadius), null));

            AddNativeType(Noesis.BaseComponent.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BaseComponent), (cPtr, ownMemory) => new Noesis.BaseComponent(cPtr, ownMemory)));

            AddNativeType(Noesis.AdornerDecorator.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.AdornerDecorator), (cPtr, ownMemory) => new Noesis.AdornerDecorator(cPtr, ownMemory)));
            AddNativeType(Noesis.Animatable.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Animatable), (cPtr, ownMemory) => new Noesis.Animatable(cPtr, ownMemory)));
            AddNativeType(Noesis.BindingBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BindingBase), (cPtr, ownMemory) => new Noesis.BindingBase(cPtr, ownMemory)));
            AddNativeType(Noesis.BindingExpressionBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BindingExpressionBase), (cPtr, ownMemory) => new Noesis.BindingExpressionBase(cPtr, ownMemory)));
            AddNativeType(Noesis.ButtonBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ButtonBase), (cPtr, ownMemory) => new Noesis.ButtonBase(cPtr, ownMemory)));
            AddNativeType(Noesis.DefinitionBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DefinitionBase), (cPtr, ownMemory) => new Noesis.DefinitionBase(cPtr, ownMemory)));
            AddNativeType(Noesis.MenuBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.MenuBase), (cPtr, ownMemory) => new Noesis.MenuBase(cPtr, ownMemory)));
            AddNativeType(Noesis.SetterBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SetterBase), (cPtr, ownMemory) => new Noesis.SetterBase(cPtr, ownMemory)));
            AddNativeType(Noesis.SetterBaseCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SetterBaseCollection), (cPtr, ownMemory) => new Noesis.SetterBaseCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.TextBoxBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TextBoxBase), (cPtr, ownMemory) => new Noesis.TextBoxBase(cPtr, ownMemory)));
            AddNativeType(Noesis.TriggerBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TriggerBase), (cPtr, ownMemory) => new Noesis.TriggerBase(cPtr, ownMemory)));
            AddNativeType(Noesis.Binding.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Binding), (cPtr, ownMemory) => new Noesis.Binding(cPtr, ownMemory)));
            AddNativeType(Noesis.Border.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Border), (cPtr, ownMemory) => new Noesis.Border(cPtr, ownMemory)));
            AddNativeType(Noesis.Brush.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Brush), (cPtr, ownMemory) => new Noesis.Brush(cPtr, ownMemory)));
            AddNativeType(Noesis.BulletDecorator.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BulletDecorator), (cPtr, ownMemory) => new Noesis.BulletDecorator(cPtr, ownMemory)));
            AddNativeType(Noesis.Button.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Button), (cPtr, ownMemory) => new Noesis.Button(cPtr, ownMemory)));
            AddNativeType(Noesis.Canvas.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Canvas), (cPtr, ownMemory) => new Noesis.Canvas(cPtr, ownMemory)));
            AddNativeType(Noesis.CheckBox.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.CheckBox), (cPtr, ownMemory) => new Noesis.CheckBox(cPtr, ownMemory)));
            AddNativeType(Noesis.Collection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Collection), (cPtr, ownMemory) => new Noesis.Collection(cPtr, ownMemory)));
            AddNativeType(Noesis.CollectionView.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.CollectionView), (cPtr, ownMemory) => new Noesis.CollectionView(cPtr, ownMemory)));
            AddNativeType(Noesis.CollectionViewSource.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.CollectionViewSource), (cPtr, ownMemory) => new Noesis.CollectionViewSource(cPtr, ownMemory)));
            AddNativeType(Noesis.ColumnDefinition.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ColumnDefinition), (cPtr, ownMemory) => new Noesis.ColumnDefinition(cPtr, ownMemory)));
            AddNativeType(Noesis.ColumnDefinitionCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ColumnDefinitionCollection), (cPtr, ownMemory) => new Noesis.ColumnDefinitionCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.CombinedGeometry.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.CombinedGeometry), (cPtr, ownMemory) => new Noesis.CombinedGeometry(cPtr, ownMemory)));
            AddNativeType(Noesis.ComboBox.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ComboBox), (cPtr, ownMemory) => new Noesis.ComboBox(cPtr, ownMemory)));
            AddNativeType(Noesis.ComboBoxItem.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ComboBoxItem), (cPtr, ownMemory) => new Noesis.ComboBoxItem(cPtr, ownMemory)));
            AddNativeType(Noesis.CommandBinding.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.CommandBinding), (cPtr, ownMemory) => new Noesis.CommandBinding(cPtr, ownMemory)));
            AddNativeType(Noesis.CommandBindingCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.CommandBindingCollection), (cPtr, ownMemory) => new Noesis.CommandBindingCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.CompositeTransform.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.CompositeTransform), (cPtr, ownMemory) => new Noesis.CompositeTransform(cPtr, ownMemory)));
            AddNativeType(Noesis.Condition.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Condition), (cPtr, ownMemory) => new Noesis.Condition(cPtr, ownMemory)));
            AddNativeType(Noesis.ConditionCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ConditionCollection), (cPtr, ownMemory) => new Noesis.ConditionCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.ContentControl.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ContentControl), (cPtr, ownMemory) => new Noesis.ContentControl(cPtr, ownMemory)));
            AddNativeType(Noesis.ContentPresenter.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ContentPresenter), (cPtr, ownMemory) => new Noesis.ContentPresenter(cPtr, ownMemory)));
            AddNativeType(Noesis.ContextMenu.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ContextMenu), (cPtr, ownMemory) => new Noesis.ContextMenu(cPtr, ownMemory)));
            AddNativeType(Noesis.Control.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Control), (cPtr, ownMemory) => new Noesis.Control(cPtr, ownMemory)));
            AddNativeType(Noesis.ControlTemplate.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ControlTemplate), (cPtr, ownMemory) => new Noesis.ControlTemplate(cPtr, ownMemory)));
            AddNativeType(Noesis.DashStyle.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DashStyle), (cPtr, ownMemory) => new Noesis.DashStyle(cPtr, ownMemory)));
            AddNativeType(Noesis.DataTemplate.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DataTemplate), (cPtr, ownMemory) => new Noesis.DataTemplate(cPtr, ownMemory)));
            AddNativeType(Noesis.DataTemplateSelector.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DataTemplateSelector), (cPtr, ownMemory) => new Noesis.DataTemplateSelector(cPtr, ownMemory)));
            AddNativeType(Noesis.Decorator.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Decorator), (cPtr, ownMemory) => new Noesis.Decorator(cPtr, ownMemory)));
            AddNativeType(Noesis.DockPanel.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DockPanel), (cPtr, ownMemory) => new Noesis.DockPanel(cPtr, ownMemory)));
            AddNativeType(Noesis.Ellipse.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Ellipse), (cPtr, ownMemory) => new Noesis.Ellipse(cPtr, ownMemory)));
            AddNativeType(Noesis.EllipseGeometry.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EllipseGeometry), (cPtr, ownMemory) => new Noesis.EllipseGeometry(cPtr, ownMemory)));
            AddNativeType(Noesis.EventTrigger.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EventTrigger), (cPtr, ownMemory) => new Noesis.EventTrigger(cPtr, ownMemory)));
            AddNativeType(Noesis.Expander.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Expander), (cPtr, ownMemory) => new Noesis.Expander(cPtr, ownMemory)));
            AddNativeType(Noesis.FamilyTypeface.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.FamilyTypeface), (cPtr, ownMemory) => new Noesis.FamilyTypeface(cPtr, ownMemory)));
            AddNativeType(Noesis.FamilyTypefaceCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.FamilyTypefaceCollection), (cPtr, ownMemory) => new Noesis.FamilyTypefaceCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.FontFamily.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.FontFamily), (cPtr, ownMemory) => new Noesis.FontFamily(cPtr, ownMemory)));
            AddNativeType(Noesis.FormattedText.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.FormattedText), (cPtr, ownMemory) => new Noesis.FormattedText(cPtr, ownMemory)));
            AddNativeType(Noesis.FrameworkElement.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.FrameworkElement), (cPtr, ownMemory) => new Noesis.FrameworkElement(cPtr, ownMemory)));
            AddNativeType(Noesis.FrameworkTemplate.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.FrameworkTemplate), (cPtr, ownMemory) => new Noesis.FrameworkTemplate(cPtr, ownMemory)));
            AddNativeType(Noesis.FreezableCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.FreezableCollection), (cPtr, ownMemory) => new Noesis.FreezableCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.Geometry.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Geometry), (cPtr, ownMemory) => new Noesis.Geometry(cPtr, ownMemory)));
            AddNativeType(Noesis.GeometryCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GeometryCollection), (cPtr, ownMemory) => new Noesis.GeometryCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.GeometryGroup.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GeometryGroup), (cPtr, ownMemory) => new Noesis.GeometryGroup(cPtr, ownMemory)));
            AddNativeType(Noesis.GradientBrush.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GradientBrush), (cPtr, ownMemory) => new Noesis.GradientBrush(cPtr, ownMemory)));
            AddNativeType(Noesis.GradientStop.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GradientStop), (cPtr, ownMemory) => new Noesis.GradientStop(cPtr, ownMemory)));
            AddNativeType(Noesis.GradientStopCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GradientStopCollection), (cPtr, ownMemory) => new Noesis.GradientStopCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.Grid.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Grid), (cPtr, ownMemory) => new Noesis.Grid(cPtr, ownMemory)));
            AddNativeType(Noesis.GroupBox.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GroupBox), (cPtr, ownMemory) => new Noesis.GroupBox(cPtr, ownMemory)));
            AddNativeType(Noesis.HeaderedContentControl.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.HeaderedContentControl), (cPtr, ownMemory) => new Noesis.HeaderedContentControl(cPtr, ownMemory)));
            AddNativeType(Noesis.HeaderedItemsControl.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.HeaderedItemsControl), (cPtr, ownMemory) => new Noesis.HeaderedItemsControl(cPtr, ownMemory)));
            AddNativeType(Noesis.Image.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Image), (cPtr, ownMemory) => new Noesis.Image(cPtr, ownMemory)));
            AddNativeType(Noesis.ImageBrush.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ImageBrush), (cPtr, ownMemory) => new Noesis.ImageBrush(cPtr, ownMemory)));
            AddNativeType(Noesis.ImageSource.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ImageSource), (cPtr, ownMemory) => new Noesis.ImageSource(cPtr, ownMemory)));
            AddNativeType(Noesis.Inline.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Inline), (cPtr, ownMemory) => new Noesis.Inline(cPtr, ownMemory)));
            AddNativeType(Noesis.InlineCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.InlineCollection), (cPtr, ownMemory) => new Noesis.InlineCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.InputBinding.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.InputBinding), (cPtr, ownMemory) => new Noesis.InputBinding(cPtr, ownMemory)));
            AddNativeType(Noesis.InputBindingCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.InputBindingCollection), (cPtr, ownMemory) => new Noesis.InputBindingCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.InputGesture.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.InputGesture), (cPtr, ownMemory) => new Noesis.InputGesture(cPtr, ownMemory)));
            AddNativeType(Noesis.InputGestureCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.InputGestureCollection), (cPtr, ownMemory) => new Noesis.InputGestureCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.ItemCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ItemCollection), (cPtr, ownMemory) => new Noesis.ItemCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.ItemsControl.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ItemsControl), (cPtr, ownMemory) => new Noesis.ItemsControl(cPtr, ownMemory)));
            AddNativeType(Noesis.ItemsPanelTemplate.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ItemsPanelTemplate), (cPtr, ownMemory) => new Noesis.ItemsPanelTemplate(cPtr, ownMemory)));
            AddNativeType(Noesis.ItemsPresenter.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ItemsPresenter), (cPtr, ownMemory) => new Noesis.ItemsPresenter(cPtr, ownMemory)));
            AddNativeType(Noesis.KeyBinding.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.KeyBinding), (cPtr, ownMemory) => new Noesis.KeyBinding(cPtr, ownMemory)));
            AddNativeType(Noesis.Keyboard.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Keyboard), (cPtr, ownMemory) => new Noesis.Keyboard(cPtr, ownMemory)));
            AddNativeType(Noesis.KeyboardNavigation.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.KeyboardNavigation), (cPtr, ownMemory) => new Noesis.KeyboardNavigation(cPtr, ownMemory)));
            AddNativeType(Noesis.KeyGesture.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.KeyGesture), (cPtr, ownMemory) => new Noesis.KeyGesture(cPtr, ownMemory)));
            AddNativeType(Noesis.Label.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Label), (cPtr, ownMemory) => new Noesis.Label(cPtr, ownMemory)));
            AddNativeType(Noesis.Line.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Line), (cPtr, ownMemory) => new Noesis.Line(cPtr, ownMemory)));
            AddNativeType(Noesis.LinearGradientBrush.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LinearGradientBrush), (cPtr, ownMemory) => new Noesis.LinearGradientBrush(cPtr, ownMemory)));
            AddNativeType(Noesis.LineBreak.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LineBreak), (cPtr, ownMemory) => new Noesis.LineBreak(cPtr, ownMemory)));
            AddNativeType(Noesis.LineGeometry.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LineGeometry), (cPtr, ownMemory) => new Noesis.LineGeometry(cPtr, ownMemory)));
            AddNativeType(Noesis.ListBox.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ListBox), (cPtr, ownMemory) => new Noesis.ListBox(cPtr, ownMemory)));
            AddNativeType(Noesis.ListBoxItem.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ListBoxItem), (cPtr, ownMemory) => new Noesis.ListBoxItem(cPtr, ownMemory)));
            AddNativeType(Noesis.Matrix3DProjection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Matrix3DProjection), (cPtr, ownMemory) => new Noesis.Matrix3DProjection(cPtr, ownMemory)));
            AddNativeType(Noesis.MatrixTransform.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.MatrixTransform), (cPtr, ownMemory) => new Noesis.MatrixTransform(cPtr, ownMemory)));
            AddNativeType(Noesis.Menu.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Menu), (cPtr, ownMemory) => new Noesis.Menu(cPtr, ownMemory)));
            AddNativeType(Noesis.MenuItem.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.MenuItem), (cPtr, ownMemory) => new Noesis.MenuItem(cPtr, ownMemory)));
            AddNativeType(Noesis.MultiTrigger.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.MultiTrigger), (cPtr, ownMemory) => new Noesis.MultiTrigger(cPtr, ownMemory)));
            AddNativeType(Noesis.NameScope.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.NameScope), (cPtr, ownMemory) => new Noesis.NameScope(cPtr, ownMemory)));
            AddNativeType(Noesis.Page.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Page), (cPtr, ownMemory) => new Noesis.Page(cPtr, ownMemory)));
            AddNativeType(Noesis.Panel.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Panel), (cPtr, ownMemory) => new Noesis.Panel(cPtr, ownMemory)));
            AddNativeType(Noesis.PasswordBox.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.PasswordBox), (cPtr, ownMemory) => new Noesis.PasswordBox(cPtr, ownMemory)));
            AddNativeType(Noesis.Path.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Path), (cPtr, ownMemory) => new Noesis.Path(cPtr, ownMemory)));
            AddNativeType(Noesis.Pen.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Pen), (cPtr, ownMemory) => new Noesis.Pen(cPtr, ownMemory)));
            AddNativeType(Noesis.PlaneProjection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.PlaneProjection), (cPtr, ownMemory) => new Noesis.PlaneProjection(cPtr, ownMemory)));
            AddNativeType(Noesis.Popup.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Popup), (cPtr, ownMemory) => new Noesis.Popup(cPtr, ownMemory)));
            AddNativeType(Noesis.ProgressBar.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ProgressBar), (cPtr, ownMemory) => new Noesis.ProgressBar(cPtr, ownMemory)));
            AddNativeType(Noesis.Projection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Projection), (cPtr, ownMemory) => new Noesis.Projection(cPtr, ownMemory)));
            AddNativeType(Noesis.PropertyPath.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.PropertyPath), (cPtr, ownMemory) => new Noesis.PropertyPath(cPtr, ownMemory)));
            AddNativeType(Noesis.RadialGradientBrush.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RadialGradientBrush), (cPtr, ownMemory) => new Noesis.RadialGradientBrush(cPtr, ownMemory)));
            AddNativeType(Noesis.RadioButton.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RadioButton), (cPtr, ownMemory) => new Noesis.RadioButton(cPtr, ownMemory)));
            AddNativeType(Noesis.RangeBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RangeBase), (cPtr, ownMemory) => new Noesis.RangeBase(cPtr, ownMemory)));
            AddNativeType(Noesis.Rectangle.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Rectangle), (cPtr, ownMemory) => new Noesis.Rectangle(cPtr, ownMemory)));
            AddNativeType(Noesis.RectangleGeometry.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RectangleGeometry), (cPtr, ownMemory) => new Noesis.RectangleGeometry(cPtr, ownMemory)));
            AddNativeType(Noesis.RelativeSource.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RelativeSource), (cPtr, ownMemory) => new Noesis.RelativeSource(cPtr, ownMemory)));
            AddNativeType(Noesis.RepeatButton.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RepeatButton), (cPtr, ownMemory) => new Noesis.RepeatButton(cPtr, ownMemory)));
            AddNativeType(Noesis.ResourceDictionary.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ResourceDictionary), (cPtr, ownMemory) => new Noesis.ResourceDictionary(cPtr, ownMemory)));
            AddNativeType(Noesis.ResourceDictionaryCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ResourceDictionaryCollection), (cPtr, ownMemory) => new Noesis.ResourceDictionaryCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.ResourceKeyType.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ResourceKeyType), (cPtr, ownMemory) => new Noesis.ResourceKeyType(cPtr, ownMemory)));
            AddNativeType(Noesis.RotateTransform.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RotateTransform), (cPtr, ownMemory) => new Noesis.RotateTransform(cPtr, ownMemory)));
            AddNativeType(Noesis.RoutedCommand.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RoutedCommand), (cPtr, ownMemory) => new Noesis.RoutedCommand(cPtr, ownMemory)));
            AddNativeType(Noesis.RoutedEvent.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RoutedEvent), (cPtr, ownMemory) => new Noesis.RoutedEvent(cPtr, ownMemory)));
            AddNativeType(Noesis.RoutedUICommand.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RoutedUICommand), (cPtr, ownMemory) => new Noesis.RoutedUICommand(cPtr, ownMemory)));
            AddNativeType(Noesis.RowDefinition.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RowDefinition), (cPtr, ownMemory) => new Noesis.RowDefinition(cPtr, ownMemory)));
            AddNativeType(Noesis.RowDefinitionCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RowDefinitionCollection), (cPtr, ownMemory) => new Noesis.RowDefinitionCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.Run.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Run), (cPtr, ownMemory) => new Noesis.Run(cPtr, ownMemory)));
            AddNativeType(Noesis.ScaleTransform.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ScaleTransform), (cPtr, ownMemory) => new Noesis.ScaleTransform(cPtr, ownMemory)));
            AddNativeType(Noesis.ScrollBar.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ScrollBar), (cPtr, ownMemory) => new Noesis.ScrollBar(cPtr, ownMemory)));
            AddNativeType(Noesis.ScrollViewer.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ScrollViewer), (cPtr, ownMemory) => new Noesis.ScrollViewer(cPtr, ownMemory)));
            AddNativeType(Noesis.Selector.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Selector), (cPtr, ownMemory) => new Noesis.Selector(cPtr, ownMemory)));
            AddNativeType(Noesis.Separator.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Separator), (cPtr, ownMemory) => new Noesis.Separator(cPtr, ownMemory)));
            AddNativeType(Noesis.Setter.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Setter), (cPtr, ownMemory) => new Noesis.Setter(cPtr, ownMemory)));
            AddNativeType(Noesis.Shape.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Shape), (cPtr, ownMemory) => new Noesis.Shape(cPtr, ownMemory)));
            AddNativeType(Noesis.SkewTransform.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SkewTransform), (cPtr, ownMemory) => new Noesis.SkewTransform(cPtr, ownMemory)));
            AddNativeType(Noesis.Slider.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Slider), (cPtr, ownMemory) => new Noesis.Slider(cPtr, ownMemory)));
            AddNativeType(Noesis.SolidColorBrush.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SolidColorBrush), (cPtr, ownMemory) => new Noesis.SolidColorBrush(cPtr, ownMemory)));
            AddNativeType(Noesis.StackPanel.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.StackPanel), (cPtr, ownMemory) => new Noesis.StackPanel(cPtr, ownMemory)));
            AddNativeType(Noesis.StatusBar.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.StatusBar), (cPtr, ownMemory) => new Noesis.StatusBar(cPtr, ownMemory)));
            AddNativeType(Noesis.StatusBarItem.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.StatusBarItem), (cPtr, ownMemory) => new Noesis.StatusBarItem(cPtr, ownMemory)));
            AddNativeType(Noesis.StreamGeometry.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.StreamGeometry), (cPtr, ownMemory) => new Noesis.StreamGeometry(cPtr, ownMemory)));
            AddNativeType(Noesis.Style.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Style), (cPtr, ownMemory) => new Noesis.Style(cPtr, ownMemory)));
            AddNativeType(Noesis.TabControl.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TabControl), (cPtr, ownMemory) => new Noesis.TabControl(cPtr, ownMemory)));
            AddNativeType(Noesis.TabItem.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TabItem), (cPtr, ownMemory) => new Noesis.TabItem(cPtr, ownMemory)));
            AddNativeType(Noesis.TabPanel.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TabPanel), (cPtr, ownMemory) => new Noesis.TabPanel(cPtr, ownMemory)));
            AddNativeType(Noesis.TemplateBinding.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TemplateBinding), (cPtr, ownMemory) => new Noesis.TemplateBinding(cPtr, ownMemory)));
            AddNativeType(Noesis.TextBlock.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TextBlock), (cPtr, ownMemory) => new Noesis.TextBlock(cPtr, ownMemory)));
            AddNativeType(Noesis.TextBox.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TextBox), (cPtr, ownMemory) => new Noesis.TextBox(cPtr, ownMemory)));
            AddNativeType(Noesis.TextElement.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TextElement), (cPtr, ownMemory) => new Noesis.TextElement(cPtr, ownMemory)));
            AddNativeType(Noesis.TextureSource.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TextureSource), (cPtr, ownMemory) => new Noesis.TextureSource(cPtr, ownMemory)));
            AddNativeType(Noesis.Thumb.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Thumb), (cPtr, ownMemory) => new Noesis.Thumb(cPtr, ownMemory)));
            AddNativeType(Noesis.TickBar.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TickBar), (cPtr, ownMemory) => new Noesis.TickBar(cPtr, ownMemory)));
            AddNativeType(Noesis.TileBrush.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TileBrush), (cPtr, ownMemory) => new Noesis.TileBrush(cPtr, ownMemory)));
            AddNativeType(Noesis.ToggleButton.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ToggleButton), (cPtr, ownMemory) => new Noesis.ToggleButton(cPtr, ownMemory)));
            AddNativeType(Noesis.ToolBar.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ToolBar), (cPtr, ownMemory) => new Noesis.ToolBar(cPtr, ownMemory)));
            AddNativeType(Noesis.ToolBarOverflowPanel.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ToolBarOverflowPanel), (cPtr, ownMemory) => new Noesis.ToolBarOverflowPanel(cPtr, ownMemory)));
            AddNativeType(Noesis.ToolBarPanel.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ToolBarPanel), (cPtr, ownMemory) => new Noesis.ToolBarPanel(cPtr, ownMemory)));
            AddNativeType(Noesis.ToolBarTray.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ToolBarTray), (cPtr, ownMemory) => new Noesis.ToolBarTray(cPtr, ownMemory)));
            AddNativeType(Noesis.ToolTip.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ToolTip), (cPtr, ownMemory) => new Noesis.ToolTip(cPtr, ownMemory)));
            AddNativeType(Noesis.Track.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Track), (cPtr, ownMemory) => new Noesis.Track(cPtr, ownMemory)));
            AddNativeType(Noesis.TransformGroup.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TransformGroup), (cPtr, ownMemory) => new Noesis.TransformGroup(cPtr, ownMemory)));
            AddNativeType(Noesis.TranslateTransform.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TranslateTransform), (cPtr, ownMemory) => new Noesis.TranslateTransform(cPtr, ownMemory)));
            AddNativeType(Noesis.Transform.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Transform), (cPtr, ownMemory) => new Noesis.Transform(cPtr, ownMemory)));
            AddNativeType(Noesis.TransformCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TransformCollection), (cPtr, ownMemory) => new Noesis.TransformCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.TreeView.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TreeView), (cPtr, ownMemory) => new Noesis.TreeView(cPtr, ownMemory)));
            AddNativeType(Noesis.TreeViewItem.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TreeViewItem), (cPtr, ownMemory) => new Noesis.TreeViewItem(cPtr, ownMemory)));
            AddNativeType(Noesis.TriggerAction.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TriggerAction), (cPtr, ownMemory) => new Noesis.TriggerAction(cPtr, ownMemory)));
            AddNativeType(Noesis.TriggerActionCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TriggerActionCollection), (cPtr, ownMemory) => new Noesis.TriggerActionCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.Trigger.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Trigger), (cPtr, ownMemory) => new Noesis.Trigger(cPtr, ownMemory)));
            AddNativeType(Noesis.TriggerCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TriggerCollection), (cPtr, ownMemory) => new Noesis.TriggerCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.UIElement.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.UIElement), (cPtr, ownMemory) => new Noesis.UIElement(cPtr, ownMemory)));
            AddNativeType(Noesis.UIElementCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.UIElementCollection), (cPtr, ownMemory) => new Noesis.UIElementCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.UserControl.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.UserControl), (cPtr, ownMemory) => new Noesis.UserControl(cPtr, ownMemory)));
            AddNativeType(Noesis.Viewbox.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Viewbox), (cPtr, ownMemory) => new Noesis.Viewbox(cPtr, ownMemory)));
            AddNativeType(Noesis.Visual.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Visual), (cPtr, ownMemory) => new Noesis.Visual(cPtr, ownMemory)));
            AddNativeType(NoesisGUI_.GetRootVisualType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Visual), (cPtr, ownMemory) => new Noesis.Visual(cPtr, ownMemory)));
            AddNativeType(Noesis.VisualBrush.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.VisualBrush), (cPtr, ownMemory) => new Noesis.VisualBrush(cPtr, ownMemory)));
            AddNativeType(Noesis.VisualCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.VisualCollection), (cPtr, ownMemory) => new Noesis.VisualCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.WrapPanel.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.WrapPanel), (cPtr, ownMemory) => new Noesis.WrapPanel(cPtr, ownMemory)));


            AddNativeType(Noesis.Clock.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Clock), (cPtr, ownMemory) => new Noesis.Clock(cPtr, ownMemory)));
            AddNativeType(Noesis.ClockGroup.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ClockGroup), (cPtr, ownMemory) => new Noesis.ClockGroup(cPtr, ownMemory)));
            AddNativeType(Noesis.AnimationClock.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.AnimationClock), (cPtr, ownMemory) => new Noesis.AnimationClock(cPtr, ownMemory)));

            AddNativeType(Noesis.Timeline.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Timeline), (cPtr, ownMemory) => new Noesis.Timeline(cPtr, ownMemory)));
            AddNativeType(Noesis.TimelineCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TimelineCollection), (cPtr, ownMemory) => new Noesis.TimelineCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.TimelineGroup.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.TimelineGroup), (cPtr, ownMemory) => new Noesis.TimelineGroup(cPtr, ownMemory)));
            AddNativeType(Noesis.ParallelTimeline.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ParallelTimeline), (cPtr, ownMemory) => new Noesis.ParallelTimeline(cPtr, ownMemory)));
            AddNativeType(Noesis.AnimationTimeline.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.AnimationTimeline), (cPtr, ownMemory) => new Noesis.AnimationTimeline(cPtr, ownMemory)));
            AddNativeType(Noesis.Storyboard.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Storyboard), (cPtr, ownMemory) => new Noesis.Storyboard(cPtr, ownMemory)));

            AddNativeType(Noesis.Int16Animation.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Int16Animation), (cPtr, ownMemory) => new Noesis.Int16Animation(cPtr, ownMemory)));
            AddNativeType(Noesis.Int32Animation.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Int32Animation), (cPtr, ownMemory) => new Noesis.Int32Animation(cPtr, ownMemory)));
            AddNativeType(Noesis.DoubleAnimation.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DoubleAnimation), (cPtr, ownMemory) => new Noesis.DoubleAnimation(cPtr, ownMemory)));
            AddNativeType(Noesis.ColorAnimation.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Color), (cPtr, ownMemory) => new Noesis.ColorAnimation(cPtr, ownMemory)));
            AddNativeType(Noesis.PointAnimation.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.PointAnimation), (cPtr, ownMemory) => new Noesis.PointAnimation(cPtr, ownMemory)));
            AddNativeType(Noesis.RectAnimation.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RectAnimation), (cPtr, ownMemory) => new Noesis.RectAnimation(cPtr, ownMemory)));
            AddNativeType(Noesis.SizeAnimation.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SizeAnimation), (cPtr, ownMemory) => new Noesis.SizeAnimation(cPtr, ownMemory)));
            AddNativeType(Noesis.ThicknessAnimation.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ThicknessAnimation), (cPtr, ownMemory) => new Noesis.ThicknessAnimation(cPtr, ownMemory)));

            AddNativeType(Noesis.BooleanAnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BooleanAnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.BooleanAnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.Int16AnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Int16AnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.Int16AnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.Int32AnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Int32AnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.Int32AnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.DoubleAnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DoubleAnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.DoubleAnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.ColorAnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ColorAnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.ColorAnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.PointAnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.PointAnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.PointAnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.RectAnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RectAnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.RectAnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.SizeAnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SizeAnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.SizeAnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.ThicknessAnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ThicknessAnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.ThicknessAnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.ObjectAnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ObjectAnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.ObjectAnimationUsingKeyFrames(cPtr, ownMemory)));
            AddNativeType(Noesis.StringAnimationUsingKeyFrames.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.StringAnimationUsingKeyFrames), (cPtr, ownMemory) => new Noesis.StringAnimationUsingKeyFrames(cPtr, ownMemory)));

            AddNativeType(Noesis.BooleanKeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BooleanKeyFrameCollection), (cPtr, ownMemory) => new Noesis.BooleanKeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.Int16KeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Int16KeyFrameCollection), (cPtr, ownMemory) => new Noesis.Int16KeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.Int32KeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Int32KeyFrameCollection), (cPtr, ownMemory) => new Noesis.Int32KeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.DoubleKeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DoubleKeyFrameCollection), (cPtr, ownMemory) => new Noesis.DoubleKeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.ColorKeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ColorKeyFrameCollection), (cPtr, ownMemory) => new Noesis.ColorKeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.PointKeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.PointKeyFrameCollection), (cPtr, ownMemory) => new Noesis.PointKeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.RectKeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RectKeyFrameCollection), (cPtr, ownMemory) => new Noesis.RectKeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.SizeKeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SizeKeyFrameCollection), (cPtr, ownMemory) => new Noesis.SizeKeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.ThicknessKeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ThicknessKeyFrameCollection), (cPtr, ownMemory) => new Noesis.ThicknessKeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.ObjectKeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ObjectKeyFrameCollection), (cPtr, ownMemory) => new Noesis.ObjectKeyFrameCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.StringKeyFrameCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.StringKeyFrameCollection), (cPtr, ownMemory) => new Noesis.StringKeyFrameCollection(cPtr, ownMemory)));

            AddNativeType(Noesis.BooleanKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BooleanKeyFrame), (cPtr, ownMemory) => new Noesis.BooleanKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DoubleKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DoubleKeyFrame), (cPtr, ownMemory) => new Noesis.DoubleKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.Int16KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Int16KeyFrame), (cPtr, ownMemory) => new Noesis.Int16KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.Int32KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.Int32KeyFrame), (cPtr, ownMemory) => new Noesis.Int32KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.ColorKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ColorKeyFrame), (cPtr, ownMemory) => new Noesis.ColorKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.PointKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.PointKeyFrame), (cPtr, ownMemory) => new Noesis.PointKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.RectKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.RectKeyFrame), (cPtr, ownMemory) => new Noesis.RectKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.SizeKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SizeKeyFrame), (cPtr, ownMemory) => new Noesis.SizeKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.ThicknessKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ThicknessKeyFrame), (cPtr, ownMemory) => new Noesis.ThicknessKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.StringKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.StringKeyFrame), (cPtr, ownMemory) => new Noesis.StringKeyFrame(cPtr, ownMemory)));

            AddNativeType(Noesis.DiscreteBooleanKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteBooleanKeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteBooleanKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscreteInt16KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteInt16KeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteInt16KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscreteInt32KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteInt32KeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteInt32KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscreteDoubleKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteDoubleKeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteDoubleKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscreteColorKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteColorKeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteColorKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscretePointKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscretePointKeyFrame), (cPtr, ownMemory) => new Noesis.DiscretePointKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscreteRectKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteRectKeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteRectKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscreteSizeKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteSizeKeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteSizeKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscreteThicknessKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteThicknessKeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteThicknessKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscreteObjectKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteObjectKeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteObjectKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.DiscreteStringKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.DiscreteStringKeyFrame), (cPtr, ownMemory) => new Noesis.DiscreteStringKeyFrame(cPtr, ownMemory)));

            AddNativeType(Noesis.LinearInt16KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LinearInt16KeyFrame), (cPtr, ownMemory) => new Noesis.LinearInt16KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.LinearInt32KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LinearInt32KeyFrame), (cPtr, ownMemory) => new Noesis.LinearInt32KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.LinearDoubleKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LinearDoubleKeyFrame), (cPtr, ownMemory) => new Noesis.LinearDoubleKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.LinearColorKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LinearColorKeyFrame), (cPtr, ownMemory) => new Noesis.LinearColorKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.LinearPointKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LinearPointKeyFrame), (cPtr, ownMemory) => new Noesis.LinearPointKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.LinearRectKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LinearRectKeyFrame), (cPtr, ownMemory) => new Noesis.LinearRectKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.LinearSizeKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LinearSizeKeyFrame), (cPtr, ownMemory) => new Noesis.LinearSizeKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.LinearThicknessKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.LinearThicknessKeyFrame), (cPtr, ownMemory) => new Noesis.LinearThicknessKeyFrame(cPtr, ownMemory)));

            AddNativeType(Noesis.SplineInt16KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SplineInt16KeyFrame), (cPtr, ownMemory) => new Noesis.SplineInt16KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.SplineInt32KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SplineInt32KeyFrame), (cPtr, ownMemory) => new Noesis.SplineInt32KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.SplineDoubleKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SplineDoubleKeyFrame), (cPtr, ownMemory) => new Noesis.SplineDoubleKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.SplineColorKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SplineColorKeyFrame), (cPtr, ownMemory) => new Noesis.SplineColorKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.SplinePointKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SplinePointKeyFrame), (cPtr, ownMemory) => new Noesis.SplinePointKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.SplineRectKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SplineRectKeyFrame), (cPtr, ownMemory) => new Noesis.SplineRectKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.SplineSizeKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SplineSizeKeyFrame), (cPtr, ownMemory) => new Noesis.SplineSizeKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.SplineThicknessKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SplineThicknessKeyFrame), (cPtr, ownMemory) => new Noesis.SplineThicknessKeyFrame(cPtr, ownMemory)));

            AddNativeType(Noesis.KeySpline.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.KeySpline), (cPtr, ownMemory) => new Noesis.KeySpline(cPtr, ownMemory)));

            AddNativeType(Noesis.EasingInt16KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EasingInt16KeyFrame), (cPtr, ownMemory) => new Noesis.EasingInt16KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.EasingInt32KeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EasingInt32KeyFrame), (cPtr, ownMemory) => new Noesis.EasingInt32KeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.EasingDoubleKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EasingDoubleKeyFrame), (cPtr, ownMemory) => new Noesis.EasingDoubleKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.EasingColorKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EasingColorKeyFrame), (cPtr, ownMemory) => new Noesis.EasingColorKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.EasingPointKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EasingPointKeyFrame), (cPtr, ownMemory) => new Noesis.EasingPointKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.EasingRectKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EasingRectKeyFrame), (cPtr, ownMemory) => new Noesis.EasingRectKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.EasingSizeKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EasingSizeKeyFrame), (cPtr, ownMemory) => new Noesis.EasingSizeKeyFrame(cPtr, ownMemory)));
            AddNativeType(Noesis.EasingThicknessKeyFrame.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EasingThicknessKeyFrame), (cPtr, ownMemory) => new Noesis.EasingThicknessKeyFrame(cPtr, ownMemory)));

            AddNativeType(Noesis.EasingFunctionBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.EasingFunctionBase), (cPtr, ownMemory) => new Noesis.EasingFunctionBase(cPtr, ownMemory)));
            AddNativeType(Noesis.BackEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BackEase), (cPtr, ownMemory) => new Noesis.BackEase(cPtr, ownMemory)));
            AddNativeType(Noesis.BounceEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BounceEase), (cPtr, ownMemory) => new Noesis.BounceEase(cPtr, ownMemory)));
            AddNativeType(Noesis.CircleEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.CircleEase), (cPtr, ownMemory) => new Noesis.CircleEase(cPtr, ownMemory)));
            AddNativeType(Noesis.CubicEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.CubicEase), (cPtr, ownMemory) => new Noesis.CubicEase(cPtr, ownMemory)));
            AddNativeType(Noesis.ElasticEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ElasticEase), (cPtr, ownMemory) => new Noesis.ElasticEase(cPtr, ownMemory)));
            AddNativeType(Noesis.ExponentialEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ExponentialEase), (cPtr, ownMemory) => new Noesis.ExponentialEase(cPtr, ownMemory)));
            AddNativeType(Noesis.PowerEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.PowerEase), (cPtr, ownMemory) => new Noesis.PowerEase(cPtr, ownMemory)));
            AddNativeType(Noesis.QuadraticEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.QuadraticEase), (cPtr, ownMemory) => new Noesis.QuadraticEase(cPtr, ownMemory)));
            AddNativeType(Noesis.QuarticEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.QuarticEase), (cPtr, ownMemory) => new Noesis.QuarticEase(cPtr, ownMemory)));
            AddNativeType(Noesis.QuinticEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.QuinticEase), (cPtr, ownMemory) => new Noesis.QuinticEase(cPtr, ownMemory)));
            AddNativeType(Noesis.SineEase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.SineEase), (cPtr, ownMemory) => new Noesis.SineEase(cPtr, ownMemory)));

            AddNativeType(Noesis.ControllableStoryboardAction.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ControllableStoryboardAction), (cPtr, ownMemory) => new Noesis.ControllableStoryboardAction(cPtr, ownMemory)));
            AddNativeType(Noesis.BeginStoryboard.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.BeginStoryboard), (cPtr, ownMemory) => new Noesis.BeginStoryboard(cPtr, ownMemory)));
            AddNativeType(Noesis.PauseStoryboard.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.PauseStoryboard), (cPtr, ownMemory) => new Noesis.PauseStoryboard(cPtr, ownMemory)));
            AddNativeType(Noesis.ResumeStoryboard.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ResumeStoryboard), (cPtr, ownMemory) => new Noesis.ResumeStoryboard(cPtr, ownMemory)));
            AddNativeType(Noesis.StopStoryboard.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.StopStoryboard), (cPtr, ownMemory) => new Noesis.StopStoryboard(cPtr, ownMemory)));

            AddNativeType(Noesis.VisualStateManager.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.VisualStateManager), (cPtr, ownMemory) => new Noesis.VisualStateManager(cPtr, ownMemory)));
            AddNativeType(Noesis.VisualStateGroup.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.VisualStateGroup), (cPtr, ownMemory) => new Noesis.VisualStateGroup(cPtr, ownMemory)));
            AddNativeType(Noesis.VisualStateGroupCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.VisualStateGroupCollection), (cPtr, ownMemory) => new Noesis.VisualStateGroupCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.VisualTransition.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.VisualTransition), (cPtr, ownMemory) => new Noesis.VisualTransition(cPtr, ownMemory)));
            AddNativeType(Noesis.VisualTransitionCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.VisualTransitionCollection), (cPtr, ownMemory) => new Noesis.VisualTransitionCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.VisualState.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.VisualState), (cPtr, ownMemory) => new Noesis.VisualState(cPtr, ownMemory)));
            AddNativeType(Noesis.VisualStateCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.VisualStateCollection), (cPtr, ownMemory) => new Noesis.VisualStateCollection(cPtr, ownMemory)));


            AddNativeType(Noesis.ViewBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ViewBase), (cPtr, ownMemory) => new Noesis.ViewBase(cPtr, ownMemory)));
            AddNativeType(Noesis.GridView.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GridView), (cPtr, ownMemory) => new Noesis.GridView(cPtr, ownMemory)));
            AddNativeType(Noesis.GridViewColumn.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GridViewColumn), (cPtr, ownMemory) => new Noesis.GridViewColumn(cPtr, ownMemory)));
            AddNativeType(Noesis.GridViewColumnCollection.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GridViewColumnCollection), (cPtr, ownMemory) => new Noesis.GridViewColumnCollection(cPtr, ownMemory)));
            AddNativeType(Noesis.GridViewColumnHeader.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GridViewColumnHeader), (cPtr, ownMemory) => new Noesis.GridViewColumnHeader(cPtr, ownMemory)));
            AddNativeType(Noesis.GridViewRowPresenterBase.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GridViewRowPresenterBase), (cPtr, ownMemory) => new Noesis.GridViewRowPresenterBase(cPtr, ownMemory)));
            AddNativeType(Noesis.GridViewRowPresenter.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GridViewRowPresenter), (cPtr, ownMemory) => new Noesis.GridViewRowPresenter(cPtr, ownMemory)));
            AddNativeType(Noesis.GridViewHeaderRowPresenter.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.GridViewHeaderRowPresenter), (cPtr, ownMemory) => new Noesis.GridViewHeaderRowPresenter(cPtr, ownMemory)));
            AddNativeType(Noesis.ListView.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ListView), (cPtr, ownMemory) => new Noesis.ListView(cPtr, ownMemory)));
            AddNativeType(Noesis.ListViewItem.GetStaticType(), new NativeTypeInfo(NativeTypeKind.Component, typeof(Noesis.ListViewItem), (cPtr, ownMemory) => new Noesis.ListViewItem(cPtr, ownMemory)));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void TryRegisterType(Type type)
        {
            if (type.GetTypeInfo().IsSubclassOf(typeof(Noesis.BaseComponent)))
            {
                System.Reflection.MethodInfo extend = FindExtendMethod(type);
                if (extend != null)
                {
                    IntPtr nativeType = (IntPtr)extend.Invoke(null, new object[] { type });
                    AddNativeType(nativeType, new NativeTypeInfo(NativeTypeKind.Extended, type, null));

                    if (type.GetTypeInfo().IsSubclassOf(typeof(Noesis.DependencyObject)))
                    {
                        RegisterDependencyProperties(type);

                        if (type.GetTypeInfo().IsSubclassOf(typeof(Noesis.UserControl)))
                        {
                            OverrideUserControlSource(type);
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Can't register user class " + type.Name);
                }
            }
            else
            {
                IntPtr nativeType;

                if (typeof(System.Windows.Input.ICommand).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                {
                    nativeType = Noesis.ExtendCommand.Extend(type);
                }
                else if (typeof(Noesis.IValueConverter).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                {
                    nativeType = Noesis.ExtendConverter.Extend(type);
                }
                else if (typeof(System.Collections.IList).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                {
                    if (typeof(System.Collections.Specialized.INotifyCollectionChanged).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                    {
                        nativeType = Noesis.ExtendObservableList.Extend(type);
                    }
                    else
                    {
                        nativeType = Noesis.ExtendList.Extend(type);
                    }
                }
                else if (typeof(System.Collections.IDictionary).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                {
                    nativeType = Noesis.ExtendDictionary.Extend(type);
                }
                else
                {
                    nativeType = Noesis.SerializableComponent.Extend(type);
                }

                AddNativeType(nativeType, new NativeTypeInfo(NativeTypeKind.Extended, type, null));
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        static List<System.Type> _dependencyTypes = new List<System.Type>();

        private static void RegisterDependencyProperties(System.Type type)
        {
            // Note that we are requesting here all the runtime fields (ancestors included)
#if NETFX_CORE
            var fields = type.GetRuntimeFields();
#else
            var fields = type.GetFields();
#endif
            foreach (FieldInfo field in fields)
            {
                if (field.IsStatic && field.FieldType == typeof(DependencyProperty))
                {
                    if (field.IsInitOnly)
                    {
                        throw new Exception("DependencyProperty fields cannot be readonly");
                    }

                    // Ensure that static constructor is executed, so dependency properties
                    // are registered in Noesis reflection
                    DependencyProperty.RegisterCalled = false;
                    RuntimeHelpers.RunClassConstructor(type.TypeHandle);

#if UNITY_EDITOR
                    // Inside the editor it could happen that Noesis is initialized several times
                    // (for example when building resources for several platforms)
                    // We have to force the execution of the static constructor using this awful
                    // trick. This is the reason we cannot use readonly Dependency Properties.
                    // When used internal crashes happen inside Mono.
                    if (!DependencyProperty.RegisterCalled)
                    {
                        // force static ctor execution
                        type.TypeInitializer.Invoke(null, null);
                    }
#endif

                    _dependencyTypes.Add(type);
                    break;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void OverrideUserControlSource(System.Type type)
        {
#if NETFX_CORE
            var attrs = type.GetTypeInfo().GetCustomAttributes();
#else
            var attrs = System.Attribute.GetCustomAttributes(type);
#endif
            foreach (var attr in attrs)
            {
                if (attr is Noesis.UserControlSource)
                {
                    Noesis.UserControlSource source = attr as Noesis.UserControlSource;
                    if (source.source == string.Empty)
                    {
                        Debug.LogWarning("UserControl.Source is empty for class " + type.Name);
                    }
                    else
                    {
                        UserControl.SourceProperty.OverrideMetadata(type, new PropertyMetadata(source.source));
                    }

                    break;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void ResetDependencyProperties()
        {
            foreach (var type in _dependencyTypes)
            {
                ResetDependencyProperties(type);
            }

            _dependencyTypes.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void ResetDependencyProperties(System.Type type)
        {
#if NETFX_CORE
            var fields = type.GetRuntimeFields();
#else
            var fields = type.GetFields();
#endif

            foreach (FieldInfo field in fields)
            {
                if (field.IsStatic && field.FieldType == typeof(DependencyProperty))
                {
                    // set to null to free memory
                    field.SetValue(null, null);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static System.Reflection.MethodInfo FindExtendMethod(System.Type type)
        {
            System.Type baseType = type.GetTypeInfo().BaseType;
            while (baseType != null)
            {
                System.Reflection.MethodInfo extend = GetExtendMethod(baseType);
                if (extend != null)
                {
                    return extend;
                }

                baseType = baseType.GetTypeInfo().BaseType;
            }

            return null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static System.Reflection.MethodInfo GetExtendMethod(System.Type type)
        {
#if NETFX_CORE
            System.Reflection.MethodInfo extend = type.GetTypeInfo().GetDeclaredMethods("Extend").Where(m => !m.IsPublic && m.IsStatic).FirstOrDefault();
#else
            System.Reflection.MethodInfo extend = type.GetMethod("Extend", BindingFlags.NonPublic | BindingFlags.Static);
#endif
            if (extend != null && extend.IsStatic && extend.GetParameters().Length == 1)
            {
                return extend;
            }

            return null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        static Dictionary<IntPtr, System.Type> _types = new Dictionary<IntPtr, System.Type>();

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static IntPtr GetPtrForType(System.Type type)
        {
#if NETFX_CORE
            // There is no Handle.Value in WinRT. We use the hashcode and pray for no collision
            IntPtr unityType = (IntPtr)type.TypeHandle.GetHashCode();
#else
            IntPtr unityType = type.TypeHandle.Value;
#endif
            if (!_types.ContainsKey(unityType))
            {
                _types.Add(unityType, type);
            }

            return unityType;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static System.Type GetTypeFromPtr(IntPtr unityType)
        {
            return _types[unityType];
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static IntPtr GetResourceKeyType(System.Type type)
        {
            IntPtr nativeType;
            if (!_managedTypes.TryGetValue(type, out nativeType))
            {
                TryRegisterType(type);
            }

            nativeType = GetNativeType(type);
            return Noesis_GetResourceKeyType_(nativeType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_RegisterType(string typeName);
        private static Callback_RegisterType _registerType = RegisterType;
        private static System.Reflection.Assembly[] _assemblies = AppDomain.CurrentDomain.GetAssemblies();

        [MonoPInvokeCallback(typeof(Callback_RegisterType))]
        private static void RegisterType(string typeName)
        {
            foreach (var assembly in _assemblies)
            {
                Type type = assembly.GetType(typeName);
                if (type != null)
                {
                    TryRegisterType(type);
                    break;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_DependencyPropertyChanged(IntPtr unityType, IntPtr cPtr,
            IntPtr dependencyPropertyPtr);
        private static Callback_DependencyPropertyChanged _dependencyPropertyChanged = DependencyPropertyChanged;

        [MonoPInvokeCallback(typeof(Callback_DependencyPropertyChanged))]
        private static void DependencyPropertyChanged(IntPtr unityType, IntPtr cPtr,
            IntPtr dependencyPropertyPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
#if NETFX_CORE
            MethodInfo methodInfo = type.GetRuntimeMethod("DependencyPropertyChanged", new Type[] { typeof(DependencyProperty) });
#else
            MethodInfo methodInfo = type.GetMethod("DependencyPropertyChanged", new Type[] { typeof(DependencyProperty) });
#endif
            if (methodInfo != null)
            {
                DependencyProperty dependencyProperty =
                    new DependencyProperty(dependencyPropertyPtr, false);
        
                object[] parametersArray = new object[] { dependencyProperty };
                methodInfo.Invoke(instance, parametersArray);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_OnPostInit(IntPtr unityType, IntPtr cPtr);
        private static Callback_OnPostInit _onPostInit = OnPostInit;

        [MonoPInvokeCallback(typeof(Callback_OnPostInit))]
        private static void OnPostInit(IntPtr unityType, IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
#if NETFX_CORE
            MethodInfo methodInfo = type.GetRuntimeMethod("OnPostInit", new Type[] { });
#else
            MethodInfo methodInfo = type.GetMethod("OnPostInit", new Type[] { });
#endif
            if (methodInfo != null)
            {
                object[] parametersArray = new object[] { };
                methodInfo.Invoke(instance, parametersArray);
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate bool Callback_CommandCanExecute(IntPtr cPtr, IntPtr paramPtr);
        private static Callback_CommandCanExecute _commandCanExecute = CommandCanExecute;

        [MonoPInvokeCallback(typeof(Callback_CommandCanExecute))]
        private static bool CommandCanExecute(IntPtr cPtr, IntPtr paramPtr)
        {
            System.Windows.Input.ICommand command = (System.Windows.Input.ICommand)GetProxy(cPtr, false);
            return command.CanExecute(Unbox(GetProxy(paramPtr, false)));
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_CommandExecute(IntPtr cPtr, IntPtr paramPtr);
        private static Callback_CommandExecute _commandExecute = CommandExecute;

        [MonoPInvokeCallback(typeof(Callback_CommandExecute))]
        private static void CommandExecute(IntPtr cPtr, IntPtr paramPtr)
        {
            System.Windows.Input.ICommand command = (System.Windows.Input.ICommand)GetProxy(cPtr, false);
            command.Execute(Unbox(GetProxy(paramPtr, false)));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static bool AreCompatibleTypes(object source, Type targetType)
        {
            if (source == null)
            {
                return true;
            }

            Type sourceType = source.GetType();
            if (targetType.GetTypeInfo().IsAssignableFrom(sourceType.GetTypeInfo()))
            {
                return true;
            }

            if ((object)targetType.TypeHandle == typeof(Noesis.BaseComponent).TypeHandle)
            {
                return true;
            }

            return false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate IntPtr Callback_ConverterConvert(IntPtr unityType, IntPtr cPtr, IntPtr valPtr,
            IntPtr typePtr, IntPtr paramPtr);
        private static Callback_ConverterConvert _converterConvert = ConverterConvert;

        [MonoPInvokeCallback(typeof(Callback_ConverterConvert))]
        private static IntPtr ConverterConvert(IntPtr unityType, IntPtr cPtr, IntPtr valPtr,
            IntPtr typePtr, IntPtr paramPtr)
        {
            Noesis.IValueConverter converter = (Noesis.IValueConverter)GetProxy(cPtr, false);
            NativeTypeInfo info = GetNativeTypeInfo(typePtr);
            object val = Unbox(GetProxy(valPtr, false));
            object param = Unbox(GetProxy(paramPtr, false));

            object obj = converter.Convert(val, info.Type, param, CultureInfo.CurrentCulture);

            if (AreCompatibleTypes(obj, info.Type))
            {
                return GetInstanceHandle(obj).Handle;
            }
            else
            {
                Debug.LogWarning(String.Format("Converter.Convert() expects {0} and {1} is returned",
                    info.Type.Name, obj.GetType().Name));

                return IntPtr.Zero;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate IntPtr Callback_ConverterConvertBack(IntPtr unityType, IntPtr cPtr, IntPtr valPtr,
            IntPtr typePtr, IntPtr paramPtr);
        private static Callback_ConverterConvertBack _converterConvertBack = ConverterConvertBack;

        [MonoPInvokeCallback(typeof(Callback_ConverterConvertBack))]
        private static IntPtr ConverterConvertBack(IntPtr unityType, IntPtr cPtr, IntPtr valPtr,
            IntPtr typePtr, IntPtr paramPtr)
        {
            Noesis.IValueConverter converter = (Noesis.IValueConverter)GetProxy(cPtr, false);
            NativeTypeInfo info = GetNativeTypeInfo(typePtr);
            object val = Unbox(GetProxy(valPtr, false));
            object param = Unbox(GetProxy(paramPtr, false));

            object obj = converter.ConvertBack(val, info.Type, param, CultureInfo.CurrentCulture);

            if (AreCompatibleTypes(obj, info.Type))
            {
                return GetInstanceHandle(obj).Handle;
            }
            else
            {
                Debug.LogWarning(String.Format("Converter.ConvertBack() expects {0} and {1} is returned",
                    info.Type.Name, obj.GetType().Name));

                return IntPtr.Zero;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate uint Callback_ListCount(IntPtr cPtr);
        private static Callback_ListCount _listCount = ListCount;

        [MonoPInvokeCallback(typeof(Callback_ListCount))]
        private static uint ListCount(IntPtr cPtr)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            return (uint)list.Count;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate IntPtr Callback_ListGet(IntPtr cPtr, uint index);
        private static Callback_ListGet _listGet = ListGet;

        [MonoPInvokeCallback(typeof(Callback_ListGet))]
        private static IntPtr ListGet(IntPtr cPtr, uint index)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            return GetInstanceHandle(list[(int)index]).Handle;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_ListSet(IntPtr cPtr, uint index, IntPtr item);
        private static Callback_ListSet _listSet = ListSet;

        [MonoPInvokeCallback(typeof(Callback_ListSet))]
        private static void ListSet(IntPtr cPtr, uint index, IntPtr item)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            list[(int)index] = GetProxy(item, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate uint Callback_ListAdd(IntPtr cPtr, IntPtr item);
        private static Callback_ListAdd _listAdd = ListAdd;

        [MonoPInvokeCallback(typeof(Callback_ListAdd))]
        private static uint ListAdd(IntPtr cPtr, IntPtr item)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            return (uint)list.Add(GetProxy(item, false));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_ListClear(IntPtr cPtr);
        private static Callback_ListClear _listClear = ListClear;

        [MonoPInvokeCallback(typeof(Callback_ListClear))]
        private static void ListClear(IntPtr cPtr)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            list.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate bool Callback_ListContains(IntPtr cPtr, IntPtr item);
        private static Callback_ListContains _listContains = ListContains;

        [MonoPInvokeCallback(typeof(Callback_ListClear))]
        private static bool ListContains(IntPtr cPtr, IntPtr item)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            return list.Contains(GetProxy(item, false));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate int Callback_ListIndexOf(IntPtr cPtr, IntPtr item);
        private static Callback_ListIndexOf _listIndexOf = ListIndexOf;

        [MonoPInvokeCallback(typeof(Callback_ListIndexOf))]
        private static int ListIndexOf(IntPtr cPtr, IntPtr item)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            return list.IndexOf(GetProxy(item, false));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_ListInsert(IntPtr cPtr, uint index, IntPtr item);
        private static Callback_ListInsert _listInsert = ListInsert;

        [MonoPInvokeCallback(typeof(Callback_ListInsert))]
        private static void ListInsert(IntPtr cPtr, uint index, IntPtr item)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            list.Insert((int)index, GetProxy(item, false));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_ListRemove(IntPtr cPtr, IntPtr item);
        private static Callback_ListRemove _listRemove = ListRemove;

        [MonoPInvokeCallback(typeof(Callback_ListRemove))]
        private static void ListRemove(IntPtr cPtr, IntPtr item)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            list.Remove(GetProxy(item, false));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_ListRemoveAt(IntPtr cPtr, uint index);
        private static Callback_ListRemoveAt _listRemoveAt = ListRemoveAt;

        [MonoPInvokeCallback(typeof(Callback_ListRemoveAt))]
        private static void ListRemoveAt(IntPtr cPtr, uint index)
        {
            System.Collections.IList list = (System.Collections.IList)GetProxy(cPtr, false);
            list.RemoveAt((int)index);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate uint Callback_DictionaryCount(IntPtr cPtr);
        private static Callback_DictionaryCount _dictionaryCount = DictionaryCount;

        [MonoPInvokeCallback(typeof(Callback_DictionaryCount))]
        private static uint DictionaryCount(IntPtr cPtr)
        {
            System.Collections.IDictionary dictionary = (System.Collections.IDictionary)GetProxy(cPtr, false);
            return (uint)dictionary.Keys.Count;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate bool Callback_DictionaryContains(IntPtr cPtr, string key);
        private static Callback_DictionaryContains _dictionaryContains = DictionaryContains;

        [MonoPInvokeCallback(typeof(Callback_DictionaryContains))]
        private static bool DictionaryContains(IntPtr cPtr, string key)
        {
            System.Collections.IDictionary dictionary = (System.Collections.IDictionary)GetProxy(cPtr, false);
            return dictionary.Contains(key);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate bool Callback_DictionaryFind(IntPtr cPtr, string key, ref IntPtr item);
        private static Callback_DictionaryFind _dictionaryFind = DictionaryFind;

        [MonoPInvokeCallback(typeof(Callback_DictionaryFind))]
        private static bool DictionaryFind(IntPtr cPtr, string key, ref IntPtr item)
        {
            System.Collections.IDictionary dictionary = (System.Collections.IDictionary)GetProxy(cPtr, false);
            if (dictionary.Contains(key))
            {
                item = GetInstanceHandle(dictionary[key]).Handle;
                return true;
            }
            else
            {
                item = IntPtr.Zero;
                return false;
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_DictionaryAdd(IntPtr cPtr, string key, IntPtr item);
        private static Callback_DictionaryAdd _dictionaryAdd = DictionaryAdd;

        [MonoPInvokeCallback(typeof(Callback_DictionaryAdd))]
        private static void DictionaryAdd(IntPtr cPtr, string key, IntPtr item)
        {
            System.Collections.IDictionary dictionary = (System.Collections.IDictionary)GetProxy(cPtr, false);
            dictionary.Add(key, GetProxy(item, false));
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_DictionaryRemove(IntPtr cPtr, string key);
        private static Callback_DictionaryRemove _dictionaryRemove = DictionaryRemove;

        [MonoPInvokeCallback(typeof(Callback_DictionaryRemove))]
        private static void DictionaryRemove(IntPtr cPtr, string key)
        {
            System.Collections.IDictionary dictionary = (System.Collections.IDictionary)GetProxy(cPtr, false);
            dictionary.Remove(key);
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_DictionaryClear(IntPtr cPtr);
        private static Callback_DictionaryClear _dictionaryClear = DictionaryClear;

        [MonoPInvokeCallback(typeof(Callback_DictionaryClear))]
        private static void DictionaryClear(IntPtr cPtr)
        {
            System.Collections.IDictionary dictionary = (System.Collections.IDictionary)GetProxy(cPtr, false);
            dictionary.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Takes care of unmanaged memory allocated by strings returned to C++
        public class StringMarshal : IDisposable
        {
            private HandleRef handleRef;

            public StringMarshal(string str)
            {
                handleRef = new HandleRef(this, Marshal.StringToHGlobalAnsi(
                    str != null ? str : string.Empty));
            }

            public virtual void Dispose()
            {
                Marshal.FreeHGlobal(handleRef.Handle);
                GC.SuppressFinalize(this);
            }

            public static implicit operator IntPtr(StringMarshal sm)
            {
                return sm.handleRef.Handle;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate IntPtr Callback_GetName(IntPtr unityType);
        private static Callback_GetName _getName = GetName;

        [MonoPInvokeCallback(typeof(Callback_GetName))]
        private static IntPtr GetName(IntPtr unityType)
        {
            Type type = GetTypeFromPtr(unityType);
            return new StringMarshal(type.FullName);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate IntPtr Callback_GetBaseType(IntPtr unityType);
        private static Callback_GetBaseType _getBaseType = GetBaseType;

        [MonoPInvokeCallback(typeof(Callback_GetBaseType))]
        private static IntPtr GetBaseType(IntPtr unityType)
        {
            Type type = GetTypeFromPtr(unityType);
            Type baseType = type.GetTypeInfo().BaseType;
            if (baseType != typeof(object) && GetExtendMethod(baseType) == null)
            {
                return Extend.GetPtrForType(baseType);
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static PropertyInfo[] GetPublicProperties(Type type)
        {
#if NETFX_CORE
            return type.GetTypeInfo().DeclaredProperties.Where(p => p.GetMethod.IsPublic && !p.GetMethod.IsStatic).ToArray();
#else
            return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
#endif
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate int Callback_GetPropertiesCount(IntPtr unityType);
        private static Callback_GetPropertiesCount _getPropertiesCount = GetPropertiesCount;

        [MonoPInvokeCallback(typeof(Callback_GetPropertiesCount))]
        private static int GetPropertiesCount(IntPtr unityType)
        {
            Type type = GetTypeFromPtr(unityType);
            PropertyInfo[] properties = GetPublicProperties(type);
            return properties.Length;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate int Callback_GetPropertyIndex(IntPtr unityType, string propName);
        private static Callback_GetPropertyIndex _getPropertyIndex = GetPropertyIndex;


        [MonoPInvokeCallback(typeof(Callback_GetPropertyIndex))]
        private static int GetPropertyIndex(IntPtr unityType, string propName)
        {
            Type type = GetTypeFromPtr(unityType);
            PropertyInfo[] properties = GetPublicProperties(type);

            int index = 0;
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.Name == propName)
                {
                    return index;
                }

                ++index;
            }

            return -1;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private enum NativePropertyType
        {
            Type_Invalid = 0,
            Type_Bool,
            Type_Float,
            Type_Int,
            Type_UInt,
            Type_Short,
            Type_UShort,
            Type_String,
            Type_Color,
            Type_Point,
            Type_Rect,
            Type_Size,
            Type_Thickness,
            Type_BaseComponent,
            Type_CornerRadius
        };

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static int GetNativePropertyType(Type type)
        {
            if ((object)type.TypeHandle == typeof(bool).TypeHandle)
            {
                return (int)NativePropertyType.Type_Bool;
            }

            if ((object)type.TypeHandle == typeof(float).TypeHandle ||
                (object)type.TypeHandle == typeof(double).TypeHandle ||
                (object)type.TypeHandle == typeof(decimal).TypeHandle)
            {
                return (int)NativePropertyType.Type_Float;
            }

            if ((object)type.TypeHandle == typeof(int).TypeHandle ||
                (object)type.TypeHandle == typeof(long).TypeHandle)
            {
                return (int)NativePropertyType.Type_Int;
            }

            if ((object)type.TypeHandle == typeof(uint).TypeHandle ||
                (object)type.TypeHandle == typeof(long).TypeHandle ||
                (object)type.TypeHandle == typeof(char).TypeHandle)
            {
                return (int)NativePropertyType.Type_UInt;
            }

            if ((object)type.TypeHandle == typeof(short).TypeHandle ||
                (object)type.TypeHandle == typeof(sbyte).TypeHandle)
            {
                return (int)NativePropertyType.Type_Short;
            }

            if ((object)type.TypeHandle == typeof(ushort).TypeHandle ||
                (object)type.TypeHandle == typeof(byte).TypeHandle)
            {
                return (int)NativePropertyType.Type_UShort;
            }

            if ((object)type.TypeHandle == typeof(string).TypeHandle ||
                (object)type.TypeHandle == typeof(DateTime).TypeHandle ||
                type.GetTypeInfo().IsEnum)
            {
                return (int)NativePropertyType.Type_String;
            }

            if ((object)type.TypeHandle == typeof(Color).TypeHandle)
            {
                return (int)NativePropertyType.Type_Color;
            }

            if ((object)type.TypeHandle == typeof(Point).TypeHandle)
            {
                return (int)NativePropertyType.Type_Point;
            }

            if ((object)type.TypeHandle == typeof(Rect).TypeHandle)
            {
                return (int)NativePropertyType.Type_Rect;
            }

            if ((object)type.TypeHandle == typeof(Size).TypeHandle)
            {
                return (int)NativePropertyType.Type_Size;
            }

            if ((object)type.TypeHandle == typeof(Thickness).TypeHandle)
            {
                return (int)NativePropertyType.Type_Thickness;
            }

            if ((object)type.TypeHandle == typeof(Noesis.CornerRadius).TypeHandle)
            {
                return (int)NativePropertyType.Type_CornerRadius;
            }

            if (typeof(Noesis.BaseComponent).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) ||
                typeof(Noesis.IValueConverter).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) ||
                typeof(System.Windows.Input.ICommand).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) ||
                typeof(System.ComponentModel.INotifyPropertyChanged).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) ||
                typeof(System.Collections.IList).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) ||
                typeof(System.Collections.IDictionary).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) ||
                !type.GetTypeInfo().IsValueType)
            {
                return (int)NativePropertyType.Type_BaseComponent;
            }

            return (int)NativePropertyType.Type_Invalid;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate int Callback_GetPropertyType(IntPtr unityType, int propertyIndex);
        private static Callback_GetPropertyType _getPropertyType = GetPropertyType;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyType))]
        private static int GetPropertyType(IntPtr unityType, int propertyIndex)
        {
            Type type = GetTypeFromPtr(unityType);

            PropertyInfo[] properties = GetPublicProperties(type);

            return GetNativePropertyType(properties[propertyIndex].PropertyType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_GetPropertyInfo(IntPtr unityType, int propertyIndex,
            ref IntPtr name, [MarshalAs(UnmanagedType.U1)] ref bool read,
            [MarshalAs(UnmanagedType.U1)] ref bool write, ref int proptype);
        private static Callback_GetPropertyInfo _getPropertyInfo = GetPropertyInfo;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyInfo))]
        private static void GetPropertyInfo(IntPtr unityType, int propertyIndex, ref IntPtr name,
            [MarshalAs(UnmanagedType.U1)] ref bool read, [MarshalAs(UnmanagedType.U1)] ref bool write,
            ref int proptype)
        {
            Type type = GetTypeFromPtr(unityType);
            
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];
            
            name = new StringMarshal(propertyInfo.Name);
            read = propertyInfo.CanRead;
#if NETFX_CORE
            write = propertyInfo.CanWrite && propertyInfo.SetMethod.IsPublic;
#else
            write = propertyInfo.CanWrite && propertyInfo.GetSetMethod(true).IsPublic;
#endif
            proptype = GetNativePropertyType(propertyInfo.PropertyType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        [return: MarshalAs(UnmanagedType.U1)]
        private delegate bool Callback_GetPropertyValue_Bool(IntPtr unityType, int propertyIndex,
            IntPtr cPtr);
        private static Callback_GetPropertyValue_Bool _getPropertyValue_Bool = GetPropertyValue_Bool;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_Bool))]
        [return: MarshalAs(UnmanagedType.U1)]
        private static bool GetPropertyValue_Bool(IntPtr unityType, int propertyIndex,
            IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            return (bool)propertyInfo.GetValue(instance, null);
        }

        private delegate float Callback_GetPropertyValue_Float(IntPtr unityType, int propertyIndex,
            IntPtr cPtr);
        private static Callback_GetPropertyValue_Float _getPropertyValue_Float = GetPropertyValue_Float;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_Float))]
        private static float GetPropertyValue_Float(IntPtr unityType, int propertyIndex, IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            return Convert.ToSingle(propertyInfo.GetValue(instance, null));
        }

        private delegate int Callback_GetPropertyValue_Int(IntPtr unityType, int propertyIndex,
            IntPtr cPtr);
        private static Callback_GetPropertyValue_Int _getPropertyValue_Int = GetPropertyValue_Int;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_Int))]
        private static int GetPropertyValue_Int(IntPtr unityType, int propertyIndex, IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            return Convert.ToInt32(propertyInfo.GetValue(instance, null));
        }

        private delegate uint Callback_GetPropertyValue_UInt(IntPtr unityType, int propertyIndex,
            IntPtr cPtr);
        private static Callback_GetPropertyValue_UInt _getPropertyValue_UInt = GetPropertyValue_UInt;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_UInt))]
        private static uint GetPropertyValue_UInt(IntPtr unityType, int propertyIndex, IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            return Convert.ToUInt32(propertyInfo.GetValue(instance, null));
        }

        private delegate short Callback_GetPropertyValue_Short(IntPtr unityType, int propertyIndex,
            IntPtr cPtr);
        private static Callback_GetPropertyValue_Short _getPropertyValue_Short = GetPropertyValue_Short;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_Short))]
        private static short GetPropertyValue_Short(IntPtr unityType, int propertyIndex, IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            return Convert.ToInt16(propertyInfo.GetValue(instance, null));
        }

        private delegate ushort Callback_GetPropertyValue_UShort(IntPtr unityType, int propertyIndex,
            IntPtr cPtr);
        private static Callback_GetPropertyValue_UShort _getPropertyValue_UShort = GetPropertyValue_UShort;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_UShort))]
        private static ushort GetPropertyValue_UShort(IntPtr unityType, int propertyIndex, IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            return Convert.ToUInt16(propertyInfo.GetValue(instance, null));
        }

        private delegate IntPtr Callback_GetPropertyValue_String(IntPtr unityType, int propertyIndex,
            IntPtr cPtr);
        private static Callback_GetPropertyValue_String _getPropertyValue_String = GetPropertyValue_String;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_String))]
        private static IntPtr GetPropertyValue_String(IntPtr unityType, int propertyIndex, IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            return new StringMarshal(Convert.ToString(propertyInfo.GetValue(instance, null)));
        }

        private delegate void Callback_GetPropertyValue_Color(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr colorPtr);
        private static Callback_GetPropertyValue_Color _getPropertyValue_Color = GetPropertyValue_Color;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_Color))]
        private static void GetPropertyValue_Color(IntPtr unityType, int propertyIndex, IntPtr cPtr,
            IntPtr colorPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            Noesis.Color color = (Noesis.Color)propertyInfo.GetValue(instance, null);
            Marshal.StructureToPtr(color, colorPtr, false);
        }

        private delegate void Callback_GetPropertyValue_Point(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr pointPtr);
        private static Callback_GetPropertyValue_Point _getPropertyValue_Point = GetPropertyValue_Point;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_Point))]
        private static void GetPropertyValue_Point(IntPtr unityType, int propertyIndex, IntPtr cPtr,
            IntPtr pointPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            Noesis.Point point = (Noesis.Point)propertyInfo.GetValue(instance, null);
            Marshal.StructureToPtr(point, pointPtr, false);
        }

        private delegate void Callback_GetPropertyValue_Rect(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr rectPtr);
        private static Callback_GetPropertyValue_Rect _getPropertyValue_Rect = GetPropertyValue_Rect;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_Rect))]
        private static void GetPropertyValue_Rect(IntPtr unityType, int propertyIndex, IntPtr cPtr,
            IntPtr rectPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            Noesis.Rect rect = (Noesis.Rect)propertyInfo.GetValue(instance, null);
            Marshal.StructureToPtr(rect, rectPtr, false);
        }

        private delegate void Callback_GetPropertyValue_Size(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr sizePtr);
        private static Callback_GetPropertyValue_Size _getPropertyValue_Size = GetPropertyValue_Size;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_Size))]
        private static void GetPropertyValue_Size(IntPtr unityType, int propertyIndex, IntPtr cPtr,
            IntPtr sizePtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            Size size = (Size)propertyInfo.GetValue(instance, null);
            Marshal.StructureToPtr(size, sizePtr, false);
        }

        private delegate void Callback_GetPropertyValue_Thickness(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr thicknessPtr);
        private static Callback_GetPropertyValue_Thickness _getPropertyValue_Thickness = GetPropertyValue_Thickness;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_Thickness))]
        private static void GetPropertyValue_Thickness(IntPtr unityType, int propertyIndex, IntPtr cPtr,
            IntPtr thicknessPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            Noesis.Thickness thickness = (Noesis.Thickness)propertyInfo.GetValue(instance, null);
            Marshal.StructureToPtr(thickness, thicknessPtr, false);
        }

        private delegate void Callback_GetPropertyValue_CornerRadius(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr cornerRadiusPtr);
        private static Callback_GetPropertyValue_CornerRadius _getPropertyValue_CornerRadius = GetPropertyValue_CornerRadius;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_CornerRadius))]
        private static void GetPropertyValue_CornerRadius(IntPtr unityType, int propertyIndex, IntPtr cPtr,
            IntPtr cornerRadiusPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            Noesis.CornerRadius cornerRadius = (Noesis.CornerRadius)propertyInfo.GetValue(instance, null);
            Marshal.StructureToPtr(cornerRadius, cornerRadiusPtr, false);
        }

        private delegate IntPtr Callback_GetPropertyValue_BaseComponent(IntPtr unityType, int propertyIndex,
            IntPtr cPtr);
        private static Callback_GetPropertyValue_BaseComponent _getPropertyValue_BaseComponent = GetPropertyValue_BaseComponent;

        [MonoPInvokeCallback(typeof(Callback_GetPropertyValue_BaseComponent))]
        private static IntPtr GetPropertyValue_BaseComponent(IntPtr unityType, int propertyIndex, IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            object obj = propertyInfo.GetValue(instance, null);
            return GetInstanceHandle(obj).Handle;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_SetPropertyValue_Bool(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, [MarshalAs(UnmanagedType.U1)] bool val);
        private static Callback_SetPropertyValue_Bool _setPropertyValue_Bool = SetPropertyValue_Bool;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_Bool))]
        private static void SetPropertyValue_Bool(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, [MarshalAs(UnmanagedType.U1)] bool val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            propertyInfo.SetValue(instance, val, null);
        }

        private delegate void Callback_SetPropertyValue_Float(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, float val);
        private static Callback_SetPropertyValue_Float _setPropertyValue_Float = SetPropertyValue_Float;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_Float))]
        private static void SetPropertyValue_Float(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, float val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            propertyInfo.SetValue(instance, Convert.ChangeType(val, propertyInfo.PropertyType), null);
        }

        private delegate void Callback_SetPropertyValue_Int(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, int val);
        private static Callback_SetPropertyValue_Int _setPropertyValue_Int = SetPropertyValue_Int;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_Int))]
        private static void SetPropertyValue_Int(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, int val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            propertyInfo.SetValue(instance, Convert.ChangeType(val, propertyInfo.PropertyType), null);
        }

        private delegate void Callback_SetPropertyValue_UInt(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, uint val);
        private static Callback_SetPropertyValue_UInt _setPropertyValue_UInt = SetPropertyValue_UInt;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_UInt))]
        private static void SetPropertyValue_UInt(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, uint val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            propertyInfo.SetValue(instance, Convert.ChangeType(val, propertyInfo.PropertyType), null);
        }

        private delegate void Callback_SetPropertyValue_Short(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, short val);
        private static Callback_SetPropertyValue_Short _setPropertyValue_Short = SetPropertyValue_Short;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_Short))]
        private static void SetPropertyValue_Short(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, short val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            propertyInfo.SetValue(instance, Convert.ChangeType(val, propertyInfo.PropertyType), null);
        }

        private delegate void Callback_SetPropertyValue_UShort(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, ushort val);
        private static Callback_SetPropertyValue_UShort _setPropertyValue_UShort = SetPropertyValue_UShort;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_UShort))]
        private static void SetPropertyValue_UShort(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, ushort val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            propertyInfo.SetValue(instance, Convert.ChangeType(val, propertyInfo.PropertyType), null);
        }

        public static object ParseEnum(Type enumType, string val, string owner, string prop)
        {
            if (!val.Contains(","))
            {
                try
                {
                    object retVal = Enum.Parse(enumType, val);
                    if (Enum.IsDefined(enumType, retVal))
                    {
                        return retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            throw new Exception(String.Format("Invalid value '{0}' for property {1}.{2}", val, owner, prop));
        }

        private delegate void Callback_SetPropertyValue_String(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val);
        private static Callback_SetPropertyValue_String _setPropertyValue_String = SetPropertyValue_String;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_String))]
        private static void SetPropertyValue_String(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            string valStr = Marshal.PtrToStringAnsi(val);
            object valObj = propertyInfo.PropertyType.GetTypeInfo().IsEnum ?
                ParseEnum(propertyInfo.PropertyType, valStr, type.Name, propertyInfo.Name) :
                Convert.ChangeType(valStr, propertyInfo.PropertyType);

            propertyInfo.SetValue(instance, valObj, null);
        }

        private delegate void Callback_SetPropertyValue_Color(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val);
        private static Callback_SetPropertyValue_Color _setPropertyValue_Color = SetPropertyValue_Color;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_Color))]
        private static void SetPropertyValue_Color(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            object valObj = Marshal.PtrToStructure<Noesis.Color>(val);
            propertyInfo.SetValue(instance, valObj, null);
        }

        private delegate void Callback_SetPropertyValue_Point(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val);
        private static Callback_SetPropertyValue_Point _setPropertyValue_Point = SetPropertyValue_Point;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_Point))]
        private static void SetPropertyValue_Point(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            object valObj = Marshal.PtrToStructure<Noesis.Point>(val);
            propertyInfo.SetValue(instance, valObj, null);
        }

        private delegate void Callback_SetPropertyValue_Rect(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val);
        private static Callback_SetPropertyValue_Rect _setPropertyValue_Rect = SetPropertyValue_Rect;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_Rect))]
        private static void SetPropertyValue_Rect(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            object valObj = Marshal.PtrToStructure<Noesis.Rect>(val);
            propertyInfo.SetValue(instance, valObj, null);
        }

        private delegate void Callback_SetPropertyValue_Size(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val);
        private static Callback_SetPropertyValue_Size _setPropertyValue_Size = SetPropertyValue_Size;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_Size))]
        private static void SetPropertyValue_Size(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            object valObj = Marshal.PtrToStructure<Noesis.Size>(val);
            propertyInfo.SetValue(instance, valObj, null);
        }

        private delegate void Callback_SetPropertyValue_Thickness(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val);

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_Thickness))]
        private static Callback_SetPropertyValue_Thickness _setPropertyValue_Thickness = SetPropertyValue_Thickness;
        private static void SetPropertyValue_Thickness(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            object valObj = Marshal.PtrToStructure<Noesis.Thickness>(val);
            propertyInfo.SetValue(instance, valObj, null);
        }

        private delegate void Callback_SetPropertyValue_CornerRadius(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val);
        private static Callback_SetPropertyValue_CornerRadius _setPropertyValue_CornerRadius = SetPropertyValue_CornerRadius;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_CornerRadius))]
        private static void SetPropertyValue_CornerRadius(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);

            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            object valObj = Marshal.PtrToStructure<Noesis.CornerRadius>(val);
            propertyInfo.SetValue(instance, valObj, null);
        }

        private delegate void Callback_SetPropertyValue_BaseComponent(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val);
        private static Callback_SetPropertyValue_BaseComponent _setPropertyValue_BaseComponent = SetPropertyValue_BaseComponent;

        [MonoPInvokeCallback(typeof(Callback_SetPropertyValue_BaseComponent))]
        private static void SetPropertyValue_BaseComponent(IntPtr unityType, int propertyIndex,
            IntPtr cPtr, IntPtr val)
        {
            Type type = GetTypeFromPtr(unityType);
            object instance = GetExtendInstance(cPtr);
    
            PropertyInfo[] properties = GetPublicProperties(type);
            PropertyInfo propertyInfo = properties[propertyIndex];

            object valObj = GetProxy(val, false);
            propertyInfo.SetValue(instance, valObj, null);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        [ThreadStatic]
        private static IntPtr _cPtr = IntPtr.Zero;
        [ThreadStatic]
        private static Type _extendType = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool NeedsCreateCPtr(Type extendType)
        {
            return _cPtr == IntPtr.Zero || _extendType != extendType;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static IntPtr GetCPtr(BaseComponent instance, Type extendType)
        {
            if (_cPtr == IntPtr.Zero)
            {
                Debug.LogWarning("cPtr is null");
            }

            if (_extendType != extendType)
            {
                Debug.LogWarning("Invalid extend type");
            }

            return _cPtr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static IntPtr New(System.Type type, object instance)
        {
            IntPtr unityType = Extend.GetPtrForType(type);

            // This function is called when a Extend object is created from C#
            // so we create the corresponding C++ proxy
            IntPtr cPtr = Noesis_InstantiateExtend_(unityType);

            if (cPtr == IntPtr.Zero)
            {
                throw new System.Exception(String.Format("Unable to create an instance of '{0}'",
                    type.Name));
            }

            AddExtendInfo(cPtr, instance);

            RegisterInterfaces(cPtr, instance);

            return cPtr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_CreateInstance(IntPtr unityType, IntPtr cPtr);
        private static Callback_CreateInstance _createInstance = CreateInstance;

        [MonoPInvokeCallback(typeof(Callback_CreateInstance))]
        private static void CreateInstance(IntPtr unityType, IntPtr cPtr)
        {
            Type type = GetTypeFromPtr(unityType);

            bool isBaseComponent = typeof(Noesis.BaseComponent).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
            if (isBaseComponent)
            {
                _cPtr = cPtr;
                _extendType = type;
            }

            // This function is called when a Extend object is created from C++
            // so we create the corresponding C# proxy
            object instance = Activator.CreateInstance(type, new object[] { });

            AddExtendInfo(cPtr, instance);
            
            if (isBaseComponent)
            {
                _cPtr = IntPtr.Zero;
                _extendType = null;
            }
            else
            {
                // For Extended objects not inheriting from BaseComponent that are created from
                // C++ side, we need to manually add the reference that is holded by this object

                NoesisGUI_PINVOKE.BaseComponent_AddReference(new HandleRef(instance, cPtr));
            }

            RegisterInterfaces(cPtr, instance);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_DeleteInstance(IntPtr cPtr);
        private static Callback_DeleteInstance _deleteInstance = DeleteInstance;

        [MonoPInvokeCallback(typeof(Callback_DeleteInstance))]
        private static void DeleteInstance(IntPtr cPtr)
        {
            RemoveExtendInfo(cPtr);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void Callback_GrabInstance(IntPtr cPtr, [MarshalAs(UnmanagedType.U1)] bool grab);
        private static Callback_GrabInstance _grabInstance = GrabInstance;

        [MonoPInvokeCallback(typeof(Callback_GrabInstance))]
        private static void GrabInstance(IntPtr cPtr, bool grab)
        {
            ExtendInfo extend = GetExtendInfo(cPtr);
            if (extend != null)
            {
                if (grab)
                {
                    extend.instance = extend.weak.Target;

                    if (!(extend.instance is BaseComponent))
                    {
                        _weakExtends.Remove(cPtr);
                    }
                }
                else
                {
                    if (!(extend.instance is BaseComponent))
                    {
                        _weakExtends.Add(cPtr, extend.weak);
                    }

                    extend.instance = null;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static object GetExtendInstance(IntPtr cPtr)
        {
            ExtendInfo extend = GetExtendInfo(cPtr);
            return extend != null ? extend.weak.Target : null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static ExtendInfo GetExtendInfo(IntPtr cPtr)
        {
            ExtendInfo extend = null;
            if (!_extends.TryGetValue(cPtr, out extend))
            {
                //Debug.LogWarning("Extend already removed");
                return null;
            }
            else if (extend == null || !extend.weak.IsAlive)
            {
                //Debug.LogWarning("Extend already destroyed");
                return null;
            }

            return extend;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void AddExtendInfo(IntPtr cPtr, object instance)
        {
            ExtendInfo extend = new ExtendInfo();
            extend.instance = null;
            extend.weak = new WeakReference(instance);
            _extends.Add(cPtr, extend);
            _extendPtrs.Add(extend.weak, cPtr);

            if (!(instance is BaseComponent))
            {
                _weakExtends.Add(cPtr, extend.weak);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void RemoveExtendInfo(IntPtr cPtr)
        {
            ExtendInfo extend = GetExtendInfo(cPtr);
            _extendPtrs.Remove(extend != null ? extend.weak : new WeakReference(null));
            _extends.Remove(cPtr);
            _weakExtends.Remove(cPtr);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void RemoveDestroyedExtends()
        {
            foreach (KeyValuePair<IntPtr, WeakReference> kv in _weakExtends)
            {
                if (!kv.Value.IsAlive)
                {
                    _destroyedExtends.Add(kv);
                }
            }

            foreach (KeyValuePair<IntPtr, WeakReference> kv in _destroyedExtends)
            {
                // Destroying the C++ proxy will also remove the object from extend tables,
                // so nothing else should be done here

                NoesisGUI_PINVOKE.BaseComponent_Release(new HandleRef(null, kv.Key));
            }

            _destroyedExtends.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private class ExtendInfo
        {
            public object instance;
            public WeakReference weak;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        static Dictionary<IntPtr, ExtendInfo> _extends = new Dictionary<IntPtr, ExtendInfo>();

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private class ExtendPtrComparer : IEqualityComparer<WeakReference>
        {
            public bool Equals(WeakReference x, WeakReference y) { return x.Target == y.Target; }
            public int GetHashCode(WeakReference wr) { return wr.IsAlive ? wr.Target.GetHashCode() : 0; }
        }

        static Dictionary<WeakReference, IntPtr> _extendPtrs =
            new Dictionary<WeakReference, IntPtr>(new ExtendPtrComparer());

        ////////////////////////////////////////////////////////////////////////////////////////////////
        static Dictionary<IntPtr, WeakReference> _weakExtends = new Dictionary<IntPtr, WeakReference>();

        private static List<KeyValuePair<IntPtr, WeakReference>> _destroyedExtends =
            new List<KeyValuePair<IntPtr, WeakReference>>();

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static Noesis.BaseComponent GetProxyInstance(IntPtr cPtr, bool ownMemory, NativeTypeInfo info)
        {
            WeakReference wr;
            if (_proxies.TryGetValue(cPtr, out wr))
            {
                if (wr != null && wr.IsAlive)
                {
                    return (Noesis.BaseComponent)wr.Target;
                }
            }

            return AddProxy(info.Creator(cPtr, ownMemory));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static Noesis.BaseComponent AddProxy(Noesis.BaseComponent instance)
        {
            if (_proxiesMutex.WaitOne())
            {
                IntPtr cPtr = Noesis.BaseComponent.getCPtr(instance).Handle;
                _proxies.Add(cPtr, new WeakReference(instance));

                _proxiesMutex.ReleaseMutex();
            }

            return instance;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void RemoveProxy(IntPtr cPtr)
        {
            if (_proxiesMutex.WaitOne())
            {
                _proxies.Remove(cPtr);

                _proxiesMutex.ReleaseMutex();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        static Dictionary<IntPtr, WeakReference> _proxies = new Dictionary<IntPtr, WeakReference>();

        static System.Threading.Mutex _proxiesMutex = new System.Threading.Mutex();

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static HandleRef GetInstanceHandle(object instance)
        {
            if (instance != null)
            {
                object boxed = Box(instance);

                IntPtr cPtr = FindInstancePtr(boxed);

                if (cPtr == IntPtr.Zero)
                {
                    cPtr = New(instance.GetType(), boxed);
                }

                return new HandleRef(boxed, cPtr);
            }
            else
            {
                return new HandleRef(null, IntPtr.Zero);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static IntPtr FindInstancePtr(object instance)
        {
            IntPtr cPtr = IntPtr.Zero;
            
            if (instance is BaseComponent)
            {
                cPtr = BaseComponent.getCPtr((BaseComponent)instance).Handle;
            }
            else
            {
                _extendPtrs.TryGetValue(new WeakReference(instance), out cPtr);
            }

            return cPtr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void RegisterInterfaces(IntPtr cPtr, object instance)
        {
            // For INotifyPropertyChanged objects, we need to hook to the PropertyChanged event
            // so we can notify C++ side when a property is changed in Mono
            System.ComponentModel.INotifyPropertyChanged notifyP =
                instance as System.ComponentModel.INotifyPropertyChanged;
            if (notifyP != null)
            {
                notifyP.PropertyChanged += NotifyPropertyChanged;
            }

            // For INotifyCollectionChanged objects, we need to hook to the CollectionChanged event
            // so we can notify C++ side when collection has changed in Mono
            System.Collections.Specialized.INotifyCollectionChanged notifyC =
                instance as System.Collections.Specialized.INotifyCollectionChanged;
            if (notifyC != null)
            {
                notifyC.CollectionChanged += NotifyCollectionChanged;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void NotifyPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IntPtr cPtr = FindInstancePtr(sender);
            if (cPtr == IntPtr.Zero)
            {
                throw new InvalidOperationException("Extend instance not registered");
            }

            IntPtr unityType = Extend.GetPtrForType(sender.GetType());

            Noesis_LaunchPropertyChangedEvent_(unityType, cPtr, new StringMarshal(e.PropertyName));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        static void NotifyCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IntPtr cPtr = FindInstancePtr(sender);
            if (cPtr == IntPtr.Zero)
            {
                throw new InvalidOperationException("Extend instance not registered");
            }

            IntPtr unityType = Extend.GetPtrForType(sender.GetType());

            object newItem = (e.NewItems != null && e.NewItems.Count > 0 ? e.NewItems[0] : null);
            object oldItem = (e.OldItems != null && e.OldItems.Count > 0 ? e.OldItems[0] : null);

            Noesis_LaunchCollectionChangedEvent_(unityType, cPtr, (int)e.Action,
                GetInstanceHandle(newItem).Handle, GetInstanceHandle(oldItem).Handle,
                e.NewStartingIndex, e.OldStartingIndex);
        }
    }
}
