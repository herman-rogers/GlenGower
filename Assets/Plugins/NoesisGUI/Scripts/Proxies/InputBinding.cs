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

public class InputBinding : BaseComponent {

  internal InputBinding(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.InputBinding_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(InputBinding obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public System.Windows.Input.ICommand Command {
    get {
      return GetCommandHelper() as System.Windows.Input.ICommand;
    }
    set {
      SetCommandHelper(value);
    }
  }

  public InputBinding() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(InputBinding)) {
      return NoesisGUI_PINVOKE.new_InputBinding__SWIG_0();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public object CommandParameter {
    set {
      NoesisGUI_PINVOKE.InputBinding_CommandParameter_set(swigCPtr, Noesis.Extend.GetInstanceHandle(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    }

    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_CommandParameter_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.Unbox(Noesis.Extend.GetProxy(cPtr, false));
    }

  }

  public UIElement CommandTarget {
    set {
      NoesisGUI_PINVOKE.InputBinding_CommandTarget_set(swigCPtr, UIElement.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_CommandTarget_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as UIElement;
    }
  
  }

  public InputGesture Gesture {
    set {
      NoesisGUI_PINVOKE.InputBinding_Gesture_set(swigCPtr, InputGesture.getCPtr(value));
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_Gesture_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return Noesis.Extend.GetProxy(cPtr, false) as InputGesture;
    }
  
  }

  private object GetCommandHelper() {
    IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_GetCommandHelper(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return Noesis.Extend.Unbox(Noesis.Extend.GetProxy(cPtr, false));
}

  private void SetCommandHelper(object command) {
    NoesisGUI_PINVOKE.InputBinding_SetCommandHelper(swigCPtr, Noesis.Extend.GetInstanceHandle(command));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.InputBinding_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}
