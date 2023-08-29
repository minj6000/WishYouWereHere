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
            
            var upRotation = Quaternion.LookRotation(lookTransform.position - _firstPersonMovement.transform.position);

            _firstPersonMovement.transform.DORotate(new Vector3(0, upRotation.eulerAngles.y, 0), 1f);
            await _firstPersonLook.transform.DORotate(new Vector3(upRotation.eulerAngles.x, 0, 0), 1f).AsyncWaitForCompletion();
        }

        public virtual async UniTask RotateForward()
        {
            Movable(false);
            Rotatable(false);

            _firstPersonMovement.transform.DORotate(Vector3.zero, 1f);
            await _firstPersonLook.transform.DORotate(Vector3.zero, 1f).AsyncWaitForCompletion();
        }
    }
}