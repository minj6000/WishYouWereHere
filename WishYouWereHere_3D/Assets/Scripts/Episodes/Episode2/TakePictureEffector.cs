using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections.Generic;

namespace WishYouWereHere3D.EP2
{
    public class TakePictureEffector : MonoBehaviour
    {
        Volume[] _volumes;

        [SerializeField] float postExposureValue = 3f;
        [SerializeField] float shutterTime = 0.2f;

        private void Start()
        {
            _volumes = FindObjectsOfType<Volume>();
        }

        public async UniTask TakePictureEffect()
        {
            await UniTask.Delay(500);

            List<UniTask> tasks = new List<UniTask>();
            foreach (var v in _volumes)
            {
                tasks.Add(UniTask.Create(async () =>
                {
                    bool willDestroyCa = false;
                    var orgProfile = v.profile;
                    if (!v.profile.TryGet(out ColorAdjustments ca))
                    {
                        ca = v.profile.Add<ColorAdjustments>(true);
                        willDestroyCa = true;
                    }
                    ca.postExposure.value = postExposureValue;

                    await UniTask.Delay((int)(1000 * shutterTime));
                    ca.postExposure.value = 0f;

                    if(willDestroyCa)
                    {
                        v.profile.Remove<ColorAdjustments>();
                        v.profile = orgProfile;
                    }
                }));
            }

            await UniTask.WhenAll(tasks);
        }
    }
}