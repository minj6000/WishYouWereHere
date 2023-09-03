using Cysharp.Threading.Tasks;
using DG.Tweening;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.EPCommon;
using WishYouWereHere3D.TriggerEvents;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP2
{
    public class PictureSubject : CenterCursorTriggerEvent
    {
		[SerializeField] string _name;
        [SerializeField] Transform _pictureTarget;        

        FadeInOutController _fadeInOutController;
        FrameCanvasManager _frameCanvasManager;
        CameraHelper _cameraHelper;

        Animator _animator;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();

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
            if(_animator != null)
            {
                _animator.enabled = false;
            }            
        }

        public void PostPicture()
        {
            if(_animator != null)
            {
                _animator.enabled = true;
            }
        }

        public async UniTask TakePictureEffect()
        {
            _cameraHelper.ShowCameraFrame();
            await _cameraHelper.ZoomIn(1f);

            await UniTask.Delay(500);
            _fadeInOutController.SetColor(new Color(1, 1, 1, 0));
            await _fadeInOutController.FadeOut(0.2f);
            {
                _cameraHelper.HideCameraFrame();

                PrePicture();                
                _frameCanvasManager.Show();
            }
            await _fadeInOutController.FadeIn(0.3f);

            await UniTask.Delay(3000);

            await _fadeInOutController.FadeOut(0.2f);
            {
                _frameCanvasManager.Hide();                
                _cameraHelper.ZoomOut(0f).Forget();
                PostPicture();
            }
            await _fadeInOutController.FadeIn(0.3f);

            DialogueManager.Instance.ShowAlert("사진첩에 저장되었습니다.");
            

            _fadeInOutController.SetColor(new Color(0, 0, 0, 0));
        }
    }
}