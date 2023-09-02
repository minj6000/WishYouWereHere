using Cysharp.Threading.Tasks;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP4
{
    public class Episode4 : MonoBehaviour
    {
        [SerializeField] private DissolveInOnLocation[] _dissolveInLocations;
        [SerializeField] private Sofa _sofa;
        [SerializeField] FadeInOutController _fadeInOutController;

        [SerializeField] float _dissolveDuration = 5f;

        public enum States
        {
            Ready,
            DissolveIn,
            DissolveOut,
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
                    case States.DissolveIn:
                        State_DissolveIn();
                        break;
                    case States.DissolveOut:
                        State_DissolveOut();
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
            List<UniTask> tasks = new List<UniTask>();
            foreach (var dissolveInLocation in _dissolveInLocations)
            {
                tasks.Add(dissolveInLocation.DissolveObject.Hide(_dissolveDuration));
                await UniTask.Delay(Random.Range(3000, 5000));
            }

            await UniTask.WhenAll(tasks);
            await _fadeInOutController.FadeOut(2f);
        }

        private void State_DissolveOut()
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

        private void State_DissolveIn()
        {
            SetKeyboardControl();

            foreach (var dissolveInLocation in _dissolveInLocations)
            {
                var d = dissolveInLocation;
                d.Enabled = true;                
                d.OnBeginTrigger.AddListener(() =>
                {
                    d.Enabled = false;
                    DialogueLua.SetVariable("EP4_위치", d.LocationName);

                    SetMouseControl();

                    DialogueManager.Instance.StartConversationWithEndedAction("EP_4", async _ =>
                    {
                        await PlayerController.Instance.LookAt(d.DissolveObject.LookTransform);
                        PlayerController.Instance.Rotatable(true);
                        await d.DissolveObject.Show(_dissolveDuration);

                        SetKeyboardControl();

                        if (DialogueLua.GetVariable("EP4_완료공간").AsInt == 4)
                        {
                            SetMouseControl();

                            DialogueManager.Instance.StartConversationWithEndedAction("EP_4", _ =>
                            {
                                SetKeyboardControl();

                                State = States.DissolveOut;
                            });
                        }
                    });

                });
            }
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
            await _fadeInOutController.FadeIn(2f);
            State = States.DissolveIn;
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
            _dissolveInLocations = FindObjectsOfType<DissolveInOnLocation>();
            _sofa = FindObjectOfType<Sofa>();
            _fadeInOutController = FindObjectOfType<FadeInOutController>();
        }
    } 
}
