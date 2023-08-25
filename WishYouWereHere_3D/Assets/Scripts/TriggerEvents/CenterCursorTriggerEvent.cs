using UnityEngine;
using UnityEngine.Events;

namespace WishYouWereHere3D.TriggerEvents
{
    public class CenterCursorTriggerEvent : MonoBehaviour
    {
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public UnityEvent OnDown;
        public UnityEvent OnUp;

        protected virtual void OnCenterCursorEnter()
        {
            OnEnter?.Invoke();
        }

        protected virtual void OnCenterCursorExit()
        {
            OnExit?.Invoke();
        }

        protected virtual void OnCenterCursorDown()
        {
            OnDown?.Invoke();
        }

        protected virtual void OnCenterCursorUp()
        {
            OnUp?.Invoke();
        }
    }
    
}