using UnityEngine;
using UnityEngine.Events;

namespace WishYouWereHere3D.TriggerEvents
{

    public class EnterColliderTriggerEvent : TriggerEventBase
    {
        [SerializeField] string _colliderTag = "Player";

        public UnityEvent OnBeginTrigger;
        public UnityEvent OnEndTrigger;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == _colliderTag)
            {
                Debug.Log("OnTriggerEnter " + _colliderTag);
                OnBeginTrigger?.Invoke();
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == _colliderTag)
            {
                Debug.Log("OnTriggerExit " + _colliderTag);
                OnEndTrigger?.Invoke();
            }
        }
    }
    
}