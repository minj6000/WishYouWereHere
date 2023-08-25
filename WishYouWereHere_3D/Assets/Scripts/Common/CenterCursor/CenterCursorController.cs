using UnityEngine;

namespace WishYouWereHere3D.Common.CenterCursor
{
    public class CenterCursorController : MonoBehaviour
    {
        [SerializeField] private float _maxDistance = 10f;
        GameObject enteredObject;

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, transform.forward * _maxDistance, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
            {
                if(enteredObject != hit.collider.gameObject)
                {
                    if(enteredObject != null)
                    {
                        enteredObject.SendMessage("OnCenterCursorExit", SendMessageOptions.DontRequireReceiver);
                    }

                    hit.collider.gameObject.SendMessage("OnCenterCursorEnter", SendMessageOptions.DontRequireReceiver);
                    enteredObject = hit.collider.gameObject;
                }
            }
            else
            {
                if(enteredObject != null)
                {
                    enteredObject.SendMessage("OnCenterCursorExit", SendMessageOptions.DontRequireReceiver);
                    enteredObject = null;
                }
            }
            if (enteredObject != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    enteredObject.SendMessage("OnCenterCursorDown", SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    enteredObject.SendMessage("OnCenterCursorUp", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}