using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public GameObject resolutionDropdown;
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
        resolutions = Screen.resolutions;
        Dropdown dropdown = resolutionDropdown.GetComponent<Dropdown>();
        resolutionDropdown.GetComponent<Dropdown>().ClearOptions();
        List<string> options = new List<string>();
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string item = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(item);
            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height) {
                currentIndex = i;
            }
        }
        resolutionDropdown.GetComponent<Dropdown>().AddOptions(options);
        resolutionDropdown.GetComponent<Dropdown>().value = currentIndex;
        resolutionDropdown.GetComponent<Dropdown>().RefreshShownValue();

    }
}
