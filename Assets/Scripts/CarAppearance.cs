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

    private void Start()
    {
        nameText.text = playerName;
        nameText.color = carColor;

        foreach (Renderer r in carRenderers)
        {
            r.material.color = carColor;
        }
    }
}
