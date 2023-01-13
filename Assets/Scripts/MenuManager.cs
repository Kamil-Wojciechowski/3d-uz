using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Resolution[] resolutions;
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreen, vsync;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private GameObject SettingsView;
    [SerializeField] private GameObject ExitView;
    [SerializeField] private GameObject MenuView;

    void Start()
    {
        float volume = 0;
        SetExitView(false);
        SetSettingsView(false);
        SetMenuView(true);
        SetResolutions();
        SetToggles();
        audioSource.Play();
        
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            volume = PlayerPrefs.GetFloat("masterVolume");
        }
        volumeSlider.value = volume;
        //Music: Bensound.com/free-music-for-videos
    }

    private void SetExitView(bool status)
    {
        ExitView.SetActive(status);
    }

    private void SetSettingsView(bool status)
    {
        SettingsView.SetActive(status);
    }

    private void SetMenuView(bool status)
    {
        MenuView.SetActive(status);
    }

    public void SetVolume(float volume)
    {
       audioMixer.SetFloat("Volume", volume);
    }
    public void SetRes(int res, bool fullscreen)
    {
        Resolution resolution = resolutions[res];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
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

    private void SetResolutions()
    {
        resolutions = Screen.resolutions.Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string item = resolutions[i].width + " x " + resolutions[i].height;
            if (Screen.currentResolution.refreshRate == resolutions[i].refreshRate) {
                options.Add(item);
            }
            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                currentIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SaveSettings()
    {
        if (vsync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        SetRes(resolutionDropdown.value, fullscreen.isOn);

        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void CancelSettings()
    {
        float volume = 0;
        SetSettingsView(false);
        SetMenuView(true);
        SetResolutions();
        SetToggles();
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            volume = PlayerPrefs.GetFloat("masterVolume");
        }
        volumeSlider.value = volume;
    }
    public void OnEndGame()
    {
        SetExitView(true);
        SetSettingsView(false);
        SetMenuView(false);
        StartCoroutine(delay(10));
    }

   

    public void OnStartGame()
    {
        SceneManager.LoadScene("GameScene1");
    }

    public void OnSettings()
    {
        SetSettingsView(true);
        SetMenuView(false);
    }

    private IEnumerator delay(float x)
    {
        yield return new WaitForSeconds(x);
        Application.Quit();
    }
}
