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
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Resolution[] resolutions;
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreen, vsync;
    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetRes(int res, bool fullscreen) {
        Resolution resolution = resolutions[res];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
    }
    void Start()
    {
        SetResolutions();
        SetToggles();
    }

    private void SetToggles()
    {
        fullscreen.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsync.isOn = false;
        }
        else
        {
            vsync.isOn = true;
        }
    }

    private void SetResolutions() {
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
        SetRes(resolutionDropdown.value, fullscreen.isOn);
    }

    public void CancelSettings() {
        SceneManager.LoadScene("MainMenu");
    }
}
