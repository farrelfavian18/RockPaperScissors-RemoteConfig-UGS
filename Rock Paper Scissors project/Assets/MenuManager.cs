using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Toggle toggleMute;
    [SerializeField] Slider sliderMaster;
    [SerializeField] TextMeshProUGUI masterVolume;
    [SerializeField] Slider sliderMusic;
    [SerializeField] TextMeshProUGUI musicVolume;
    [SerializeField] Slider sliderSFX;
    [SerializeField] TextMeshProUGUI sfxVolume;
    [SerializeField] TMP_Dropdown windowVideo;
    [SerializeField] FullScreenMode fullScreenMode;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mute()
    {
        if (toggleMute.isOn)
        {
            sliderMaster.interactable = false;
            sliderMusic.interactable = false;
            sliderSFX.interactable = false;
            Debug.Log("Volume Audio : Muted");
        }
        else
        {
            sliderMaster.interactable = true;
            sliderMusic.interactable = true;
            sliderSFX.interactable = true;
            Debug.Log("Volume Audio : Unmuted");
        }
    }

    public void MasterVolumeChanged(float sliderValue)
    {
        var masterValue = Mathf.FloorToInt(sliderValue * 100);
        masterVolume.text = masterValue.ToString();
        Debug.Log("Volume Master : " + masterVolume.text);
    }

    public void MusicVolumeChanged(float sliderValue)
    {
        var musicValue = Mathf.FloorToInt(sliderValue * 100);
        musicVolume.text = musicValue.ToString();
        Debug.Log("Volume BGM : " + musicVolume.text);
    }

    public void SFXVolumeChanged(float sliderValue)
    {
        var sfxValue = Mathf.FloorToInt(sliderValue * 100);
        sfxVolume.text = sfxValue.ToString();
        Debug.Log("Volume SFX : " + sfxVolume.text);
    }


    public void WindowChanged()
    {
        var window = windowVideo.options[windowVideo.value].text;
        Resolution currentResolution = Screen.currentResolution;
        if (window == "Fullscreen")
        {
            fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (window == "Maximized Window")
        {
            fullScreenMode = FullScreenMode.MaximizedWindow;
        }
        else if (window == "Windowed")
        {
            fullScreenMode = FullScreenMode.Windowed;
        }
        Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreenMode);
        Debug.Log("Resolution : " + currentResolution.width.ToString() + " x " + currentResolution.height.ToString());
        Debug.Log("Window : " + window);
    }

    public void SaveSettings()
    {

    }

    public void QuitSettings()
    {

    }

    public void LoadScene(int sceneIndex)
    {
        Debug.Log("Scene : " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
