using Cysharp.Threading.Tasks;
using extOSC;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using WishYouWereHere3D.Common;

namespace WishYouWereHere3D.EPCommon
{
    public class OCSController : MonoBehaviour
    {
        [Header("OSC Settings")]
        OSCReceiver _receiver;
        OSCTransmitter _transmitter;

        static OCSController _instance = null;
        public static OCSController Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if(_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            _instance = this;

            _receiver = GetComponentInChildren<OSCReceiver>();
            _transmitter = GetComponentInChildren<OSCTransmitter>();

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _receiver.Bind(Define.OSC_ADDRESS, ReceivedMessage);
        }

        private void ReceivedMessage(OSCMessage message)
        {
            Debug.LogFormat("Received: {0}", message);

            foreach (var ocsValue in message.Values)
            {
                if(ocsValue.Type == OSCValueType.String)
                {
                    switch (ocsValue.StringValue)
                    {
                        case Define.SCENE_NAME_EP1:
                            LoadSceneAsync(Define.SCENE_NAME_EP1); 
                            break;
                        case Define.SCENE_NAME_EP2:
                            LoadSceneAsync(Define.SCENE_NAME_EP2);
                            break;
                        case Define.SCENE_NAME_EP3:
                            LoadSceneAsync(Define.SCENE_NAME_EP3);
                            break;
                        case Define.SCENE_NAME_EP4:
                            LoadSceneAsync(Define.SCENE_NAME_EP4);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Send(string address, string value)
        {
            Debug.LogFormat("Send: {0} {1}", address, value);

            var message = new OSCMessage(address);
            message.AddValue(OSCValue.String(value));

            _transmitter.Send(message);
        }

        public UniTask LoadSceneAsync(string sceneName, Action<float> progressAction = null)
        {
            IProgress<float> progress = null;
            if (progressAction != null)
            {
                progress = Progress.Create(progressAction);
            }
            else
            {
                progress = Progress.Create<float>(progress => Debug.Log($"Loading Scene : {progress * 100}%"));
            }

            return SceneManager.LoadSceneAsync(sceneName)
                .ToUniTask(progress: progress);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                Send(Define.OSC_ADDRESS, Define.SCENE_NAME_EP1);
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                Send(Define.OSC_ADDRESS, Define.SCENE_NAME_EP2);
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                Send(Define.OSC_ADDRESS, Define.SCENE_NAME_EP3);
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                Send(Define.OSC_ADDRESS, Define.SCENE_NAME_EP4);
            }
        }
    } 
}
