using UnityEngine;
using WishYouWereHere3D.Common.CenterCursor;
using WishYouWereHere3D.TriggerEvents;

namespace WishYouWereHere3D.EP1
{
    public class InteractionGuide : MonoBehaviour
    {
        [SerializeField] private GameObject _mouseInteractionIcon;

        CenterCursorController _centerCursorController;

        private void Awake()
        {
            _centerCursorController = FindObjectOfType<CenterCursorController>();
        }

        private void Start()
        {
            _mouseInteractionIcon.SetActive(false);
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
            var itemDescriptionTriggerEvent = _centerCursorController.EnteredObject.GetComponent<ItemDescriptionTriggerEvent>();
            if (itemDescriptionTriggerEvent != null)
            {
                _mouseInteractionIcon.SetActive(!itemDescriptionTriggerEvent.Clicked);
                return;
            }

            //소파가 아직 클릭 가능한 경우
            var sofa = _centerCursorController.EnteredObject.GetComponent<Sofa>();
            if (sofa != null)
            {
                _mouseInteractionIcon.SetActive(sofa.Enabled);
                return;
            }

            _mouseInteractionIcon.SetActive(false);
            return;
        }
    } 
}
