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

public class Storyboard : ParallelTimeline {

  internal Storyboard(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.Storyboard_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Storyboard obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public Storyboard() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(Storyboard)) {
      return NoesisGUI_PINVOKE.new_Storyboard();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public static string GetTargetName(DependencyObject element) {
    string ret = NoesisGUI_PINVOKE.Storyboard_GetTargetName(DependencyObject.getCPtr(element));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public static void SetTargetName(DependencyObject element, string name) {
    NoesisGUI_PINVOKE.Storyboard_SetTargetName(DependencyObject.getCPtr(element), name != null ? name : string.Empty);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public static PropertyPath GetTargetProperty(DependencyObject element) {
    IntPtr cPtr = NoesisGUI_PINVOKE.Storyboard_GetTargetProperty(DependencyObject.getCPtr(element));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return Noesis.Extend.GetProxy(cPtr, false) as PropertyPath;
  }

  public static void SetTargetProperty(DependencyObject element, PropertyPath path) {
    NoesisGUI_PINVOKE.Storyboard_SetTargetProperty(DependencyObject.getCPtr(element), PropertyPath.getCPtr(path));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public static DependencyObject GetTarget(DependencyObject element) {
    IntPtr cPtr = NoesisGUI_PINVOKE.Storyboard_GetTarget(DependencyObject.getCPtr(element));
    DependencyObject ret = (cPtr == IntPtr.Zero) ? null : new DependencyObject(cPtr, false);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public static void SetTarget(DependencyObject element, DependencyObject target) {
    NoesisGUI_PINVOKE.Storyboard_SetTarget(DependencyObject.getCPtr(element), DependencyObject.getCPtr(target));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void Begin(FrameworkElement target, FrameworkElement nameScope, bool isControllable, HandoffBehavior handoffBehavior) {
    NoesisGUI_PINVOKE.Storyboard_Begin__SWIG_0(swigCPtr, FrameworkElement.getCPtr(target), FrameworkElement.getCPtr(nameScope), isControllable, (int)handoffBehavior);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void Begin(FrameworkElement target, FrameworkElement nameScope, bool isControllable) {
    NoesisGUI_PINVOKE.Storyboard_Begin__SWIG_1(swigCPtr, FrameworkElement.getCPtr(target), FrameworkElement.getCPtr(nameScope), isControllable);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void Begin(FrameworkElement target, FrameworkElement nameScope) {
    NoesisGUI_PINVOKE.Storyboard_Begin__SWIG_2(swigCPtr, FrameworkElement.getCPtr(target), FrameworkElement.getCPtr(nameScope));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void Begin(FrameworkElement target) {
    NoesisGUI_PINVOKE.Storyboard_Begin__SWIG_3(swigCPtr, FrameworkElement.getCPtr(target));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void Pause(FrameworkElement target) {
    NoesisGUI_PINVOKE.Storyboard_Pause(swigCPtr, FrameworkElement.getCPtr(target));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void Resume(FrameworkElement target) {
    NoesisGUI_PINVOKE.Storyboard_Resume(swigCPtr, FrameworkElement.getCPtr(target));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void Stop(FrameworkElement target) {
    NoesisGUI_PINVOKE.Storyboard_Stop(swigCPtr, FrameworkElement.getCPtr(target));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void Remove(FrameworkElement target) {
    NoesisGUI_PINVOKE.Storyboard_Remove(swigCPtr, FrameworkElement.getCPtr(target));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public bool IsPlaying(FrameworkElement target) {
    bool ret = NoesisGUI_PINVOKE.Storyboard_IsPlaying(swigCPtr, FrameworkElement.getCPtr(target));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public bool IsPaused(FrameworkElement target) {
    bool ret = NoesisGUI_PINVOKE.Storyboard_IsPaused(swigCPtr, FrameworkElement.getCPtr(target));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public static DependencyProperty TargetNameProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Storyboard_TargetNameProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty TargetProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Storyboard_TargetProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty TargetPropertyProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Storyboard_TargetPropertyProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Storyboard_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}

