using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.TriggerEvents
{
    public class ItemDescriptionTrigger : CenterCursorTriggerEvent
    {
        [SerializeField] protected string _textEnterPath;
        [SerializeField] protected string _textClickedPath;
        [SerializeField] protected TextShower_TMP _textShower;

        protected float _hideToDistance;
        protected bool clicked;
        public bool Clicked => clicked;

        protected virtual void Start()
        {
            clicked = false;
            _hideToDistance = Configuration.Instance.ItemDescription.HideToDistance;
        }

        protected virtual void Update()
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

            if (clicked)
            {
                return;
            }

            clicked = true;
            _textShower.ShowText(_textClickedPath);
        }       

        public void ClearValues()
        {
            _textShower.HideText();
            clicked = false;
        }
    }
}