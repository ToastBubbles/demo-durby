using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCarControls : MonoBehaviour
{
    Rigidbody body;
    float deadZone = 0.1f;
    public float lookSensitivity = 0.3f;
    public float m_groundedDrag = 3f;
    public float maxVelocity = 50;
    public float hoverForce = 1000;
    public float gravityForce = 1000f;
    public float hoverHeight = 1.5f;
    public GameObject[] hoverPoints;
    public GameObject turret;
    public AudioSource engine;
    public float damage = 20;
    public Transform turrChaser;
    public CardInventory inv;

    //public ChacterStat speed = new ChacterStat(baseValue: 5);

    public float sensitivity = 0.3f;
    public int turrectControlType = 0;

    float angle = 0;

    bool canFire = true;
    public Rigidbody bullet;
    public float delay = 2f;

    public bool snappyTurret = false;
    public Transform lookAtTarget;


    public float forwardAcceleration = 8000f;
    public float reverseAcceleration = 4000f;
    float thrust = 0f;
    float dir;
    float flip = 1;
    private float lookval = 0;
    private float lookvalX = 0;
    private float turRot;
    float driveSpeed;
    bool grounded = false;





    public float turnStrength = 1000f;
    float turnValue = 0f;

    public ParticleSystem[] dustTrails = new ParticleSystem[2];

    int layerMask;

    void Start()
    {

        inv = gameObject.GetComponent<CardInventory>();


        body = GetComponent<Rigidbody>();
        body.centerOfMass = Vector3.down;

        layerMask = 1 << LayerMask.NameToLayer("Vehicle");
        layerMask = ~layerMask;
    }
    void Update()
    {
        //Debug.Log(speed.StatModifiers.Count);
        //Debug.Log(inv.Damage.Value);
        // Debug.Log(stats.StatModifiers);
        Quaternion turretSnap = Quaternion.Euler(Input.GetAxis("Mouse Y") * 50, turret.transform.GetChild(0).localEulerAngles.y, turret.transform.GetChild(0).localEulerAngles.z);
        //Quaternion turretSnap = Quaternion.Euler(Input.GetAxis("Mouse Y") * 50, turret.transform.GetChild(0).localEulerAngles.y, turret.transform.GetChild(0).localEulerAngles.z);

        if (turrectControlType == 1)
        {
            turret.transform.Rotate(0f, Input.GetAxis("Mouse X") * 0.5f, 0f, Space.Self);
            if (snappyTurret)
            {
                turret.transform.GetChild(0).localRotation = Quaternion.Slerp(turret.transform.GetChild(0).localRotation, turretSnap, Time.deltaTime * 5);

            }
            else
            {
                angle += (Input.GetAxis("Mouse Y") * sensitivity);
                angle = Mathf.Clamp(angle, -90, 15);
                turret.transform.GetChild(0).localRotation = Quaternion.Euler(angle, 0f, 0f);

            }
        }
        else
        {

            //turret.transform.GetChild(0).localRotation
            //Vector3 direction = lookAtTarget.position - turret.transform.GetChild(0).position;
            //Quaternion toRotation = Quaternion.FromToRotation(turret.transform.GetChild(0).forward, direction);
            // turret.transform.GetChild(0).localRotation = Quaternion.Euler(Mathf.Lerp(turret.transform.GetChild(0).localRotation.x, toRotation.x, 1 * Time.time),0f,0f);

            //Quaternion lookOnLook = Quaternion.LookRotation(lookAtTarget.position - turret.transform.GetChild(0).position);

            //turret.transform.GetChild(0).rotation = Quaternion.Slerp(turret.transform.GetChild(0).rotation, lookOnLook, Time.deltaTime);
            //turret.transform.GetChild(0).localRotation = Quaternion.Euler((Quaternion.Slerp(turret.transform.GetChild(0).rotation, lookOnLook, Time.deltaTime)).x,Quaternion.identity.y,Quaternion.identity.z);
            //turret.transform.GetChild(0).localRotation =(Quaternion.Slerp(Quaternion.Euler(turret.transform.GetChild(0).rotation.x, Quaternion.identity.y, Quaternion.identity.z), lookOnLook, Time.deltaTime));
            //turret.transform.Rotate(0f, Input.GetAxis("Mouse X") * 0.5f, 0f, Space.Self);
            /*
            var lookPos2 = lookAtTarget.position - turret.transform.position;
            var lookPos = lookAtTarget.position - turret.transform.GetChild(0).position;
            lookPos2.y = 0;
            lookPos.x = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            var rotation2 = Quaternion.LookRotation(lookPos2);
            turret.transform.GetChild(0).localRotation = Quaternion.Slerp(turret.transform.GetChild(0).localRotation, rotation, Time.deltaTime *2);
            turret.transform.localRotation = Quaternion.Slerp(turret.transform.localRotation, rotation2, Time.deltaTime * 2);*/



            /*var lookPos2 = lookAtTarget.position - turret.transform.position;
            var lookPos = lookAtTarget.position - turret.transform.GetChild(0).position;
            lookPos2.y = 0;
            lookPos.x = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            var rotation2 = Quaternion.LookRotation(lookPos2);
            turret.transform.GetChild(0).localRotation = rotation;
            turret.transform.localRotation = rotation2;*/

            //turret.transform.GetChild(0).LookAt(new Vector3(turret.transform.GetChild(0).position.x, lookAtTarget.position.y, lookAtTarget.position.z));
            // turret.transform.GetChild(0).localRotation = Quaternion.Euler(turret.transform.GetChild(0).localRotation.x, turret.transform.localRotation.y, turret.transform.GetChild(0).localRotation.z);
            //turret.transform.GetChild(0).localRotation = Quaternion.Euler(turret.transform.GetChild(0).rotation.x, turret.transform.GetChild(0).rotation.y, turret.transform.GetChild(0).rotation.z);
            //turret.transform.GetChild(0).LookAt(new Vector3(turret.transform.GetChild(0).position.x, lookAtTarget.position.y, lookAtTarget.position.z));
            //turret.transform.LookAt(new Vector3(lookAtTarget.position.x, turret.transform.position.x, lookAtTarget.position.z));
            ////turret.transform.LookAt(new Vector3(lookAtTarget.position.x, lookAtTarget.position.y, lookAtTarget.position.z));
            ///              
            // turret.transform.rotation.SetLookRotation(lookAtTarget.position);
            // Quaternion q = transform.rotation;
            //  q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            //  transform.rotation = q;


            // Vector3 relativePos = lookAtTarget.position - turret.transform.position;

            // Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            // turret.transform.rotation = rotation;



            // Vector3 relativePos = lookAtTarget.position - turret.transform.position;

            // Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            // turret.transform.rotation = rotation;

            // turret.transform.localEulerAngles = new Vector3(Quaternion.identity.x, rotation.eulerAngles.y, Quaternion.identity.z);



            // Vector3 relativePos = lookAtTarget.position - turret.transform.position;
            // relativePos.x = transform.rotation.eulerAngles.x;
            // relativePos.z = transform.rotation.eulerAngles.z;
            // turret.transform.rotation = Quaternion.LookRotation(relativePos.normalized, Vector3.up);
            // rotation.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
            //turret.transform.rotation = rotation;

            // Quaternion q = turret.transform.GetChild(0).rotation;

            // q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, Quaternion.identity.z);

            // turret.transform.rotation = q;
            // Vector3 horz = new Vector3(lookAtTarget.position.x, turret.transform.position.y, lookAtTarget.position.z);
            // Vector3 relativePos = lookAtTarget.position - transform.position;
            // // turret.transform.LookAt(relativePos, Vector3.up);

            // // turret.transform.Ro


            // // turret.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
            // // turret.transform.GetChild(0).Rotate(Input.GetAxis("Mouse Y"), 0, 0);


            // // turret.transform.Rotate(0, Mathf.Lerp(turret.transform.rotation.eulerAngles.y, 0, Time.deltaTime), 0);


            // // Quaternion q = turret.transform.rotation;
            // // Vector3 rot = new Vector3(0, Mathf.Lerp(turret.transform.localEulerAngles.y, (Mathf.Atan2(turret.transform.position.x - lookAtTarget.position.x, turret.transform.position.z - lookAtTarget.position.z) * (180 / Mathf.PI) + 180), Time.deltaTime), 0);
            // // Debug.Log(Mathf.Atan2(turret.transform.position.y - lookAtTarget.position.y, turret.transform.position.x - lookAtTarget.position.x) * (180 / Mathf.PI));
            // // q.eulerAngles = rot;
            // // turret.transform.rotation = q;
            // // turret.transform.GetChild(0).Rotate(Input.GetAxis("Mouse Y"), 0, 0);


            // Quaternion q = turret.transform.localRotation;
            // Vector3 rot = new Vector3(turret.transform.rotation.eulerAngles.x, Mathf.Atan2(turret.transform.position.x - lookAtTarget.position.x, turret.transform.position.z - lookAtTarget.position.z) * (180 / Mathf.PI) + 180, turret.transform.rotation.eulerAngles.z);

            // q.eulerAngles = rot;
            // turret.transform.rotation = q;


            ////////////////////////////
            // Quaternion q = turret.transform.rotation;
            // Vector3 rot = new Vector3(0, turret.transform.rotation.eulerAngles.y, 0);
            // q.eulerAngles = rot;
            // turret.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
            // Debug.Log(turret.transform.GetChild(0).transform.localEulerAngles.x + " " + Input.GetAxis("Mouse Y"));
            ////////////////////////////

            if (Input.GetButtonDown("A_xbox") && grounded)
            {
                Debug.Log("Jump");
                body.AddForce(transform.up * 1000f, ForceMode.Impulse);

            }
            if (Input.GetAxis("Mouse Y") > 0 && lookval > -50)
            {

                lookval += Input.GetAxis("Mouse Y") * lookSensitivity;
            }
            else if (Input.GetAxis("Mouse Y") < 0 && lookval < 15)
            {

                lookval += Input.GetAxis("Mouse Y") * lookSensitivity;
            }
            if (lookval < -50)
            {
                lookval = -49.5f;
            }
            else if (lookval > 15)
            {
                lookval = 14.9f;
            }
            //Debug.Log(lookval);
            Quaternion eulerRots = Quaternion.Euler(lookval, Quaternion.identity.y, Quaternion.identity.z);
            //hoverPoints[0].transform.localRotation = Quaternion.Slerp(hoverPoints[0].transform.localRotation, eulerRot, Time.deltaTime * 5);
            turret.transform.GetChild(0).localRotation = Quaternion.Slerp(turret.transform.GetChild(0).localRotation, eulerRots, Time.deltaTime * 5);
            // /Debug.Log(turret.transform.GetChild(0).localEulerAngles.x);
            // turret.transform.GetChild(0).localRotation.eulerAngles.x = Mathf.Clamp(turret.transform.GetChild(0).localEulerAngles.x, -10, 30);

            //turret.transform.GetChild(0).Rotate(Input.GetAxis("Mouse Y"), 0, 0);
























            if (Input.GetAxis("Mouse X") != 0)
            {
                lookvalX += Input.GetAxis("Mouse X") * lookSensitivity;
            }

            // Vector3 xAxisrot = new Vector3(Quaternion.identity.x, lookvalX, Quaternion.identity.z);
            Quaternion eulerRotsX = Quaternion.Euler(Quaternion.identity.x, lookvalX, Quaternion.identity.z);
            //hoverPoints[0].transform.localRotation = Quaternion.Slerp(hoverPoints[0].transform.localRotation, eulerRot, Time.deltaTime * 5);
            turret.transform.localRotation = Quaternion.Slerp(turret.transform.localRotation, eulerRotsX, Time.deltaTime * 5);





            //rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);

            // Quaternion worldRotation = turret.transform.parent.rotation * turret.transform.localRotation;
            // turret.transform.eulerAngles = new Vector3(worldRotation.x, rotation.y, worldRotation.z);
            /* Vector3 relativePosC = lookAtTarget.position - turret.transform.GetChild(0).position;
             Quaternion rotationC = Quaternion.LookRotation(relativePosC, Vector3.up);
             rotationC.eulerAngles = new Vector3(rotationC.eulerAngles.x, turret.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
             turret.transform.GetChild(0).rotation = rotationC;*/
            //// turret.transform.localEulerAngles = new Vector3(0, turrChaser.rotation.eulerAngles.y, 0);
            //Debug.Log(turret.transform.eulerAngles.y + " is turret " + turrChaser.localEulerAngles.y + " is temp");

            /* Vector3 dir =lookAtTarget.position - transform.position;
             Quaternion lookRot = Quaternion.LookRotation(dir);
             Vector3 rot = Quaternion.Lerp(turret.transform.rotation, lookRot, Time.deltaTime * 1).eulerAngles;
             turret.transform.rotation = Quaternion.Euler(turret.transform.rotation.x, rot.y, turret.transform.rotation.z);
             turret.transform.GetChild(0).rotation = Quaternion.Euler(rot.x, rot.y, 0f);*/
            // var lookPos = lookAtTarget.position - transform.position;
            //lookPos.y = 0;
            // var rotation = Quaternion.LookRotation(lookPos);
            // turret.transform.localRotation = Quaternion.Slerp(turret.transform.rotation, rotation, Time.deltaTime );
            // Debug.Log(turret.transform.eulerAngles.z + " is turret " + transform.eulerAngles.z + " is main");

        }

        // Main Thrust

        thrust = 0.0f;
        float acceleration = Input.GetAxis("Vertical");


        if (acceleration > deadZone)
            thrust = acceleration * forwardAcceleration + (10 * inv.DriveSpeed.Value);
        else if (acceleration < -deadZone)
            thrust = acceleration * reverseAcceleration;

        // Turning
        turnValue = 0.0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(turnAxis) > deadZone)
            turnValue = turnAxis;


        if (Input.GetAxis("Fire1") == 1 && canFire)
        {
            StartCoroutine("FireCannon");
            canFire = false;
        }
    }
    IEnumerator FireCannon()
    {
        // object[] tempStorage = new object[2];
        // tempStorage[0] = inv.Damage.Value;
        // tempStorage[1] = inv.DamageEffects;
        float damageConvert = inv.Damage.Value / 5;
        Rigidbody bull = Instantiate(bullet, turret.transform.GetChild(0).GetChild(0).position, turret.transform.GetChild(0).GetChild(0).rotation) as Rigidbody;
        bull.transform.localScale = new Vector3(damageConvert, damageConvert, damageConvert);
        ////bull.SendMessage("ApplyDamage", tempStorage);
        bull.GetComponent<Bullet>().ApplyDamage(inv.Damage.Value, inv.DamageEffects);
        //bull.SendMessage("DamageType", inv.DamageEffects);
        // Vector3 localForward = bull.transform.worldToLocalMatrix.MultiplyVector(bull.transform.forward);
        bull.AddForce(bull.transform.forward * 1000 * inv.BulletSpeed.Value);
        yield return new WaitForSeconds(delay * 5 / inv.FireRate.Value);
        canFire = true;
    }

    private void FixedUpdate()
    {
        //  Hover Force
        RaycastHit hit;

        for (int i = 0; i < hoverPoints.Length; i++)
        {
            var hoverPoint = hoverPoints[i];
            Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hoverHeight, layerMask);
            if (hoverPoint.transform.position.y - hit.point.y < hoverHeight)
            {
                grounded = true;
                body.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
            }
            else
            {

                grounded = false;
            }
            // if (Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hoverHeight, layerMask))
            // {
            //     body.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);

            //     grounded = true;

            // }
            // else
            // {
            //     grounded = false;
            //     // Self levelling - returns the vehicle to horizontal when not grounded
            //     // if (transform.position.y > hoverPoint.transform.position.y)
            //     // {
            //     //     body.AddForceAtPosition(hoverPoint.transform.up * gravityForce, hoverPoint.transform.position);
            //     // }
            //     // else
            //     // {
            //     //     body.AddForceAtPosition(hoverPoint.transform.up * -gravityForce, hoverPoint.transform.position);
            //     // }
            // }
            // Debug.Log(hoverPoint.transform.position.y - hit.point.y);
            Debug.DrawRay(hoverPoint.transform.position, -hoverPoint.transform.up, Color.red);

        }


        var emissionRate = 0;
        if (grounded)
        {

            body.drag = m_groundedDrag;
            emissionRate = 10;
        }
        else
        {
            body.drag = 0.1f;
            thrust /= 100f;
            //turnValue /= 100f;
        }

        for (int i = 0; i < dustTrails.Length; i++)
        {
            var emission = dustTrails[i].emission;
            emission.rate = new ParticleSystem.MinMaxCurve(emissionRate);
        }

        if (Mathf.Abs(thrust) > 0)
        {
            driveSpeed = Mathf.Lerp(driveSpeed, thrust, 0.05f);
        }
        else
        {
            driveSpeed = Mathf.Lerp(driveSpeed, thrust, 0.005f);//coast
        }
        engine.pitch = Mathf.Clamp((Mathf.Lerp(engine.pitch, (thrust / forwardAcceleration), Time.deltaTime)), 0.2f, 1);//(motor / maxMotorTorque);

        body.AddForce(transform.forward * driveSpeed);
        // Handle Turn forces
        //Debug.Log(turnValue);

        Quaternion eulerRot = Quaternion.Euler(Quaternion.identity.x, turnValue * 40, Quaternion.identity.z);
        if (thrust < 0)
            flip = -1;
        else
            flip = 1;


        dir += (body.velocity.sqrMagnitude / 5f) * flip;


        if (turnValue > 0 && body.velocity.sqrMagnitude > 1)
        {
            body.AddRelativeTorque(Vector3.up * turnValue * turnStrength);


        }
        else if (turnValue < 0 && body.velocity.sqrMagnitude > 1)
        {
            body.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
        }
        //Debug.Log(thrust);

        //hoverPoints[0].transform.localRotation = eulerRot;
        hoverPoints[0].transform.localRotation = Quaternion.Slerp(hoverPoints[0].transform.localRotation, eulerRot, Time.deltaTime * 5);
        hoverPoints[1].transform.localRotation = Quaternion.Slerp(hoverPoints[1].transform.localRotation, eulerRot, Time.deltaTime * 5);

        hoverPoints[0].transform.GetChild(0).localRotation = Quaternion.Euler(dir, hoverPoints[0].transform.GetChild(0).localRotation.y, hoverPoints[0].transform.GetChild(0).localRotation.z + 90);
        hoverPoints[1].transform.GetChild(0).localRotation = Quaternion.Euler(dir, hoverPoints[1].transform.GetChild(0).localRotation.y, hoverPoints[1].transform.GetChild(0).localRotation.z + 90);
        hoverPoints[2].transform.GetChild(0).localRotation = Quaternion.Euler(dir, hoverPoints[2].transform.GetChild(0).localRotation.y, hoverPoints[2].transform.GetChild(0).localRotation.z + 90);
        hoverPoints[3].transform.GetChild(0).localRotation = Quaternion.Euler(dir, hoverPoints[3].transform.GetChild(0).localRotation.y, hoverPoints[3].transform.GetChild(0).localRotation.z + 90);
        // hoverPoints[1].transform.Rotate(Quaternion.identity.x, Quaternion.identity.y, turnValue);

        // Limit max velocity
        if (body.velocity.sqrMagnitude > (body.velocity.normalized * maxVelocity).sqrMagnitude)
        {
            body.velocity = body.velocity.normalized * maxVelocity;
        }
    }
}
