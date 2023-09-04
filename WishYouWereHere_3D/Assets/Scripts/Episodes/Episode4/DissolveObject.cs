using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WishYouWereHere3D.EP4
{
    public class DissolveObject : MonoBehaviour
    {
        [SerializeField]
        Renderer[] _renderers;

        private void Reset()
        {
            AssignRenderers();
        }

        public async UniTask Show(float duration = 5f)
        {
            gameObject.SetActive(true);
            foreach (var renderer in _renderers)
            {
                foreach (var mat in renderer.materials)
                {
                    mat.SetFloat("_DissolveAmount", 0f);
                    mat.DOFloat(1.1f, "_DissolveAmount", duration);
                }
            }

            await UniTask.Delay((int)(1000 * duration));
        }

        public async UniTask Hide(float duration = 5f)
        {
            foreach (var renderer in _renderers)
            {
                foreach (var mat in renderer.materials)
                {
                    mat.SetFloat("_DissolveAmount", 1.1f);
                    mat.DOFloat(0f, "_DissolveAmount", duration);
                }
            }

            await UniTask.Delay((int)(1000 * duration));
        }

        [Button]
        void AssignRenderers()
        {
            _renderers = GetComponentsInChildren<Renderer>();
        }
    }

}