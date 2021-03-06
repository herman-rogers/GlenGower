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

public class BeginStoryboard : TriggerAction {

  internal BeginStoryboard(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.BeginStoryboard_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(BeginStoryboard obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public BeginStoryboard() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(BeginStoryboard)) {
      return NoesisGUI_PINVOKE.new_BeginStoryboard();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public static DependencyProperty StoryboardProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.BeginStoryboard_StoryboardProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public HandoffBehavior HandoffBehavior {
    set {
      NoesisGUI_PINVOKE.BeginStoryboard_HandoffBehavior_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      HandoffBehavior ret = (HandoffBehavior)NoesisGUI_PINVOKE.BeginStoryboard_HandoffBehavior_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public string Name {
  set {
    NoesisGUI_PINVOKE.BeginStoryboard_Name_set(swigCPtr, value != null ? value : string.Empty);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
  }

  get {
    string ret = NoesisGUI_PINVOKE.BeginStoryboard_Name_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    return ret;
  }

  }

  public Storyboard Storyboard {
    set {
      NoesisGUI_PINVOKE.BeginStoryboard_Storyboard_set(swigCPtr, Storyboard.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.BeginStoryboard_Storyboard_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as Storyboard;
    }
  
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.BeginStoryboard_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

