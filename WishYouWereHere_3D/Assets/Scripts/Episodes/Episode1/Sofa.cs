using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP1
{
    public class Sofa : CenterCursorTriggerEvent
    {
        [SerializeField]
        Transform _sitTransform;

        public Transform SitTransform => _sitTransform;
    }
}