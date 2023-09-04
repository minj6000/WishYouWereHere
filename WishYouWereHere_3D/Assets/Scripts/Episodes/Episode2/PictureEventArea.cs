using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP2
{
    public class PictureEventArea : EnterColliderTriggerEvent
    {
        [SerializeField] private PictureSubject _pictureSubject;
        public PictureSubject PictureSubject
        {
            get { return _pictureSubject; }
        }
    }
}