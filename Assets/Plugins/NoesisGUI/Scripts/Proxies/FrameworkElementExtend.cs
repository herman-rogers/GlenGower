using System;
using System.Runtime.InteropServices;

namespace Noesis
{

    public partial class FrameworkElement
    {
        public object FindResource(object key)
        {
            if (key is string)
            {
                return FindStringResource(key as string);
            }

            if (key is System.Type)
            {
                return FindTypeResource(key as Type);
            }

            throw new Exception("Only String or Type resource keys supported.");
        }

        private object FindStringResource(string resourceKeyString)
        {
            IntPtr stringPtr = new Noesis.Extend.StringMarshal(resourceKeyString);
            IntPtr cPtr = NoesisGUI_PINVOKE.FrameworkElement_FindStringResource(swigCPtr, stringPtr);
            #if UNITY_EDITOR || NOESIS_API
            if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
            #endif
            return Noesis.Extend.GetProxy(cPtr, false);
        }

        private object FindTypeResource(System.Type resourceKeyType)
        {
            IntPtr nativeType = Noesis.Extend.GetNativeType(resourceKeyType);
            IntPtr cPtr = NoesisGUI_PINVOKE.FrameworkElement_FindTypeResource(swigCPtr, nativeType);
            #if UNITY_EDITOR || NOESIS_API
            if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
            #endif
            return Noesis.Extend.GetProxy(cPtr, false);
        }

        public object TryFindResource(object key)
        {
            if (key is string)
            {
                return TryFindStringResource(key as string);
            }

            if (key is System.Type)
            {
                return TryFindTypeResource(key as Type);
            }

            throw new Exception("Only String or Type resource keys supported.");
        }

        private object TryFindStringResource(string resourceKeyString)
        {
            IntPtr stringPtr = new Noesis.Extend.StringMarshal(resourceKeyString);
            IntPtr cPtr = NoesisGUI_PINVOKE.FrameworkElement_TryFindStringResource(swigCPtr, stringPtr);
            #if UNITY_EDITOR || NOESIS_API
            if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
            #endif
            return Noesis.Extend.GetProxy(cPtr, false);
        }

        private object TryFindTypeResource(System.Type resourceKeyType)
        {
            IntPtr nativeType = Noesis.Extend.GetNativeType(resourceKeyType);
            IntPtr cPtr = NoesisGUI_PINVOKE.FrameworkElement_TryFindTypeResource(swigCPtr, nativeType);
            #if UNITY_EDITOR || NOESIS_API
            if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
            #endif
            return Noesis.Extend.GetProxy(cPtr, false);
        }
    }

}