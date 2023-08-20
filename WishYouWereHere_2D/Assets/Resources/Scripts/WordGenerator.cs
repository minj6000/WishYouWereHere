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
        screenHeight = Camera.main.orthographicSize * 2f; //���彺���̽� ��ũ�� ũ�� ������
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
            //�������� �������� ��ȯ
        }
    }
}
