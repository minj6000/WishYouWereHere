using Cysharp.Threading.Tasks;
using DG.Tweening;
using MultiProjectorWarpSystem;
using UnityEngine;

namespace WishYouWereHere3D.EPCommon
{
    public class CameraHelper : MonoBehaviour
    {
        [SerializeField]
        Camera _playerCamera;

        [SerializeField]
        ProjectionWarpSystem _projectionWarpSystem;

        [SerializeField]
        GameObject _cameraFrame;

        float _originFOV;
        bool _manualZoomInOut = false;

        [SerializeField]
        float _manualZoomSpeed = 2f;

        [SerializeField]
        float _limitFOV = 40;

        public float FOV
        {
            get
            {
                if(_playerCamera.enabled)
                {
                    return _playerCamera.fieldOfView;
                }
                else if(_projectionWarpSystem.enabled)
                {
                    return _projectionWarpSystem.fieldOfView;
                }

                return 0;
            }

            set
            {
                if(_playerCamera.enabled)
                {
                    _playerCamera.fieldOfView = value;
                }
                else if(_projectionWarpSystem.enabled)
                {
                    _projectionWarpSystem.fieldOfView = value;
                }
            }   
        }

        private void Start()
        {
            _originFOV = FOV;

            HideCameraFrame();
        }

        public UniTask ZoomIn(float duration)
        {
            return DOTween.To(() => FOV, x => FOV = x, _originFOV - _limitFOV, duration)
                .AsyncWaitForCompletion()
                .AsUniTask();
        }

        public UniTask ZoomOut(float duration)
        {
            return DOTween.To(() => FOV, x => FOV = x, _originFOV + _limitFOV, duration)
                .AsyncWaitForCompletion()
                .AsUniTask();
        }

        public UniTask ZoomOrigin(float duration)
        {
            return DOTween.To(() => FOV, x => FOV = x, _originFOV, duration)
                .AsyncWaitForCompletion()
                .AsUniTask();
        }

        public void ManualZoomIn(bool enable)
        {
            _manualZoomInOut = enable;
        }

        public void ShowCameraFrame()
        {
            if(_cameraFrame != null && _playerCamera.enabled)
                _cameraFrame.SetActive(true);
        }

        public void HideCameraFrame()
        {
            if(_cameraFrame != null)
                _cameraFrame.SetActive(false);
        }

        private void Update()
        {
            if(_manualZoomInOut)
            {
                if(Input.mouseScrollDelta.y > 0)
                {
                    FOV -= _manualZoomSpeed;
                }
                else if(Input.mouseScrollDelta.y < 0)
                {
                    FOV += _manualZoomSpeed;
                }

                FOV = Mathf.Clamp(FOV, _originFOV - _limitFOV, _originFOV + _limitFOV);
            }
        }
    }
}