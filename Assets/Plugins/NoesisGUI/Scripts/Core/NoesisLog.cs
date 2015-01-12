using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    public class Log
    {
        /// Logs an info message in NoesisConsole
        public static void Info(string text)
        {
            Noesis_LogInfo(text);
        }

        /// Logs a warning message in NoesisConsole
        public static void Warning(string text)
        {
            Noesis_LogWarning(text);
        }

    #if UNITY_EDITOR
        public static void RegisterFunctions(Library lib)
        {
            _noesisLogInfo = lib.Find<NoesisLogInfoDelegate>("Noesis_LogInfo");
            _noesisLogWarning = lib.Find<NoesisLogWarningDelegate>("Noesis_LogWarning");
        }

        public static void UnregisterFunctions()
        {
            _noesisLogInfo = null;
            _noesisLogWarning = null;
        }

        delegate void NoesisLogInfoDelegate(string text);
        static NoesisLogInfoDelegate _noesisLogInfo = null;
        public static void Noesis_LogInfo(string text)
        {
            _noesisLogInfo(text);
        }


        delegate void NoesisLogWarningDelegate(string text);
        static NoesisLogWarningDelegate _noesisLogWarning = null;
        public static void Noesis_LogWarning(string text)
        {
            _noesisLogWarning(text);
        }
    #else

        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_LogInfo")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_LogInfo")]
        #endif
        static extern void Noesis_LogInfo(string message);

        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_LogWarning")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_LogWarning")]
        #endif
        static extern void Noesis_LogWarning(string message);

    #endif
    }
}
