using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireCondition : MonoBehaviour
{
    public bool isFlat = false;
    void Start()
    {

    }


    void Update()
    {

    }

    public void MakeFlat()
    {
        Debug.Log("Hit tire");
        isFlat = true;
    }
}
