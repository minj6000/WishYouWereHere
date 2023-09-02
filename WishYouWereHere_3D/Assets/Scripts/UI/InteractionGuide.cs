using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WishYouWereHere3D.Common;

namespace WishYouWereHere3D.UI
{
    public class InteractionGuide : SerializedMonoBehaviour
    {
        public enum Styles
        {
            Style1,
            Style2
        }

        public enum Icons
        {
            Mouse_L,
            Space
        }

        Styles _style;

        [SerializeField] GameObject _style1Panel;
        [SerializeField] GameObject _style2Panel;

        [SerializeField]
        Dictionary<Styles, Dictionary<Icons, Sprite>> _iconSprites;

        Text _text;
        Image _icon;

        private static InteractionGuide _instance;
        public static InteractionGuide Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<InteractionGuide>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _style = Configuration.Instance.InteractionGuideStyle;

            if (_style == Styles.Style1)
            {
                _text = _style1Panel.transform.Find("Text")?.GetComponent<Text>();
                _icon = _style1Panel.transform.Find("Icon")?.GetComponent<Image>();
            }
            else if(_style == Styles.Style2)   
            {
                _text = _style2Panel.transform.Find("Text")?.GetComponent<Text>(); 
                _icon = _style2Panel.transform.Find("Icon")?.GetComponent<Image>();
            }
        }

        public void Show(Icons icon, string text)
        {             
            if(_style == Styles.Style1)
            {
                _style1Panel.SetActive(true);

                _text.text = text;
                _icon.sprite = _iconSprites[_style][icon];
                _icon.SetNativeSize();
            }
            else if(_style == Styles.Style2)
            {
                _style2Panel.SetActive(true);
                _icon.sprite = _iconSprites[_style][icon];
                _icon.SetNativeSize();
            }
        }

        public void Hide()
        {
            if(_style == Styles.Style1)
            {
                _style1Panel.SetActive(false);
            }
            else if(_style == Styles.Style2)
            {
                _style2Panel.SetActive(false);
            }
        }

    }

}