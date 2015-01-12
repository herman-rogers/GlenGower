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

public class Viewbox : FrameworkElement {

  internal Viewbox(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.Viewbox_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Viewbox obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public Viewbox() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(Viewbox)) {
      return NoesisGUI_PINVOKE.new_Viewbox();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public static Point GetStretchScale(Size elementSize, Size availableSize, Stretch stretch, StretchDirection stretchDirection) {
    IntPtr ret = NoesisGUI_PINVOKE.Viewbox_GetStretchScale__SWIG_0(elementSize, availableSize, (int)stretch, (int)stretchDirection);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    if (ret != IntPtr.Zero) {
      Point jret = Marshal.PtrToStructure<Point>(ret);
      NoesisGUI_PINVOKE.Point_Delete(ret);
      return jret;
    }
    else {
      return new Point();
    }
  }

  public static Point GetStretchScale(Size elementSize, Size availableSize, Stretch stretch) {
    IntPtr ret = NoesisGUI_PINVOKE.Viewbox_GetStretchScale__SWIG_1(elementSize, availableSize, (int)stretch);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    if (ret != IntPtr.Zero) {
      Point jret = Marshal.PtrToStructure<Point>(ret);
      NoesisGUI_PINVOKE.Point_Delete(ret);
      return jret;
    }
    else {
      return new Point();
    }
  }

  public static DependencyProperty StretchDirectionProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Viewbox_StretchDirectionProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty StretchProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Viewbox_StretchProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public StretchDirection StretchDirection {
    set {
      NoesisGUI_PINVOKE.Viewbox_StretchDirection_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      StretchDirection ret = (StretchDirection)NoesisGUI_PINVOKE.Viewbox_StretchDirection_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public Stretch Stretch {
    set {
      NoesisGUI_PINVOKE.Viewbox_Stretch_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      Stretch ret = (Stretch)NoesisGUI_PINVOKE.Viewbox_Stretch_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public UIElement Child {
    set {
      NoesisGUI_PINVOKE.Viewbox_Child_set(swigCPtr, UIElement.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Viewbox_Child_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as UIElement;
    }
  
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Viewbox_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }


  internal new static IntPtr Extend(System.Type type) {
    IntPtr unityType = Noesis.Extend.GetPtrForType(type);
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_Viewbox(unityType);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return nativeType;
  }
}

}
