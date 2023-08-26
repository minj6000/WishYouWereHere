using PixelCrushers;
using PixelCrushers.DialogueSystem;

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
            }
            else
            {
                _dialogueInputDeviceManager.keyInputSwitchesModeTo = InputDeviceManager.KeyInputSwitchesModeTo.Keyboard;
                _dialogueInputDeviceManager.detectMouseControl = false;
            }
        }
    } 
}
