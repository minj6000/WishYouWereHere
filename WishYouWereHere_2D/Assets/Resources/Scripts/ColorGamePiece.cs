using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorGamePiece : MonoBehaviour
{
    public bool clickedB4;
    public Color color;
    public ColorGameCompleteCheck ColorGameCompleteCheck;
    // Start is called before the first frame update
    void Start()
    {
        clickedB4 = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DifferentColorClicked()
    {
        if (!clickedB4)
        {
            clickedB4 = true;
            this.GetComponent<Image>().color = color;
            ColorGameCompleteCheck.PiecePicked++;
            ColorGameCompleteCheck.OnStageCompleted();
        }
    }

}
