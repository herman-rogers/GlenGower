using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace Noesis
{
////////////////////////////////////////////////////////////////////////////////////////////////////
/// Manages the software keyboard.
/// Users can derive this class and override th desired methods to customize the behavior of the
/// software keyboard. For example, a user may only override the OpenTextBoxKeyboard() method to
/// change the KeyboardType depending on the focused TextBox in the application.
////////////////////////////////////////////////////////////////////////////////////////////////////
public class SoftwareKeyboardManager
{
#if !UNITY_STANDALONE
    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Called when a UI element gets the focus and software keyboard should be opened
    protected virtual void OnShowKeyboard(Noesis.UIElement focusedElement)
    {
        if (focusedElement != null)
        {
            _textBox = focusedElement as Noesis.TextBox;
            if (_textBox != null)
            {
                _keyboard = OpenTextBoxKeyboard(_textBox);
                _isOpen = _keyboard != null;
                return;
            }

            _passwordBox = focusedElement as Noesis.PasswordBox;
            if (_passwordBox != null)
            {
                _keyboard = OpenPasswordBoxKeyboard(_passwordBox);
                _isOpen = _keyboard != null;
                return;
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Override if you want to open the software keyboard for a TextBox with your own options
    protected virtual UnityEngine.TouchScreenKeyboard OpenTextBoxKeyboard(Noesis.TextBox textBox)
    {
        return UnityEngine.TouchScreenKeyboard.Open(textBox.Text);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Override if you want to open the software keyboard for a PasswordBox with your own options
    protected virtual UnityEngine.TouchScreenKeyboard OpenPasswordBoxKeyboard(
        Noesis.PasswordBox passwordBox)
    {
        return UnityEngine.TouchScreenKeyboard.Open(passwordBox.Password,
            UnityEngine.TouchScreenKeyboardType.Default, false, false, true);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Called when UI element loses focus and software keyboard should be closed
    protected virtual void OnHideKeyboard()
    {
        if (_keyboard != null)
        {
            // TODO: Force software keyboard to hide

            _textBox = null;
            _passwordBox = null;
            _keyboard = null;
            _isOpen = false;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Called to update focused UI element text
    protected virtual void OnUpdateText()
    {
        if (_keyboard != null)
        {
            if (_keyboard.active)
            {
                _isActive = true;

                if (_textBox != null)
                {
                    _textBox.Text = _keyboard.text;
                }
                else
                {
                    _passwordBox.Password = _keyboard.text;
                }
            }

            if (_isActive)
            {
                if (_keyboard.done || _keyboard.wasCanceled)
                {
                    _isActive = false;

                    // Remove focus from the UI element
                    if (_textBox != null)
                    {
                        _textBox.GetKeyboard().Focus(null);
                    }
                    else
                    {
                        _passwordBox.GetKeyboard().Focus(null);
                    }
                }
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Indicates if software keyboard is open
    protected virtual bool IsOpenOverride()
    {
        return _isOpen;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    protected Noesis.TextBox _textBox = null;
    protected Noesis.PasswordBox _passwordBox = null;
    protected UnityEngine.TouchScreenKeyboard _keyboard = null;
    protected bool _isOpen = false;
    protected bool _isActive = false;
#endif

    ////////////////////////////////////////////////////////////////////////////////////////////////
    internal void ShowKeyboard(Noesis.UIElement focusedElement)
    {
#if !UNITY_STANDALONE
        OnShowKeyboard(focusedElement);
#endif
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    internal void HideKeyboard()
    {
#if !UNITY_STANDALONE
        OnHideKeyboard();
#endif
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    internal void UpdateText()
    {
#if !UNITY_STANDALONE
        OnUpdateText();
#endif
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    internal bool IsOpen
    {
        get
        {
#if !UNITY_STANDALONE
            return IsOpenOverride();
#else
            return false;
#endif
        }
    }
}

}