using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP1
{
    public class RecordPlayer : ItemDescriptionTrigger
    {
        [SerializeField] string _textClickedPathAfterConversation;

        public void ChangeClickedPathAfterConversation()
        {
            _textClickedPath = _textClickedPathAfterConversation;
        }
    } 
}
