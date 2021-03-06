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

public class MenuItem : HeaderedItemsControl {

  internal MenuItem(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.MenuItem_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(MenuItem obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public delegate void CheckedHandler(object sender, RoutedEventArgs e);
  public event CheckedHandler Checked {
    add {
      if (!_Checked.ContainsKey(swigCPtr.Handle)) {
        _Checked.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_MenuItem_Checked(_raiseChecked, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _Checked[swigCPtr.Handle] += value;
    }
    remove {
      if (!_Checked.ContainsKey(swigCPtr.Handle)) {
        throw new System.Exception("Delegate not found");
      }

      _Checked[swigCPtr.Handle] -= value;

      if (_Checked[swigCPtr.Handle] == null) {
        NoesisGUI_PINVOKE.UnbindEvent_MenuItem_Checked(_raiseChecked, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _Checked.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseCheckedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseCheckedCallback _raiseChecked = RaiseChecked;

  [MonoPInvokeCallback(typeof(RaiseCheckedCallback))]
  private static void RaiseChecked(IntPtr cPtr, IntPtr sender, IntPtr e) {
    if (!_Checked.ContainsKey(cPtr)) {
      throw new System.Exception("Delegate not found");
    }
    if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
      _Checked.Remove(cPtr);
      return;
    }
    CheckedHandler handler = _Checked[cPtr];
    if (handler != null) {
      handler(Noesis.Extend.Unbox(Noesis.Extend.GetProxy(sender, false)), new RoutedEventArgs(e, false));
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, CheckedHandler> _Checked =
      new System.Collections.Generic.Dictionary<System.IntPtr, CheckedHandler>();


  public delegate void ClickHandler(object sender, RoutedEventArgs e);
  public event ClickHandler Click {
    add {
      if (!_Click.ContainsKey(swigCPtr.Handle)) {
        _Click.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_MenuItem_Click(_raiseClick, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _Click[swigCPtr.Handle] += value;
    }
    remove {
      if (!_Click.ContainsKey(swigCPtr.Handle)) {
        throw new System.Exception("Delegate not found");
      }

      _Click[swigCPtr.Handle] -= value;

      if (_Click[swigCPtr.Handle] == null) {
        NoesisGUI_PINVOKE.UnbindEvent_MenuItem_Click(_raiseClick, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _Click.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseClickCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseClickCallback _raiseClick = RaiseClick;

  [MonoPInvokeCallback(typeof(RaiseClickCallback))]
  private static void RaiseClick(IntPtr cPtr, IntPtr sender, IntPtr e) {
    if (!_Click.ContainsKey(cPtr)) {
      throw new System.Exception("Delegate not found");
    }
    if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
      _Click.Remove(cPtr);
      return;
    }
    ClickHandler handler = _Click[cPtr];
    if (handler != null) {
      handler(Noesis.Extend.Unbox(Noesis.Extend.GetProxy(sender, false)), new RoutedEventArgs(e, false));
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, ClickHandler> _Click =
      new System.Collections.Generic.Dictionary<System.IntPtr, ClickHandler>();


  public delegate void SubmenuClosedHandler(object sender, RoutedEventArgs e);
  public event SubmenuClosedHandler SubmenuClosed {
    add {
      if (!_SubmenuClosed.ContainsKey(swigCPtr.Handle)) {
        _SubmenuClosed.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_MenuItem_SubmenuClosed(_raiseSubmenuClosed, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _SubmenuClosed[swigCPtr.Handle] += value;
    }
    remove {
      if (!_SubmenuClosed.ContainsKey(swigCPtr.Handle)) {
        throw new System.Exception("Delegate not found");
      }

      _SubmenuClosed[swigCPtr.Handle] -= value;

      if (_SubmenuClosed[swigCPtr.Handle] == null) {
        NoesisGUI_PINVOKE.UnbindEvent_MenuItem_SubmenuClosed(_raiseSubmenuClosed, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _SubmenuClosed.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseSubmenuClosedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseSubmenuClosedCallback _raiseSubmenuClosed = RaiseSubmenuClosed;

  [MonoPInvokeCallback(typeof(RaiseSubmenuClosedCallback))]
  private static void RaiseSubmenuClosed(IntPtr cPtr, IntPtr sender, IntPtr e) {
    if (!_SubmenuClosed.ContainsKey(cPtr)) {
      throw new System.Exception("Delegate not found");
    }
    if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
      _SubmenuClosed.Remove(cPtr);
      return;
    }
    SubmenuClosedHandler handler = _SubmenuClosed[cPtr];
    if (handler != null) {
      handler(Noesis.Extend.Unbox(Noesis.Extend.GetProxy(sender, false)), new RoutedEventArgs(e, false));
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, SubmenuClosedHandler> _SubmenuClosed =
      new System.Collections.Generic.Dictionary<System.IntPtr, SubmenuClosedHandler>();


  public delegate void SubmenuOpenedHandler(object sender, RoutedEventArgs e);
  public event SubmenuOpenedHandler SubmenuOpened {
    add {
      if (!_SubmenuOpened.ContainsKey(swigCPtr.Handle)) {
        _SubmenuOpened.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_MenuItem_SubmenuOpened(_raiseSubmenuOpened, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _SubmenuOpened[swigCPtr.Handle] += value;
    }
    remove {
      if (!_SubmenuOpened.ContainsKey(swigCPtr.Handle)) {
        throw new System.Exception("Delegate not found");
      }

      _SubmenuOpened[swigCPtr.Handle] -= value;

      if (_SubmenuOpened[swigCPtr.Handle] == null) {
        NoesisGUI_PINVOKE.UnbindEvent_MenuItem_SubmenuOpened(_raiseSubmenuOpened, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _SubmenuOpened.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseSubmenuOpenedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseSubmenuOpenedCallback _raiseSubmenuOpened = RaiseSubmenuOpened;

  [MonoPInvokeCallback(typeof(RaiseSubmenuOpenedCallback))]
  private static void RaiseSubmenuOpened(IntPtr cPtr, IntPtr sender, IntPtr e) {
    if (!_SubmenuOpened.ContainsKey(cPtr)) {
      throw new System.Exception("Delegate not found");
    }
    if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
      _SubmenuOpened.Remove(cPtr);
      return;
    }
    SubmenuOpenedHandler handler = _SubmenuOpened[cPtr];
    if (handler != null) {
      handler(Noesis.Extend.Unbox(Noesis.Extend.GetProxy(sender, false)), new RoutedEventArgs(e, false));
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, SubmenuOpenedHandler> _SubmenuOpened =
      new System.Collections.Generic.Dictionary<System.IntPtr, SubmenuOpenedHandler>();


  public delegate void UncheckedHandler(object sender, RoutedEventArgs e);
  public event UncheckedHandler Unchecked {
    add {
      if (!_Unchecked.ContainsKey(swigCPtr.Handle)) {
        _Unchecked.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_MenuItem_Unchecked(_raiseUnchecked, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif
      }

      _Unchecked[swigCPtr.Handle] += value;
    }
    remove {
      if (!_Unchecked.ContainsKey(swigCPtr.Handle)) {
        throw new System.Exception("Delegate not found");
      }

      _Unchecked[swigCPtr.Handle] -= value;

      if (_Unchecked[swigCPtr.Handle] == null) {
        NoesisGUI_PINVOKE.UnbindEvent_MenuItem_Unchecked(_raiseUnchecked, swigCPtr.Handle);
        #if UNITY_EDITOR
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
        #endif

        _Unchecked.Remove(swigCPtr.Handle);
      }
    }
  }

  internal delegate void RaiseUncheckedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseUncheckedCallback _raiseUnchecked = RaiseUnchecked;

  [MonoPInvokeCallback(typeof(RaiseUncheckedCallback))]
  private static void RaiseUnchecked(IntPtr cPtr, IntPtr sender, IntPtr e) {
    if (!_Unchecked.ContainsKey(cPtr)) {
      throw new System.Exception("Delegate not found");
    }
    if (sender == System.IntPtr.Zero && e == System.IntPtr.Zero) {
      _Unchecked.Remove(cPtr);
      return;
    }
    UncheckedHandler handler = _Unchecked[cPtr];
    if (handler != null) {
      handler(Noesis.Extend.Unbox(Noesis.Extend.GetProxy(sender, false)), new RoutedEventArgs(e, false));
    }
  }

  static System.Collections.Generic.Dictionary<System.IntPtr, UncheckedHandler> _Unchecked =
      new System.Collections.Generic.Dictionary<System.IntPtr, UncheckedHandler>();

  public System.Windows.Input.ICommand Command {
    get {
      return GetCommandHelper() as System.Windows.Input.ICommand;
    }
    set {
      SetCommandHelper(value);
    }
  }

  public MenuItem() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(MenuItem)) {
      return NoesisGUI_PINVOKE.new_MenuItem();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public UIElement GetCommandTarget() {
    IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_GetCommandTarget(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return Noesis.Extend.GetProxy(cPtr, false) as UIElement;
  }

  public void SetCommandTarget(UIElement target) {
    NoesisGUI_PINVOKE.MenuItem_SetCommandTarget(swigCPtr, UIElement.getCPtr(target));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public static DependencyProperty CommandParameterProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_CommandParameterProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty CommandProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_CommandProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty CommandTargetProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_CommandTargetProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty IconProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_IconProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty InputGestureTextProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_InputGestureTextProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty IsCheckableProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_IsCheckableProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty IsCheckedProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_IsCheckedProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty IsHighlightedProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_IsHighlightedProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty IsPressedProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_IsPressedProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty IsSubmenuOpenProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_IsSubmenuOpenProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty RoleProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_RoleProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty StaysOpenOnClickProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_StaysOpenOnClickProperty_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public static DependencyProperty SeparatorStyleKey {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_SeparatorStyleKey_get();
      DependencyProperty ret = (cPtr == IntPtr.Zero) ? null : new DependencyProperty(cPtr, false);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public object CommandParameter {
    set {
      NoesisGUI_PINVOKE.MenuItem_CommandParameter_set(swigCPtr, Noesis.Extend.GetInstanceHandle(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }

    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_CommandParameter_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.Unbox(Noesis.Extend.GetProxy(cPtr, false));
    }

  }

  public object Icon {
    set {
      NoesisGUI_PINVOKE.MenuItem_Icon_set(swigCPtr, Noesis.Extend.GetInstanceHandle(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }

    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_Icon_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.Unbox(Noesis.Extend.GetProxy(cPtr, false));
    }

  }

  public string InputGestureText {
  set {
    NoesisGUI_PINVOKE.MenuItem_InputGestureText_set(swigCPtr, value != null ? value : string.Empty);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
  }

  get {
    string ret = NoesisGUI_PINVOKE.MenuItem_InputGestureText_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    return ret;
  }

  }

  public bool IsCheckable {
    set {
      NoesisGUI_PINVOKE.MenuItem_IsCheckable_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.MenuItem_IsCheckable_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool IsChecked {
    set {
      NoesisGUI_PINVOKE.MenuItem_IsChecked_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.MenuItem_IsChecked_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool IsHighlighted {
    get {
      bool ret = NoesisGUI_PINVOKE.MenuItem_IsHighlighted_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool IsPressed {
    get {
      bool ret = NoesisGUI_PINVOKE.MenuItem_IsPressed_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool IsSubmenuOpen {
    set {
      NoesisGUI_PINVOKE.MenuItem_IsSubmenuOpen_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.MenuItem_IsSubmenuOpen_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public MenuItemRole Role {
    get {
      MenuItemRole ret = (MenuItemRole)NoesisGUI_PINVOKE.MenuItem_Role_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  public bool StaysOpenOnClick {
    set {
      NoesisGUI_PINVOKE.MenuItem_StaysOpenOnClick_set(swigCPtr, value);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.MenuItem_StaysOpenOnClick_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  private object GetCommandHelper() {
    IntPtr cPtr = NoesisGUI_PINVOKE.MenuItem_GetCommandHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return Noesis.Extend.Unbox(Noesis.Extend.GetProxy(cPtr, false));
}

  private void SetCommandHelper(object command) {
    NoesisGUI_PINVOKE.MenuItem_SetCommandHelper(swigCPtr, Noesis.Extend.GetInstanceHandle(command));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.MenuItem_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }


  internal new static IntPtr Extend(System.Type type) {
    IntPtr unityType = Noesis.Extend.GetPtrForType(type);
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_MenuItem(unityType);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return nativeType;
  }
}

}

