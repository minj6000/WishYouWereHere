using TMPro;
using UnityEngine;

namespace WishYouWereHere3D.EP2
{
    public class EventText : MonoBehaviour
    {
        TextMeshProUGUI _text;

        [SerializeField] float _fadeStartDistance = 15f;
        [SerializeField] float _fadeEndDistance = 5f;


        void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        void Update()
        {
            float distance = Vector3.Distance(BicycleController.Instance.transform.position, transform.position);

            float alpha = Mathf.Clamp((distance - _fadeEndDistance) / _fadeStartDistance, 0f, 1f);

            Color color = _text.color;
            color.a = alpha;
            _text.color = color;

            if(alpha < 0.1f)
            {
                gameObject.SetActive(false);
            }
        }
    } 
}
