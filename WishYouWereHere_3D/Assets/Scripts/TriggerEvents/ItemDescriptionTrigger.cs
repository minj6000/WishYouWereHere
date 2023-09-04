using Sirenix.OdinInspector;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.UI;
using DarkTonic.MasterAudio;

namespace WishYouWereHere3D.TriggerEvents
{
    public class ItemDescriptionTrigger : CenterCursorTriggerEvent
    {
        [SerializeField] protected string _textEnterPath;
        [SerializeField] protected string _textClickedPath;
        [SerializeField] protected TextShower_TMP _textShower;

        [SerializeField] protected string _cursorEnterSoundName;
        [SerializeField] protected string _cursorDownSoundName;
        [SerializeField] protected string _textSoundName;

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

        private void OnEnable()
        {
            _textShower.OnTextAppearing.AddListener(PlayTextSound);
        }

        private void OnDisable()
        {
            _textShower.OnTextAppearing.RemoveListener(PlayTextSound);
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

        protected virtual void PlayTextSound()
        {
            if(!string.IsNullOrEmpty(_textSoundName))
            {
                MasterAudio.PlaySound3DAtTransform(_textSoundName, transform);
            }
        }

        protected override void OnCenterCursorEnter()
        {
            base.OnCenterCursorEnter();

            if(!string.IsNullOrEmpty(_cursorEnterSoundName))
            {
                MasterAudio.PlaySound3DAtTransform(_cursorEnterSoundName, transform);
            }


            if (clicked)
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

            if(!string.IsNullOrEmpty(_cursorDownSoundName))
            {
                MasterAudio.PlaySound3DAtTransform(_cursorDownSoundName, transform);
            }

            if (clicked)
            {
                return;
            }
            
            InteractionGuide.Instance.Hide();
            clicked = true;

            if(!string.IsNullOrEmpty(_textClickedPath))
            {
                _textShower.ShowText(_textClickedPath);
            }

        }       

        public void ClearValues()
        {
            _textShower.HideText();
            clicked = false;
        }
    }
}