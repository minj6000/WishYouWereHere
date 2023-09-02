using UnityEngine;
using UnityEngine.UI;
using WishYouWereHere3D.Common.CenterCursor;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP3
{
    public class InteractionGuide : MonoBehaviour
    {
        [SerializeField] private GameObject _mouseInteractionIcon;
        [SerializeField] private GameObject _spaceInteractionIcon;
        private Text _guideText;

        CenterCursorController _centerCursorController;

        const string GuideText_ItemDescription = "살펴보기";

        public bool SpaceInteractionIconActive
        {
            get => _spaceInteractionIcon.activeSelf;
            set => _spaceInteractionIcon.SetActive(value);
        }

        private void Awake()
        {
            _centerCursorController = FindObjectOfType<CenterCursorController>();
            _guideText = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            _mouseInteractionIcon.SetActive(false);
            _spaceInteractionIcon.SetActive(false);
        }

        private void Update()
        {
            ProcessClickableIcon();
        }

        /// <summary>
        /// 클릭가능하다는 아이콘 가이드 출력
        /// </summary>
        void ProcessClickableIcon()
        {
            if(_centerCursorController.EnteredObject == null)
            {
                _mouseInteractionIcon.SetActive(false);
                return;
            }

            //아이템 설명 트리거에서 클릭되지 않았을 경우
            var itemDescriptionTriggerEvent = _centerCursorController.EnteredObject.GetComponent<ItemDescriptionTrigger>();
            if (itemDescriptionTriggerEvent != null)
            {
                _guideText.text = GuideText_ItemDescription;
                _mouseInteractionIcon.SetActive(!itemDescriptionTriggerEvent.Clicked);
                return;
            }            

            _mouseInteractionIcon.SetActive(false);
            return;
        }
    } 
}
