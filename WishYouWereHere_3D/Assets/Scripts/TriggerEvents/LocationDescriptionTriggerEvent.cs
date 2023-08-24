using Febucci.UI.Core;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;

namespace WishYouWereHere3D.TriggerEvents
{
    public class LocationDescriptionTriggerEvent : EnterColliderTriggerEvent
    {
        [SerializeField] string _locationName;
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] bool _showOnce = false;
        [SerializeField] float _delay = 0f;

        bool _triggered = false;
        TypewriterCore _typeWriter;

        private void Start()
        {
            _typeWriter = _text.GetComponent<TypewriterCore>();            
        }

        private void OnEnable()
        {
            OnBeginTrigger.AddListener(Set);
            OnEndTrigger.AddListener(Unset);
        }

        private void OnDisable()
        {
            OnBeginTrigger.RemoveListener(Set);
            OnEndTrigger.RemoveListener(Unset);
        }

        void Set()
        {
            if(_showOnce && _triggered)
            {
                return;
            }

            Location location = DialogueManager.Instance.MasterDatabase.GetLocation(_locationName);
            if(_typeWriter != null)
            {
                if (_typeWriter.isHidingText)
                {
                    _typeWriter.StopDisappearingText();
                }

                _typeWriter.ShowText(location.Description);
                _typeWriter.onTextShowed.AddListener(OnTextShowed);
            }
            else
            {
                _text.text = location.Description;
            }

            _triggered = true;
        }

        void OnTextShowed()
        {
            _typeWriter.onTextShowed.RemoveListener(OnTextShowed);
            if (_delay > 0f)
            {
                Invoke("Unset", _delay);
            }
        }

        void Unset()
        {
            if (_typeWriter != null)
            {
                if(_typeWriter.isShowingText)
                {
                    _typeWriter.StopShowingText();
                }

                if(_typeWriter.isHidingText)
                {
                    return;
                }
                
                _typeWriter.StartDisappearingText();
                _typeWriter.onTextDisappeared.AddListener(OnTextDisappeared);
            }
            else
            {
                _text.text = string.Empty;
            }
        }

        void OnTextDisappeared()
        {
            _typeWriter.onTextDisappeared.RemoveListener(OnTextDisappeared);
            _text.text = string.Empty;
        }
    } 
}
