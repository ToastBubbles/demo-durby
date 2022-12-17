using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject smallExplosion;
    bool hasExplode = false;

    float dmg = 20;


    void Start()
    {
        StartCoroutine("DestroySelf");
        AudioSource ass = GetComponent<AudioSource>();
        if (dmg > 80)
        {
            ass.pitch = 0.6f;
        }
        else if (dmg < 1)
        {
            ass.pitch = 2.97f;
        }
        else
        {
            ass.pitch = (dmg / 200) * -6 + 3;
        }






        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void ApplyDamage(float damage)
    {
        dmg = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasExplode)
        {
            GameObject clone = Instantiate(smallExplosion, transform.position, transform.rotation);
            clone.transform.localScale = new Vector3(dmg/20, dmg/20, dmg/20);
            clone.SendMessage("ApplyDamage", dmg);

            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            hasExplode = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        //Destroy(gameObject);
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
