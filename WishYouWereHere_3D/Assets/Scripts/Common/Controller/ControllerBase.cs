using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace WishYouWereHere3D
{

    public abstract class ControllerBase : MonoBehaviour
    {
        [SerializeField] protected FirstPersonMovement _firstPersonMovement;
        [SerializeField] protected FirstPersonLook _firstPersonLook;

        public abstract void Movable(bool enable);

        public virtual void Rotatable(bool enable)
        {
            _firstPersonLook.enabled = enable;
        }

        public virtual async UniTask LookAt(Transform lookTransform)
        {
            Movable(false);
            Rotatable(false);

            var lookAtPosition = lookTransform.position;
            lookAtPosition.y = _firstPersonMovement.transform.position.y;

            _firstPersonMovement.transform.DOLookAt(lookAtPosition, 1f);
            await _firstPersonLook.transform.DOLookAt(lookTransform.position, 1f).AsyncWaitForCompletion();
        }

        public virtual async UniTask LookForward()
        {
            Movable(false);
            Rotatable(false);

            _firstPersonMovement.transform.DOLocalRotate(Vector3.zero, 1f);
            await _firstPersonLook.transform.DOLocalRotate(Vector3.zero, 1f).AsyncWaitForCompletion();
        }
    }
}