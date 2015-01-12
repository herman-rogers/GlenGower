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

[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
internal struct NullableBool {

  [MarshalAs(UnmanagedType.U1)]
  private bool _hasValue;
  private bool _value;

  public bool HasValue { get { return this._hasValue; } }

  public bool Value {
    get {
      if (!HasValue) {
        throw new InvalidOperationException("Nullable does not have a value");
      }
      return this._value;
    }
  }

  public NullableBool(bool v) {
    this._hasValue = true;
    this._value = v;
  }

  public static explicit operator bool(NullableBool n) {
    if (!n.HasValue) {
      throw new InvalidOperationException("Nullable does not have a value");
    }
    return n.Value;
  }

  public static implicit operator NullableBool(bool v) {
    return new NullableBool(v);
  }

  public static implicit operator System.Nullable<bool>(NullableBool n) {
    return n.HasValue ? new System.Nullable<bool>(n.Value) : new System.Nullable<bool>();
  }

  public static implicit operator NullableBool(System.Nullable<bool> n) {
    return n.HasValue ? new NullableBool(n.Value) : new NullableBool();
  }

  public static bool operator==(NullableBool n, bool v) {
    return n.HasValue && n.Value == v;
  }

  public static bool operator!=(NullableBool n, bool v) {
    return !(n == v);
  }

  public static bool operator==(bool v, NullableBool n) {
    return n == v;
  }
  
  public static bool operator!=(bool v, NullableBool n) {
    return n != v;
  }

  public static bool operator==(NullableBool n0, NullableBool n1) {
    return n0.HasValue && n1.HasValue ? n0.Value == n1.Value : n0.HasValue == n1.HasValue;
  }

  public static bool operator!=(NullableBool n0, NullableBool n1) {
    return !(n0 == n1);
  }

  public override bool Equals(System.Object obj) {
    return obj is NullableBool && this == (NullableBool)obj;
  }

  public bool Equals(NullableBool n) {
    return this == n;
  }

  public override int GetHashCode() {
    return HasValue ? Value.GetHashCode() : 0;
  }

}

}
