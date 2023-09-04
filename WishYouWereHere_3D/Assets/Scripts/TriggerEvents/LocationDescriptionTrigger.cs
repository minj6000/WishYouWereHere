using DarkTonic.MasterAudio;
using System;
using UniRx;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.TriggerEvents
{
    public class LocationDescriptionTrigger : EnterColliderTriggerEvent
    {
        [SerializeField] string _textPath;
        [SerializeField] TextShower_TMP _textShower;
        [SerializeField] string _soundOnExcuteName;

        bool _showOnce;
        float _showDuration;

        bool _triggered;

        IDisposable _disposableTextAppeared;

        private void Start()
        {
            _showOnce = Configuration.Instance.LocationDescription.ShowOnce;
            _showDuration = Configuration.Instance.LocationDescription.ShowDuration;

            _triggered = false;

            if (_showDuration > 0f)
            {
                _textShower.OnTextAppearing.AsObservable()
                    .Subscribe(_ => {
                        _disposableTextAppeared?.Dispose();
                        _disposableTextAppeared = _textShower.OnTextAppeared.AsObservable()
                            .Where(_ => _textShower.IsText(_textPath))
                            .Delay(TimeSpan.FromSeconds(_showDuration))
                            .Subscribe(_ => UnExecute());
                    })
                    .AddTo(gameObject);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            Execute();
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            if(_showDuration == 0f)
            {
                UnExecute();
            }
        }

        void Execute()
        {
            if(_showOnce && _triggered)
            {
                return;
            }

            if(!string.IsNullOrEmpty(_soundOnExcuteName))
            {
                MasterAudio.PlaySound3DAtTransform(_soundOnExcuteName, transform);
            }
            _textShower.ShowText(_textPath);
            _triggered = true;
        }

        void UnExecute(bool clearTriggered = false)
        {
            if(_textShower.IsText(_textPath))
            {
                if (!string.IsNullOrEmpty(_soundOnExcuteName))
                {
                    MasterAudio.StopAllSoundsOfTransform(transform);
                }

                _textShower.HideText();
            }

            if(clearTriggered)
            {
                _triggered = false;
            }
        }

        public void ClearValues()
        {
            UnExecute(true);
        }
    } 
}
