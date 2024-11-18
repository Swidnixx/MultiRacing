using Photon.Pun;
using UnityEngine;

public class OnlinePlayer : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        if(photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
            GetComponentInChildren<CarAppearance>().SetLocalPlayer();
        }
        else
        {
            string playerName = (string)photonView.InstantiationData[0];
            Color playerColor = new Color((float)photonView.InstantiationData[1],
            (float)photonView.InstantiationData[2], (float)photonView.InstantiationData[3]);

            GetComponentInChildren<CarAppearance>().SetNameAndColor(playerName, playerColor);
        }
    }
}
