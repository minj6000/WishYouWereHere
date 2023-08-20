using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NoteClickCheck : MonoBehaviour
{
    public Button[] sequenceButtons; //�� ���� �迭 - ��ε� ������ �߰��ؾ� ��
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
        Debug.Log("���� ��!");
        ResetSequence();
    }
}
