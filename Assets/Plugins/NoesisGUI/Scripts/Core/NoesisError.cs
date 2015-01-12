using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    public class Error
    {
        public static void RegisterCallback()
        {
            Noesis_RegisterErrorCallback(_errorCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void Check()
        {
            if (_pendingError.Length > 0)
            {
                string message = _pendingError;
                _pendingError = "";

                throw new Exception(message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void ErrorCallback(string desc);
        private static ErrorCallback _errorCallback = SetPendingError;

        [MonoPInvokeCallback(typeof(ErrorCallback))]
        private static void SetPendingError(string desc)
        {
            // Do not overwrite if there is already an exception pending
            if (_pendingError.Length == 0)
            {
                _pendingError = desc;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static string _pendingError = "";

    #if UNITY_EDITOR
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void RegisterFunctions(Library lib)
        {
            _registerErrorCallback = lib.Find<RegisterErrorCallbackDelegate>("Noesis_RegisterErrorCallback");
            RegisterCallback();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void UnregisterFunctions()
        {
            _registerErrorCallback = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void RegisterErrorCallbackDelegate(ErrorCallback errorCallback);
        static RegisterErrorCallbackDelegate _registerErrorCallback;
        static void Noesis_RegisterErrorCallback(ErrorCallback errorCallback)
        {
            _registerErrorCallback(errorCallback);
        }
    #else

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_RegisterErrorCallback")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_RegisterErrorCallback")]
        #endif
        private static extern void Noesis_RegisterErrorCallback(ErrorCallback errorCallback);

    #endif
    }
}

