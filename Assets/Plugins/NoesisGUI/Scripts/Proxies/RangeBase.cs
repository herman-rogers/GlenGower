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

public class RangeBase : Control {

  internal RangeBase(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.RangeBase_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(RangeBase obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  protected RangeBase() {
  }

  public static DependencyProperty LargeChangeProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.RangeBase_LargeChangeProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty MaximumProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.RangeBase_MaximumProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty MinimumProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.RangeBase_MinimumProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty SmallChangeProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.RangeBase_SmallChangeProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ValueProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.RangeBase_ValueProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float LargeChange {
    set {
      NoesisGUI_PINVOKE.RangeBase_LargeChange_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.RangeBase_LargeChange_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float Maximum {
    set {
      NoesisGUI_PINVOKE.RangeBase_Maximum_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.RangeBase_Maximum_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float Minimum {
    set {
      NoesisGUI_PINVOKE.RangeBase_Minimum_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.RangeBase_Minimum_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float SmallChange {
    set {
      NoesisGUI_PINVOKE.RangeBase_SmallChange_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.RangeBase_SmallChange_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float Value {
    set {
      NoesisGUI_PINVOKE.RangeBase_Value_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.RangeBase_Value_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.RangeBase_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public delegate void ValueChangedHandler(float oldValue, float newValue);
  public event ValueChangedHandler ValueChanged
  {
    add
    {
      if (!_ValueChanged.ContainsKey(swigCPtr.Handle))
      {
        _ValueChanged.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_RangeBase_ValueChanged(_raiseValueChanged, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _ValueChanged[swigCPtr.Handle] += value;
    }
    remove
    {
      if (!_ValueChanged.ContainsKey(swigCPtr.Handle))
      {
        throw new System.Exception("Delegate not found");
      }

      _ValueChanged[swigCPtr.Handle] -= value;

      if (_ValueChanged[swigCPtr.Handle] == null)
      {
        NoesisGUI_PINVOKE.UnbindEvent_RangeBase_ValueChanged(_raiseValueChanged, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _ValueChanged.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseValueChangedCallback(IntPtr cPtr, float oldValue, float newValue);
  private static RaiseValueChangedCallback _raiseValueChanged = RaiseValueChanged;

  [MonoPInvokeCallback(typeof(RaiseValueChangedCallback))]
  private static void RaiseValueChanged(IntPtr cPtr, float oldValue, float newValue)
  {
    if (!_ValueChanged.ContainsKey(cPtr))
    {
      throw new System.Exception("Delegate not found");
    }
    if (oldValue == -1234.5678f && newValue == -1234.5678f) {
      _ValueChanged.Remove(cPtr);
      return;
    }
    ValueChangedHandler handler = _ValueChanged[cPtr];
    if (handler != null)
    {
      handler(oldValue, newValue);
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, ValueChangedHandler> _ValueChanged =
      new System.Collections.Generic.Dictionary<System.IntPtr, ValueChangedHandler>();


  internal new static IntPtr Extend(System.Type type) {
    IntPtr unityType = Noesis.Extend.GetPtrForType(type);
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_RangeBase(unityType);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return nativeType;
  }
}

}
