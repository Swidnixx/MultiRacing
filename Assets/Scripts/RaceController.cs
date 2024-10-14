using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racePending = false;
    public static int totalLaps = 1;
    public int timer = 4;

    CheckpointController[] cars;

    private void Start()
    {
        InvokeRepeating(nameof(CountDown), 3, 1);
        cars = GameObject.FindObjectsOfType<CheckpointController>();
    }

    private void LateUpdate()
    {
        int finishers = 0;

        foreach(var c in cars)
        {
            if (c.lap >= totalLaps + 1) finishers++;
        }

        if(finishers == cars.Length)
        {
            Debug.Log("Race Finished!");
            racePending = false;
        }
    }

    void CountDown()
    {
        timer--;
        Debug.Log("Race starts in: " + timer);

        if(timer < 1)
        {
            Debug.Log("Start!");
            racePending = true;
            CancelInvoke();
        }
    }
}
