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

public class ScrollViewer : ContentControl {

  internal ScrollViewer(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.ScrollViewer_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(ScrollViewer obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }


  public delegate void ScrollChangedHandler(object sender, ScrollChangedEventArgs e);
  public event ScrollChangedHandler ScrollChanged {
    add {
      if (!_ScrollChanged.ContainsKey(swigCPtr.Handle)) {
        _ScrollChanged.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_ScrollViewer_ScrollChanged(_raiseScrollChanged, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _ScrollChanged[swigCPtr.Handle] += value;
    }
    remove {
      if (!_ScrollChanged.ContainsKey(swigCPtr.Handle)) {
        throw new System.Exception("Delegate not found");
      }

      _ScrollChanged[swigCPtr.Handle] -= value;

      if (_ScrollChanged[swigCPtr.Handle] == null) {
        NoesisGUI_PINVOKE.UnbindEvent_ScrollViewer_ScrollChanged(_raiseScrollChanged, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _ScrollChanged.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseScrollChangedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseScrollChangedCallback _raiseScrollChanged = RaiseScrollChanged;

  [MonoPInvokeCallback(typeof(RaiseScrollChangedCallback))]
  private static void RaiseScrollChanged(IntPtr cPtr, IntPtr sender, IntPtr e) {
    if (!_ScrollChanged.ContainsKey(cPtr)) {
      throw new System.Exception("Delegate not found");
    }
    if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
      _ScrollChanged.Remove(cPtr);
      return;
    }
    ScrollChangedHandler handler = _ScrollChanged[cPtr];
    if (handler != null) {
      handler(Noesis.Extend.Unbox(Noesis.Extend.GetProxy(sender, false)), new ScrollChangedEventArgs(e, false));
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, ScrollChangedHandler> _ScrollChanged =
      new System.Collections.Generic.Dictionary<System.IntPtr, ScrollChangedHandler>();


  public ScrollViewer() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(ScrollViewer)) {
      return NoesisGUI_PINVOKE.new_ScrollViewer();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public static ScrollBarVisibility GetHorizontalScrollBarVisibility(DependencyObject element) {
    ScrollBarVisibility ret = (ScrollBarVisibility)NoesisGUI_PINVOKE.ScrollViewer_GetHorizontalScrollBarVisibility(DependencyObject.getCPtr(element));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public static void SetHorizontalScrollBarVisibility(DependencyObject element, ScrollBarVisibility visibility) {
    NoesisGUI_PINVOKE.ScrollViewer_SetHorizontalScrollBarVisibility(DependencyObject.getCPtr(element), (int)visibility);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public static ScrollBarVisibility GetVerticalScrollBarVisibility(DependencyObject element) {
    ScrollBarVisibility ret = (ScrollBarVisibility)NoesisGUI_PINVOKE.ScrollViewer_GetVerticalScrollBarVisibility(DependencyObject.getCPtr(element));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public static void SetVerticalScrollBarVisibility(DependencyObject element, ScrollBarVisibility visibility) {
    NoesisGUI_PINVOKE.ScrollViewer_SetVerticalScrollBarVisibility(DependencyObject.getCPtr(element), (int)visibility);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public ScrollContentPresenter GetScrollInfo() {
    IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_GetScrollInfo(swigCPtr);
    ScrollContentPresenter ret = (cPtr == IntPtr.Zero) ? null : new ScrollContentPresenter(cPtr, false);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public void SetScrollInfo(ScrollContentPresenter scrollInfo) {
    NoesisGUI_PINVOKE.ScrollViewer_SetScrollInfo(swigCPtr, ScrollContentPresenter.getCPtr(scrollInfo));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void InvalidateScrollInfo() {
    NoesisGUI_PINVOKE.ScrollViewer_InvalidateScrollInfo(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void ScrollToHorizontalOffset(float offset) {
    NoesisGUI_PINVOKE.ScrollViewer_ScrollToHorizontalOffset(swigCPtr, offset);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void ScrollToVerticalOffset(float offset) {
    NoesisGUI_PINVOKE.ScrollViewer_ScrollToVerticalOffset(swigCPtr, offset);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public static DependencyProperty CanContentScrollProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_CanContentScrollProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ComputedHorizontalScrollBarVisibilityProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_ComputedHorizontalScrollBarVisibilityProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ComputedVerticalScrollBarVisibilityProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_ComputedVerticalScrollBarVisibilityProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ExtentHeightProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_ExtentHeightProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ExtentWidthProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_ExtentWidthProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty HorizontalOffsetProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_HorizontalOffsetProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty HorizontalScrollBarVisibilityProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_HorizontalScrollBarVisibilityProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ScrollableHeightProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_ScrollableHeightProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ScrollableWidthProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_ScrollableWidthProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty VerticalOffsetProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_VerticalOffsetProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty VerticalScrollBarVisibilityProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_VerticalScrollBarVisibilityProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ViewportHeightProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_ViewportHeightProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty ViewportWidthProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_ViewportWidthProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty PanningModeProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_PanningModeProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty PanningDecelerationProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_PanningDecelerationProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty PanningRatioProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ScrollViewer_PanningRatioProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool CanContentScroll {
    set {
      NoesisGUI_PINVOKE.ScrollViewer_CanContentScroll_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.ScrollViewer_CanContentScroll_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public PanningMode PanningMode {
    set {
      NoesisGUI_PINVOKE.ScrollViewer_PanningMode_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      PanningMode ret = (PanningMode)NoesisGUI_PINVOKE.ScrollViewer_PanningMode_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float PanningDeceleration {
    set {
      NoesisGUI_PINVOKE.ScrollViewer_PanningDeceleration_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_PanningDeceleration_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float PanningRatio {
    set {
      NoesisGUI_PINVOKE.ScrollViewer_PanningRatio_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_PanningRatio_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public Visibility ComputedHorizontalScrollBarVisibility {
    get {
      Visibility ret = (Visibility)NoesisGUI_PINVOKE.ScrollViewer_ComputedHorizontalScrollBarVisibility_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public Visibility ComputedVerticalScrollBarVisibility {
    get {
      Visibility ret = (Visibility)NoesisGUI_PINVOKE.ScrollViewer_ComputedVerticalScrollBarVisibility_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float ExtentWidth {
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_ExtentWidth_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float ExtentHeight {
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_ExtentHeight_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float HorizontalOffset {
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_HorizontalOffset_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float VerticalOffset {
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_VerticalOffset_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float ScrollableWidth {
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_ScrollableWidth_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float ScrollableHeight {
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_ScrollableHeight_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float ViewportWidth {
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_ViewportWidth_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public float ViewportHeight {
    get {
      float ret = NoesisGUI_PINVOKE.ScrollViewer_ViewportHeight_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.ScrollViewer_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }


  internal new static IntPtr Extend(System.Type type) {
    IntPtr unityType = Noesis.Extend.GetPtrForType(type);
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_ScrollViewer(unityType);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return nativeType;
  }
}

}

