using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NoteClickCheck : MonoBehaviour
{
    public Button[] sequenceButtons; //음 순서 배열 - 멜로디 받으면 추가해야 함
    private int currentButtonIndex = 0; 

    private void Start()
    {
        ResetSequence();
    }

    public void ButtonClicked(Button clickedButton)
    {
        if (sequenceButtons[currentButtonIndex] == clickedButton)
        {
            currentButtonIndex++;

            if (currentButtonIndex >= sequenceButtons.Length)
            {
                PlayComplete();
            }
        }
        else
        {
            ResetSequence();
        }
    }

    private void ResetSequence()
    {
        currentButtonIndex = 0;
    }

    private void PlayComplete()
    {
        Debug.Log("연주 끝!");
        ResetSequence();
    }
}
