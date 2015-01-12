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

public class VisualState : DependencyObject {

  internal VisualState(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.VisualState_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(VisualState obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public VisualState() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(VisualState)) {
      return NoesisGUI_PINVOKE.new_VisualState();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public string Name {
  set {
    NoesisGUI_PINVOKE.VisualState_Name_set(swigCPtr, value != null ? value : string.Empty);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
  }

  get {
    string ret = NoesisGUI_PINVOKE.VisualState_Name_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    return ret;
  }

  }

  public Storyboard Storyboard {
    set {
      NoesisGUI_PINVOKE.VisualState_Storyboard_set(swigCPtr, Storyboard.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.VisualState_Storyboard_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as Storyboard;
    }
  
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.VisualState_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}
