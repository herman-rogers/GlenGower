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

public class Collection : BaseComponent, System.Collections.IEnumerable {

  internal Collection(IntPtr cPtr, bool cMemoryOwn) : base(NoesisGUI_PINVOKE.Collection_SWIGUpcast(cPtr), cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Collection obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  private struct CollectionEnumerator : System.Collections.IEnumerator {
    object System.Collections.IEnumerator.Current {
      get { return Current; }
    }
    public object Current {
      get { return this._collection[(uint)this._index]; }
    }
    public bool MoveNext() {
      if (++this._index >= (int)this._collection.Count) {
        return false;
      }
      return true;
    }
    public void Reset() {
      this._index = -1;
    }
    public CollectionEnumerator(Collection c) {
      this._collection = c;
      this._index = -1;
    }
    private Collection _collection;
    private int _index;
  }

  public System.Collections.IEnumerator GetEnumerator() {
    return new CollectionEnumerator(this);
  }

  public object this[uint index] {
    get {
      if (index >= Count) {
        throw new ArgumentOutOfRangeException("index");
      }
      return Get(index);
    }
    set {
      if (index >= Count) {
        throw new ArgumentOutOfRangeException("index");
      }
      Set(index, value);
    }
  }

  public Collection() {
  }

  protected override System.IntPtr CreateCPtr(System.Type type) {
    if (type == typeof(Collection)) {
      return NoesisGUI_PINVOKE.new_Collection__SWIG_0();
    }
    else {
      return base.CreateCPtr(type);
    }
  }

  public object Get(uint index) {
    IntPtr cPtr = NoesisGUI_PINVOKE.Collection_Get(swigCPtr, index);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return Noesis.Extend.Unbox(Noesis.Extend.GetProxy(cPtr, false));
}

  public void Set(uint index, object item) {
    NoesisGUI_PINVOKE.Collection_Set(swigCPtr, index, Noesis.Extend.GetInstanceHandle(item));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public uint Add(object item) {
    uint ret = NoesisGUI_PINVOKE.Collection_Add(swigCPtr, Noesis.Extend.GetInstanceHandle(item));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public void Clear() {
    NoesisGUI_PINVOKE.Collection_Clear(swigCPtr);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public bool Contains(object item) {
    bool ret = NoesisGUI_PINVOKE.Collection_Contains(swigCPtr, Noesis.Extend.GetInstanceHandle(item));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public int IndexOf(object item) {
    int ret = NoesisGUI_PINVOKE.Collection_IndexOf(swigCPtr, Noesis.Extend.GetInstanceHandle(item));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

  public void Insert(uint index, object item) {
    NoesisGUI_PINVOKE.Collection_Insert(swigCPtr, index, Noesis.Extend.GetInstanceHandle(item));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void Remove(object item) {
    NoesisGUI_PINVOKE.Collection_Remove(swigCPtr, Noesis.Extend.GetInstanceHandle(item));
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public void RemoveAt(uint index) {
    NoesisGUI_PINVOKE.Collection_RemoveAt(swigCPtr, index);
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
  }

  public uint Count {
    get {
      uint ret = NoesisGUI_PINVOKE.Collection_Count_get(swigCPtr);
      #if UNITY_EDITOR || NOESIS_API
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      #endif
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Collection_GetStaticType();
    #if UNITY_EDITOR || NOESIS_API
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    #endif
    return ret;
  }

}

}
