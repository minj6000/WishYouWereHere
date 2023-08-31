using Cysharp.Threading.Tasks;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP2
{
    public class Episode2 : MonoBehaviour
    {
        [SerializeField] EventArea[] _pictureAreas;
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
            DialogueManager.Instance.StartConversationWithEndedAction("EP2_����", _ =>
            {
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

            DialogueLua.SetVariable("EP2_�ǻ�ü", pictureSubject.Name);

            InputHelper.EnableMouseControl(true);
            DialogueManager.Instance.StartConversationWithEndedAction("EP2_�ǻ�ü_����", async _ =>
            {
                InputHelper.EnableMouseControl(false);

                var result = DialogueLua.GetVariable("EP2_��������");
                
                if(result.AsBool)
                {
                    await TakePictureEffect(pictureSubject);
                }

                BicycleController.Instance.Movable(true);
                BicycleController.Instance.Rotatable(true);

                if (pictureSubject.Name == "þ")
                {
                    State = States.Ending;
                }
            });
        }

        async UniTask TakePictureEffect(PictureSubject pictureSubject)
        {
            await UniTask.Delay(500);
            _fadeInOutController.SetColor(new Color(1, 1, 1, 0));
            await _fadeInOutController.FadeOut(0.2f);
            {
                pictureSubject.PrePicture();
                _frameCanvasManager.Show();
            }
            await _fadeInOutController.FadeIn(0.3f);
            
            await UniTask.Delay(3000);

            await _fadeInOutController.FadeOut(0.2f);
            {
                _frameCanvasManager.Hide();
                pictureSubject.PostPicture();
            }
            await _fadeInOutController.FadeIn(0.3f);

            _fadeInOutController.SetColor(new Color(0, 0, 0, 0));
        }

        private void State_Prolog()
        {
            InputHelper.EnableMouseControl(true);
            _chenBicycleController.Movable(true);
            DialogueManager.Instance.StartConversationWithEndedAction("EP2", _ =>
            {
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
            _pictureAreas = FindObjectsOfType<EventArea>();
            _chenBicycleController = FindObjectsOfType<BicycleController>().FirstOrDefault(controller=> !controller.IsPlayer);
        }
    }
}