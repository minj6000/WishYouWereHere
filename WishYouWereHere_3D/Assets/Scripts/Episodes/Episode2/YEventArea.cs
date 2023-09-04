using Cysharp.Threading.Tasks;
using PixelCrushers.DialogueSystem;
using System;
using UnityEngine;
using WishYouWereHere3D.Common;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP2
{
    public class YEventArea : EnterColliderTriggerEvent
    {
        [SerializeField] Transform[] _areas;

        private void OnEnable()
        {
            OnBeginTrigger.AddListener(OnBeginTriggerHandler);
        }

        private void OnDisable()
        {
            OnBeginTrigger.RemoveListener(OnBeginTriggerHandler);
        }

        private async void OnBeginTriggerHandler()
        {
            BicycleController.Instance.Movable(false);
            BicycleController.Instance.Rotatable(false);

            foreach (var area in _areas)
            {
                await BicycleController.Instance.LookAt(area);
                await UniTask.Delay(500);
            }

            await BicycleController.Instance.LookForward();

            InputHelper.EnableMouseControl(true);
            DialogueManager.Instance.StartConversationWithEndedAction("EP2_갈림길", _ =>
            {
                InputHelper.EnableMouseControl(false);

                BicycleController.Instance.Movable(true);
                BicycleController.Instance.Rotatable(true);
            });
        }
    }
}