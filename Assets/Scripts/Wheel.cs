using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool frontWheel;
    WheelCollider wheelCollider;
    Transform wheelModel;

    private void Start()
    {
        wheelCollider = GetComponentInChildren<WheelCollider>();
        wheelModel = GetComponentInChildren<MeshRenderer>().transform;
    }

    private void Update()
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);
        wheelModel.position = position;
        wheelModel.rotation = rotation;
    }
}
