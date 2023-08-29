using Cysharp.Threading.Tasks;
using UnityEngine;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP2
{
    public class PictureSubject : CenterCursorTriggerEvent
    {
		[SerializeField] string _name;

        public string Name
		{
            get { return _name; }
        }
    }
}