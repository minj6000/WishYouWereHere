using UnityEngine;

namespace WishYouWereHere3D
{
    public class Episode1 : MonoBehaviour
    {
        public enum States
        {
            Ready,
            MovableSpace,
            Dialogue,
            MovableObject,
            End
        }

        private States _state = States.Ready;
        public States State { 
            get => _state;
            private set => _state = value;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    } 
}
