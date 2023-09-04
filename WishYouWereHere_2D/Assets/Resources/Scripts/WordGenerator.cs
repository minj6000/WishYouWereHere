using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WordGenerator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] texts;
    private int currentIndex = 0; 

    private float screenHeight;
    private float screenWidth;

    private void Start()
    {
        float aspect = (float)Screen.width / Screen.height;
        screenHeight = Camera.main.orthographicSize * 2f; //월드스페이스 스크린 크기 컨버팅
        screenWidth = screenHeight * aspect;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentenceTexts();

        }
    }

    private void DisplayNextSentenceTexts()
    {
        if (texts.Length > currentIndex)
        {
            texts[currentIndex].gameObject.SetActive(true);
            currentIndex++;
        }
        else if (texts.Length == currentIndex)
        {
            GameManager.Instance.IsProjectorOn = true;
            //프로젝션 맵핑으로 전환
        }
    }
}
