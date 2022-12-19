using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCarControls : MonoBehaviour
{
    Rigidbody body;
    float deadZone = 0.1f;
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

    private float turRot;
    float driveSpeed;





    public float turnStrength = 1000f;
    float turnValue = 0f;

    public ParticleSystem[] dustTrails = new ParticleSystem[2];

    int layerMask;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.centerOfMass = Vector3.down;

        layerMask = 1 << LayerMask.NameToLayer("Vehicle");
        layerMask = ~layerMask;
    }
    void Update()
    {


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



            Vector3 relativePos = lookAtTarget.position - turret.transform.position;

            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            rotation.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
            turret.transform.rotation = rotation;

            // Quaternion q = turret.transform.GetChild(0).rotation;

            // q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, Quaternion.identity.z);

            // turret.transform.rotation = q;





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
            thrust = acceleration * forwardAcceleration;
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
        float damageConvert = damage / 20;
        Rigidbody bull = Instantiate(bullet, turret.transform.GetChild(0).GetChild(0).position, turret.transform.GetChild(0).GetChild(0).rotation) as Rigidbody;
        bull.transform.localScale = new Vector3(damageConvert, damageConvert, damageConvert);
        bull.SendMessage("ApplyDamage", damage);
        // Vector3 localForward = bull.transform.worldToLocalMatrix.MultiplyVector(bull.transform.forward);
        bull.AddForce(bull.transform.forward * 5000);
        yield return new WaitForSeconds(delay);
        canFire = true;
    }

    private void FixedUpdate()
    {
        //  Hover Force
        RaycastHit hit;
        bool grounded = false;
        for (int i = 0; i < hoverPoints.Length; i++)
        {
            var hoverPoint = hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hoverHeight, layerMask))
            {
                body.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
                grounded = true;

            }
            else
            {
                // Self levelling - returns the vehicle to horizontal when not grounded
                if (transform.position.y > hoverPoint.transform.position.y)
                {
                    body.AddForceAtPosition(hoverPoint.transform.up * gravityForce, hoverPoint.transform.position);
                }
                else
                {
                    body.AddForceAtPosition(hoverPoint.transform.up * -gravityForce, hoverPoint.transform.position);
                }
            }
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
