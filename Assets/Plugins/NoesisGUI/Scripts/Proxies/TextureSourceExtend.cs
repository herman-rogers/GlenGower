using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Noesis
{

    public partial class TextureSource
    {
#if !NOESIS_API
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public TextureSource(UnityEngine.Texture texture)
            : this(Create(texture, 1), true)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public TextureSource(UnityEngine.Texture2D texture)
            : this(Create(texture, texture.mipmapCount), true)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static IntPtr Create(UnityEngine.Texture texture, int mipmapCount)
        {
            if (texture == null)
            {
                throw new System.Exception("Can't create TextureSource, Unity texture is null");
            }

            IntPtr nativeTexturePtr = GetNativeTexturePointer(texture, true);
            IntPtr cPtr = Noesis_CreateTextureSource(nativeTexturePtr, texture.width, texture.height,
                mipmapCount);

            // Register Texture for updating native pointer after LostDevice/Reset
            Register(cPtr, texture);

            return cPtr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static Dictionary<IntPtr, UnityEngine.Texture> _textures =
            new Dictionary<IntPtr, UnityEngine.Texture>();

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static void Register(IntPtr cPtr, UnityEngine.Texture texture)
        {
            _textures.Add(cPtr, texture);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void UnregisterTextureSourceCallback(IntPtr cPtr);
        private static UnregisterTextureSourceCallback _unregister = Unregister;
        [MonoPInvokeCallback(typeof(UnregisterTextureSourceCallback))]
        private static void Unregister(IntPtr cPtr)
        {
            _textures.Remove(cPtr);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void UpdateTextureSourcesCallback();
        private static UpdateTextureSourcesCallback _update = Update;
        [MonoPInvokeCallback(typeof(UpdateTextureSourcesCallback))]
        private static void Update()
        {
            foreach (KeyValuePair<IntPtr, UnityEngine.Texture> kv in _textures)
            {
                // Check that texture was not destroyed
                if (kv.Value != null)
                {
                    IntPtr nativeTexturePtr = GetNativeTexturePointer(kv.Value, false);
                    int width = kv.Value.width;
                    int height = kv.Value.height;
                    int mipmapCount = GetMipMapCount(kv.Value);

                    Noesis_UpdateTextureSource(kv.Key, nativeTexturePtr, width, height, mipmapCount);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static IntPtr GetNativeTexturePointer(UnityEngine.Texture tex, bool throwError)
        {
            IntPtr nativeTexturePtr = tex.GetNativeTexturePtr();
            if (nativeTexturePtr == IntPtr.Zero)
            {
                UnityEngine.RenderTexture renderTexture = tex as UnityEngine.RenderTexture;
                if (renderTexture != null)
                {
                    renderTexture.Create();
                }

                nativeTexturePtr = tex.GetNativeTexturePtr();
                if (nativeTexturePtr == IntPtr.Zero && throwError)
                {
                    throw new System.Exception("Can't create TextureSource, texture native pointer is null");
                }
            }

            return nativeTexturePtr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static int GetMipMapCount(UnityEngine.Texture tex)
        {
            UnityEngine.Texture2D tex2D = tex as UnityEngine.Texture2D;
            if (tex2D != null)
            {
                return tex2D.mipmapCount;
            }
            else
            {
                return 1;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void RegisterCallbacks()
        {
            Noesis_RegisterTextureSourceCallbacks(
                Noesis.TextureSource._unregister,
                Noesis.TextureSource._update);
        }

    #if UNITY_EDITOR

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public new static void RegisterFunctions(Library lib)
        {
            _CreateTextureSource = lib.Find<CreateTextureSourceDelegate>("Noesis_CreateTextureSource");
            _UpdateTextureSource = lib.Find<UpdateTextureSourceDelegate>("Noesis_UpdateTextureSource");
            _RegisterTextureSourceCallbacks = lib.Find<RegisterTextureSourceCallbacksDelegate>(
                "Noesis_RegisterTextureSourceCallbacks");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public new static void UnregisterFunctions()
        {
            _CreateTextureSource = null;
            _UpdateTextureSource = null;
            _RegisterTextureSourceCallbacks = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate IntPtr CreateTextureSourceDelegate(IntPtr texture, int width, int height, int numMipMaps);
        static CreateTextureSourceDelegate _CreateTextureSource;
        static IntPtr Noesis_CreateTextureSource(IntPtr texture, int width, int height, int numMipMaps)
        {
            IntPtr ret = _CreateTextureSource(texture, width, height, numMipMaps);
            Error.Check();
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void UpdateTextureSourceDelegate(IntPtr cPtr, IntPtr texture, int width, int height, int numMipMaps);
        static UpdateTextureSourceDelegate _UpdateTextureSource;
        static void Noesis_UpdateTextureSource(IntPtr cPtr, IntPtr texture, int width, int height, int numMipMaps)
        {
            _UpdateTextureSource(cPtr, texture, width, height, numMipMaps);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void RegisterTextureSourceCallbacksDelegate(
            UnregisterTextureSourceCallback unregisterTextureSourceCallback,
            UpdateTextureSourcesCallback updateTextureSourcesCallback);
        static RegisterTextureSourceCallbacksDelegate _RegisterTextureSourceCallbacks = null;
        static void Noesis_RegisterTextureSourceCallbacks(
            UnregisterTextureSourceCallback unregisterTextureSourceCallback,
            UpdateTextureSourcesCallback updateTextureSourcesCallback)
        {
            _RegisterTextureSourceCallbacks(
                unregisterTextureSourceCallback,
                updateTextureSourcesCallback);
        }

    #else

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateTextureSource")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateTextureSource")]
        #endif
        static extern IntPtr Noesis_CreateTextureSource(IntPtr texture, int width, int height, int numMipMaps);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_UpdateTextureSource")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_UpdateTextureSource")]
        #endif
        static extern void Noesis_UpdateTextureSource(IntPtr cPtr, IntPtr texture, int width, int height, int numMipMaps);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_RegisterTextureSourceCallbacks")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_RegisterTextureSourceCallbacks")]
        #endif
        static extern void Noesis_RegisterTextureSourceCallbacks(
            UnregisterTextureSourceCallback unregisterTextureSourceCallback,
            UpdateTextureSourcesCallback updateTextureSourcesCallback);

    #endif

#endif
    }

}