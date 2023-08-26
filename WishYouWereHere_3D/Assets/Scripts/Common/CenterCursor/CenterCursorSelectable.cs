using UnityEngine;

namespace WishYouWereHere3D.Common.CenterCursor
{
    [RequireComponent(typeof(Outline))]
    public class CenterCursorSelectable : MonoBehaviour
    {
        Outline outline;
        // Start is called before the first frame update
        void Start()
        {
            outline = GetComponent<Outline>();
            outline.enabled = false;
        }

        void OnCenterCursorEnter()
        {
            outline.enabled = true;
        }

        void OnCenterCursorExit()
        {
            outline.enabled = false;
        }

        private void OnDisable()
        {
            outline.enabled = false;
        }
    } 
}
