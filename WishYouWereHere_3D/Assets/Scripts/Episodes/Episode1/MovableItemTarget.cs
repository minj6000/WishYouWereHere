using System.Linq;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.TriggerEvents;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP1
{
    public class MovableItemTarget : CenterCursorTriggerEvent
    {
        Transform _targetTransform;
        public Transform TargetTransform => _targetTransform;

        [SerializeField] Transform[] _targetTransforms;

        PlayerWithMovableItemController _playerWithMovableItemController;

        protected override void Awake()
        {
            base.Awake();
            _playerWithMovableItemController = PlayerController.Instance as PlayerWithMovableItemController;
        }

        private void Reset()
        {
            _targetTransforms = GetComponentsInChildren<Transform>().Where(x => x != transform).ToArray();
        }

        private void Update()
        {
            if(_targetTransform != null)
            {
                if (_playerWithMovableItemController.HoldingItem == null)
                {
                    Enabled = false;
                    _targetTransform = null;
                }
            }
            else
            {
                if (_playerWithMovableItemController.HoldingItem != null)
                {
                    var itemTarget = _playerWithMovableItemController.HoldingItem.ReleasableTransforms.Where(x => _targetTransforms.Contains(x.transform)).FirstOrDefault();
                    if (itemTarget != null)
                    {
                        Enabled = true;
                        _targetTransform = itemTarget.transform;
                    }
                }
            }
        }

        public void ClearValues()
        {
            _targetTransform = null;
        }

        protected override void OnCenterCursorEnter()
        {
            base.OnCenterCursorEnter();

            if(_targetTransform == null)
            {
                return;
            }

            if(_playerWithMovableItemController.HoldingItem != null)
            {
                _playerWithMovableItemController.HoldingItem.ShowToReleasePosition(_targetTransform);
            }

            InteractionGuide.Instance.Show(InteractionGuide.Icons.Mouse_L, $"{_playerWithMovableItemController.HoldingItem.ItemName} 놓기");
        }

        override protected void OnCenterCursorExit()
        {
            base.OnCenterCursorExit();

            if (_targetTransform == null)
            {
                return;
            }

            if (_playerWithMovableItemController.HoldingItem != null)
            {
                _playerWithMovableItemController.HoldingItem.CancelToReleasePosition();
            }

            InteractionGuide.Instance.Hide();
        }

        protected override void OnCenterCursorDown()
        {
            base.OnCenterCursorDown();

            if (_targetTransform == null)
            {
                return;
            }

            if (_playerWithMovableItemController.HoldingItem != null)
            {
                _playerWithMovableItemController.HoldingItem.ConfirmToReleasePostion();
                InteractionGuide.Instance.Hide();
            }
        }
    } 
}
