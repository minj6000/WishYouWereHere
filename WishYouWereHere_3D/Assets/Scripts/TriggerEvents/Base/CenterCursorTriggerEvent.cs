using UnityEngine;
using UnityEngine.Events;

namespace WishYouWereHere3D.TriggerEvents
{

    public class CenterCursorTriggerEvent : TriggerEventBase
    {
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public UnityEvent OnDown;
        public UnityEvent OnUp;

        protected virtual void OnCenterCursorEnter()
        {
            Debug.Log($"{name} {GetType().Name} OnCenterCursorEnter");
            OnEnter?.Invoke();
        }

        protected virtual void OnCenterCursorExit()
        {
            Debug.Log($"{name} {GetType().Name} OnCenterCursorExit");
            OnExit?.Invoke();
        }

        protected virtual void OnCenterCursorDown()
        {
            Debug.Log($"{name} {GetType().Name} OnCenterCursorDown");
            OnDown?.Invoke();
        }

        protected virtual void OnCenterCursorUp()
        {
            Debug.Log($"{name} {GetType().Name} OnCenterCursorUp");
            OnUp?.Invoke();
        }
    }    
}