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

public class TextBoxBase : Control {

  internal TextBoxBase(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.TextBoxBase_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(TextBoxBase obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  protected TextBoxBase() {
  }


  public delegate void SelectionChangedHandler(object sender, RoutedEventArgs e);
  public event SelectionChangedHandler SelectionChanged {
    add {
      if (!_SelectionChanged.ContainsKey(swigCPtr.Handle)) {
        _SelectionChanged.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_TextBoxBase_SelectionChanged(_raiseSelectionChanged, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _SelectionChanged[swigCPtr.Handle] += value;
    }
    remove {
      if (!_SelectionChanged.ContainsKey(swigCPtr.Handle)) {
        throw new System.Exception("Delegate not found");
      }

      _SelectionChanged[swigCPtr.Handle] -= value;

      if (_SelectionChanged[swigCPtr.Handle] == null) {
        NoesisGUI_PINVOKE.UnbindEvent_TextBoxBase_SelectionChanged(_raiseSelectionChanged, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _SelectionChanged.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseSelectionChangedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseSelectionChangedCallback _raiseSelectionChanged = RaiseSelectionChanged;

  [MonoPInvokeCallback(typeof(RaiseSelectionChangedCallback))]
  private static void RaiseSelectionChanged(IntPtr cPtr, IntPtr sender, IntPtr e) {
    if (!_SelectionChanged.ContainsKey(cPtr)) {
      throw new System.Exception("Delegate not found");
    }
    if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
      _SelectionChanged.Remove(cPtr);
      return;
    }
    SelectionChangedHandler handler = _SelectionChanged[cPtr];
    if (handler != null) {
      handler(Noesis.Extend.Unbox(Noesis.Extend.GetProxy(sender, false)), new RoutedEventArgs(e, false));
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, SelectionChangedHandler> _SelectionChanged =
      new System.Collections.Generic.Dictionary<System.IntPtr, SelectionChangedHandler>();


  public delegate void TextChangedHandler(object sender, RoutedEventArgs e);
  public event TextChangedHandler TextChanged {
    add {
      if (!_TextChanged.ContainsKey(swigCPtr.Handle)) {
        _TextChanged.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_TextBoxBase_TextChanged(_raiseTextChanged, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _TextChanged[swigCPtr.Handle] += value;
    }
    remove {
      if (!_TextChanged.ContainsKey(swigCPtr.Handle)) {
        throw new System.Exception("Delegate not found");
      }

      _TextChanged[swigCPtr.Handle] -= value;

      if (_TextChanged[swigCPtr.Handle] == null) {
        NoesisGUI_PINVOKE.UnbindEvent_TextBoxBase_TextChanged(_raiseTextChanged, swigCPtr.Handle);
        #if UNITY_EDITOR || NOESIS_API
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _TextChanged.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseTextChangedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseTextChangedCallback _raiseTextChanged = RaiseTextChanged;

  [MonoPInvokeCallback(typeof(RaiseTextChangedCallback))]
  private static void RaiseTextChanged(IntPtr cPtr, IntPtr sender, IntPtr e) {
    if (!_TextChanged.ContainsKey(cPtr)) {
      throw new System.Exception("Delegate not found");
    }
    if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
      _TextChanged.Remove(cPtr);
      return;
    }
    TextChangedHandler handler = _TextChanged[cPtr];
    if (handler != null) {
      handler(Noesis.Extend.Unbox(Noesis.Extend.GetProxy(sender, false)), new RoutedEventArgs(e, false));
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, TextChangedHandler> _TextChanged =
      new System.Collections.Generic.Dictionary<System.IntPtr, TextChangedHandler>();


  public static DependencyProperty AcceptsReturnProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_AcceptsReturnProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty AcceptsTabProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_AcceptsTabProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty CaretBrushProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_CaretBrushProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty HorizontalScrollBarVisibilityProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_HorizontalScrollBarVisibilityProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty IsReadOnlyProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_IsReadOnlyProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty SelectionBrushProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_SelectionBrushProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty SelectionOpacityProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_SelectionOpacityProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty VerticalScrollBarVisibilityProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_VerticalScrollBarVisibilityProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool AcceptsReturn {
    set {
      NoesisGUI_PINVOKE.TextBoxBase_AcceptsReturn_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.TextBoxBase_AcceptsReturn_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool AcceptsTab {
    set {
      NoesisGUI_PINVOKE.TextBoxBase_AcceptsTab_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.TextBoxBase_AcceptsTab_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public Brush CaretBrush {
    set {
      NoesisGUI_PINVOKE.TextBoxBase_CaretBrush_set(swigCPtr, Brush.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_CaretBrush_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as Brush;
    }
  
  }

  public ScrollBarVisibility HorizontalScrollBarVisibility {
    set {
      NoesisGUI_PINVOKE.TextBoxBase_HorizontalScrollBarVisibility_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      ScrollBarVisibility ret = (ScrollBarVisibility)NoesisGUI_PINVOKE.TextBoxBase_HorizontalScrollBarVisibility_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool IsReadOnly {
    set {
      NoesisGUI_PINVOKE.TextBoxBase_IsReadOnly_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.TextBoxBase_IsReadOnly_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public Brush SelectionBrush {
    set {
      NoesisGUI_PINVOKE.TextBoxBase_SelectionBrush_set(swigCPtr, Brush.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextBoxBase_SelectionBrush_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as Brush;
    }
  
  }

  public float SelectionOpacity {
    set {
      NoesisGUI_PINVOKE.TextBoxBase_SelectionOpacity_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      float ret = NoesisGUI_PINVOKE.TextBoxBase_SelectionOpacity_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public ScrollBarVisibility VerticalScrollBarVisibility {
    set {
      NoesisGUI_PINVOKE.TextBoxBase_VerticalScrollBarVisibility_set(swigCPtr, (int)value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      ScrollBarVisibility ret = (ScrollBarVisibility)NoesisGUI_PINVOKE.TextBoxBase_VerticalScrollBarVisibility_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.TextBoxBase_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }


  internal new static IntPtr Extend(System.Type type) {
    IntPtr unityType = Noesis.Extend.GetPtrForType(type);
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_TextBoxBase(unityType);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return nativeType;
  }
}

}
