using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamageDummy : MonoBehaviour
{
    public Transform player;
    public GameObject turret;
    private bool canFire = true;
    public Rigidbody bullet;
    public float damage = 20;
    public float delay = 5;


    void Start()
    {
        
    }

    void Update()
    {
        Vector3 spin = new Vector3(0, 0.1f, 0);
        transform.Rotate(spin, Space.World);

        Vector3 relativePos = player.position - turret.transform.position;
        float dist = Vector3.Distance(player.position, turret.transform.position);
        relativePos = new Vector3(relativePos.x, relativePos.y + (dist /20), relativePos.z);
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        turret.transform.rotation = rotation;


        if (canFire)
        {
            StartCoroutine("FireCannon");
            canFire = false;
        }
    }


    IEnumerator FireCannon()
    {
        float damageConvert = damage / 20;
        Rigidbody bull = Instantiate(bullet, turret.transform.GetChild(0).position, turret.transform.GetChild(0).rotation) as Rigidbody;
        bull.transform.localScale = new Vector3(damageConvert, damageConvert, damageConvert);
        bull.SendMessage("ApplyDamage", damage);
        // Vector3 localForward = bull.transform.worldToLocalMatrix.MultiplyVector(bull.transform.forward);
        bull.AddForce(bull.transform.forward * 5000);
        yield return new WaitForSeconds(delay);
        canFire = true;
    }


}
