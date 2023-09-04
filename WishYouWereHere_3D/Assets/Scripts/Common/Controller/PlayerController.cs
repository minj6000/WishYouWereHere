using Cysharp.Threading.Tasks;
using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;

namespace WishYouWereHere3D.Common
{
    public class PlayerController : ControllerBase
    {
        static PlayerController _instance = null;
        public static PlayerController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<PlayerController>();
                }
                return _instance;
            }
        }

        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected string _sitSoundName;

        Vector3 _lookOrgPosition;

        void Awake()
        {
            _lookOrgPosition = _firstPersonLook.transform.localPosition;
        }

        public override void Movable(bool enable)
        {
            _firstPersonMovement.enabled = enable;
            _rigidbody.isKinematic = !enable;
        }

        //앉는 애니메이션
        public async UniTask SitDown(Transform sitTransform)
        {
            Movable(false);
            Rotatable(false);

            //앉는 곳까지 이동
            LookAt(sitTransform).Forget();
            await _firstPersonMovement.transform.DOMove(sitTransform.position, 1f).SetSpeedBased().AsyncWaitForCompletion();

            //시선을 앞으로
            await LookForward();

            if(!string.IsNullOrEmpty(_sitSoundName))
            {
                MasterAudio.PlaySound3DAtTransform(_sitSoundName, sitTransform);
            }
            //카메라를 조금 밑으로 이동하여 앉는느낌을 줌
            await _firstPersonLook.transform.DOLocalMoveY(_lookOrgPosition.y - 0.5f, 1f).SetEase(Ease.InQuart).AsyncWaitForCompletion();
        }

        //일어나는 애니메이션
        public async UniTask StandUp()
        {
            Movable(false);
            Rotatable(false);

            await _firstPersonLook.transform.DOLocalMoveY(_lookOrgPosition.y, 1f).SetEase(Ease.OutQuart).AsyncWaitForCompletion();
        }
    }
}