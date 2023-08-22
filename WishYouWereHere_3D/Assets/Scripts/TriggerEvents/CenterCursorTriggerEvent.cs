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

        void OnCenterCursorEnter()
        {
            OnEnter?.Invoke();
        }

        void OnCenterCursorExit()
        {
            OnExit?.Invoke();
        }

        void OnCenterCursorDown()
        {
            OnDown?.Invoke();
        }

        void OnCenterCursorUp()
        {
            OnUp?.Invoke();
        }
    }
    
}