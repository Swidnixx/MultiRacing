using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript driveScript;
    float lastTimeMoving;
    CheckpointController checkpointController;

    private void Start()
    {
        driveScript = GetComponent<DrivingScript>();
        checkpointController = GetComponent<CheckpointController>();
    }

    private void Update()
    {
        if (checkpointController.lap > RaceController.totalLaps) return;
        FailCheck();

        if (RaceController.racePending == false) return;

        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");
        driveScript.Drive(accel, brake, steer);
    }

    void FailCheck()
    {
        if (driveScript.rb.velocity.magnitude > 1 || !RaceController.racePending)
        {
            lastTimeMoving = Time.time;
        }
        if(Time.time > lastTimeMoving + 5)
        {
            Vector3 fixPos = checkpointController.lastPoint.transform.position;
            fixPos.y = 0;
            Vector3 carPos = transform.position;
            carPos.y = 0;
            float distance = Vector2.Distance(fixPos,carPos);
            if (distance < 0.5f) return;
            //kraksa
            driveScript.rb.position = checkpointController.lastPoint.transform.position;
            driveScript.rb.rotation = checkpointController.lastPoint.transform.rotation;
        }
    }
}
