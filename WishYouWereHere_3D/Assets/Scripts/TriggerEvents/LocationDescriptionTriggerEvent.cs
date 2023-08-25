using UnityEngine;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.TriggerEvents
{
    public class LocationDescriptionTriggerEvent : EnterColliderTriggerEvent
    {
        [SerializeField] string _textPath;
        [SerializeField] TextShower_TMP _textShower;
        [SerializeField] bool _showOnce = false;
        [SerializeField] float _delay = 0f;

        bool _triggered = false;

        protected override void OnTriggerEnter(Collider other)
        {
            Set();
            base.OnTriggerEnter(other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            Unset();
            base.OnTriggerExit(other);
        }

        void Set()
        {
            if(_showOnce && _triggered)
            {
                return;
            }

            _textShower.OnTextShowed.AddListener(OnTextShowed);
            _textShower.ShowText(_textPath);

            _triggered = true;
        }

        void OnTextShowed()
        {
            _textShower.OnTextShowed.RemoveListener(OnTextShowed);
            if (_delay > 0f)
            {
                Invoke("Unset", _delay);
            }
        }

        void Unset()
        {
            _textShower.OnTextDisappeared.AddListener(OnTextDisappeared);
            _textShower.HideText();
        }

        void OnTextDisappeared()
        {
            _textShower.OnTextDisappeared.RemoveListener(OnTextDisappeared);
        }
    } 
}
