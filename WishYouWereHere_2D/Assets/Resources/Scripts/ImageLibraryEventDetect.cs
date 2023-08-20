using Michsky.DreamOS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLibraryEventDetect : MonoBehaviour
{
    public Image image;
    public Sprite FirstEventImg;
    public Sprite SecondEventImg;
    public Sprite ThirdEventImg;
    public Sprite FourthEventImg;


    private void Update()
    {
        if (GameManager.Instance.CurEventNum == 0)
        {
            if (image.sprite != FirstEventImg) //일러스트 들어오면 이름 바꿀 예정
            {
                image.color = Color.gray;
                this.GetComponent<ButtonManager>().isInteractable = false;
            }
            else
            {
                image.color = Color.white;
                this.GetComponent<ButtonManager>().isInteractable = true;
            }
        }
        if (GameManager.Instance.CurEventNum == 1)
        {
            if (image.sprite != SecondEventImg) //일러스트 들어오면 이름 바꿀 예정
            {
                image.color = Color.gray;
                this.GetComponent<ButtonManager>().isInteractable = false;
            }
            else
            {
                image.color = Color.white;
                this.GetComponent<ButtonManager>().isInteractable = true;
            }
        }
        if (GameManager.Instance.CurEventNum == 2)
        {
            if (image.sprite != ThirdEventImg) //일러스트 들어오면 이름 바꿀 예정
            {
                image.color = Color.gray;
                this.GetComponent<ButtonManager>().isInteractable = false;
            }
            else
            {
                image.color = Color.white;
                this.GetComponent<ButtonManager>().isInteractable = true;
            }
        }        
        if (GameManager.Instance.CurEventNum == 3)
        {
            if (image.sprite != FourthEventImg) //일러스트 들어오면 이름 바꿀 예정
            {
                image.color = Color.gray;
                this.GetComponent<ButtonManager>().isInteractable = false;
            }
            else
            {
                image.color = Color.white;
                this.GetComponent<ButtonManager>().isInteractable = true;
            }
        }
    }
}
