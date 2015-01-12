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

public class Condition : BaseComponent {

  internal Condition(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.Condition_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Condition obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public Condition() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(Condition)) {
      return NoesisGUI_PINVOKE.new_Condition();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public DependencyProperty Property {
    set {
      NoesisGUI_PINVOKE.Condition_Property_set(swigCPtr, DependencyProperty.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Condition_Property_get(swigCPtr);
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public string SourceName {
  set {
    NoesisGUI_PINVOKE.Condition_SourceName_set(swigCPtr, value != null ? value : string.Empty);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
  }

  get {
    string ret = NoesisGUI_PINVOKE.Condition_SourceName_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    return ret;
  }

  }

  public object Value {
    set {
      NoesisGUI_PINVOKE.Condition_Value_set(swigCPtr, Noesis.Extend.GetInstanceHandle(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }

    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Condition_Value_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.Unbox(Noesis.Extend.GetProxy(cPtr, false));
    }

  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Condition_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}
