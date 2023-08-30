using System;
using UnityEngine;

namespace WishYouWereHere3D.EP2
{
    public class FrameCanvas : MonoBehaviour
    {
        [Flags]
        public enum FrameCanvasType
        {
            Left = 1,
            Right = 2,
            Up = 4,
            Down = 8,
            First = Left | Up | Down,
            Last = Right | Up | Down,
            All = First | Last
        }
        
        [SerializeField] GameObject _left;
        [SerializeField] GameObject _right;
        [SerializeField] GameObject _up;
        [SerializeField] GameObject _down;

        private void Start()
        {
            
        }

        public void SetFrameCanvasType(FrameCanvasType type)
        {
            if((type & FrameCanvasType.Left) == FrameCanvasType.Left)
            {
                _left.SetActive(true);
            }
            if ((type & FrameCanvasType.Right) == FrameCanvasType.Right)
            {
                _right.SetActive(true);
            }
            if ((type & FrameCanvasType.Up) == FrameCanvasType.Up)
            {
                _up.SetActive(true);
            }
            if ((type & FrameCanvasType.Down) == FrameCanvasType.Down)
            {
                _down.SetActive(true);
            }
        }

        public void HideAll()
        {
            _left.SetActive(false);
            _right.SetActive(false);
            _up.SetActive(false);
            _down.SetActive(false);
        }


    }

}