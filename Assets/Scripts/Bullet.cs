using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject smallExplosion;

    bool hasExplode = false;

    float dmg = 20;

    Vector3 iniPos;
    List<string> dmgTypes;
    public LayerMask lm;


    void Start()
    {


        iniPos = transform.position;
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
        // Debug.Log(Vector3.Distance(iniPos, transform.position));
        // if (Vector3.Distance(iniPos, transform.position) > 6)
        // {
        //     Debug.Log("enable");
        // }
        //rayCheck();


    }
    void FixedUpdate()
    {
        rayCheck();
    }

    private void rayCheck()
    {

        var vel = GetComponent<Rigidbody>().velocity;
        //Debug.Log(vel);
        //transform.position+move_speed*Time.fixedDeltaTime
        Vector3 nextPos = transform.position + vel * Time.fixedDeltaTime;
        Vector3 dir = nextPos - transform.position;


        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, Vector3.Distance(transform.position, nextPos)))
        {
            // if(hit)
            // {
            Debug.Log(hit.collider.name);

            if (hit.collider.tag == "Player" && Vector3.Distance(iniPos, transform.position) < 10)
            {
                Debug.Log("stop hitting urself");

            }
            else
            {
                if (!hasExplode)
                {


                    if (hit.collider.tag == "Player" || hit.collider.tag == "Dummy" || hit.collider.tag == "Tire")
                    {
                        if (hit.collider.tag == "Tire" && dmgTypes.Contains("needle"))
                        {
                            
                            hit.collider.GetComponent<TireCondition>().MakeFlat();
                        }

                        //Debug.Log(other.tag);
                        GameObject clone = Instantiate(smallExplosion, hit.point, transform.rotation);
                        //clone.transform.localScale = new Vector3(dmg / 20, dmg / 20, dmg / 20);
                        //clone.SendMessage("ApplyDamage", (dmg, dmgTypes));
                        clone.GetComponent<smallExplosion>().ApplyDamage(dmg, dmgTypes, true);

                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        hasExplode = true;
                        GetComponent<Rigidbody>().isKinematic = true;
                    }
                    else
                    {
                        GameObject clone = Instantiate(smallExplosion, hit.point, transform.rotation);
                        //clone.transform.localScale = new Vector3(dmg / 20, dmg / 20, dmg / 20);
                        //clone.SendMessage("ApplyDamage", (dmg, dmgTypes));
                        clone.GetComponent<smallExplosion>().ApplyDamage(dmg, dmgTypes, false);

                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        hasExplode = true;
                        GetComponent<Rigidbody>().isKinematic = true;
                    }


                }
            }
        }

        // for (var i: int = 0; i < pathNodes.length; i++){
        // lineRenderer.SetPosition(i, pathNodes[i].transform.position);

        //Debug.DrawLine(transform.position, nextPos, Color.red);
        //Debug.Log(hit.collider.gameObject.name);
        //Debug.Log(lastPosition + "" + transform.position);

        Debug.DrawRay(transform.position, transform.position - nextPos, Color.red);

    }


    public void ApplyDamage(float damage, List<string> damageTypes)
    {
        dmg = damage;
        dmgTypes = damageTypes;
        // for(var i = 0; i< damageTypes.Count; i++){
        //     if (damageTypes[i] =="fire"){

        //     }
        // }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player" && Vector3.Distance(iniPos, transform.position) < 10)
    //     {
    //         Debug.Log("stop hitting urself");

    //     }
    //     else
    //     {
    //         if (!hasExplode)
    //         {


    //             if (other.tag == "Player" || other.tag == "Dummy")
    //             {

    //                 //Debug.Log(other.tag);
    //                 GameObject clone = Instantiate(smallExplosion, transform.position, transform.rotation);
    //                 //clone.transform.localScale = new Vector3(dmg / 20, dmg / 20, dmg / 20);
    //                 //clone.SendMessage("ApplyDamage", (dmg, dmgTypes));
    //                 clone.GetComponent<smallExplosion>().ApplyDamage(dmg, dmgTypes, true);

    //                 gameObject.transform.GetChild(0).gameObject.SetActive(false);
    //                 hasExplode = true;
    //                 GetComponent<Rigidbody>().isKinematic = true;
    //             }
    //             else
    //             {
    //                 GameObject clone = Instantiate(smallExplosion, transform.position, transform.rotation);
    //                 //clone.transform.localScale = new Vector3(dmg / 20, dmg / 20, dmg / 20);
    //                 //clone.SendMessage("ApplyDamage", (dmg, dmgTypes));
    //                 clone.GetComponent<smallExplosion>().ApplyDamage(dmg, dmgTypes, false);

    //                 gameObject.transform.GetChild(0).gameObject.SetActive(false);
    //                 hasExplode = true;
    //                 GetComponent<Rigidbody>().isKinematic = true;
    //             }


    //         }
    //     }
    //     //Destroy(gameObject);
    // }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
