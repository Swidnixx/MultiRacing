using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3[] positions;
    public CinemachineVirtualCamera cam;
    CinemachineTransposer transposer;
    int activePositon;

    private void Start()
    {
        transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = positions[activePositon];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            activePositon++;
            activePositon %= positions.Length;
            transposer.m_FollowOffset = positions[activePositon];
        }
    }

    public void SetCameraProperties(GameObject car)
    {
        cam.Follow = car.transform;
        cam.LookAt = car.transform;
    }
}
