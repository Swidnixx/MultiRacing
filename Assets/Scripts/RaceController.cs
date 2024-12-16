using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class RaceController : MonoBehaviourPunCallbacks
{
    public GameObject startButton;
    public GameObject waitingText;

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

    int playerNumber;

    public void CallStartRaceRPC()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            photonView.RPC(nameof(StartRace), RpcTarget.All); 
        }
    }
    [PunRPC] //Remote Procedure Call - Zdalne wywo³anie Procedury
    public void StartRace()
    {
        startButton.SetActive(false);
        waitingText.SetActive(false);
        InvokeRepeating(nameof(CountDown), 3, 1);
        cars = GameObject.FindObjectsOfType<CheckpointController>();
    }

    private void Start()
    {
        startButton.SetActive(false);
        waitingText.SetActive(false);

        endRacePanel.SetActive(false);
        startText.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        //new version
        playerNumber = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        if(PhotonNetwork.IsConnected)
        {
            Vector3 startPos = spawnPos[playerNumber].position;
            Quaternion startRot = spawnPos[playerNumber].rotation;
            object[] instanceData = new object[4];
            instanceData[0] = PlayerPrefs.GetString("PlayerName");
            instanceData[1] = PlayerPrefs.GetFloat("R");
            instanceData[2] = PlayerPrefs.GetFloat("G");
            instanceData[3] = PlayerPrefs.GetFloat("B");
            GameObject player = PhotonNetwork.Instantiate(carPrefab.name, startPos, startRot, 0, instanceData);

            if (PhotonNetwork.IsMasterClient)
                startButton.SetActive(true);
            else
                waitingText.SetActive(true);

            player.GetComponentInChildren<PlayerController>().enabled = true;
            //configure CarAppearance
            //configureCamera
        }
    }

    private void LateUpdate()
    {
        if (!racePending) return;

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

    [PunRPC]
    public void RestartRPC()
    {
       timer = 4;

        OnlinePlayer.LocalPlayerInstance.GetComponentInChildren<DrivingScript>()
            .StopRbForces();

        OnlinePlayer.LocalPlayerInstance.transform.GetChild(0).position =
            spawnPos[playerNumber].position;
        OnlinePlayer.LocalPlayerInstance.transform.GetChild(0).rotation =
            spawnPos[playerNumber].rotation;

        foreach (var c in cars)
        {
            c.Restart();
        }

        endRacePanel.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
            startButton.SetActive(true);
        else
            waitingText.SetActive(true);
    }
    public void RestartRace()
    {
        photonView.RPC(nameof(RestartRPC), RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        PhotonNetwork.CurrentRoom.IsOpen = false;
    }
}
