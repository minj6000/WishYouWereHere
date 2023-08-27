using UnityEngine;
using WishYouWereHere3D.Common.CenterCursor;

namespace WishYouWereHere3D.Common
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] Transform target;

        private void Start()
        {
            if(target == null)
            {
                if (CenterCursorController.Instance != null)
                {
                    target = CenterCursorController.Instance.transform;
                }
                else
                {
                    if (Camera.main != null)
                    {
                        target = Camera.main.transform;
                    }
                }
            }
        }

        private void Reset()
        {
            if (CenterCursorController.Instance != null)
            {
                target = CenterCursorController.Instance.transform;
            }
            else
            {
                if (Camera.main != null)
                {
                    target = Camera.main.transform;
                }
            }
        }

        void Update()
        {
            if(target != null)
            {
                transform.LookAt(target);
            }
        }        
    }
}