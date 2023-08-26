using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP1
{
    public class Episode1 : MonoBehaviour
    {
        [SerializeField] PlayerController _playerController;
        [SerializeField] ItemDescriptionTriggerEvent[] itemDescriptionTriggerEvents;
        [SerializeField] LocationDescriptionTriggerEvent[] locationDescriptionTriggerEvents;

        [SerializeField] Sofa _sofa;
        [SerializeField] RecordPlayer _recordPlayer;

        public enum States
        {
            Ready,
            MovableSpace,
            Dialogue,
            MovableObject,
            End
        }

        private States _state = States.Ready;
        public States State { 
            get => _state;
            private set
            {
                _state = value;
                switch (_state)
                {
                    case States.MovableSpace:
                        State_MovableSpace();
                        break;
                    case States.Dialogue:
                        State_Dialogue();
                        break;
                    case States.MovableObject:
                        State_MovableObject();
                        break;
                    case States.End:
                        State_End();
                        break;
                }
            }
        }

        private void State_End()
        {
            
        }

        private void State_MovableObject()
        {
            _playerController.Movable(true);
            _playerController.Rotatable(true);

            _recordPlayer.ChangeClickedPathAfterConversation();

            InputHelper.EnableMouseControl(false);
        }

        #region Dialogue
        private void State_Dialogue()
        {
            _playerController.Movable(false);
            _playerController.Rotatable(false);

            if(Configuration.Instance.ConversationControllerType == Configuration.ConversationController.Mouse)
            {
                InputHelper.EnableMouseControl(true);
            }

            DialogueManager.Instance.StartConversation("EP1");

            Observable.FromEvent<TransformDelegate, Transform>(
                h => t => h(t),
                h => DialogueManager.Instance.conversationEnded += h, 
                h => DialogueManager.Instance.conversationEnded -= h)
                .First()
                .Subscribe(async _ =>
                {
                    Debug.Log($"OnConversationEnded {_.name}");
                    await _playerController.StandUp();
                    State = States.MovableObject;
                });
        }
        #endregion

        private void State_MovableSpace()
        {
            _playerController.Movable(true);
            _playerController.Rotatable(true);

            _sofa.OnDown.AsObservable()
                .First()
                .Subscribe(async _ =>
                {
                    _sofa.Enabled = false;

                    foreach (var item in itemDescriptionTriggerEvents)
                    {
                        item.ClearValues();
                    }
                    foreach (var item in locationDescriptionTriggerEvents)
                    {
                        item.ClearValues();
                        item.Enabled = false;
                    }

                    //소파에 앉는 연출
                    await _playerController.SitDown(_sofa.SitTransform);
                    State = States.Dialogue;
                });
        }

        private void Ready()
        {
            InputHelper.EnableMouseControl(false);
            State = States.MovableSpace;
        }

        private void Start()
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
            _playerController = FindObjectOfType<PlayerController>();
            itemDescriptionTriggerEvents = FindObjectsOfType<ItemDescriptionTriggerEvent>();
            locationDescriptionTriggerEvents = FindObjectsOfType<LocationDescriptionTriggerEvent>();
            
            _sofa = FindObjectOfType<Sofa>();
            _recordPlayer = FindObjectOfType<RecordPlayer>();
        }
    } 
}
