using extOSC;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.EPCommon;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP2
{
    public class Episode2 : MonoBehaviour
    {
        [SerializeField] PictureEventArea[] _pictureAreas;
        [SerializeField] Plaza _plaza;

        [SerializeField] FadeInOutController _fadeInOutController;
        [SerializeField] FrameCanvasManager _frameCanvasManager;

        [SerializeField] BicycleController _chenBicycleController;

        [SerializeField] TextAsset[] _chenFirstMoveDatas;

        public enum States
        {
            Ready,
            Prolog,
            Movable,
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
                    case States.Prolog:
                        State_Prolog();
                        break;
                    case States.Movable:
                        State_Movable();
                        break;
                    case States.Ending:
                        State_Ending();
                        break;
                }
            }
        }

        private async void State_Ending()
        {            
            await _fadeInOutController.FadeOut(2f);
            OSCController.Instance?.Send(Define.OSC_PROJECTORON_ADDRESS, OSCValue.Bool(false));
            SceneManager.LoadScene(Define.SCENE_LISTEN);
        }

        private void State_Movable()
        {
            InputHelper.EnableMouseControl(false);

            BicycleController.Instance.Movable(true);
            BicycleController.Instance.Rotatable(true);

            foreach (var pictureArea in _pictureAreas)
            {
                pictureArea.Enabled = true;

                var pa = pictureArea;
                pa.OnBeginTrigger.AddListener(() =>
                {
                    pa.OnBeginTrigger.RemoveAllListeners();
                    pa.Enabled = false;                    

                    BicycleController.Instance.Movable(false);

                    pa.PictureSubject.OnEnter.AddListener(() =>
                    {
                        pa.PictureSubject.OnEnter.RemoveAllListeners();
                        StartPictureSubjectEvent(pa.PictureSubject);
                    });
                });
            }

            _plaza.OnBeginTrigger.AddListener(() =>
            {
                _plaza.OnBeginTrigger.RemoveAllListeners();
                StartPlazaEvent();
            });
        }

        void StartPlazaEvent()
        {
            _plaza.Enabled = false;

            BicycleController.Instance.Movable(false);
            BicycleController.Instance.Rotatable(false);

            InputHelper.EnableMouseControl(true);
            DialogueManager.Instance.StartConversationWithEndedAction("EP2_광장", _ =>
            {
                var selectedItem = DialogueLua.GetVariable("EP2_선택아이템");

                OSCController.Instance?.Send(Define.OSC_EP2_RANDOMGIFT, OSCValue.Int(selectedItem.AsInt));

                InputHelper.EnableMouseControl(false);

                BicycleController.Instance.Movable(true);
                BicycleController.Instance.Rotatable(true);
            });
        }

        async void StartPictureSubjectEvent(PictureSubject pictureSubject)
        {
            pictureSubject.Enabled = false;

            BicycleController.Instance.Movable(false);
            BicycleController.Instance.Rotatable(false);

            await BicycleController.Instance.LookAt(pictureSubject.PictureTarget);

            DialogueLua.SetVariable("EP2_피사체", pictureSubject.Name);            

            InputHelper.EnableMouseControl(true);
            DialogueManager.Instance.StartConversationWithEndedAction("EP2_피사체_선택", async _ =>
            {
                InputHelper.EnableMouseControl(false);

                var result = DialogueLua.GetVariable("EP2_사진찍음");
                
                if(result.AsBool)
                {                    
                    await pictureSubject.TakePictureEffect();
                }

                switch (pictureSubject.Name)
                {
                    case "고양이":
                        OSCController.Instance?.Send(Define.OSC_EP2_CAT, OSCValue.Bool(result.AsBool));
                        break;
                    case "버스":
                        OSCController.Instance?.Send(Define.OSC_EP2_BUS, OSCValue.Bool(result.AsBool));
                        break;
                    case "아저씨":
                        OSCController.Instance?.Send(Define.OSC_EP2_PAUL, OSCValue.Bool(result.AsBool));
                        break;
                    case "카페":
                        OSCController.Instance?.Send(Define.OSC_EP2_CAFE, OSCValue.Bool(result.AsBool));
                        break;
                    case "첸":
                        OSCController.Instance?.Send(Define.OSC_EP2_CHEN, OSCValue.Bool(result.AsBool));
                        break;
                }

                await BicycleController.Instance.LookForward();

                BicycleController.Instance.Movable(true);
                BicycleController.Instance.Rotatable(true);

                if (pictureSubject.Name == "첸")
                {
                    State = States.Ending;
                }
            });
        }

        void StartMoveChen(bool value)
        {
            _chenBicycleController.Movable(value);
        }

        private void State_Prolog()
        {
            InputHelper.EnableMouseControl(true);

            Lua.RegisterFunction("StartMoveChen", this, 
                SymbolExtensions.GetMethodInfo(() => StartMoveChen(default)));
            DialogueManager.Instance.StartConversationWithEndedAction("EP2", _ =>
            {
                Lua.UnregisterFunction("StartMoveChen");
                State = States.Movable;
            });
        }



        async void Ready()
        {
            InputHelper.EnableMouseControl(false);

            BicycleController.Instance.Movable(false);
            BicycleController.Instance.Rotatable(false);
            
            await _fadeInOutController.FadeIn(2f);

            foreach (var pictureArea in _pictureAreas)
            {
                pictureArea.Enabled = false;
            }

            _chenBicycleController.LoadWaypointData(_chenFirstMoveDatas[Random.Range(0, _chenFirstMoveDatas.Length)].text);

            State = States.Prolog;
        }

        void Start()
        {
            Ready();
        }

        [Button]
        void AssignReferences()
        {
            _fadeInOutController = FindObjectOfType<FadeInOutController>();
            _plaza = FindObjectOfType<Plaza>();

            _frameCanvasManager = FindObjectOfType<FrameCanvasManager>();
            _pictureAreas = FindObjectsOfType<PictureEventArea>();
            _chenBicycleController = FindObjectsOfType<BicycleController>().FirstOrDefault(controller=> !controller.IsPlayer);
        }
    }
}