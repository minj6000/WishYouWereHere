using Sirenix.OdinInspector;
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

        [SerializeField] protected bool _useCustomHideToDistance = false;
        
        [ShowIf("_useCustomHideToDistance")]
        [SerializeField] protected float _hideToDistance;

        protected bool clicked;
        public bool Clicked => clicked;

        protected virtual void Start()
        {
            clicked = false;
            if(!_useCustomHideToDistance)
            {
                _hideToDistance = Configuration.Instance.ItemDescription.HideToDistance;
            }
        }

        protected virtual void Update()
        {
            if (_textShower.AppearingState != TextShower_TMP.AppearingStates.Appeared || _hideToDistance == 0f)
                return;

            //거리가 멀어지면 텍스트를 숨긴다.
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > _hideToDistance)
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
            InteractionGuide.Instance.Show(InteractionGuide.Icons.Mouse_L, "살펴 보기");
        }

        protected override void OnCenterCursorExit()
        {
            base.OnCenterCursorExit();

            if(_hideToDistance == 0f)
            {
                ClearValues();
            }

            InteractionGuide.Instance.Hide();
        }

        protected override void OnCenterCursorDown()
        {
            base.OnCenterCursorDown();

            if (clicked)
            {
                return;
            }

            InteractionGuide.Instance.Hide();
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