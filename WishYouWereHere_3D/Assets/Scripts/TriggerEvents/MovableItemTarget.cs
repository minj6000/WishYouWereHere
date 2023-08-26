using System.Linq;
using UnityEngine;

namespace WishYouWereHere3D.TriggerEvents
{
    public class MovableItemTarget : CenterCursorTriggerEvent
    {
        Transform _targetTransform;
        public Transform TargetTransform => _targetTransform;

        [SerializeField] Transform[] _targetTransforms;

        private void Reset()
        {
            _targetTransforms = GetComponentsInChildren<Transform>().Where(x => x != transform).ToArray();
        }

        private void Update()
        {
            if(_targetTransform != null)
            {
                if (PlayerController.Instance.HoldingItem == null)
                {
                    Enabled = false;
                    _targetTransform = null;
                }
            }
            else
            {
                if (PlayerController.Instance.HoldingItem != null)
                {
                    var itemTarget = PlayerController.Instance.HoldingItem.ReleasableTransforms.Where(x => _targetTransforms.Contains(x.transform)).FirstOrDefault();
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

            if(PlayerController.Instance.HoldingItem != null)
            {
                PlayerController.Instance.HoldingItem.ShowToReleasePosition(_targetTransform);
            }
        }

        override protected void OnCenterCursorExit()
        {
            base.OnCenterCursorExit();

            if (_targetTransform == null)
            {
                return;
            }

            if (PlayerController.Instance.HoldingItem != null)
            {
                PlayerController.Instance.HoldingItem.CancelToReleasePosition();
            }
        }

        protected override void OnCenterCursorDown()
        {
            base.OnCenterCursorDown();

            if (_targetTransform == null)
            {
                return;
            }

            if (PlayerController.Instance.HoldingItem != null)
            {
                PlayerController.Instance.HoldingItem.ConfirmToReleasePostion();
            }
        }
    } 
}
