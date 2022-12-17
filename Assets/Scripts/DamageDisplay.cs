using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageDisplay : MonoBehaviour
{
    private TextMeshProUGUI dmg;
    float num = 0;
    private GameObject player;
    void Start()
    {
        dmg = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dmg.text = num.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }


    void Update()
    {
        Camera camera = Camera.main;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        transform.Translate(Vector3.up * Time.deltaTime);

    }

    public void SetDamage(float hit)
    {
        num = hit;
    }
}
