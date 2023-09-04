using PixelCrushers.DialogueSystem;
using UnityEngine;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.Common
{
    public class DialogInteractionGuide : MonoBehaviour
    {
        [SerializeField] GameObject _normalPanel;
        [SerializeField] GameObject _systemPanel;
        [SerializeField] GameObject _responsePanel;

        const string _normalVariableName = "일반가이드";
        const string _systemVariableName = "시스템가이드";
        const string _responseVariableName = "선택가이드";

        bool _activate = false;

        private void Update()
        {
            if (DialogueManager.Instance.isConversationActive && !_activate)
            {
                _activate = true;
            }
            else if(!DialogueManager.Instance.isConversationActive && _activate)
            {
                _activate = false;
                InteractionGuide.Instance.Hide();
            }

            if (!_activate)
            {
                return;
            }

            if (_responsePanel.activeSelf)
            {
                UpdateGuide(_responseVariableName);
            }
            else if(_systemPanel.activeSelf)
            {
                UpdateGuide(_systemVariableName);
            }
            else if (_normalPanel.activeSelf)
            {
                UpdateGuide(_normalVariableName);
            }
        }

        private void UpdateGuide(string normalVariableName)
        {
            string guideText = DialogueLua.GetVariable(normalVariableName).AsString;

            InteractionGuide.Instance.Show(InteractionGuide.Icons.Mouse_L, guideText);
        }
    } 
}
