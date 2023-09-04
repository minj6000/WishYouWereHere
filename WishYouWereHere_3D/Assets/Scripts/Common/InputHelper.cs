using PixelCrushers;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace WishYouWereHere3D.Common
{
    public class InputHelper
    {
        public static void EnableMouseControl(bool enable)
        {
            InputDeviceManager _dialogueInputDeviceManager = DialogueManager.Instance.GetComponent<InputDeviceManager>();

            if (enable)
            {
                _dialogueInputDeviceManager.keyInputSwitchesModeTo = InputDeviceManager.KeyInputSwitchesModeTo.Mouse;
                _dialogueInputDeviceManager.detectMouseControl = true;
                _dialogueInputDeviceManager.alwaysAutoFocus = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                _dialogueInputDeviceManager.keyInputSwitchesModeTo = InputDeviceManager.KeyInputSwitchesModeTo.Keyboard;
                _dialogueInputDeviceManager.detectMouseControl = false;
                _dialogueInputDeviceManager.alwaysAutoFocus = true;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    } 
}
