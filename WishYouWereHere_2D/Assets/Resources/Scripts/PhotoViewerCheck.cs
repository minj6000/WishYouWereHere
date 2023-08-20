using Michsky.DreamOS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoViewerCheck : MonoBehaviour
{
    [SerializeField] GameObject PhotoManager;
    [SerializeField] Sprite[] Photos;
    [SerializeField] Sprite[] SecondEventPhotos;
    [SerializeField] Sprite[] ThirdEventPhotos;
    [SerializeField] Sprite AfterPriceTagPhoto;
    int SecondEventPhotoIndex;
    int ThirdEventPhotoIndex;
    bool secondEventQue;
    bool ThirdEventQue;

    [SerializeField] GameObject TextBox4;
    [SerializeField] GameObject TextBox6;
    [SerializeField] GameObject TextBox7;
    [SerializeField] GameObject TextBox8;
    [SerializeField] GameObject TextBox9;

    public Transform BicycleImgPos;

    [SerializeField] GameObject calculator;
    [SerializeField] GameObject Pricetag;
    public bool PriceTagEventFinished;



    private void OnEnable()
    {
        PriceTagEventFinished = false;
        BicycleImgPos.gameObject.SetActive(false);
        SecondEventPhotoIndex = 0;
        ThirdEventPhotoIndex= 0;
        if (this.GetComponent<Image>().sprite == Photos[0])
        {
            TextBox4.SetActive(true);
        }
        else if(this.GetComponent<Image>().sprite == Photos[1])
        {
            TextBox6.SetActive(true);
            secondEventQue= true;
        }
        else if(this.GetComponent<Image>().sprite == Photos[2])
        {
            TextBox7.SetActive(true);
            ThirdEventQue = true;
        }        
        else if(this.GetComponent<Image>().sprite == Photos[3])
        {
            TextBox9.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!TextBox6.transform.GetChild(0).GetComponent<TypewriterEffect>().isTyping && secondEventQue && Input.GetKeyDown(KeyCode.Space) && SecondEventPhotoIndex < SecondEventPhotos.Length)
        {
            this.GetComponent<Image>().sprite = SecondEventPhotos[SecondEventPhotoIndex];
            SecondEventPhotoIndex++;
        }

        if (SecondEventPhotoIndex == 2 || SecondEventPhotoIndex == 3)
        {
            BicycleImgPos.gameObject.SetActive(true);
        }



        if (!TextBox7.transform.GetChild(0).GetComponent<TypewriterEffect>().isTyping && ThirdEventQue && Input.GetKeyDown(KeyCode.Space) && ThirdEventPhotoIndex < ThirdEventPhotos.Length)
        {
            //계산기 쪽에서 가격 매겼는지 확인 
            this.GetComponent<Image>().sprite = ThirdEventPhotos[ThirdEventPhotoIndex];
            ThirdEventPhotoIndex++;
        }
        if (ThirdEventPhotoIndex == ThirdEventPhotos.Length)
        {
            Pricetag.SetActive(true);
            calculator.SetActive(true);
            ThirdEventPhotoIndex++;
        }


        if (PriceTagEventFinished)
        {
            TextBox8.SetActive(true);
            this.GetComponent<Image>().sprite = AfterPriceTagPhoto;
            PriceTagEventFinished = false;
        }
    }

}
