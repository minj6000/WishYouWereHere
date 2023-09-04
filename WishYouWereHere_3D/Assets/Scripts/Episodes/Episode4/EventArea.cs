using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP4
{
    public class EventArea : CenterCursorTriggerEvent
    {
        [SerializeField] private Transform _lookTransform;
        public Transform LookTransform
        {
            get
            {
                if (_lookTransform == null)
                    return transform;

                return _lookTransform;
            }
        }

    } 
}
