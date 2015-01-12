using System;

namespace Noesis
{

////////////////////////////////////////////////////////////////////////////////////////////////////
public enum MouseButton
{
    Left,
    Right,
    Middle,
    XButton1,
    XButton2
};

////////////////////////////////////////////////////////////////////////////////////////////////////
public enum MouseButtonState
{
    Pressed,
    Released
};

////////////////////////////////////////////////////////////////////////////////////////////////////
public enum Key
{
    None,
    Back,
    Tab,
    Clear,
    Return,
    Pause,

    Shift,
    Control,
    Alt,

    Escape,

    Space,
    Prior,
    Next,
    End,
    Home,
    Left,
    Up,
    Right,
    Down,
    Select,
    Print,
    Execute,
    SnapShot,
    Insert,
    Delete,
    Help,

    Alpha0,
    Alpha1,
    Alpha2,
    Alpha3,
    Alpha4,
    Alpha5,
    Alpha6,
    Alpha7,
    Alpha8,
    Alpha9,

    Pad0,
    Pad1,
    Pad2,
    Pad3,
    Pad4,
    Pad5,
    Pad6,
    Pad7,
    Pad8,
    Pad9,

    Multiply,
    Add,
    Separator,
    Subtract,
    Decimal,
    Divide,

    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,

    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,
    F13,
    F14,
    F15,
    F16,
    F17,
    F18,
    F19,
    F20,
    F21,
    F22,
    F23,
    F24,

    NumLock,
    Scroll,

    Count // for static array dimension
};

////////////////////////////////////////////////////////////////////////////////////////////////////
public enum KeyStateFlags
{
    /// The key is not pressed.
    None = 0x00,

    /// The key is pressed.
    Down = 0x01,

    /// The key is toggled.
    Toggled = 0x02
};

////////////////////////////////////////////////////////////////////////////////////////////////////
public enum ModifierKeys
{
    None = 0,
    Alt = 1,
    Control = 2,
    Shift = 4
};

////////////////////////////////////////////////////////////////////////////////////////////////////
public enum VirtualEvent
{
    /// Usually raised when Ctrl+Tab is pressed
    ControlTabNext,

    /// Usually raised when Shift+Ctrl+Tab is pressed
    ControlTabPrev,

    /// Usually raised when Tab is pressed
    DirectionalTabNext,

    /// Usually raised when Shift+Tab is pressed
    DirectionalTabPrev,

    /// Usually raised when arrow keys are pressed
    //@{
    DirectionalLeft,
    DirectionalRight,
    DirectionalUp,
    DirectionalDown
    //@}
};

}
