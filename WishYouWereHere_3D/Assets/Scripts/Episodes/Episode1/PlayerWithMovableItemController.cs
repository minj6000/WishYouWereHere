using UnityEngine;
using WishYouWereHere3D.Common;

namespace WishYouWereHere3D.EP1
{
    public class PlayerWithMovableItemController : PlayerController
    {
        [SerializeField] Transform _socketTransform;
        public Transform SocketTransform => _socketTransform;

        public MovableItem HoldingItem { get; private set; } = null;

        private void LateUpdate()
        {
            if (HoldingItem != null && HoldingItem.State == MovableItem.States.Holding)
            {
                HoldingItem.transform.position = SocketTransform.position;
                HoldingItem.transform.rotation = SocketTransform.rotation;
            }
        }

        public bool CanHoldItem()
        {
            if (HoldingItem != null)
            {
                return false;
            }
            return true;
        }

        public bool HoldItem(MovableItem item)
        {
            if (!CanHoldItem())
            {
                return false;
            }

            HoldingItem = item;
            return true;
        }

        public void ReleaseItem()
        {
            HoldingItem = null;
        }
    }
}