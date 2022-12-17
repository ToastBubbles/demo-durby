using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject turret;
    public GameObject turretCannon;
    public Transform bulletSpawn;
    public Rigidbody bullet;
    public Vector3 com;
    private Rigidbody rbSelf;

    public AudioSource engine;
    private float pitchShift;
    private float brakes;


    public List<AxleInfo> axleInfos; // the information about each individual axle

    public WheelCollider[] wheels;
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float turboTorque;
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    float angleKeep = 0;
    float vel = 0f;
    float currentSteeringMax = 30;
    float steeringAngle = 30;

    public bool snappyTurret = false;

    private bool canFire = true;
    private float delay = 1f;


    void Start()
    {
        rbSelf = GetComponent<Rigidbody>();
        rbSelf.centerOfMass = com;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.rotation * com, 1f);
    }
    void Update()
    {

        turret.transform.Rotate(0f, Input.GetAxis("Mouse X") * 0.5f, 0f, Space.Self);

        angleKeep -= Input.GetAxis("Mouse Y");

        if(angleKeep < 0)
        {
            angleKeep = 0;
        }else if(angleKeep > 90)
        {
            angleKeep = 90;
        }
        Quaternion eulerRot = Quaternion.Euler(Input.GetAxis("Mouse Y") * 50, turretCannon.transform.eulerAngles.y, turretCannon.transform.eulerAngles.z);

        if (snappyTurret)
        {
            
            turretCannon.transform.rotation = Quaternion.Slerp(turretCannon.transform.rotation, eulerRot, Time.deltaTime * 10);
        }
        else
        {
            turretCannon.transform.Rotate(Input.GetAxis("Mouse Y") * 0.3f, 0f, 0f, Space.Self);

        }


   
        if (Input.GetAxis("Fire1") == 1 && canFire)
        {
            StartCoroutine("FireCannon");
            canFire = false;
        }
        if (Input.GetAxis("LT") == 1)
        {
            Debug.Log("turbo");
        }

        Debug.Log(wheels[1].brakeTorque);



    }




    IEnumerator FireCannon()
    {
        Rigidbody bull = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation) as Rigidbody;
        // Vector3 localForward = bull.transform.worldToLocalMatrix.MultiplyVector(bull.transform.forward);
        bull.AddForce(bull.transform.forward * 5000);
        yield return new WaitForSeconds(delay);
        canFire = true;
    }
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }


    public void FixedUpdate()
    {

        
        steeringAngle = Mathf.SmoothDamp(currentSteeringMax, maxSteeringAngle, ref vel, 0.3f);

        float motor = maxMotorTorque * Input.GetAxis("Vertical") + (turboTorque * Input.GetAxis("LT"));
        float steering = steeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
        engine.pitch = Mathf.Clamp((Mathf.Lerp(engine.pitch, (motor / maxMotorTorque), Time.deltaTime)),0.2f,1);//(motor / maxMotorTorque);

        
        if (Input.GetAxis("A") > 0)
        {
            Debug.Log("Braking");
            brakes = 5000;
        }
        else
        {
            brakes = 0f;
        }
        foreach (WheelCollider wc in wheels)
        {
            wc.brakeTorque = brakes;
        }

        maxSteeringAngle = (35f - ((motor / turboTorque)*10));

       // Debug.Log(wheels[1].rpm);
        
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}

