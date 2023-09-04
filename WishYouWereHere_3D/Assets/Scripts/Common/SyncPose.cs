using UnityEngine;

namespace WishYouWereHere3D.Common
{
    public class SyncPose : MonoBehaviour
    {
        [SerializeField]
        Transform _targetTransform;

        private void LateUpdate()
        {
            transform.position = _targetTransform.position;
            transform.rotation = _targetTransform.rotation;
        }
    } 
}
