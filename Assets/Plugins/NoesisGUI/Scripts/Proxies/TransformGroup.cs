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

public class TransformGroup : Transform {

  internal TransformGroup(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.TransformGroup_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(TransformGroup obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public TransformGroup() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(TransformGroup)) {
      return NoesisGUI_PINVOKE.new_TransformGroup();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public static DependencyProperty ChildrenProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TransformGroup_ChildrenProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public TransformCollection Children {
    set {
      NoesisGUI_PINVOKE.TransformGroup_Children_set(swigCPtr, TransformCollection.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TransformGroup_Children_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as TransformCollection;
    }
  
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.TransformGroup_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

