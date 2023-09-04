using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceTagInteraction : MonoBehaviour
{
    public TextMeshProUGUI numTxt;
    public TextMeshProUGUI PriceTagNum;
    public MMF_Player PriceTagEffect;
    public int CurObjectIndex;
    public Sprite[] ObjectToSell;
    public Image photoviewer;
    [SerializeField] Transform pricetagStartPos;
    [SerializeField] MMF_Player CalculatorOut;
    [SerializeField] GameObject TooExpensive;
    [SerializeField] GameObject TooCheap;
    [SerializeField] GameObject ThisMuch;

    private Dictionary<int, (int minPrice, int maxPrice)> priceRanges = new Dictionary<int, (int, int)>(); // 가격 범위

    private void Start()
    {
        photoviewer.sprite = ObjectToSell[CurObjectIndex];
        ClearNumber();
        priceRanges.Add(0, (Random.Range(2000, 6000), Random.Range(7000,10000)));
        priceRanges.Add(1, (Random.Range(4000, 8000), Random.Range(8500, 15000)));
        priceRanges.Add(2, (Random.Range(1000, 2000), Random.Range(3000, 4000)));
    }

    public void OnNumberButtonPressed(int number)
    {
        numTxt.text += number.ToString();
    }

    public void ClearNumber()
    {
        numTxt.text = "";
    }

    private void Update()
    {
        if(CurObjectIndex > 2)
        {
            photoviewer.gameObject.GetComponent<PhotoViewerCheck>().PriceTagEventFinished = true;
            this.gameObject.SetActive(false);
            PriceTagEffect.gameObject.SetActive(false);
        }
    }

    public void ConfirmNumber()
    {
        ThisMuch.SetActive(true);
        if (numTxt.text != "")
        {
            int enteredPrice = int.Parse(numTxt.text);

            if (priceRanges.ContainsKey(CurObjectIndex))
            {
                (int minPrice, int maxPrice) = priceRanges[CurObjectIndex];

                if (enteredPrice >= minPrice && enteredPrice <= maxPrice)
                {
                    PriceTagNum.text = enteredPrice + "원";
                    PriceTagEffect.PlayFeedbacks();

                    CalculatorOut.PlayFeedbacks();
                    StartCoroutine(PriceTagged());
                }
                else if(enteredPrice > maxPrice)
                {
                    TooExpensive.SetActive(true);
                    Debug.Log("너무 비싸!");
                }
                else if(enteredPrice < minPrice)
                {
                    TooCheap.SetActive(true);
                    Debug.Log("좀 너무 싼 거 아냐?");
                }
                else
                {
                    Debug.Log("가격 범위 벗어남.");
                }
            }
        }
    }

    IEnumerator PriceTagged()
    {
        yield return new WaitForSeconds(3f);
        PriceTagEffect.gameObject.transform.position = pricetagStartPos.position;
        ClearNumber();
        PriceTagNum.text = "";
        CurObjectIndex++;
        photoviewer.sprite = ObjectToSell[CurObjectIndex];
        this.GetComponent<MMF_Player>().PlayFeedbacks();

    }
}
