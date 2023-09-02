using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.TriggerEvents;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP1
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

        [SerializeField] string _itemName;
        public string ItemName => _itemName;
        public Transform[] ReleasableTransforms => _releasableTransforms;

        Outline _outline;

        States _state = States.Idle;
        public States State => _state;

        PlayerWithMovableItemController _playerController;

        protected override void Awake()
        {
            base.Awake();
            _outline = GetComponent<Outline>();
            _playerController = PlayerController.Instance as PlayerWithMovableItemController;
        }

        private void Update()
        {
            if(_state != States.Holdable)
            {
                Enabled = false;
                return;
            }

            if (!_playerController.CanHoldItem())
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
            if (_playerController.CanHoldItem())
            {
                _playerController.HoldItem(this);
                _state = States.Holding;
            }
        }

        protected override void OnCenterCursorEnter()
        {
            base.OnCenterCursorEnter();

            InteractionGuide.Instance.Show(InteractionGuide.Icons.Mouse_L, $"{_itemName} 잡기");
        }

        override protected void OnCenterCursorExit()
        {
            base.OnCenterCursorExit();

            InteractionGuide.Instance.Hide();
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

                _playerController.ReleaseItem();                
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