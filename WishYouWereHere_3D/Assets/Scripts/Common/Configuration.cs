using UnityEngine;
using WishYouWereHere3D.UI;

namespace WishYouWereHere3D.Common
{
    [CreateAssetMenu(fileName = "Configuration", menuName = "WishYouWereHere3D/Configuration", order = 1)]
    public class Configuration : ScriptableObject
    {
        static Configuration _instance;
        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<Configuration>("Configuration");
                }
                return _instance;
            }
        }

        [System.Serializable]
        public class ItemDescriptionConfiguration
        {
            [Tooltip("아이템과 거리에 의해 텍스트가 숨겨집니다. 값이 0일 경우 포커스가 아이템에서 빠져나가면 텍스트가 숨겨집니다.")]
            [SerializeField]
            float _hideToDistance = 0f;
            public float HideToDistance => _hideToDistance;
        }


        [SerializeField] ItemDescriptionConfiguration _itemDescription;
        public ItemDescriptionConfiguration ItemDescription => _itemDescription;

        [System.Serializable]
        public class LocationDescriptionConfiguration
        {
            [Tooltip("공간 정보가 한번만 보여집니다.")]
            [SerializeField] 
            bool _showOnce = false;
            public bool ShowOnce => _showOnce;

            [Tooltip("설정한 시간 이후에 텍스트가 숨겨집니다. 값이 0일 경우 공간 콜라이더에서 빠져나가면 텍스트가 숨겨집니다.")]
            [SerializeField] 
            float _showDuration = 0f;
            public float ShowDuration => _showDuration;
        }

        [SerializeField] LocationDescriptionConfiguration _locationDescription;
        public LocationDescriptionConfiguration LocationDescription => _locationDescription;

        public enum ConversationController
        {
            Mouse,
            Keyboard
        }

        [Tooltip("대화 컨트롤러를 지정합니다.")]
        [SerializeField] 
        ConversationController _conversationController;
        public ConversationController ConversationControllerType => _conversationController;

        [Tooltip("인터렉션 가이드 UI 스타일을 설정합니다.")]
        InteractionGuide.Styles _interactionGuideStyle = InteractionGuide.Styles.Style1;
        public InteractionGuide.Styles InteractionGuideStyle => _interactionGuideStyle;


    }
}