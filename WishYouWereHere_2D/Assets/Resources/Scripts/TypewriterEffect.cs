using TMPro;
using UnityEngine;
using System.Collections;
using MoreMountains.Feedbacks;
using Michsky.DreamOS;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    private GameObject SecondPageBtn;
    [SerializeField] GameObject WordsPocket;
    [SerializeField] WindowManager WebWindowManager;
    [SerializeField] ButtonManager InternetButton;
    [SerializeField] GameObject PhotoGallery;
    [SerializeField] string CurEvent;
    [SerializeField] TextMeshProUGUI textObject;
    [SerializeField] string[] texts;
    private int currentTextIndex = 0;
    public bool isTyping = false;

    public MMF_Player FadeOutFeedback;

    public GameObject scratch;

    private void Start()
    {
        ShowNextText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            if (currentTextIndex < texts.Length)
            {
                currentTextIndex++;
                ShowNextText();
            }
            else
            {
                HandleEndOfTexts();
            }
        }

        if (SecondPageBtn == null)
        {
            SecondPageBtn = GameObject.Find("SecondPageBtn");
        }
    }

    private void ShowNextText()
    {
        if (currentTextIndex < texts.Length)
        {
            string text = texts[currentTextIndex];
            textObject.text = text;
        }
        else
        {
            HandleEndOfTexts();
        }
    }

    private void HandleEndOfTexts()
    {
        if (CurEvent == "FirstWeb")
        {
            SecondPageBtn.GetComponent<Button>().interactable = true;
        }
        if (CurEvent == "Website")
        {
            scratch.SetActive(true);

            WebWindowManager.CloseWindow();
            InternetButton.isInteractable = false;
        }
        if (CurEvent == "Scratch")
        {
            scratch.GetComponent<MMF_Player>().PlayFeedbacks();
            PhotoGallery.SetActive(true);
        }
        if (CurEvent == "WordsPocket")
        {
            WordsPocket.SetActive(true);
        }

        FadeOutFeedback.PlayFeedbacks();
    }

    public void OntypewriterStart()
    {
        isTyping = true;
    }
    public void OntextShowed()
    {

        isTyping = false;
    }
}
