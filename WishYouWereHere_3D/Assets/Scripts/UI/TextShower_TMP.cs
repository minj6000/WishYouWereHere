using Febucci.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace WishYouWereHere3D.UI
{
    public class TextShower_TMP : MonoBehaviour
    {
        public enum AppearingStates
        {
            Appearing,
            Appeared,
            Disappearing,
            Disappeared
        }

        [SerializeField] protected TMP_Text _textMeshPro;
        [SerializeField] protected TypewriterCore _typeWriter;

        public UnityEvent OnTextAppearing;
        public UnityEvent OnTextAppeared;

        public UnityEvent OnTextDisappearing;
        public UnityEvent OnTextDisappeared;

        private AppearingStates _appearingState = AppearingStates.Disappeared;
        public AppearingStates AppearingState { 
            get => _appearingState; 
            private set
            {
                if(_appearingState != value)
                {
                    _appearingState = value;
                    Debug.Log($"{name} AppearingState Changed {_appearingState}");
                    switch (_appearingState)
                    {
                        case AppearingStates.Appearing:
                            OnTextAppearing?.Invoke();                            
                            break;
                        case AppearingStates.Appeared:
                            OnTextAppeared?.Invoke();
                            break;
                        case AppearingStates.Disappearing:
                            OnTextDisappearing?.Invoke();
                            break;
                        case AppearingStates.Disappeared:
                            OnTextDisappeared?.Invoke();
                            _textMeshPro.text = string.Empty;
                            break;
                    }                    
                }
            }
        }

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

        public virtual bool IsText(string text)
        {
            return _textMeshPro.text == text;
        }

        public virtual void ShowText(string text)
        {
            if (_textMeshPro != null)
            {
                if(_textMeshPro.text != text)
                {
                    Debug.Log($"{name} Change and ShowText '{_textMeshPro.text}' => '{text}'");
                    HideText();
                }

                if (_typeWriter != null)
                {
                    if (AppearingState == AppearingStates.Disappearing)
                    {
                        _typeWriter.StopDisappearingText();
                        AppearingState = AppearingStates.Disappeared;
                    }

                    if (AppearingState != AppearingStates.Disappeared)
                    {
                        return;
                    }

                    AppearingState = AppearingStates.Appearing;
                    _typeWriter.ShowText(text);
                }
                else
                {
                    AppearingState = AppearingStates.Appearing;
                    _textMeshPro.text = text;
                    OnTextShowedInternal();
                }                
            }
        }

        public virtual void HideText()
        {
            if(_textMeshPro != null)
            {
                if(AppearingState == AppearingStates.Disappeared)
                {
                    return;
                }

                if (_typeWriter != null)
                {
                    if (AppearingState == AppearingStates.Appearing)
                    {
                        _typeWriter.StopShowingText();
                        AppearingState = AppearingStates.Appeared;
                    }

                    if(AppearingState != AppearingStates.Appeared)
                    {
                        return;
                    }

                    AppearingState = AppearingStates.Disappearing;
                    _typeWriter.StartDisappearingText();
                }
                else
                {
                    AppearingState = AppearingStates.Disappearing;
                    OnTextDisappearedInternal();
                }
            }

        }

        void OnTextShowedInternal()
        {            
            AppearingState = AppearingStates.Appeared;
        }

        void OnTextDisappearedInternal()
        {
            AppearingState = AppearingStates.Disappeared;
        }
    } 
}
