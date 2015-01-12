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

public class RadioButton : ToggleButton {

  internal RadioButton(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.RadioButton_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(RadioButton obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public RadioButton() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(RadioButton)) {
      return NoesisGUI_PINVOKE.new_RadioButton();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public static DependencyProperty GroupNameProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.RadioButton_GroupNameProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public string GroupName {
  set {
    NoesisGUI_PINVOKE.RadioButton_GroupName_set(swigCPtr, value != null ? value : string.Empty);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
  }

  get {
    string ret = NoesisGUI_PINVOKE.RadioButton_GroupName_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    return ret;
  }

  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.RadioButton_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }


  internal new static IntPtr Extend(System.Type type) {
    IntPtr unityType = Noesis.Extend.GetPtrForType(type);
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_RadioButton(unityType);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return nativeType;
  }
}

}
