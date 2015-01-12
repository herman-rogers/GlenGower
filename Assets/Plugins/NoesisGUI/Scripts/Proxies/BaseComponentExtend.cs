using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Noesis
{

    public partial class BaseComponent
    {
        protected BaseComponent()
        {
            Type type = this.GetType();
            if (Noesis.Extend.NeedsCreateCPtr(type))
            {
                Init(CreateCPtr(type), true);
            }
            else
            {
                Init(Noesis.Extend.GetCPtr(this, type), false);
            }
        }

        private void Init(System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCPtr = new HandleRef(this, cPtr);

            if (cPtr != IntPtr.Zero && !cMemoryOwn)
            {
                AddReference();
            }
        }

        protected virtual System.IntPtr CreateCPtr(System.Type type)
        {
            return Noesis.Extend.New(type, this);
        }

        public static bool operator ==(BaseComponent a, BaseComponent b)
        {
            // If both are null, or both are the same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            // Return true if wrapped c++ objects match:
            return a.swigCPtr.Handle == b.swigCPtr.Handle;
        }

        public static bool operator !=(BaseComponent a, BaseComponent b)
        {
            return !(a == b);
        }

        public override bool Equals(object o)
        {
            return this == o as BaseComponent;
        }

        public override int GetHashCode()
        {
            return swigCPtr.Handle.GetHashCode();
        }
    }

}
