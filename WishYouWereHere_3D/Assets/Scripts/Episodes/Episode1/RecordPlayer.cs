using DarkTonic.MasterAudio;
using UnityEngine;
using WishYouWereHere3D.TriggerEvents;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.EP1
{
    public class RecordPlayer : ItemDescriptionTrigger
    {
        bool _changedMusic = false;
        [SerializeField] string _textClickedPathAfterConversation;

        [SerializeField] string _firstMusicName;
        [SerializeField] string _changedMusicName;

        public void ChangeClickedPathAfterConversation()
        {
            _changedMusic = true;
            _textClickedPath = _textClickedPathAfterConversation;
        }

        protected override void OnCenterCursorEnter()
        {
            base.OnCenterCursorEnter();

            if(_changedMusic)
            {
                InteractionGuide.Instance.Show(InteractionGuide.Icons.Mouse_L, "음악 바꾸기");
            }
        }

        protected override void OnCenterCursorDown()
        {
            base.OnCenterCursorDown();

            if(!_changedMusic && !string.IsNullOrEmpty(_firstMusicName))
            {
                MasterAudio.PlaySound3DAtTransform(_firstMusicName, transform);
            }
            else if (_changedMusic && !string.IsNullOrEmpty(_changedMusicName))
            {
                MasterAudio.PlaySound3DAtTransform(_changedMusicName, transform);
            }
        }
    } 
}
