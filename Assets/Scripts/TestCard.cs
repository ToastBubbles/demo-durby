using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCard : MonoBehaviour
{
    public bool entered = false;
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        entered = true;


    }

    private void OnTriggerExit(Collider other)
    {
        entered = false;
    }



    void Update()
    {

    }
}
