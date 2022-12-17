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



    void Update()
    {
        
    }
}
