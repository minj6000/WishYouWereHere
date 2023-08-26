using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace WishYouWereHere3D.TriggerEvents
{
    public class MovableItem : CenterCursorTriggerEvent
    {
        public enum States
        {
            Idle,
            Holdable,
            Holding,
            Releasing,
            Released,
        }
            
        
        [SerializeField] Vector3 _originPosition;
        [SerializeField] Vector3 _originRotation;

        [SerializeField] Transform[] _releasableTransforms;
        [SerializeField] bool _reHoldable = false;  
        public Transform[] ReleasableTransforms => _releasableTransforms;

        Outline _outline;

        States _state = States.Idle;
        public States State => _state;

        protected override void Awake()
        {
            base.Awake();
            _outline = GetComponent<Outline>();
        }

        private void Update()
        {
            if(_state != States.Holdable)
            {
                Enabled = false;
                return;
            }

            if (!PlayerController.Instance.CanHoldItem())
            {
                Enabled = false;
            }
            else
            {
                Enabled = true;
            }
        }

        public void SetHoldable()
        {
            _state = States.Holdable;
        }

        protected override void OnCenterCursorDown()
        {
            if (PlayerController.Instance.CanHoldItem())
            {
                PlayerController.Instance.HoldItem(this);
                _state = States.Holding;
            }
        }

        public void ShowToReleasePosition(Transform targetTransform)
        {
            if(!ReleasableTransforms.Contains(targetTransform))
            {
                return;
            }

            _state = States.Releasing;
            _outline.enabled = true;
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
        }

        public void CancelToReleasePosition()
        {
            _outline.enabled = false;
            _state = States.Holding;
        }

        public void ConfirmToReleasePostion()
        {
            if(_state == States.Releasing)
            {
                _state = States.Released;
                _outline.enabled = false;

                PlayerController.Instance.ReleaseItem();                
                if(_reHoldable)
                {
                    _state = States.Holdable;
                }

            }
        }

        public void ClearValues()
        {
            _state = States.Idle;
            _outline.enabled = false;
        }

        //[Button]
        void WriteOrigin()
        {
            _originPosition = transform.position;
            _originRotation = transform.eulerAngles;
        }

        [Button]
        void MoveToOrigin()
        {
            transform.position = _originPosition;
            transform.rotation = Quaternion.Euler(_originRotation);
        }

        [Button]
        void MoveToReleasableTransform(int index)
        {
            transform.position = _releasableTransforms[index].position;
            transform.rotation = _releasableTransforms[index].rotation;
        }
    }
}