using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.TriggerEvents
{
    public class ItemDescriptionTriggerEvent : CenterCursorTriggerEvent
    {
        [SerializeField] string _textEnterPath;
        [SerializeField] string _textClickedPath;
        [SerializeField] TextShower_TMP _textShower;

        float _hideToDistance;
        bool clicked;

        private void Start()
        {
            clicked = false;
            _hideToDistance = Configuration.Instance.ItemDescription.HideToDistance;
        }

        private void Update()
        {
            if (_textShower.AppearingState != TextShower_TMP.AppearingStates.Appeared || _hideToDistance == 0f)
                return;

            //거리가 멀어지면 텍스트를 숨긴다.
            if (Vector3.Distance(transform.position, Camera.main.transform.position) > _hideToDistance)
            {
                ClearValues();
            }
        }

        protected override void OnCenterCursorEnter()
        {
            base.OnCenterCursorEnter();

            if(clicked)
            {
                return;
            }

            _textShower.ShowText(_textEnterPath);
        }

        protected override void OnCenterCursorExit()
        {
            base.OnCenterCursorExit();

            if(_hideToDistance == 0f)
            {
                ClearValues();
            }
        }

        protected override void OnCenterCursorDown()
        {
            base.OnCenterCursorDown();
            
            clicked = true;
            _textShower.ShowText(_textClickedPath);
        }
        
        public void ChangeClickedPath(string clickedPath)
        {
            _textClickedPath = clickedPath;
        }

        public void ClearValues()
        {
            _textShower.HideText();
            clicked = false;
        }
    }
}