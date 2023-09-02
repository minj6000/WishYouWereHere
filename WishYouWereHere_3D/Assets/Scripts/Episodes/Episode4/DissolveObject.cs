using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WishYouWereHere3D.EP4
{
    public class DissolveObject : MonoBehaviour
    {
        [SerializeField]
        Transform _lookTransform;
        public Transform LookTransform => _lookTransform;

        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }

}