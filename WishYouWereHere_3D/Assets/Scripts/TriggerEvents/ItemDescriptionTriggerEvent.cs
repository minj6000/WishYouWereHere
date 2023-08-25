using UnityEngine;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.TriggerEvents
{
    public class ItemDescriptionTriggerEvent : CenterCursorTriggerEvent
    {
        [SerializeField] string _textEnterPath;
        [SerializeField] string _textClickedPath;
        [SerializeField] TextShower_TMP _textShower;

        bool clicked = false;

        protected override void OnCenterCursorEnter()
        {
            if(clicked)
            {
                return;
            }

            _textShower.ShowText(_textEnterPath);
            base.OnCenterCursorEnter();
        }

        protected override void OnCenterCursorExit()
        {
            if(clicked)
            {
                return;
            }

            _textShower.HideText();
            base.OnCenterCursorExit();
        }

        protected override void OnCenterCursorDown()
        {
            _textShower.ShowText(_textClickedPath);
            clicked = true;
            base.OnCenterCursorDown();
        }
        
        public void SetClickedPath(string clickedPath)
        {
            if(clicked)
            {
                _textShower.HideText();
                _textShower.OnTextDisappeared.AddListener(OnClickedPathTextDisappeared);
            }
            _textClickedPath = clickedPath;
        }

        private void OnClickedPathTextDisappeared()
        {
            _textShower.OnTextDisappeared.RemoveListener(OnClickedPathTextDisappeared);
            _textShower.ShowText(_textClickedPath);
        }
    }
}