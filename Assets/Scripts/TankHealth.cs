using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public float health = 100f;
    private List<string> currentBuffs = new List<string>();
    bool canBurn = true;
    public GameObject hitMark;
    List<string> myDefaultList = new List<string>();
    public GameObject particleFire;
    Vector3 offset = new Vector3();//(transform.position.x, transform.position.y + 2, transform.position.z);

    void Start()
    {
        //print(health);

    }


    void Update()
    {
        if (health <= 0)
        {
            Defeated();
        }
        if (currentBuffs.Contains("fire") && canBurn)
        {
            StartCoroutine("OnFire");
        }

    }
    bool ContainsOnly(List<string> myList, string val)
    {
        // List<string> approved = new List<string> { "Bob", "Amy" };
        for (int i = 0; i < myList.Count; i++)
        {
            if (myList.Contains(val))
            {
                return true;
            }
        }
        return false;
        // {
        //     foreach (item in myList) {
        //         if (!approved.Contains(item)) return false;
        //     }
        // }
        //return true;
    }

    public void ApplyDamage(float damage)
    {


        ApplyDamage(damage, myDefaultList, "");
    }
    // public void ApplyDamage(float damage, Color32 col)
    // {
    //     var myDefaultList = new List<string>();
    //     ApplyDamage(damage, myDefaultList, col);
    // }

    public void ApplyDamage(float damage, List<string> damageTypes, string damageColor)
    {
        health -= damage;
        //Color32 thisColor = new Color32(255,255,255,255);

        for (var i = 0; i < damageTypes.Count; i++)
        {
            Debug.Log(damageTypes[i]);
            if (damageTypes[i] == "fire")
            {
                if (currentBuffs.Contains("fire"))
                {
                    StopCoroutine("FireTimer");
                    StartCoroutine("FireTimer");
                }
                else
                {
                    currentBuffs.Add("fire");
                    StartCoroutine("FireTimer");

                }
                Debug.Log("Fire damage applied");
            }
        }
        if (damageColor != "")
        {
            Color fireColor = new Color32(255, 170, 0, 255);
            makeHitmarker(damage, fireColor);
        }
        else
        {
            makeHitmarker(damage);
        }
        print(health);
    }
    public void updateOffset(Vector3 pos)
    {
        offset = pos;
    }

    private void makeHitmarker(float dmg)
    {
        Color defaultColor = new Color32(255, 255, 255, 255);
        makeHitmarker(dmg, defaultColor);
    }
    private void makeHitmarker(float dmg, Color32 color)
    {

        GameObject clone = Instantiate(hitMark, offset, transform.rotation);
        //clone.SendMessage("SetDamage", dmg);
        clone.GetComponent<DamageDisplay>().SetDamage(dmg, color);
    }
    IEnumerator OnFire()
    {

        canBurn = false;
        yield return new WaitForSeconds(0.5f);
        //makeHitmarker(3f, fireColor);
        // GameObject clone = Instantiate(hitMark, offset, transform.rotation);
        // clone.SendMessage("SetDamage", 3);
        ApplyDamage(3f, myDefaultList, "fire");
        canBurn = true;

    }
    IEnumerator FireTimer()
    {
        particleFire.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3);
        particleFire.GetComponent<ParticleSystem>().Stop();
        currentBuffs.Remove("fire");
    }
    public void Defeated()
    {
        print("Destroyed!");

    }


}
