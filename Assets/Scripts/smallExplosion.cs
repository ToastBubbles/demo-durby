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
    float dmg = 20;
    List<string> dmgTypes = new List<string>();
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        GameObject gmAM = GameObject.FindGameObjectWithTag("Audio Manager");
        audioMan = gmAM.GetComponent<AudioManager>();
        StartCoroutine("DestroySelf");
        ExplosionDamage(transform.position, (dmg / 30));

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

    public void ApplyDamage(float damage, List<string> damageTypes)
    {
        dmg = damage;
        dmgTypes = damageTypes;
    }
    void Update()
    {
        _exp.transform.localScale = Vector3.Lerp(_exp.transform.localScale, _exp.transform.localScale * 1.2f, Time.deltaTime * 20);
    }
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        foreach (var hitCollider in hitColliders)
        {
            //hitCollider.SendMessage("AddDamage");
            //Debug.Log(hitCollider.name);
            if (hitCollider.tag == "Dummy")
            {
                audioMan.PlayHitAudio();
                //hitCollider.SendMessage("ApplyDamage", (dmg, dmgTypes));
                hitCollider.GetComponent<TestHealth>().ApplyDamage(dmg, dmgTypes, "");
                Vector3 offset = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                // GameObject clone = Instantiate(hitMark, offset, transform.rotation);
                // clone.SendMessage("SetDamage", dmg);

            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, (dmg / 30));
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.25f);
        _exp.SetActive(false);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
