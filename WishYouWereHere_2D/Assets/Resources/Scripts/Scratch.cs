using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scratch : MonoBehaviour
{
    public GameObject maskPrefab;
    public GameObject TextBox3;
    bool isPressing;
    private int scratchedPixels;  

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        if (isPressing)
        {
            CreateMask(pos);
            UpdateScratchProgress();

            if (scratchedPixels >= 600)
            {
                Debug.Log("거의 다 긁었음");
                TextBox3.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            isPressing = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPressing = false;
        }
    }

    void CreateMask(Vector3 position)
    {
        GameObject maskInstance = Instantiate(maskPrefab, position, Quaternion.identity);
        maskInstance.transform.parent = transform;
    }

    void UpdateScratchProgress()
    {
        scratchedPixels = transform.childCount;
    }



}
