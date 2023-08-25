using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace WishYouWereHere3D.Common
{
    public class DialogueDatabaseHelper : MonoBehaviour
    {
        const string LOCATION_PREFIX = "location";
        const string ITEM_PREFIX = "item";
        const string DESCRIPTION_PROPERTY = "description";

        public static string Get(string path)
        {
            string []tokens = path.Split('\\', '/');

            if(tokens.Length == 1)
            {
                return tokens[0];
            }
            else if(tokens.Length == 2)
            {
                if (tokens[0].ToLower() == LOCATION_PREFIX)
                {
                    return GetLocationProperty(tokens[1]);
                }
                else if (tokens[0].ToLower() == ITEM_PREFIX)
                {
                    return GetItemProperty(tokens[1]);
                }
            }
            else if(tokens.Length == 3)
            {
                if (tokens[0].ToLower() == LOCATION_PREFIX)
                {
                    return GetLocationProperty(tokens[1], tokens[2]);
                }
                else if (tokens[0].ToLower() == ITEM_PREFIX)
                {
                    return GetItemProperty(tokens[1], tokens[2]);
                }
            }

            return string.Empty;
        }

        public static string GetLocationProperty(string locationName, string propertyName = DESCRIPTION_PROPERTY)
        {
            Location location = DialogueManager.Instance.MasterDatabase.GetLocation(locationName);
            if(propertyName.ToLower() == DESCRIPTION_PROPERTY)
            {
                return location.Description;
            }
            else
            {
                return location.LookupValue(propertyName);
            }
        }

        public static string GetItemProperty(string itemName, string propertyName = DESCRIPTION_PROPERTY)
        {
            Item item = DialogueManager.Instance.MasterDatabase.GetItem(itemName);
            if (propertyName.ToLower() == DESCRIPTION_PROPERTY)
            {
                return item.Description;
            }
            else
            {
                return item.LookupValue(propertyName);
            }
        }
    }

}