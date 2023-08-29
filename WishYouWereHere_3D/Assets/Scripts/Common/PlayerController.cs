using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D
{
    public class PlayerController : MonoBehaviour
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

        [SerializeField] FirstPersonMovement _firstPersonMovement;
        [SerializeField] FirstPersonLook _firstPersonLook;
        [SerializeField] Rigidbody _rigidbody;

        [SerializeField] Transform _socketTransform;
        public Transform SocketTransform => _socketTransform;

        Vector3 _lookOrgPosition;

        public MovableItem HoldingItem { get; private set; } = null;

        private void Awake()
        {
            _lookOrgPosition = _firstPersonLook.transform.localPosition;
        }

        public void Movable(bool enable)
        {
            _firstPersonMovement.enabled = enable;
            _rigidbody.isKinematic = !enable;
        }

        public void Rotatable(bool enable)
        {
            _firstPersonLook.enabled = enable;
        }

        public async UniTask LookAt(Transform lookTransform)
        {
            Movable(false);
            Rotatable(false);

            var upRotation = Quaternion.LookRotation(lookTransform.position - _firstPersonMovement.transform.position);

            _firstPersonMovement.transform.DOLocalRotate(new Vector3(0, upRotation.eulerAngles.y, 0), 1f);
            await _firstPersonLook.transform.DOLocalRotate(new Vector3(upRotation.eulerAngles.x, 0, 0), 1f).AsyncWaitForCompletion();
        }

        public async UniTask RotateForward()
        {
            Movable(false);
            Rotatable(false);

            _firstPersonMovement.transform.DORotate(Vector3.zero, 1f);
            await _firstPersonLook.transform.DORotate(Vector3.zero, 1f).AsyncWaitForCompletion();
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
            await RotateForward();

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