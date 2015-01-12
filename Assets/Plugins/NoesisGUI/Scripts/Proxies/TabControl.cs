/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.4
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

public class TabControl : Selector {

  internal TabControl(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.TabControl_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(TabControl obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public TabControl() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(TabControl)) {
      return NoesisGUI_PINVOKE.new_TabControl();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public static DependencyProperty ContentTemplateProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_ContentTemplateProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ContentTemplateSelectorProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_ContentTemplateSelectorProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty SelectedContentProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_SelectedContentProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty SelectedContentTemplateProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_SelectedContentTemplateProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty SelectedContentTemplateSelectorProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_SelectedContentTemplateSelectorProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty TabStripPlacementProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_TabStripPlacementProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public DataTemplate ContentTemplate {
    set {
      NoesisGUI_PINVOKE.TabControl_ContentTemplate_set(swigCPtr, DataTemplate.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_ContentTemplate_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as DataTemplate;
    }
  
  }

  public DataTemplateSelector ContentTemplateSelector {
    set {
      NoesisGUI_PINVOKE.TabControl_ContentTemplateSelector_set(swigCPtr, DataTemplateSelector.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_ContentTemplateSelector_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as DataTemplateSelector;
    }
  
  }

  public object SelectedContent {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_SelectedContent_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.Unbox(Noesis.Extend.GetProxy(cPtr, false));
    }

  }

  public DataTemplate SelectedContentTemplate {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_SelectedContentTemplate_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as DataTemplate;
    }
  
  }

  public DataTemplateSelector SelectedContentTemplateSelector {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TabControl_SelectedContentTemplateSelector_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as DataTemplateSelector;
    }
  
  }

  public Dock TabStripPlacement {
    set {
      NoesisGUI_PINVOKE.TabControl_TabStripPlacement_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      Dock ret = (Dock)NoesisGUI_PINVOKE.TabControl_TabStripPlacement_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.TabControl_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }


  internal new static IntPtr Extend(System.Type type) {
    IntPtr unityType = Noesis.Extend.GetPtrForType(type);
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_TabControl(unityType);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return nativeType;
  }
}

}
