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

public class Timeline : Animatable {

  internal Timeline(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.Timeline_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Timeline obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  protected Timeline() {
  }

  public static int GetDesiredFrameRate(DependencyObject timeline) {
    int ret = NoesisGUI_PINVOKE.Timeline_GetDesiredFrameRate(DependencyObject.getCPtr(timeline));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public static void SetDesiredFrameRate(DependencyObject timeline, int rate) {
    NoesisGUI_PINVOKE.Timeline_SetDesiredFrameRate(DependencyObject.getCPtr(timeline), rate);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public static DependencyProperty AccelerationRatioProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_AccelerationRatioProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty AutoReverseProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_AutoReverseProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty BeginTimeProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_BeginTimeProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty DecelerationRatioProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_DecelerationRatioProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty DesiredFrameRateProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_DesiredFrameRateProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty DurationProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_DurationProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty FillBehaviorProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_FillBehaviorProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty NameProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_NameProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty RepeatBehaviorProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_RepeatBehaviorProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty SpeedRatioProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Timeline_SpeedRatioProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float AccelerationRatio {
    set {
      NoesisGUI_PINVOKE.Timeline_AccelerationRatio_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.Timeline_AccelerationRatio_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool AutoReverse {
    set {
      NoesisGUI_PINVOKE.Timeline_AutoReverse_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.Timeline_AutoReverse_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public System.Nullable<System.TimeSpan> BeginTime {
  set {
    NoesisGUI_PINVOKE.Timeline_BeginTime_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
  }

  get {
    IntPtr ret = NoesisGUI_PINVOKE.Timeline_BeginTime_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    if (ret != IntPtr.Zero) {
      return Marshal.PtrToStructure<NullableTimeSpan>(ret);
    }
    else {
      return new System.Nullable<System.TimeSpan>();
    }
  }

  }

  public float DecelerationRatio {
    set {
      NoesisGUI_PINVOKE.Timeline_DecelerationRatio_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.Timeline_DecelerationRatio_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public Duration Duration {
    set {
      NoesisGUI_PINVOKE.Timeline_Duration_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }

    get {
      IntPtr ret = NoesisGUI_PINVOKE.Timeline_Duration_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      if (ret != IntPtr.Zero) {
        Duration jret = Marshal.PtrToStructure<Duration>(ret);
        return jret;
      }
      else {
        return new Duration();
      }
    }

  }

  public FillBehavior FillBehavior {
    set {
      NoesisGUI_PINVOKE.Timeline_FillBehavior_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      FillBehavior ret = (FillBehavior)NoesisGUI_PINVOKE.Timeline_FillBehavior_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public string Name {
  set {
    NoesisGUI_PINVOKE.Timeline_Name_set(swigCPtr, value != null ? value : string.Empty);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
  }

  get {
    string ret = NoesisGUI_PINVOKE.Timeline_Name_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    return ret;
  }

  }

  public RepeatBehavior RepeatBehavior {
    set {
      NoesisGUI_PINVOKE.Timeline_RepeatBehavior_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }

    get {
      IntPtr ret = NoesisGUI_PINVOKE.Timeline_RepeatBehavior_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      if (ret != IntPtr.Zero) {
        RepeatBehavior jret = Marshal.PtrToStructure<RepeatBehavior>(ret);
        return jret;
      }
      else {
        return new RepeatBehavior();
      }
    }

  }

  public float SpeedRatio {
    set {
      NoesisGUI_PINVOKE.Timeline_SpeedRatio_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.Timeline_SpeedRatio_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Timeline_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public delegate void CompletedHandler(object sender, TimelineEventArgs e);
  public event CompletedHandler Completed
  {
    add
    {
      if (!_Completed.ContainsKey(swigCPtr.Handle))
      {
        _Completed.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_Timeline_Completed(_raiseCompleted, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _Completed[swigCPtr.Handle] += value;
    }
    remove
    {
      if (!_Completed.ContainsKey(swigCPtr.Handle))
      {
        throw new System.Exception("Delegate not found");
      }

      _Completed[swigCPtr.Handle] -= value;

      if (_Completed[swigCPtr.Handle] == null)
      {
        NoesisGUI_PINVOKE.UnbindEvent_Timeline_Completed(_raiseCompleted, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _Completed.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseCompletedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseCompletedCallback _raiseCompleted = RaiseCompleted;

  [MonoPInvokeCallback(typeof(RaiseCompletedCallback))]
  private static void RaiseCompleted(IntPtr cPtr, IntPtr sender, IntPtr e)
  {
    if (!_Completed.ContainsKey(cPtr))
    {
      throw new System.Exception("Delegate not found");
    }
    if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
      _Completed.Remove(cPtr);
      return;
    }
    CompletedHandler handler = _Completed[cPtr];
    if (handler != null)
    {
      handler(Noesis.Extend.Unbox(Noesis.Extend.GetProxy(sender, false)), new TimelineEventArgs(e, false));
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, CompletedHandler> _Completed =
      new System.Collections.Generic.Dictionary<System.IntPtr, CompletedHandler>();

}

}
