using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace WishYouWereHere3D
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] FirstPersonMovement _firstPersonMovement;
        [SerializeField] FirstPersonLook _firstPersonLook;
        [SerializeField] Rigidbody _rigidbody;

        Vector3 _lookOrgPosition;

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

        //앉는 애니메이션
        public async UniTask SitDown(Transform sitTransform)
        {
            Movable(false);
            Rotatable(false);

            //앉는 곳까지 이동
            _firstPersonMovement.transform.DOLookAt(sitTransform.position, 1f);
            await _firstPersonMovement.transform.DOMove(sitTransform.position, 1f).SetSpeedBased().AsyncWaitForCompletion();

            //시선을 앞으로
            _firstPersonMovement.transform.DORotate(Vector3.zero, 1f);            
            await _firstPersonLook.transform.DORotate(Vector3.zero, 1f).AsyncWaitForCompletion();

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