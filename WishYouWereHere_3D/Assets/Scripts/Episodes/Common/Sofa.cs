using UnityEngine;
using WishYouWereHere3D.TriggerEvents;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EPCommon
{
    public class Sofa : CenterCursorTriggerEvent
    {
        [SerializeField]
        Transform _sitTransform;

        public Transform SitTransform => _sitTransform;

        protected override void OnCenterCursorEnter()
        {
            base.OnCenterCursorEnter();

            InteractionGuide.Instance.Show(InteractionGuide.Icons.Mouse_L, "소파에 앉기");            
        }

        protected override void OnCenterCursorExit()
        {
            base.OnCenterCursorExit();

            InteractionGuide.Instance.Hide();
        }
    }
}