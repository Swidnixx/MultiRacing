using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardPanel : MonoBehaviour
{
    public TMP_Text[] texts;

    void Start()
    {
        LeaderboardController.Prepare();
    }

    private void LateUpdate()
    {
        List<string> playersOrdered = LeaderboardController.GetPlayersOrdered();
        for(int i=0; i<texts.Length; i++)
        {
            if( i < playersOrdered.Count )
            {
                texts[i].text = playersOrdered[i];
            }
            else
            {
                texts[i].text = "";
            }
        }
    }
}

struct Car
{
    public string name;
    public int position;

    public Car(string n, int p)
    {
        name = n;
        position = p;
    }
}

public static class LeaderboardController
{
    static Dictionary<int, Car> carsRegistered = new Dictionary<int, Car>();

    public static void Prepare() //may be invoked after some cars register?
    {
        carsRegistered.Clear();
    }
    public static List<string> GetPlayersOrdered()
    {
        return carsRegistered.OrderByDescending(c => c.Value.position).Select(c=>c.Value.name).ToList<string>();
    }

    public static int Register(string name)
    {
        int id = carsRegistered.Count;
        Car car = new Car(name, 0);
        carsRegistered[id] = car;
        return id;
    }

    public static void SetPosition(int id, int lap, int checkpoint)
    {
        int p = lap * 10000 + checkpoint;
        Car c = carsRegistered[id];
        c.position = p;
        carsRegistered[id] = c;
    }
}

