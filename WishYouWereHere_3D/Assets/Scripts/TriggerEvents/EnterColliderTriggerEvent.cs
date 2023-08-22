using UnityEngine;
using UnityEngine.Events;

namespace WishYouWereHere3D.TriggerEvents
{

    public class EnterColliderTriggerEvent
    {
        [SerializeField] string _colliderTag = "Player";

        public UnityEvent OnBeginTrigger;
        public UnityEvent OnEndTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == _colliderTag)
            {
                OnBeginTrigger?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == _colliderTag)
            {
                OnEndTrigger?.Invoke();
            }
        }
    }
    
}