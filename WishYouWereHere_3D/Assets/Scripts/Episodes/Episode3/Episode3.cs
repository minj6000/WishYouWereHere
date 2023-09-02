using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP3
{
    public class Episode3 : MonoBehaviour
    {
        [SerializeField] PictureSubject[] _pictureSubjects;
        [SerializeField] FadeInOutController _fadeInOutController;

        public enum States
        {
            Ready,
            Talk,
            TakePicture,
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
                    case States.Talk:
                        State_Talk();
                        break;
                    case States.TakePicture:
                        State_TakePicture();
                        break;
                    case States.Ending:
                        State_Ending();
                        break;
                }
            }
        }



        private void State_Ending()
        {
            InputHelper.EnableMouseControl(true);

            PlayerController.Instance.Movable(false);
            PlayerController.Instance.Rotatable(false);

            if (DialogueLua.GetVariable("EP3_완료아이템").AsInt == 3)
            {
                DialogueManager.Instance.StartConversationWithEndedAction("EP_3", async _ =>
                {
                    await _fadeInOutController.FadeOut(2f);
                });
            }
        }

        PictureSubject _selectedPictureSubject = null;
        IDisposable _updateDisposable = null;

        private void State_TakePicture()
        {
            InputHelper.EnableMouseControl(false);            
            _selectedPictureSubject.ReadyToTakePicture();

            InteractionGuide.Instance.Show(InteractionGuide.Icons.Space, "사진 찍기");

            _updateDisposable = gameObject.UpdateAsObservable()
                .Subscribe(async _ =>
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        InteractionGuide.Instance.Hide();
                        _updateDisposable.Dispose();
                        
                        await _selectedPictureSubject.TakePictureEffect();
                        
                        if(DialogueLua.GetVariable("EP3_완료아이템").AsInt == 3)
                        {
                            State = States.Ending;
                        }
                        else
                        {
                            State = States.Talk;
                        }
                    }                        
                });

            
        }

        private void State_Talk()
        {
            InputHelper.EnableMouseControl(false);

            PlayerController.Instance.Movable(true);
            PlayerController.Instance.Rotatable(true);
        }

        async void Ready()
        {
            InputHelper.EnableMouseControl(false);
            await _fadeInOutController.FadeIn(2f);

            foreach (var pictureSubject in _pictureSubjects)
            {
                var p = pictureSubject;
                p.OnDown.AddListener(() =>
                {
                    PlayerController.Instance.Movable(false);
                    PlayerController.Instance.Rotatable(false);

                    p.OnDown.RemoveAllListeners();
                    InputHelper.EnableMouseControl(true);

                    p.Enabled = false;
                    _selectedPictureSubject = p;
                    DialogueLua.SetVariable("EP3_선택아이템", _selectedPictureSubject.Name);
                    DialogueManager.Instance.StartConversationWithEndedAction("EP_3", async _ =>
                    {
                        await PlayerController.Instance.LookAt(_selectedPictureSubject.PictureTarget);
                        State = States.TakePicture;
                    });
                });
            }

            State = States.Talk;
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
            _fadeInOutController = FindObjectOfType<FadeInOutController>();
            _pictureSubjects = FindObjectsOfType<PictureSubject>();
        }
    }
}