using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Noesis;

internal class NoesisKeyCodes
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private static Dictionary<KeyCode, int> noesisKeyCode;
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    static NoesisKeyCodes()
    {
        noesisKeyCode = new Dictionary<KeyCode, int>();
        
        noesisKeyCode.Add(KeyCode.Backspace, (int)Key.Back);
        noesisKeyCode.Add(KeyCode.Tab, (int)Key.Tab);
        noesisKeyCode.Add(KeyCode.Clear, (int)Key.Clear);
        noesisKeyCode.Add(KeyCode.Return, (int)Key.Return);
        noesisKeyCode.Add(KeyCode.Pause, (int)Key.Pause);
        noesisKeyCode.Add(KeyCode.Break, (int)Key.Pause);          // same as Pause
        
        noesisKeyCode.Add(KeyCode.Escape, (int)Key.Escape);
        
        noesisKeyCode.Add(KeyCode.Space, (int)Key.Space);
        noesisKeyCode.Add(KeyCode.PageUp, (int)Key.Prior);         // prior?
        noesisKeyCode.Add(KeyCode.PageDown, (int)Key.Next);        // next?
        noesisKeyCode.Add(KeyCode.End, (int)Key.End);
        noesisKeyCode.Add(KeyCode.Home, (int)Key.Home);
        noesisKeyCode.Add(KeyCode.LeftArrow, (int)Key.Left);
        noesisKeyCode.Add(KeyCode.UpArrow, (int)Key.Up);
        noesisKeyCode.Add(KeyCode.RightArrow, (int)Key.Right);
        noesisKeyCode.Add(KeyCode.DownArrow, (int)Key.Down);
        // SELECT KEY not defined
        noesisKeyCode.Add(KeyCode.Print, (int)Key.Print);
        // EXECUTE KEY not defined
        // PRINTSCR KEY not defined
        noesisKeyCode.Add(KeyCode.Insert, (int)Key.Insert);
        noesisKeyCode.Add(KeyCode.Delete, (int)Key.Delete);
        noesisKeyCode.Add(KeyCode.Help, (int)Key.Help);
        
        noesisKeyCode.Add(KeyCode.Alpha0, (int)Key.Alpha0);
        noesisKeyCode.Add(KeyCode.Alpha1, (int)Key.Alpha1);
        noesisKeyCode.Add(KeyCode.Alpha2, (int)Key.Alpha2);
        noesisKeyCode.Add(KeyCode.Alpha3, (int)Key.Alpha3);
        noesisKeyCode.Add(KeyCode.Alpha4, (int)Key.Alpha4);
        noesisKeyCode.Add(KeyCode.Alpha5, (int)Key.Alpha5);
        noesisKeyCode.Add(KeyCode.Alpha6, (int)Key.Alpha6);
        noesisKeyCode.Add(KeyCode.Alpha7, (int)Key.Alpha7);
        noesisKeyCode.Add(KeyCode.Alpha8, (int)Key.Alpha8);
        noesisKeyCode.Add(KeyCode.Alpha9, (int)Key.Alpha9);
        
        noesisKeyCode.Add(KeyCode.Keypad0, (int)Key.Pad0);
        noesisKeyCode.Add(KeyCode.Keypad1, (int)Key.Pad1);
        noesisKeyCode.Add(KeyCode.Keypad2, (int)Key.Pad2);
        noesisKeyCode.Add(KeyCode.Keypad3, (int)Key.Pad3);
        noesisKeyCode.Add(KeyCode.Keypad4, (int)Key.Pad4);
        noesisKeyCode.Add(KeyCode.Keypad5, (int)Key.Pad5);
        noesisKeyCode.Add(KeyCode.Keypad6, (int)Key.Pad6);
        noesisKeyCode.Add(KeyCode.Keypad7, (int)Key.Pad7);
        noesisKeyCode.Add(KeyCode.Keypad8, (int)Key.Pad8);
        noesisKeyCode.Add(KeyCode.Keypad9, (int)Key.Pad9);
        noesisKeyCode.Add(KeyCode.KeypadMultiply, (int)Key.Multiply);
        noesisKeyCode.Add(KeyCode.KeypadPlus, (int)Key.Add);
        // SEPARATOR KEY not defined
        noesisKeyCode.Add(KeyCode.KeypadMinus, (int)Key.Subtract);
        noesisKeyCode.Add(KeyCode.KeypadPeriod, (int)Key.Decimal);
        noesisKeyCode.Add(KeyCode.KeypadDivide, (int)Key.Divide);
        noesisKeyCode.Add(KeyCode.KeypadEnter, (int)Key.Return);      // same as Return
        
        noesisKeyCode.Add(KeyCode.A, (int)Key.A);
        noesisKeyCode.Add(KeyCode.B, (int)Key.B);
        noesisKeyCode.Add(KeyCode.C, (int)Key.C);
        noesisKeyCode.Add(KeyCode.D, (int)Key.D);
        noesisKeyCode.Add(KeyCode.E, (int)Key.E);
        noesisKeyCode.Add(KeyCode.F, (int)Key.F);
        noesisKeyCode.Add(KeyCode.G, (int)Key.G);
        noesisKeyCode.Add(KeyCode.H, (int)Key.H);
        noesisKeyCode.Add(KeyCode.I, (int)Key.I);
        noesisKeyCode.Add(KeyCode.J, (int)Key.J);
        noesisKeyCode.Add(KeyCode.K, (int)Key.K);
        noesisKeyCode.Add(KeyCode.L, (int)Key.L);
        noesisKeyCode.Add(KeyCode.M, (int)Key.M);
        noesisKeyCode.Add(KeyCode.N, (int)Key.N);
        noesisKeyCode.Add(KeyCode.O, (int)Key.O);
        noesisKeyCode.Add(KeyCode.P, (int)Key.P);
        noesisKeyCode.Add(KeyCode.Q, (int)Key.Q);
        noesisKeyCode.Add(KeyCode.R, (int)Key.R);
        noesisKeyCode.Add(KeyCode.S, (int)Key.S);
        noesisKeyCode.Add(KeyCode.T, (int)Key.T);
        noesisKeyCode.Add(KeyCode.U, (int)Key.U);
        noesisKeyCode.Add(KeyCode.V, (int)Key.V);
        noesisKeyCode.Add(KeyCode.W, (int)Key.W);
        noesisKeyCode.Add(KeyCode.X, (int)Key.X);
        noesisKeyCode.Add(KeyCode.Y, (int)Key.Y);
        noesisKeyCode.Add(KeyCode.Z, (int)Key.Z);
        
        noesisKeyCode.Add(KeyCode.F1, (int)Key.F1);
        noesisKeyCode.Add(KeyCode.F2, (int)Key.F2);
        noesisKeyCode.Add(KeyCode.F3, (int)Key.F3);
        noesisKeyCode.Add(KeyCode.F4, (int)Key.F4);
        noesisKeyCode.Add(KeyCode.F5, (int)Key.F5);
        noesisKeyCode.Add(KeyCode.F6, (int)Key.F6);
        noesisKeyCode.Add(KeyCode.F7, (int)Key.F7);
        noesisKeyCode.Add(KeyCode.F8, (int)Key.F8);
        noesisKeyCode.Add(KeyCode.F9, (int)Key.F9);
        noesisKeyCode.Add(KeyCode.F10, (int)Key.F10);
        noesisKeyCode.Add(KeyCode.F11, (int)Key.F11);
        noesisKeyCode.Add(KeyCode.F12, (int)Key.F12);
        noesisKeyCode.Add(KeyCode.F13, (int)Key.F13);
        noesisKeyCode.Add(KeyCode.F14, (int)Key.F14);
        noesisKeyCode.Add(KeyCode.F15, (int)Key.F15);
        
        noesisKeyCode.Add(KeyCode.Numlock, (int)Key.NumLock);
        noesisKeyCode.Add(KeyCode.ScrollLock, (int)Key.Scroll);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int Convert(KeyCode key)
    {
        int noesisKey = 0;
        noesisKeyCode.TryGetValue(key, out noesisKey);
        return noesisKey;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
#if UNITY_STANDALONE_LINUX
    public static char KeySymToUnicode(char ch)
    {
        // http://www.cl.cam.ac.uk/~mgk25/ucs/keysyms.txt
        if (ch >= '\ufd01' && ch <= '\ufefd') return '\u0000';
        if (ch >= '\uff20' && ch <= '\uff7f') return '\u0000';
        if (ch >= '\uff91' && ch <= '\uff9f') return '\u0000';
        if (ch >= '\uffbe' && ch <= '\uffff') return '\u0000';

        switch (ch)
        {
            case '\uff08': return '\u0008'; // BackSpace : back space, back char
            case '\uff09': return '\u0009'; // Tab
            case '\uff0a': return '\u000a'; // Linefeed : Linefeed, LF
            case '\uff0b': return '\u000b'; // Clear
            case '\uff0d': return '\u000d'; // Return : Return, enter
            case '\uff13': return '\u0013'; // Pause /* Pause, hold */
            case '\uff14': return '\u0014'; // Scroll_Lock
            case '\uff15': return '\u0015'; // Sys_Req
            case '\uff1b': return '\u001b'; // Escape
            case '\uff80': return '\u0020'; // Space
            case '\uff89': return '\u0009'; // Tab
            case '\uff8d': return '\u000d'; // Return : Return, enter
            case '\uffaa': return '\u002a'; // KP_Multiply
            case '\uffab': return '\u002b'; // KP_Add
            case '\uffac': return '\u002c'; // KP_Separator
            case '\uffad': return '\u002d'; // KP_Subtract
            case '\uffae': return '\u002e'; // KP_Decimal
            case '\uffaf': return '\u002f'; // KP_Divide
            case '\uffb0': return '\u0030'; // KP_0
            case '\uffb1': return '\u0031'; // KP_1
            case '\uffb2': return '\u0032'; // KP_2
            case '\uffb3': return '\u0033'; // KP_3
            case '\uffb4': return '\u0034'; // KP_4
            case '\uffb5': return '\u0035'; // KP_5
            case '\uffb6': return '\u0036'; // KP_6
            case '\uffb7': return '\u0037'; // KP_7
            case '\uffb8': return '\u0038'; // KP_8
            case '\uffb9': return '\u0039'; // KP_9
            case '\uffbd': return '\u003d'; // KP_Equal
            default: return ch;
        }
    }
#endif
}
