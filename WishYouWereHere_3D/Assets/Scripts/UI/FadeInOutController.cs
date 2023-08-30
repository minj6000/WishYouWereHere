using Cysharp.Threading.Tasks;
using DG.Tweening;
using MultiProjectorWarpSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WishYouWereHere3D.UI
{
    public class FadeInOutController : MonoBehaviour
    {
        Image[] _images;

        [SerializeField] GameObject _fadeInOutCanvasPrefab;

        private void Awake()
        {
            _images = GetComponentsInChildren<Image>();
        }

        public void SetColor(Color color)
        {
            Array.ForEach(_images, x => x.color = color);
        }

        public async UniTask FadeIn(float fadeTime = 1f)
        {
            await UniTask.WhenAll(_images.Select(x => x.DOFade(0f, fadeTime).AsyncWaitForCompletion().AsUniTask()));            
        }

        public async UniTask FadeOut(float fadeTime = 1f)
        {
            await UniTask.WhenAll(_images.Select(x => x.DOFade(1f, fadeTime).AsyncWaitForCompletion().AsUniTask()));
        }

        [Button]
        public void InstantiateCanvasesForAllDisplay()
        {
            DestroyAllCanvases();
            int displayIndex = 0;

            ProjectionWarpSystem projectionWarpSystem = FindObjectOfType<ProjectionWarpSystem>();
            do
            {
                var canvas = Instantiate(_fadeInOutCanvasPrefab, transform);
                canvas.name = $"FadeInOutCanvas_{displayIndex}";
                canvas.GetComponent<Canvas>().targetDisplay = displayIndex++;
            } while (projectionWarpSystem != null && projectionWarpSystem.sourceCameras.Count > displayIndex);
        }

        [Button]
        void DestroyAllCanvases()
        {
            List<Transform> childList = new List<Transform>();
            foreach(Transform child in transform)
            {
                childList.Add(child);                
            }

            foreach (var item in childList)
            {
                DestroyImmediate(item.gameObject);
            }
        }
    }

}