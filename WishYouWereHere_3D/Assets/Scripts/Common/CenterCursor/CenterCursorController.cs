using UnityEngine;

namespace WishYouWereHere3D.Common.CenterCursor
{
    public class CenterCursorController : MonoBehaviour
    {
        [SerializeField] private float _maxDistance = 10f;

        private GameObject _enteredObject;
        public GameObject EnteredObject => _enteredObject;

        private void FixedUpdate()
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, transform.forward * _maxDistance, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
            {
                if (_enteredObject != hit.collider.gameObject)
                {
                    if (_enteredObject != null)
                    {
                        _enteredObject.SendMessage("OnCenterCursorExit", SendMessageOptions.DontRequireReceiver);
                    }

                    hit.collider.gameObject.SendMessage("OnCenterCursorEnter", SendMessageOptions.DontRequireReceiver);
                    _enteredObject = hit.collider.gameObject;
                }
            }
            else
            {
                if (_enteredObject != null)
                {
                    _enteredObject.SendMessage("OnCenterCursorExit", SendMessageOptions.DontRequireReceiver);
                    _enteredObject = null;
                }
            }
        }

        private void Update()
        {
            if (_enteredObject != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _enteredObject.SendMessage("OnCenterCursorDown", SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    _enteredObject.SendMessage("OnCenterCursorUp", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}