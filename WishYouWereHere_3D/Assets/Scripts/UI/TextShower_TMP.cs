using Febucci.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace WishYouWereHere3D.UI
{
    public class TextShower_TMP : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _textMeshPro;
        [SerializeField] protected TypewriterCore _typeWriter;

        public UnityEvent OnTextShowed;
        public UnityEvent OnTextDisappeared;

        private void Reset()
        {
            _textMeshPro = GetComponent<TMP_Text>();
            _typeWriter = GetComponent<TypewriterCore>();
        }

        private void OnEnable()
        {
            if(_typeWriter != null)
            {
                _typeWriter.onTextShowed.AddListener(OnTextShowedInternal);
                _typeWriter.onTextDisappeared.AddListener(OnTextDisappearedInternal);
            }
        }

        private void OnDisable()
        {
            if (_typeWriter != null)
            {
                _typeWriter.onTextShowed.RemoveListener(OnTextShowedInternal);
                _typeWriter.onTextDisappeared.RemoveListener(OnTextDisappearedInternal);
            }
        }

        public virtual void ShowText(string text)
        {
            if (_textMeshPro != null)
            {
                if (_typeWriter != null)
                {
                    if (_typeWriter.isHidingText)
                    {
                        _typeWriter.StopDisappearingText();
                    }

                    _typeWriter.ShowText(text);
                }
                else
                {
                    _textMeshPro.text = text;
                    OnTextShowedInternal();
                }                
            }
        }

        public virtual void HideText()
        {
            if(_textMeshPro != null)
            {
                if (_typeWriter != null)
                {
                    if (_typeWriter.isShowingText)
                    {
                        _typeWriter.StopShowingText();
                    }

                    if (_typeWriter.isHidingText)
                    {
                        return;
                    }

                    _typeWriter.StartDisappearingText();
                }
                else
                {
                    _textMeshPro.text = string.Empty;
                    OnTextDisappearedInternal();
                }
            }

        }

        void OnTextShowedInternal()
        {
            OnTextShowed?.Invoke();
        }

        void OnTextDisappearedInternal()
        {
            OnTextDisappeared?.Invoke();
            _textMeshPro.text = string.Empty;
        }
    } 
}
