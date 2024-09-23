using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSupport : MonoBehaviour
{
    public float antiRoll = 5000;
    [Header("0 - lewe ko³o, 1 - prawe ko³o")]
    public WheelCollider[] frontWheels = new WheelCollider[2];
    public WheelCollider[] backWheels = new WheelCollider[2];

    Rigidbody rb;

    private void Start()
    {
         rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HoldWheelsOnGround(frontWheels);
        HoldWheelsOnGround(backWheels);
    }

    void HoldWheelsOnGround(WheelCollider[] wheels)
    {
        WheelHit hit;
        float leftRiding = 1;
        float rightRiding = 1;

        bool groundedL = wheels[0].GetGroundHit(out hit);
        if (groundedL) leftRiding = (-wheels[0].transform.InverseTransformPoint(hit.point).y
                - wheels[0].radius) / wheels[0].suspensionDistance;
        else
            leftRiding = 1;

        bool groundedR = wheels[1].GetGroundHit(out hit);
        if (groundedR) rightRiding = (-wheels[1].transform.InverseTransformPoint(hit.point).y
                - wheels[1].radius) / wheels[1].suspensionDistance;
        else
            rightRiding = 1;

        float antiRollForce = (leftRiding - rightRiding) * antiRoll;

        if (groundedL) rb.AddForceAtPosition(wheels[0].transform.up * -antiRollForce,
            wheels[0].transform.position);

        if (groundedR) rb.AddForceAtPosition(wheels[1].transform.up * antiRollForce,
            wheels[1].transform.position);
    }
}
