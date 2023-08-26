using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace WishYouWereHere3D.UI
{
    public class FadeInOutController : MonoBehaviour
    {
        [SerializeField] float fadeTime = 1f;
        Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public async UniTask FadeIn()
        {
            await _image.DOFade(0, fadeTime).AsyncWaitForCompletion();
        }

        public async UniTask FadeOut()
        {
            await _image.DOFade(1f, fadeTime).AsyncWaitForCompletion();
        }
    }

}