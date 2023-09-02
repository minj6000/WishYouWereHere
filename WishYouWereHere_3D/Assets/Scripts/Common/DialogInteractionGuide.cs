using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

namespace WishYouWereHere3D.Common
{
    public class DialogInteractionGuide : MonoBehaviour
    {
        [SerializeField] Text[] guideTexts;

        private void Update()
        {
            string guideText = DialogueLua.GetVariable("GuideText").AsString;

            foreach (var gt in guideTexts)
            {
                gt.text = guideText;
            }
        }


    } 
}
