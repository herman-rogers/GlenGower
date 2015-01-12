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

public class ThicknessAnimationUsingKeyFrames : AnimationTimeline {

  internal ThicknessAnimationUsingKeyFrames(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.ThicknessAnimationUsingKeyFrames_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(ThicknessAnimationUsingKeyFrames obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public ThicknessAnimationUsingKeyFrames() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(ThicknessAnimationUsingKeyFrames)) {
      return NoesisGUI_PINVOKE.new_ThicknessAnimationUsingKeyFrames();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public ThicknessKeyFrameCollection KeyFrames {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ThicknessAnimationUsingKeyFrames_KeyFrames_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as ThicknessKeyFrameCollection;
    }
  
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.ThicknessAnimationUsingKeyFrames_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}
