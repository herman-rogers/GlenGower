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

public class SplineInt16KeyFrame : Int16KeyFrame {

  internal SplineInt16KeyFrame(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.SplineInt16KeyFrame_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(SplineInt16KeyFrame obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public SplineInt16KeyFrame() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(SplineInt16KeyFrame)) {
      return NoesisGUI_PINVOKE.new_SplineInt16KeyFrame();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public static DependencyProperty KeySplineProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.SplineInt16KeyFrame_KeySplineProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public KeySpline KeySpline {
    set {
      NoesisGUI_PINVOKE.SplineInt16KeyFrame_KeySpline_set(swigCPtr, KeySpline.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.SplineInt16KeyFrame_KeySpline_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as KeySpline;
    }
  
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.SplineInt16KeyFrame_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}
