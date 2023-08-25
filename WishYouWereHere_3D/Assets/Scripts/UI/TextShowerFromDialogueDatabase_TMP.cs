using UnityEngine;
using WishYouWereHere3D.Common;

namespace WishYouWereHere3D.UI
{
    public class TextShowerFromDialogueDatabase_TMP : TextShower_TMP
    {
        public override void ShowText(string path)
        {
            string text = DialogueDatabaseHelper.Get(path);
            if(string.IsNullOrEmpty(text))
            {
                Debug.LogWarning($"No text found for path: {path}");
            }

            base.ShowText(text);
        }
    } 
}
