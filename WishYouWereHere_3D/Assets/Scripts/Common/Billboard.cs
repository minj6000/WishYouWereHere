using UnityEngine;

namespace WishYouWereHere3D.Common
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] bool fixedYPosition = false;

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
                Vector3 targetPosition = target.position;
                if(fixedYPosition)
                {
                    targetPosition.y = transform.position.y;
                }

                transform.LookAt(targetPosition);
            }
        }        
    }
}