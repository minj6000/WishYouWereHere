using UnityEngine;

namespace WishYouWereHere3D.EP3
{
    public class Episode3 : MonoBehaviour
    {
        public enum States
        {
            Ready,
            TalkAndTakePicture,
            Ending
        }

        private States _state = States.Ready;
        public States State
        {
            get => _state;
            private set
            {
                _state = value;
                switch (_state)
                {
                    case States.TalkAndTakePicture:
                        State_TalkAndTakePicture();
                        break;
                    case States.Ending:
                        State_Ending();
                        break;
                }
            }
        }

        private void State_Ending()
        {
            
        }

        private void State_TalkAndTakePicture()
        {
            
        }

        void Ready()
        {

        }

        void Start()
        {
            Ready();
        }
    }
}