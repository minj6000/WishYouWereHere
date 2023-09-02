using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP4
{
    public class DissolveInOnLocation : EnterColliderTriggerEvent
    {
        [SerializeField]
        string _locationName;
        public string LocationName => _locationName;

        [SerializeField]
        DissolveObject _dissolveObject;
        public DissolveObject DissolveObject => _dissolveObject;
    } 
}
