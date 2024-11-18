using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarAppearance : MonoBehaviour
{
    public string playerName;
    public Color carColor;
    public TMP_Text nameText;
    public Renderer[] carRenderers;

    public int playerNumber;

    public void SetLocalPlayer()
    {
        playerName = PlayerPrefs.GetString("PlayerName");
        carColor = new Color(PlayerPrefs.GetFloat("R"),
            PlayerPrefs.GetFloat("G"), PlayerPrefs.GetFloat("B")); 

        nameText.text = playerName;
        nameText.color = carColor;

        foreach (Renderer r in carRenderers)
        {
            r.material.color = carColor;
        }

        FindObjectOfType<CameraController>().SetCameraProperties(this.gameObject);
    }

    public void SetNameAndColor(string name, Color color)
    {
        nameText.text = name;
        nameText.color = color;

        foreach (Renderer r in carRenderers)
        {
            r.material.color = color;
        }
    }
}
