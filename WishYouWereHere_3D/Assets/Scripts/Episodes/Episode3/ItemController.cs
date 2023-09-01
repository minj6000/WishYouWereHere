using PixelCrushers.DialogueSystem;
using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP3
{
    public class ItemController : CenterCursorTriggerEvent
    {
        [SerializeField] string _itemName;
        public string ItemName => _itemName;

        protected override void OnCenterCursorEnter()
        {
            base.OnCenterCursorEnter();
        }

        protected override void OnCenterCursorDown()
        {
            base.OnCenterCursorDown();

            Enabled = false;

            DialogueLua.SetVariable("EP3_완료아이템", 3);

            //DialogueLua.SetVariable("EP3_선택아이템", _itemName);
            DialogueManager.Instance.StartConversationWithEndedAction("EP_3", _ => {
                string value = DialogueLua.GetVariable("EP3_선택아이템").AsString;
                Debug.Log(value);
            });

        }
    }

}