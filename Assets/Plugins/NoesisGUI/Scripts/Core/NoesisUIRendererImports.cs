using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    /////////////////////////////////////////////////////////////////////////////////////
    /// Imports Renderer functions from Noesis library
    /////////////////////////////////////////////////////////////////////////////////////
    internal partial class UIRenderer
    {
    #if UNITY_EDITOR

        public static void RegisterFunctions(Library lib)
        {
            _notifyDestroyRenderer = lib.Find<NotifyDestroyRendererDelegate>("Noesis_NotifyDestroyRenderer");
            _createRenderer = lib.Find<CreateRendererDelegate>("Noesis_CreateRenderer");
            _setRendererSurfaceSize = lib.Find<SetRendererSurfaceSizeDelegate>("Noesis_RendererSurfaceSize");
            _setRendererAntialiasingMode = lib.Find<SetRendererAntialiasingModeDelegate>("Noesis_RendererAntialiasingMode");
            _setRendererTessMode = lib.Find<SetRendererTessModeDelegate>("Noesis_RendererTessMode");
            _setRendererTessQuality = lib.Find<SetRendererTessQualityDelegate>("Noesis_RendererTessQuality");
            _setRendererFlags = lib.Find<SetRendererFlagsDelegate>("Noesis_RendererFlags");
            _updateRenderer = lib.Find<UpdateRendererDelegate>("Noesis_UpdateRenderer");
            _waitUpdateRenderer = lib.Find<WaitUpdateRendererDelegate>("Noesis_WaitUpdateRenderer");
            _hitTest = lib.Find<HitTestDelegate>("Noesis_HitTest");
            _mouseButtonDown = lib.Find<MouseButtonDownDelegate>("Noesis_MouseButtonDown");
            _mouseButtonUp = lib.Find<MouseButtonUpDelegate>("Noesis_MouseButtonUp");
            _mouseDoubleClick = lib.Find<MouseDoubleClickDelegate>("Noesis_MouseDoubleClick");
            _mouseMove = lib.Find<MouseMoveDelegate>("Noesis_MouseMove");
            _mouseWheel = lib.Find<MouseWheelDelegate>("Noesis_MouseWheel");
            _touchDown = lib.Find<TouchDownDelegate>("Noesis_TouchDown");
            _touchMove = lib.Find<TouchMoveDelegate>("Noesis_TouchMove");
            _touchUp = lib.Find<TouchUpDelegate>("Noesis_TouchUp");
            _keyDown = lib.Find<KeyDownDelegate>("Noesis_KeyDown");
            _keyUp = lib.Find<KeyUpDelegate>("Noesis_KeyUp");
            _char = lib.Find<CharDelegate>("Noesis_Char");
            _activate = lib.Find<ActivateDelegate>("Noesis_Activate");
            _deactivate = lib.Find<DeactivateDelegate>("Noesis_Deactivate");

            TextureSource.RegisterFunctions(lib);
        }

        public static void UnregisterFunctions()
        {
            _notifyDestroyRenderer = null;
            _createRenderer = null;
            _setRendererSurfaceSize = null;
            _setRendererAntialiasingMode = null;
            _setRendererTessMode = null;
            _setRendererTessQuality = null;
            _setRendererFlags = null;
            _updateRenderer = null;
            _waitUpdateRenderer = null;
            _hitTest = null;
            _mouseButtonDown = null;
            _mouseButtonUp = null;
            _mouseDoubleClick = null;
            _mouseMove = null;
            _mouseWheel = null;
            _keyDown = null;
            _keyUp = null;
            _char = null;
            _activate = null;
            _deactivate = null;

            TextureSource.UnregisterFunctions();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void NotifyDestroyRendererDelegate(int rendererId);
        static NotifyDestroyRendererDelegate _notifyDestroyRenderer;
        static void Noesis_NotifyDestroyRenderer(int rendererId)
        {
            _notifyDestroyRenderer(rendererId);
            Error.Check();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate int CreateRendererDelegate(IntPtr root);
        static CreateRendererDelegate _createRenderer;
        static int Noesis_CreateRenderer(IntPtr root)
        {
            int ret = _createRenderer(root);
            Error.Check();

            return ret;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void SetRendererSurfaceSizeDelegate(int rendererId, int width, int height,
            float offscreenWidth, float offscreenHeight, int msaa);
        static SetRendererSurfaceSizeDelegate _setRendererSurfaceSize;
        static void Noesis_RendererSurfaceSize(int rendererId, int width, int height,
            float offscreenWidth, float offscreenHeight, int msaa)
        {
            _setRendererSurfaceSize(rendererId, width, height, offscreenWidth, offscreenHeight, msaa);
            Error.Check();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void SetRendererAntialiasingModeDelegate(int rendererId, int mode);
        static SetRendererAntialiasingModeDelegate _setRendererAntialiasingMode;
        static void Noesis_RendererAntialiasingMode(int rendererId, int mode)
        {
            _setRendererAntialiasingMode(rendererId, mode);
            Error.Check();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void SetRendererTessModeDelegate(int rendererId, int mode);
        static SetRendererTessModeDelegate _setRendererTessMode;
        static void Noesis_RendererTessMode(int rendererId, int mode)
        {
            _setRendererTessMode(rendererId, mode);
            Error.Check();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void SetRendererTessQualityDelegate(int rendererId, int quality);
        static SetRendererTessQualityDelegate _setRendererTessQuality;
        static void Noesis_RendererTessQuality(int rendererId, int quality)
        {
            _setRendererTessQuality(rendererId, quality);
            Error.Check();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void SetRendererFlagsDelegate(int rendererId, int flags);
        static SetRendererFlagsDelegate _setRendererFlags;
        static void Noesis_RendererFlags(int rendererId, int flags)
        {
            _setRendererFlags(rendererId, flags);
            Error.Check();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate bool UpdateRendererDelegate(int rendererId, double timeInSeconds);
        static UpdateRendererDelegate _updateRenderer;
        static bool Noesis_UpdateRenderer(int rendererId, double timeInSeconds)
        {
            bool ret = _updateRenderer(rendererId, timeInSeconds);
            Error.Check();
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void WaitUpdateRendererDelegate(int rendererId);
        static WaitUpdateRendererDelegate _waitUpdateRenderer;
        static void Noesis_WaitUpdateRenderer(int rendererId)
        {
            _waitUpdateRenderer(rendererId);
            Error.Check();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate bool HitTestDelegate(IntPtr root, float x, float y);
        static HitTestDelegate _hitTest;
        static bool Noesis_HitTest(IntPtr root, float x, float y)
        {
            bool hit = _hitTest(root, x, y);
            Error.Check();

            return hit;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void MouseButtonDownDelegate(int rendererId, float x, float y, int button);
        static MouseButtonDownDelegate _mouseButtonDown;
        static void Noesis_MouseButtonDown(int rendererId, float x, float y, int button)
        {
            _mouseButtonDown(rendererId, x, y, button);
            Error.Check();
        }
    
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void MouseButtonUpDelegate(int rendererId, float x, float y, int button);
        static MouseButtonUpDelegate _mouseButtonUp;
        static void Noesis_MouseButtonUp(int rendererId, float x, float y, int button)
        {
            _mouseButtonUp(rendererId, x, y, button);
            Error.Check();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void MouseDoubleClickDelegate(int rendererId, float x, float y, int button);
        static MouseDoubleClickDelegate _mouseDoubleClick;
        static void Noesis_MouseDoubleClick(int rendererId, float x, float y, int button)
        {
            _mouseDoubleClick(rendererId, x, y, button);
            Error.Check();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void MouseMoveDelegate(int rendererId, float x, float y);
        static MouseMoveDelegate _mouseMove;
        static void Noesis_MouseMove(int rendererId, float x, float y)
        {
            _mouseMove(rendererId, x, y);
            Error.Check();
        }
    
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void MouseWheelDelegate(int rendererId, float x, float y, int wheelRotation);
        static MouseWheelDelegate _mouseWheel;
        static void Noesis_MouseWheel(int rendererId, float x, float y, int wheelRotation)
        {
            _mouseWheel(rendererId, x, y, wheelRotation);
            Error.Check();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void TouchDownDelegate(int rendererId, float x, float y, int id);
        static TouchDownDelegate _touchDown;
        static void Noesis_TouchDown(int rendererId, float x, float y, int id)
        {
            _touchDown(rendererId, x, y, id);
            Error.Check();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void TouchMoveDelegate(int rendererId, float x, float y, int id);
        static TouchMoveDelegate _touchMove;
        static void Noesis_TouchMove(int rendererId, float x, float y, int id)
        {
            _touchMove(rendererId, x, y, id);
            Error.Check();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void TouchUpDelegate(int rendererId, float x, float y, int id);
        static TouchUpDelegate _touchUp;
        static void Noesis_TouchUp(int rendererId, float x, float y, int id)
        {
            _touchUp(rendererId, x, y, id);
            Error.Check();
        }
    
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void KeyDownDelegate(int rendererId, int key);
        static KeyDownDelegate _keyDown;
        static void Noesis_KeyDown(int rendererId, int key)
        {
            _keyDown(rendererId, key);
            Error.Check();
        }
    
        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void KeyUpDelegate(int rendererId, int key);
        static KeyUpDelegate _keyUp;
        static void Noesis_KeyUp(int rendererId, int key)
        {
            _keyUp(rendererId, key);
            Error.Check();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void CharDelegate(int rendererId, uint ch);
        static CharDelegate _char;
        static void Noesis_Char(int rendererId, uint ch)
        {
            _char(rendererId, ch);
            Error.Check();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void ActivateDelegate(int rendererId);
        static ActivateDelegate _activate;
        static void Noesis_Activate(int rendererId)
        {
            _activate(rendererId);
            Error.Check();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        delegate void DeactivateDelegate(int rendererId);
        static DeactivateDelegate _deactivate;
        static void Noesis_Deactivate(int rendererId)
        {
            _deactivate(rendererId);
            Error.Check();
        }

    #else
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_CreateRenderer")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_CreateRenderer")]
        #endif
        static extern int Noesis_CreateRenderer(IntPtr root);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_NotifyDestroyRenderer")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_NotifyDestroyRenderer")]
        #endif
        static extern void Noesis_NotifyDestroyRenderer(int rendererId);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_RendererSurfaceSize")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_RendererSurfaceSize")]
        #endif
        static extern void Noesis_RendererSurfaceSize(int rendererId, int width, int height,
            float offscreenWidth, float offscreenHeight, int msaa);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_RendererAntialiasingMode")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_RendererAntialiasingMode")]
        #endif
        static extern void Noesis_RendererAntialiasingMode(int rendererId, int mode);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_RendererTessMode")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_RendererTessMode")]
        #endif
        static extern void Noesis_RendererTessMode(int rendererId, int mode);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_RendererTessQuality")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_RendererTessQuality")]
        #endif
        static extern void Noesis_RendererTessQuality(int rendererId, int quality);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_RendererFlags")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_RendererFlags")]
        #endif
        static extern void Noesis_RendererFlags(int rendererId, int flags);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_UpdateRenderer")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_UpdateRenderer")]
        #endif
        static extern bool Noesis_UpdateRenderer(int rendererId, double timeInSeconds);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_WaitUpdateRenderer")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_WaitUpdateRenderer")]
        #endif
        static extern void Noesis_WaitUpdateRenderer(int rendererId);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_HitTest")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_HitTest")]
        #endif
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Noesis_HitTest(IntPtr root, float x, float y);
    
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_MouseButtonDown")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_MouseButtonDown")]
        #endif
        static extern void Noesis_MouseButtonDown(int rendererId, float x, float y, int button);
    
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_MouseButtonUp")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_MouseButtonUp")]
        #endif
        static extern void Noesis_MouseButtonUp(int rendererId, float x, float y, int button);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_MouseDoubleClick")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_MouseDoubleClick")]
        #endif
        static extern void Noesis_MouseDoubleClick(int rendererId, float x, float y, int button);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_MouseMove")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_MouseMove")]
        #endif
        static extern void Noesis_MouseMove(int rendererId, float x, float y);
    
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_MouseWheel")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_MouseWheel")]
        #endif
        static extern void Noesis_MouseWheel(int rendererId, float x, float y, int wheelRotation);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_TouchDown")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_TouchDown")]
        #endif
        static extern void Noesis_TouchDown(int rendererId, float x, float y, int id);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_TouchUp")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_TouchUp")]
        #endif
        static extern void Noesis_TouchUp(int rendererId, float x, float y, int id);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_TouchMove")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_TouchMove")]
        #endif
        static extern void Noesis_TouchMove(int rendererId, float x, float y, int id);
    
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_KeyDown")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_KeyDown")]
        #endif
        static extern void Noesis_KeyDown(int rendererId, int key);    
    
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_KeyUp")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_KeyUp")]
        #endif
        static extern void Noesis_KeyUp(int rendererId, int key);    

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_Char")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_Char")]
        #endif
        static extern void Noesis_Char(int rendererId, uint ch);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_Activate")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_Activate")]
        #endif
        static extern void Noesis_Activate(int rendererId);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE || UNITY_XBOX360
        [DllImport("__Internal", EntryPoint="Noesis_Deactivate")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_Deactivate")]
        #endif
        static extern void Noesis_Deactivate(int rendererId);

    #endif

        ////////////////////////////////////////////////////////////////////////////////////////////////
        #if UNITY_IPHONE
        [DllImport("__Internal", EntryPoint = "Noesis_UnityRenderEvent")]
        #else
        [DllImport("Noesis", EntryPoint = "Noesis_UnityRenderEvent")]
        #endif
        private static extern void Noesis_UnityRenderEvent(int eventId);

    }
}

