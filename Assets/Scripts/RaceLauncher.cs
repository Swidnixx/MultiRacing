using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public TMP_InputField playerName;
    public TMP_Text networkText;
    bool isConnecting;
    byte maxPlayerPerRoom = 4;
    string raceScene = "TestTrack";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            networkText.text += "Connected to master \n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to join random room.\n";
        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            MaxPlayers = this.maxPlayerPerRoom
        });
    }

    public override void OnJoinedRoom()
    {
        networkText.text = "Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + "players.\n";
        PhotonNetwork.LoadLevel(raceScene);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += "Disconnected because " + cause + "\n";
        isConnecting = false;
    }

    public void Connect()
    {
        networkText.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;
        if(PhotonNetwork.IsConnected)
        {
            networkText.text += "Joining Room... \n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting... \n";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
