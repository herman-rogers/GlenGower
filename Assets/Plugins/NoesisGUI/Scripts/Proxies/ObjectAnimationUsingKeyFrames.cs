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

public class ObjectAnimationUsingKeyFrames : AnimationTimeline {

  internal ObjectAnimationUsingKeyFrames(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.ObjectAnimationUsingKeyFrames_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(ObjectAnimationUsingKeyFrames obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public ObjectAnimationUsingKeyFrames() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(ObjectAnimationUsingKeyFrames)) {
      return NoesisGUI_PINVOKE.new_ObjectAnimationUsingKeyFrames();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public ColorKeyFrameCollection KeyFrames {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ObjectAnimationUsingKeyFrames_KeyFrames_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as ColorKeyFrameCollection;
    }
  
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.ObjectAnimationUsingKeyFrames_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}
