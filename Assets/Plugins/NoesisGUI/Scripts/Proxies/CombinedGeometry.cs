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

public class CombinedGeometry : Geometry {

  internal CombinedGeometry(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.CombinedGeometry_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(CombinedGeometry obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public CombinedGeometry() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(CombinedGeometry)) {
      return NoesisGUI_PINVOKE.new_CombinedGeometry__SWIG_0();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public CombinedGeometry(Geometry geometry1, Geometry geometry2, GeometryCombineMode mode) : this(NoesisGUI_PINVOKE.new_CombinedGeometry__SWIG_1(Geometry.getCPtr(geometry1), Geometry.getCPtr(geometry2), (int)mode), true) {
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public static DependencyProperty Geometry1Property {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.CombinedGeometry_Geometry1Property_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty Geometry2Property {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.CombinedGeometry_Geometry2Property_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty GeometryCombineModeProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.CombinedGeometry_GeometryCombineModeProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public Geometry Geometry1 {
    set {
      NoesisGUI_PINVOKE.CombinedGeometry_Geometry1_set(swigCPtr, Geometry.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.CombinedGeometry_Geometry1_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as Geometry;
    }
  
  }

  public Geometry Geometry2 {
    set {
      NoesisGUI_PINVOKE.CombinedGeometry_Geometry2_set(swigCPtr, Geometry.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.CombinedGeometry_Geometry2_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as Geometry;
    }
  
  }

  public GeometryCombineMode GeometryCombineMode {
    set {
      NoesisGUI_PINVOKE.CombinedGeometry_GeometryCombineMode_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      GeometryCombineMode ret = (GeometryCombineMode)NoesisGUI_PINVOKE.CombinedGeometry_GeometryCombineMode_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.CombinedGeometry_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

