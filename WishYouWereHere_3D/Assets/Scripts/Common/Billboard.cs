using UnityEngine;

namespace WishYouWereHere3D.Common
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] Transform target;

        private void Start()
        {
            if(target == null)
            {
                target = Camera.main.transform;
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