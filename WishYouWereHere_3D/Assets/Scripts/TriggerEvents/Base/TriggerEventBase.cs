using UnityEngine;

namespace WishYouWereHere3D.TriggerEvents
{
    public class TriggerEventBase : MonoBehaviour
    {
        protected Collider _collider;

        public bool Enabled
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
        }
    }
}