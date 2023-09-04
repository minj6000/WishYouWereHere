using extOSC;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.EPCommon;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP3
{
    public class Episode3 : MonoBehaviour
    {
        [SerializeField] PictureSubject[] _pictureSubjects;
        [SerializeField] FadeInOutController _fadeInOutController;
        [SerializeField] CameraHelper _cameraHelper;

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
                    var item = DialogueLua.GetVariable("EP3_선택아이템");
                    switch (item.AsString)
                    {
                        case "드레스":
                            OSCController.Instance?.Send(Define.OSC_EP3_GIFT, OSCValue.Int(0));
                            break;
                        case "장갑":
                            OSCController.Instance?.Send(Define.OSC_EP3_GIFT, OSCValue.Int(1));
                            break;
                        case "도덕경":
                            OSCController.Instance?.Send(Define.OSC_EP3_GIFT, OSCValue.Int(2));
                            break;
                        default:
                            break;
                    }

                    await _fadeInOutController.FadeOut(2f);
                    OSCController.Instance?.Send(Define.OSC_PROJECTORON_ADDRESS, OSCValue.Bool(false));
                    SceneManager.LoadScene(Define.SCENE_LISTEN);
                });
            }
        }

        PictureSubject _selectedPictureSubject = null;
        IDisposable _updateDisposable = null;

        private void State_TakePicture()
        {
            InputHelper.EnableMouseControl(false);            
            //_selectedPictureSubject.ReadyToTakePicture();
            _cameraHelper.ShowCameraFrame();
            _cameraHelper.ManualZoomIn(true);

            InteractionGuide.Instance.Show(InteractionGuide.Icons.Mouse_L, "사진 찍기");

            _updateDisposable = gameObject.UpdateAsObservable()
                .Subscribe(async _ =>
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _cameraHelper.ManualZoomIn(false);
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
            _cameraHelper = FindObjectOfType<CameraHelper>();
        }
    }
}