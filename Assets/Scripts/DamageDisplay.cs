using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageDisplay : MonoBehaviour
{
    private TextMeshProUGUI dmg;
    private Color32 baseColor = new Color32(255, 255, 255, 255);
    float num = 0;
    bool isFading = false;
    float elapsedTime = 0;
    float alpha = 255;
    private GameObject player;
    void Start()
    {
        dmg = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dmg.text = num.ToString();

        dmg.color = baseColor;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Delay");

    }


    void Update()
    {


        //dmg.color = baseColor;

        Camera camera = Camera.main;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        transform.Translate(Vector3.up * Time.deltaTime);
        if (isFading)
        {
            Debug.Log("running");
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Lerp(1, 0, elapsedTime / 3f);

            dmg.color = new Color32(baseColor.a, baseColor.g, baseColor.b, (byte)(alpha * 255));
        }

    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        isFading = true;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void SetDamage(float hit)
    {
        SetDamage(hit, baseColor);
    }
    public void SetDamage(float hit, Color32 color)
    {
        num = hit;
        baseColor = color;
    }
}
