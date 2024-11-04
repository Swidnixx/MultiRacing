using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public TMP_InputField playerName;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Color color;

    private void Start()
    {
        playerName.text = PlayerPrefs.GetString("PlayerName");
        redSlider.value = PlayerPrefs.GetFloat("R");
        greenSlider.value = PlayerPrefs.GetFloat("G");
        blueSlider.value = PlayerPrefs.GetFloat("B");
    }

    private void Update()
    {
        SetCarColor(redSlider.value, 
            greenSlider.value, blueSlider.value);
    }

    void SetCarColor(float r, float g, float b)
    {
        color = new Color(r, g, b);
        PlayerPrefs.SetFloat("R", r);
        PlayerPrefs.SetFloat("G", g);
        PlayerPrefs.SetFloat("B", b);
    }

    public void SetPlayerName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

    public void StartRace()
    {
        SceneManager.LoadScene(1);
    }
}
