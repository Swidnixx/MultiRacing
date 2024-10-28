using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int lap = 0;
    public int checkpoint = -1;
    int pointCount;
    public int nextPoint;

    public GameObject lastPoint;

    private void Start()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        pointCount = checkpoints.Length;

        foreach(GameObject checkpoint in checkpoints)
        {
            if(checkpoint.name == "0")
                lastPoint = checkpoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int number = int.Parse(other.name);
            if( number == nextPoint )
            {
                lastPoint = other.gameObject;
                checkpoint = nextPoint;
                nextPoint++;

                nextPoint = nextPoint % pointCount;

                if(checkpoint == 0)
                {
                    lap++;
                    Debug.Log("Lap: " + lap);
                }
            }
        }
    }
}
