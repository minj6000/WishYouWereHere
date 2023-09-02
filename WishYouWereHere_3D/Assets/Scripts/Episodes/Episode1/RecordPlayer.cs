using UnityEngine;
using WishYouWereHere3D.TriggerEvents;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP1
{
    public class RecordPlayer : ItemDescriptionTrigger
    {
        [SerializeField] string _textClickedPathAfterConversation;

        public void ChangeClickedPathAfterConversation()
        {
            _textClickedPath = _textClickedPathAfterConversation;
        }

        protected override void OnCenterCursorEnter()
        {
            base.OnCenterCursorEnter();
            InteractionGuide.Instance.Show(InteractionGuide.Icons.Mouse_L, "음악 바꾸기");
        }
    } 
}
