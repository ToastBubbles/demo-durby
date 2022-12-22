using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallExplosion : MonoBehaviour
{

    public GameObject _exp;
    public GameObject hitMark;

    private AudioSource audiosource;
    private AudioManager audioMan;

    // public float damage = 20;
    float dmg = 1;
    float size = 1;
    bool didHitDirect = false;
    GameObject itemHit;
    float lerpTime = 0;
    List<string> dmgTypes = new List<string>();
    void Start()
    {



        /* if (dmg > 80)
         {
             audiosource.pitch = 0.6f;
         }
         else if (dmg < 1)
         {
             audiosource.pitch = 2.97f;
         }
         else
         {
             audiosource.pitch = (dmg / 200) * -6 + 3;
         }*///adjust later

    }

    public void ApplyDamage(float damage, List<string> damageTypes, bool directHit)
    {
        dmg = damage;
        size = dmg / 1;
        dmgTypes = damageTypes;
        didHitDirect = directHit;
        audiosource = GetComponent<AudioSource>();
        GameObject gmAM = GameObject.FindGameObjectWithTag("Audio Manager");
        audioMan = gmAM.GetComponent<AudioManager>();
        StartCoroutine("DestroySelf");
        ExplosionDamage(transform.position, size);
    }
    Vector3 startSize = Vector3.zero;

    void Update()
    {
        Vector3 endSize = new Vector3(size, size, size);
        _exp.transform.localScale = Vector3.Lerp(startSize, endSize, lerpTime);
        lerpTime += Time.deltaTime * 20;
        //_exp.transform.localScale = Vector3.Lerp(_exp.transform.localScale, _exp.transform.localScale * 3f, Time.deltaTime * 20);
    }

    // Vector3 cp;
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius / 2);

        foreach (var hitCollider in hitColliders)
        {
            //hitCollider.SendMessage("AddDamage");
            //Debug.Log(hitCollider.name);
            if (hitCollider.tag == "Dummy" || hitCollider.tag == "Player")
            {
                audioMan.PlayHitAudio();
                //hitCollider.SendMessage("ApplyDamage", (dmg, dmgTypes));

                Vector3 offset = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                // hitCollider.GetComponent<TankHealth>().updateOffset(offset);
                // hitCollider.GetComponent<TankHealth>().ApplyDamage(dmg, dmgTypes, "");
                // GameObject clone = Instantiate(hitMark, offset, transform.rotation);
                // clone.SendMessage("SetDamage", dmg);
                if (didHitDirect)
                {
                    Debug.Log("direct Hit");
                    hitCollider.GetComponent<TankHealth>().updateOffset(offset);
                    hitCollider.GetComponent<TankHealth>().ApplyDamage(dmg, dmgTypes, "");
                }
                else
                {
                    //RaycastHit hit;
                    //Physics.Raycast(center, , out hit, hoverHeight, layerMask);
                    Vector3 closestPoint = hitCollider.ClosestPoint(center);
                    // cp = closestPoint;

                    float explosionDamage = dmg - (Vector3.Distance(center, closestPoint)) / (size / 2) * dmg;
                    explosionDamage = RoundToNearestHalf(explosionDamage);
                    //Debug.Log(size + " is size " + Vector3.Distance(center, closestPoint) + " is distance ");
                    //Debug.Log(Vector3.Distance(center, closestPoint));
                    hitCollider.GetComponent<TankHealth>().updateOffset(offset);
                    hitCollider.GetComponent<TankHealth>().ApplyDamage(explosionDamage, dmgTypes, "");
                    ;
                }
            }
        }
    }
    private static float RoundToNearestHalf(float a)
    {
        return a = Mathf.Round(a * 2f) * 0.5f;
    }

    // private void OnDrawGizmos()
    // {

    //     Gizmos.color = Color.red;
    //     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    //     // Gizmos.DrawWireSphere(transform.position, size / 2);
    //     Gizmos.DrawWireSphere(cp, 0.5f);
    // }

    IEnumerator DestroySelf()
    {

        yield return new WaitForSeconds(0.25f);
        _exp.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
