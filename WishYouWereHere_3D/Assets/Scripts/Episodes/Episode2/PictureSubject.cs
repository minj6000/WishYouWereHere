using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP2
{
    public class PictureSubject : CenterCursorTriggerEvent
    {
		[SerializeField] string _name;
        [SerializeField] Transform _pictureTarget;

        Animator _animator;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        public string Name
		{
            get { return _name; }
        }

        public Transform PictureTarget
        {
            get 
            { 
                if(_pictureTarget == null)
                    return transform;

                return _pictureTarget; 
            }
        }

        public void PrePicture()
        {
            if(_animator != null)
            {
                _animator.enabled = false;
            }
        }

        public void PostPicture()
        {
            if(_animator != null)
            {
                _animator.enabled = true;
            }
        }
    }
}