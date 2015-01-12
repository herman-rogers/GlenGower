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

public class SerializableComponent : BaseComponent {

  internal SerializableComponent(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.SerializableComponent_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(SerializableComponent obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public SerializableComponent() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(SerializableComponent)) {
      return NoesisGUI_PINVOKE.new_SerializableComponent();
    }
    else {
      return base.CreateCPtr(type);
    }
  }


  internal static IntPtr Extend(System.Type type) {
    IntPtr unityType = Noesis.Extend.GetPtrForType(type);
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_SerializableComponent(unityType);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return nativeType;
  }
}

}
