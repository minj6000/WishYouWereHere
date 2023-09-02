using MultiProjectorWarpSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.Util
{
    public class ProjectionWarpSystemModeChanger : MonoBehaviour
	{
		[SerializeField] int _canvasDisplayIndex = 0;
		[SerializeField] Camera _mainCamera;
		[SerializeField] Canvas _mainCanvas;
		[SerializeField] ProjectionWarpSystem _projectionWarpSystem;
		[SerializeField] FadeInOutController _fadeInOutController;
		[SerializeField] FrameCanvasManager _frameCanvasManager;

		[Button]
		void ChangeToNormalMode()
		{
            _mainCamera.enabled = true;
			_projectionWarpSystem.gameObject.SetActive(false);
			if(_mainCanvas != null)
			{
				_mainCanvas.targetDisplay = 0;
			}
            _fadeInOutController?.InstantiateCanvasesForAllDisplay();
            _frameCanvasManager?.InstantiateCanvasesForAllDisplay();

        }

		[Button]
		void ChangeToProjectionWarpSystem()
		{
			_mainCamera.enabled = false;
			_projectionWarpSystem.gameObject.SetActive(true);
			if(_mainCanvas != null)
			{
				_mainCanvas.targetDisplay = _canvasDisplayIndex;
			}
            _fadeInOutController?.InstantiateCanvasesForAllDisplay();
            _frameCanvasManager?.InstantiateCanvasesForAllDisplay();
        }
	}
}