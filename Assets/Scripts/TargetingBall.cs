using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBall : MonoBehaviour
{
    public Transform playerTurret;
    public float lookSensitivity = 0.2f;
    void Start()
    {
        //transform.localRotation = Quaternion.Euler(playerTurret.rotation.x, playerTurret.rotation.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(Input.GetAxis("Mouse Y") * 0.3f, Input.GetAxis("Mouse X")* 0.3f, 0);
        transform.position = playerTurret.position;

        //transform.RotateAround(offside, Vector3.left, rotateSpeed * Time.deltaTime);
        transform.Rotate(Input.GetAxis("Mouse Y") * lookSensitivity, Input.GetAxis("Mouse X") * lookSensitivity, 0);
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;
    }
}
