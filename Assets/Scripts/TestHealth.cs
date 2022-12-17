using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealth : MonoBehaviour
{
    public float health = 100f;
    void Start()
    {
        print(health);
    }


    void Update()
    {
        if(health <= 0)
        {
            Defeated();
        }
        
    }
    public void ApplyDamage(float damage)
    {
        health -= damage;
        print(health);
    }
    public void Defeated()
    {
        print("Destroyed!");

    }
}
