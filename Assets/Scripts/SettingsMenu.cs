using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public Toggle fullscreen, vsync;
    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
        Debug.Log(volume);
    }

    public void SetFullScreen(bool isFull) {
        Screen.fullScreen = isFull;
    }

    public void SetRes(int res) {
        Resolution resolution = resolutions[res];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    void Start()
    {
        SetResolutions();
        fullscreen.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsync.isOn = false;
        }
        else {
            vsync.isOn = true;
        }

    }

    public void SetResolutions() {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string item = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(item);
            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                currentIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SaveSettings() {
        if (vsync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else { 
            QualitySettings.vSyncCount = 0;
        }
        SetRes(resolutionDropdown.value);
        SetFullScreen(fullscreen);

    }

    public void CancelSettings() {
        SceneManager.LoadScene("MainMenu");
    }
}
