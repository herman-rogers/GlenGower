#if NETFX_CORE

namespace System.Runtime.InteropServices
{
    using System;

    // There is no HandleRef in WinRT Api. This class emulates it although the solution is not
    // perfect because a reference to the wrapper is not maintained and in theory the GC could
    // collect it while inside the native call.
    // SWIG should solve this in the future to be compatible with WinRT
    [StructLayout(LayoutKind.Sequential)]
    public struct HandleRef
    {
        internal IntPtr _handle;

        public HandleRef(Object wrapper, IntPtr handle)
        {
            _handle = handle;
        }

        public IntPtr Handle
        {
            get
            {
                return _handle;
            }
        }
    }
}

namespace System.Security
{
    // There is no [SuppressUnmanagedCodeSecurity] attribute in WinRT
    [AttributeUsageAttribute(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Interface|
        AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
    public sealed class SuppressUnmanagedCodeSecurityAttribute: Attribute {}
}

namespace System
{
    // SWIG uses (incorrectly) System.SystemException instead of System.Exception and there is no
    // SystemException class in WinRT
    public class SystemException: Exception
    {
        public SystemException(string message, Exception innerException) : base(message, innerException) { }
    }
}

namespace Noesis
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections;
    using System.Collections.Generic;

    sealed class AppDomain
    {
        public static AppDomain CurrentDomain { get; private set; }

        static AppDomain()
        {
            CurrentDomain = new AppDomain();
        }

        public Assembly[] GetAssemblies()
        {
            return GetAssemblyListAsync().Result.ToArray();
        }

        private async System.Threading.Tasks.Task<IEnumerable<Assembly>> GetAssemblyListAsync()
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var assemblies = new List<Assembly>();

            foreach (var file in await folder.GetFilesAsync())
            {
                if (file.FileType == ".dll" || file.FileType == ".exe")
                {
                    try
                    {
                        // Loading UnityPlayer.dll causes an internal error in the .NET Runtime
                        var assemblyName = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                        if (assemblyName != "UnityPlayer" && assemblyName != "BridgeInterface")
                        {
                            var assemblyRef = new AssemblyName() { Name = assemblyName };
                            assemblies.Add(Assembly.Load(assemblyRef));
                        }
                    }
                    catch { }
                }
            }

            return assemblies;
        }
    }
}
#endif

#if !NETFX_CORE
namespace Noesis
{
    using System;
    using System.Runtime;

    public static class ExtensionMethods
    {
        // In .NET 4.5 Type and TypeInfo are different classes. This extension method adapts the
        // version of Mono used in Unity to that API. That way we can mantain the same code without
        // using preprocessor
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }
    }

    public static class Marshal
    {
        public static void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld)
        {
            System.Runtime.InteropServices.Marshal.StructureToPtr(structure, ptr, fDeleteOld);
        }

        public static T PtrToStructure<T>(IntPtr ptr)
        {
            return (T)System.Runtime.InteropServices.Marshal.PtrToStructure(ptr, typeof(T));
        }

        public static IntPtr StringToHGlobalAnsi(string s)
        {
            return System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(s);
        }

        public static string PtrToStringAnsi(IntPtr ptr)
        {
            return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
        }

        public static void FreeHGlobal(IntPtr hglobal)
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(hglobal);
        }

        public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
        {
            return System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer(ptr, t);
        }

        public static int GetLastWin32Error()
        {
            return System.Runtime.InteropServices.Marshal.GetLastWin32Error();
        }
    }
}
#endif


