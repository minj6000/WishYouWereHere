using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGameCompleteCheck : MonoBehaviour
{
    public int gameStage;
    public int PiecePicked;
    public GameObject[] Stages;
    // Start is called before the first frame update

    private void Start()
    {
        Stages[0].SetActive(true);

    }

    public void OnStageCompleted()
    {
        if (gameStage == 0)
        {
            if (PiecePicked == 1)
            {
                gameStage++;
                foreach(GameObject stage in Stages)
                {
                    stage.SetActive(false);
                }
                Stages[1].SetActive(true);
                PiecePicked = 0;
            }
        }
        else if (gameStage == 1)
        {
            if (PiecePicked == 3)
            {
                gameStage++;
                foreach (GameObject stage in Stages)
                {
                    stage.SetActive(false);
                }
                Stages[2].SetActive(true);
                PiecePicked = 0;

            }
        }
        else if (gameStage == 2)
        {
            if (PiecePicked == 4)
            {
                foreach(GameObject stage in Stages)
                {
                    stage.SetActive(false);
                }
                PiecePicked = 0;

            }
        }
    }
    public void DifferentColorClicked(bool Notclicked)
    {
        if (Notclicked)
        {
            Notclicked = false;
            PiecePicked++;
            OnStageCompleted();
        }
        else
        {
            return;
        }
    }


}
