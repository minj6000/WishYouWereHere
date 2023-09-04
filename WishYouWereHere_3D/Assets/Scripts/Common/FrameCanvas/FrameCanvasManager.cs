using MultiProjectorWarpSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace WishYouWereHere3D.Common
{
    public class FrameCanvasManager : MonoBehaviour
    {
        [SerializeField] GameObject _frameCanvasPrefab;
        [SerializeField] List<FrameCanvas> frameCanvases;

        [Button]
        public void Show()
        {
            for (int i = 0; i < frameCanvases.Count; i++)
            {
                if (i == 0)
                {
                    frameCanvases[i].SetFrameCanvasType(FrameCanvas.FrameCanvasType.First);
                }

                frameCanvases[i].SetFrameCanvasType(FrameCanvas.FrameCanvasType.Up);
                frameCanvases[i].SetFrameCanvasType(FrameCanvas.FrameCanvasType.Down);

                if (i == frameCanvases.Count - 1)
                {
                    frameCanvases[i].SetFrameCanvasType(FrameCanvas.FrameCanvasType.Last);
                }
            }
        }

        [Button]
        public void Hide()
        {
            foreach (var frameCanvas in frameCanvases)
            {
                frameCanvas.HideAll();
            }
        }

        [Button]
        public void InstantiateCanvasesForAllDisplay()
        {
            DestroyAllCanvases();
            int displayIndex = 0;

            ProjectionWarpSystem projectionWarpSystem = FindObjectOfType<ProjectionWarpSystem>();
            do
            {
                var canvas = Instantiate(_frameCanvasPrefab, transform);
                canvas.name = $"FrameCanvas_{displayIndex}";
                canvas.GetComponent<Canvas>().targetDisplay = displayIndex++;
                var frameCanvas = canvas.GetComponent<FrameCanvas>();
                frameCanvas.HideAll();
                frameCanvases.Add(frameCanvas);
            } while (projectionWarpSystem != null && projectionWarpSystem.sourceCameras.Count > displayIndex);
        }

        [Button]
        void DestroyAllCanvases()
        {
            List<Transform> childList = new List<Transform>();
            foreach (Transform child in transform)
            {
                childList.Add(child);
            }

            foreach (var item in childList)
            {
                DestroyImmediate(item.gameObject);
            }

            frameCanvases.Clear();
        }
    }

}