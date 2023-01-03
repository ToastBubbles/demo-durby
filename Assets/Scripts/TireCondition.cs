using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireCondition : MonoBehaviour
{
    public bool isFlat = false;
    private Vector3 originalSize;
    void Start()
    {
        originalSize = gameObject.transform.localScale;
    }


    void Update()
    {
        if (isFlat)
        {
            gameObject.transform.localScale = new Vector3(originalSize.x * 0.01f, originalSize.y * 0.01f, originalSize.z * 0.01f);
        }
    }

    public void MakeFlat()
    {
        Debug.Log("Hit tire");
        isFlat = true;
    }
}
