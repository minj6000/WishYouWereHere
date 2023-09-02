using System;
using UniRx;
using UnityEngine;

namespace WishYouWereHere3D.Common
{
    public class CenterCursorController : MonoBehaviour
    {
        [SerializeField] private float _maxDistance = 10f;

        private GameObject _enteredObject;
        public GameObject EnteredObject => _enteredObject;

        private static CenterCursorController _instance;
        public static CenterCursorController Instance => _instance;

        private GameObject _tempEnteredObject;
        Subject<bool> EnterEventSubject = new Subject<bool>();        

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            EnterEventSubject
                .DistinctUntilChanged()
                .Throttle(TimeSpan.FromSeconds(0.2f))
                .Subscribe(x =>
                {
                    if (x)
                    {
                        if(_enteredObject != null)
                        {
                            _enteredObject.SendMessage("OnCenterCursorExit", SendMessageOptions.DontRequireReceiver);
                        }

                        _enteredObject = _tempEnteredObject;
                        _enteredObject.SendMessage("OnCenterCursorEnter", SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {          
                        if(_enteredObject != null)
                        {
                            _enteredObject.SendMessage("OnCenterCursorExit", SendMessageOptions.DontRequireReceiver);
                            _enteredObject = null;
                        }
                    }
                });
        }

        private void FixedUpdate()
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, transform.forward * _maxDistance, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
            {
                if (_tempEnteredObject != hit.collider.gameObject)
                {
                    if (_tempEnteredObject != null)
                    {
                        EnterEventSubject.OnNext(false);
                    }

                    EnterEventSubject.OnNext(true);
                    _tempEnteredObject = hit.collider.gameObject;                    
                }
            }
            else
            {
                if (_tempEnteredObject != null)
                {
                    EnterEventSubject.OnNext(false);
                    _tempEnteredObject = null;
                }
            }
        }

        private void Update()
        {
            if (_enteredObject != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _enteredObject.SendMessage("OnCenterCursorDown", SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    _enteredObject.SendMessage("OnCenterCursorUp", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}