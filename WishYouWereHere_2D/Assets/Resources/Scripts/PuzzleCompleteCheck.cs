using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleCompleteCheck : MonoBehaviour
{
    public int PuzzleCompleteNum;
    [SerializeField] Image[] puzzles;

    private void Update()
    {
        if(PuzzleCompleteNum > 6)
        {
            for(int i = 0; i < puzzles.Length; i++)
            {
                puzzles[i].color = Color.white;
            }
        }
    }
}
