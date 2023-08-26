using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
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

            DialogueManager.Instance.conversationEnded -= OnConversationEnded;
            DialogueManager.Instance.conversationEnded += OnConversationEnded;
        }

        private async void OnConversationEnded(Transform t)
        {
            Debug.Log($"OnConversationEnded {t.name}");
            DialogueManager.Instance.conversationEnded -= OnConversationEnded;

            await _playerController.StandUp();
            State = States.MovableObject;
        } 
        #endregion

        #region MovableSpace
        private void State_MovableSpace()
        {
            _playerController.Movable(true);
            _playerController.Rotatable(true);

            _sofa.OnDown.AddListener(OnSofaDown);
        }

        private async void OnSofaDown()
        {
            _sofa.OnDown.RemoveListener(OnSofaDown);
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
        } 
        #endregion

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
        }
    } 
}
