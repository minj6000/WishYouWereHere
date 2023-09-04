using Michsky.DreamOS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventQueManager : MonoBehaviour
{
    public GameObject textBox1;
    public GameObject textBox2;

    private void Update()
    {
        if (textBox1 == null)
        {
            textBox1 = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        }

        if(textBox2 == null)
        {
            textBox2 = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        }

    }

    public void TextOnWebpageQue()
    {
        if (textBox1 != null)
        {
            textBox1.SetActive(true);
        }
        else
        {
            Debug.LogWarning("TextBox-1 발견 되지 않음.");
        }
    }

    public void TextOnSecondWebQue()
    {
        if (textBox2 != null)
        {
            textBox2.SetActive(true);
        }
        else
        {
            Debug.LogWarning("TextBox-2 발견 되지 않음.");
        }
    }


}
