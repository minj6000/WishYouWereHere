using Cysharp.Threading.Tasks;
using extOSC;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using WishYouWereHere3D.Common;

namespace WishYouWereHere3D.EPCommon
{
    public class OSCController : MonoBehaviour
    {
        [Header("OSC Settings")]
        OSCReceiver _receiver;
        OSCTransmitter _transmitter;

        static OSCController _instance = null;
        public static OSCController Instance
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
            _receiver.Bind(Define.OSC_PROJECTORON_ADDRESS, ReceivedMessage);
            _receiver.Bind(Define.OSC_EVENTNUM_ADDRESS, ReceivedMessage);
        }

        private void ReceivedMessage(OSCMessage message)
        {            
            Debug.LogFormat("Received: {0}", message);

            if (message.Address == Define.OSC_EVENTNUM_ADDRESS)
            {
                foreach (var oscValue in message.Values)
                {
                    if(oscValue.Type == OSCValueType.Int)
                    {
                        switch (oscValue.IntValue)
                        {
                            case 0:
                                LoadSceneAsync(Define.SCENE_NAME_EP1);
                                break;
                            case 1:
                                LoadSceneAsync(Define.SCENE_NAME_EP2);
                                break;
                            case 2:
                                LoadSceneAsync(Define.SCENE_NAME_EP3);
                                break;
                            case 3:
                                LoadSceneAsync(Define.SCENE_NAME_EP4);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public void Send(string address, OSCValue value)
        {
            Debug.LogFormat("Send: {0} {1}", address, value);

            var message = new OSCMessage(address);
            message.AddValue(value);

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
    } 
}
