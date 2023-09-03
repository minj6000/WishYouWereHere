using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.EPCommon;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP4
{
    public class Episode4 : MonoBehaviour
    {
        [SerializeField] private EventArea[] _eventAreas;
        [SerializeField] private Sofa _sofa;
        [SerializeField] FadeInOutController _fadeInOutController;

        [SerializeField] float _dissolveDuration = 5f;

        public enum States
        {
            Ready,
            Start,
            EventArea,
            Sitable,
            Ending
        }

        private States _state = States.Ready;
        public States State
        {
            get => _state;
            private set
            {
                _state = value;
                switch (_state)
                {
                    case States.Start:                        
                        State_Start();
                        break;
                    case States.EventArea:
                        State_EventArea();
                        break;
                    case States.Sitable:
                        State_Sitable();
                        break;
                    case States.Ending:
                        State_Ending();
                        break;
                }
            }
        }              

        private async void State_Ending()
        {
            PlayerController.Instance.Rotatable(true);
            // TODO: 디졸브 효과?



            await _fadeInOutController.FadeOut(2f);
        }

        private void State_Sitable()
        {
            SetKeyboardControl();
            _sofa.Enabled = true;

            _sofa.OnDown.AsObservable()
                .First()
                .Subscribe(async _ =>
                {
                    _sofa.Enabled = false;

                    //소파에 앉는 연출
                    await PlayerController.Instance.SitDown(_sofa.SitTransform);
                    State = States.Ending;
                });
        }

        private void State_EventArea()
        {
            DialogueLua.SetVariable("시스템가이드", "이야기 나누기");
            SetKeyboardControl();

            foreach (var _event in _eventAreas)
            {
                var e = _event;
                e.OnEnter.AddListener(async () =>
                {
                    e.Enabled = false;
                    await PlayerController.Instance.LookAt(e.LookTransform);
                    SetMouseControl();
                    DialogueManager.Instance.StartConversationWithEndedAction("EP_4", _ =>
                    {
                        SetKeyboardControl();
                        if (DialogueLua.GetVariable("EP4_완료공간").AsInt == 4)
                        {
                            SetMouseControl();
                            DialogueManager.Instance.StartConversationWithEndedAction("EP_4", _ =>
                            {
                                SetKeyboardControl();
                                State = States.Sitable;
                            });
                        }
                    });
                });
            }
        }

        void State_Start()
        {
            SetMouseControl();
            DialogueManager.Instance.StartConversationWithEndedAction("EP_4_시작", _ =>
            {
                SetKeyboardControl();
                State = States.EventArea;
            });
        }

        void SetKeyboardControl()
        {
            PlayerController.Instance.Movable(true);
            PlayerController.Instance.Rotatable(true);
            InputHelper.EnableMouseControl(false);
        }

        void SetMouseControl()
        {
            PlayerController.Instance.Movable(false);
            PlayerController.Instance.Rotatable(false);
            InputHelper.EnableMouseControl(true);
        }



        private async void Ready()
        {           
            _sofa.Enabled = false;
            SetMouseControl();
            await _fadeInOutController.FadeIn(2f);
            State = States.Start;
        }

        void Start()
        {
            Ready();
        }

        private void Reset()
        {
            AssignReferences();
        }

        [Button]
        void AssignReferences()
        {
            _eventAreas = FindObjectsOfType<EventArea>();
            _sofa = FindObjectOfType<Sofa>();
            _fadeInOutController = FindObjectOfType<FadeInOutController>();
        }
    } 
}
