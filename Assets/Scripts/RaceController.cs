using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{
    public static bool racePending = false;
    public static int totalLaps = 1;
    public int timer = 4;
    public TMP_Text startText;
    public AudioClip count;
    public AudioClip start;
    public GameObject endRacePanel;

    public GameObject carPrefab;
    public Transform[] spawnPos;
    public int playerCount = 4;

    CheckpointController[] cars;
    AudioSource audioSource;

    private void Start()
    {
        endRacePanel.SetActive(false);
        startText.gameObject.SetActive(false);

        InvokeRepeating(nameof(CountDown), 3, 1);
        audioSource = GetComponent<AudioSource>();

        for(int i=0; i<playerCount; i++)
        {
            GameObject car = Instantiate(carPrefab).transform.GetChild(0).gameObject;
            car.transform.position = spawnPos[i].position;
            car.transform.rotation = spawnPos[i].rotation;
            car.GetComponent<CarAppearance>().playerNumber = i;
            if(i == 0)
            {
                car.GetComponent<PlayerController>().enabled = true;
                GameObject.FindObjectOfType<CameraController>().SetCameraProperties(car);
            }
        }

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
            endRacePanel.SetActive(true);
            racePending = false;
        }
    }

    void CountDown()
    {
        startText.gameObject.SetActive(true);

        timer--;
        startText.text = "Race starts in: " + timer;

        if(timer < 1)
        {
            startText.text = "Start!";
            audioSource.PlayOneShot(start);
            racePending = true;
            CancelInvoke();
            Invoke(nameof(HideStartText), 1.6f);
        }
        else
        {
            audioSource.PlayOneShot(count);
        }
    }

    void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }

    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
