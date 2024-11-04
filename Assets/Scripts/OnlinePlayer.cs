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
        }
    }
}
