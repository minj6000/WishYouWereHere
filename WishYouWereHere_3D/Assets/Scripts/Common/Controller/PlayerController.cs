using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D
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

        [SerializeField] Rigidbody _rigidbody;

        [SerializeField] Transform _socketTransform;
        public Transform SocketTransform => _socketTransform;

        public MovableItem HoldingItem { get; private set; } = null;

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

        private void LateUpdate()
        {
            if(HoldingItem != null && HoldingItem.State == MovableItem.States.Holding)
            {
                HoldingItem.transform.position = SocketTransform.position;
                HoldingItem.transform.rotation = SocketTransform.rotation;
            }
        }

        public bool CanHoldItem()
        {
            if(HoldingItem != null)
            {
                return false;
            }
            return true;
        }

        public bool HoldItem(MovableItem item)
        {
            if(!CanHoldItem())
            {
                return false;
            }

            HoldingItem = item;
            return true;
        }

        public void ReleaseItem()
        {
            HoldingItem = null;
        }
    }
}