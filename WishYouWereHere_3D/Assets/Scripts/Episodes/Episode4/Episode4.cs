using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using WishYouWereHere3D.Common;

namespace WishYouWereHere3D.EP4
{
    public class Episode4 : MonoBehaviour
    {
        [SerializeField] private DissolveInOnLocation[] _dissolveInLocations;
        [SerializeField] private Sofa _sofa;

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

        



        private void State_Ending()
        {
            
        }

        private void State_DissolveOut()
        {
            SetKeyboardControl();

            _sofa.Enabled = true;

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
                        await d.DissolveObject.Show();

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



        private void Ready()
        {
            _sofa.Enabled = false;
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
        }
    } 
}
