using Cysharp.Threading.Tasks;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP2
{
    public class Episode2 : MonoBehaviour
    {
        [SerializeField] PictureSubject[] _pictureSubjects;
        [SerializeField] Plaza _plaza;

        [SerializeField] FadeInOutController _fadeInOutController;
        [SerializeField] FrameCanvasManager _frameCanvasManager;

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

            PlayerController.Instance.Movable(true);
            PlayerController.Instance.Rotatable(true);

            foreach (var pictureSubject in _pictureSubjects)
            {
                pictureSubject.Enabled = true;

                var ps = pictureSubject;
                pictureSubject.OnEnter.AddListener(() =>
                {
                    StartPictureSubjectEvent(ps);
                });
            }

            _plaza.OnBeginTrigger.AddListener(() =>
            {
                StartPlazaEvent();
            });
        }

        void StartPlazaEvent()
        {
            _plaza.Enabled = false;

            PlayerController.Instance.Movable(false);
            PlayerController.Instance.Rotatable(false);

            InputHelper.EnableMouseControl(true);
            DialogueManager.Instance.StartConversationWithEndedAction("EP2_����", _ =>
            {
                InputHelper.EnableMouseControl(false);

                PlayerController.Instance.Movable(true);
                PlayerController.Instance.Rotatable(true);
            });
        }

        async void StartPictureSubjectEvent(PictureSubject pictureSubject)
        {
            pictureSubject.Enabled = false;

            PlayerController.Instance.Movable(false);
            PlayerController.Instance.Rotatable(false);

            await PlayerController.Instance.LookAt(pictureSubject.transform);

            DialogueLua.SetVariable("EP2_�ǻ�ü", pictureSubject.Name);

            InputHelper.EnableMouseControl(true);
            DialogueManager.Instance.StartConversationWithEndedAction("EP2_�ǻ�ü_����", async _ =>
            {
                InputHelper.EnableMouseControl(false);

                var result = DialogueLua.GetVariable("EP2_��������");
                
                if(result.AsBool)
                {
                    await TakePictureEffect();
                }

                PlayerController.Instance.Movable(true);
                PlayerController.Instance.Rotatable(true);

                if (pictureSubject.Name == "þ")
                {
                    State = States.Ending;
                }
            });
        }

        async UniTask TakePictureEffect()
        {
            await UniTask.Delay(500);
            _fadeInOutController.SetColor(new Color(1, 1, 1, 0));
            await _fadeInOutController.FadeOut(0.2f);
            _frameCanvasManager.Show();
            await _fadeInOutController.FadeIn(0.3f);

            await UniTask.Delay(3000);

            await _fadeInOutController.FadeOut(0.2f);
            _frameCanvasManager.Hide();
            await _fadeInOutController.FadeIn(0.3f);

            _fadeInOutController.SetColor(new Color(0, 0, 0, 0));
        }

        private void State_Prolog()
        {
            InputHelper.EnableMouseControl(true);
            DialogueManager.Instance.StartConversationWithEndedAction("EP2", _ =>
            {
                State = States.Movable;
            });
        }

        async void Ready()
        {
            InputHelper.EnableMouseControl(false);

            PlayerController.Instance.Movable(false);
            PlayerController.Instance.Rotatable(false);
            
            await _fadeInOutController.FadeIn(2f);

            foreach (var pictureSubject in _pictureSubjects)
            {
                pictureSubject.Enabled = false;
            }

            State = States.Prolog;
        }

        void Start()
        {
            Ready();
        }

        [Button]
        void AssignReferences()
        {
            _pictureSubjects = FindObjectsOfType<PictureSubject>();
            _fadeInOutController = FindObjectOfType<FadeInOutController>();
            _plaza = FindObjectOfType<Plaza>();

            _frameCanvasManager = FindObjectOfType<FrameCanvasManager>();
        }
    }
}