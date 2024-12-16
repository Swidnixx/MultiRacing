using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    public Wheel[] wheels;
    public Rigidbody rb;

    public float torque = 450;
    public float brakeTorque = 1500;
    public float maxSteerAngle = 30; //maks. k¹t skrêcenia kó³

    public void Drive(float accel, float brake, float steer)
    {
        accel = Mathf.Clamp(accel, -1, 1) * torque;
        brake = Mathf.Clamp01(brake) * brakeTorque;
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;

        foreach(Wheel wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = accel;
            if(wheel.frontWheel)
            {
                wheel.wheelCollider.steerAngle = steer;
            }
            wheel.wheelCollider.brakeTorque = brake;
        }
    }

    public void StopRbForces()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        foreach(var w in wheels)
        {
            w.wheelCollider.motorTorque = 0;
            w.wheelCollider.brakeTorque = brakeTorque;
        }
    }
}
