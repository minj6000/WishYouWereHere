using extOSC;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.EPCommon;
using WishYouWereHere3D.TriggerEvents;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP1
{
    public class Episode1 : MonoBehaviour
    {
        [SerializeField] ItemDescriptionTrigger[] itemDescriptionTriggers;
        [SerializeField] LocationDescriptionTrigger[] locationDescriptionTriggers;
        [SerializeField] MovableItem[] movableItems;
        [SerializeField] MovableItemTarget[] movableItemTargets;

        [SerializeField] Sofa _sofa;
        [SerializeField] RecordPlayer _recordPlayer;
        [SerializeField] FadeInOutController _fadeInOutController;

        IDisposable _checkMoveItemCountDisposable;

        public enum States
        {
            Ready,
            MovableSpace,
            Dialogue,
            MoveItem,
            Ending            
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
                    case States.MoveItem:
                        State_MoveItem();
                        break;
                    case States.Ending:
                        State_Ending();
                        break;
                }
            }
        }

        async void EndOfContent()
        {            
            await _fadeInOutController.FadeOut(2f);
            OSCController.Instance?.Send(Define.OSC_PROJECTORON_ADDRESS, OSCValue.Bool(false));
            SceneManager.LoadScene(Define.SCENE_LISTEN);
        }

        private void State_Ending()
        {
            PlayerController.Instance.Movable(false);
            PlayerController.Instance.Rotatable(false);

            if (Configuration.Instance.ConversationControllerType == Configuration.ConversationController.Mouse)
            {
                InputHelper.EnableMouseControl(true);
            }

            DialogueManager.Instance.StartConversationWithEndedAction("EP1_Ending", _ =>
            {
                //완전 종료
                EndOfContent();
            });
        }

        private void State_MoveItem()
        {
            PlayerController.Instance.Movable(true);
            PlayerController.Instance.Rotatable(true);

            _recordPlayer.ChangeClickedPathAfterConversation();

            InputHelper.EnableMouseControl(false);

            foreach (var item in movableItems)
            {
                item.ClearValues();
                item.SetHoldable();
            }

            _checkMoveItemCountDisposable = gameObject.ObserveEveryValueChanged(_ => movableItems.Count(item => item.State == MovableItem.States.Released))
                .Subscribe(count =>
                {
                    if(count == 1)
                    {
                        string message = DialogueDatabaseHelper.Get("items\\EP1_TEXT_1");
                        DialogueManager.Instance.ShowAlert(message);
                    }
                    else if(count == 2)
                    {
                        _checkMoveItemCountDisposable.Dispose();
                        State = States.Ending;                        
                    }
                });
        }

        private void State_Dialogue()
        {
            PlayerController.Instance.Movable(false);
            PlayerController.Instance.Rotatable(false);

            if(Configuration.Instance.ConversationControllerType == Configuration.ConversationController.Mouse)
            {
                InputHelper.EnableMouseControl(true);
            }

            DialogueManager.Instance.StartConversationWithEndedAction("EP1", async _ =>
            {
                var complete = DialogueLua.GetVariable("EP1_대화완료").AsBool;
                OSCController.Instance?.Send(Define.OSC_EP1_LETTER, OSCValue.Bool(complete));

                await PlayerController.Instance.StandUp();
                State = States.MoveItem;
            });
        }

        private void State_MovableSpace()
        {
            PlayerController.Instance.Movable(true);
            PlayerController.Instance.Rotatable(true);

            _sofa.OnDown.AsObservable()
                .First()
                .Subscribe(async _ =>
                {
                    _sofa.Enabled = false;

                    foreach (var item in itemDescriptionTriggers)
                    {
                        item.ClearValues();
                    }
                    foreach (var item in locationDescriptionTriggers)
                    {
                        item.ClearValues();
                        item.Enabled = false;
                    }

                    //소파에 앉는 연출
                    await PlayerController.Instance.SitDown(_sofa.SitTransform);
                    State = States.Dialogue;
                });
        }

        private async void Ready()
        {
            InputHelper.EnableMouseControl(false);
            await _fadeInOutController.FadeIn(2f);

            State = States.MovableSpace;

            foreach (var item in movableItems)
            {
                item.ClearValues();
                item.Enabled = false;
            }
        }

        private void Reset()
        {
            AssignReferences();
        }

        private void Start()
        {
            Ready();
        }

        [Button]
        void AssignReferences()
        {
            itemDescriptionTriggers = FindObjectsOfType<ItemDescriptionTrigger>();
            locationDescriptionTriggers = FindObjectsOfType<LocationDescriptionTrigger>();
            movableItems = FindObjectsOfType<MovableItem>();
            movableItemTargets = FindObjectsOfType<MovableItemTarget>();
            
            _sofa = FindObjectOfType<Sofa>();
            _recordPlayer = FindObjectOfType<RecordPlayer>();
            _fadeInOutController = FindObjectOfType<FadeInOutController>();
        }
    } 
}
