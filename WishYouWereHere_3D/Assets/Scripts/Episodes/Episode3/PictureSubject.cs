using Cysharp.Threading.Tasks;
using DarkTonic.MasterAudio;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.EPCommon;
using WishYouWereHere3D.TriggerEvents;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP3
{
    public class PictureSubject : CenterCursorTriggerEvent
    {
		[SerializeField] string _name;
        [SerializeField] Transform _pictureTarget;
        [SerializeField] string _takePictureSoundName;

        FadeInOutController _fadeInOutController;
        FrameCanvasManager _frameCanvasManager;
        CameraHelper _cameraHelper;

        protected override void Awake()
        {
            base.Awake();
            _fadeInOutController = FindObjectOfType<FadeInOutController>();
            _frameCanvasManager = FindObjectOfType<FrameCanvasManager>();
            _cameraHelper = FindObjectOfType<CameraHelper>();
        }

        public string Name
		{
            get { return _name; }
        }

        public Transform PictureTarget
        {
            get 
            { 
                if(_pictureTarget == null)
                    return transform;

                return _pictureTarget; 
            }
        }

        public void PrePicture()
        {
            var animators = FindObjectsOfType<Animator>();
            if (animators != null)
            {
                foreach (var animator in animators)
                {
                    animator.enabled = false;
                }                
            }
        }

        public void PostPicture()
        {
            var animators = FindObjectsOfType<Animator>();
            if (animators != null)
            {
                foreach (var animator in animators)
                {
                    animator.enabled = true;
                }
            }
        }

        public void ReadyToTakePicture()
        {
            _frameCanvasManager.Show();
        }

        public async UniTask TakePictureEffect()
        {
            await UniTask.Delay(500);
            _fadeInOutController.SetColor(new Color(1, 1, 1, 0));

            if(!string.IsNullOrEmpty(_takePictureSoundName))
            {
                MasterAudio.PlaySound3DAtTransform(_takePictureSoundName, transform);
            }
            await _fadeInOutController.FadeOut(0.2f);
            {
                PrePicture();
                _cameraHelper.HideCameraFrame();
                _frameCanvasManager.Hide();
            }
            await _fadeInOutController.FadeIn(0.3f);

            await UniTask.Delay(1000);

            await _fadeInOutController.FadeOut(0.2f);
            {
                _cameraHelper.ZoomOrigin(0f).Forget();
                PostPicture();
            }
            await _fadeInOutController.FadeIn(0.3f);

            _fadeInOutController.SetColor(new Color(0, 0, 0, 0));
        }
    }
}