using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject Author;
    [SerializeField] private GameObject StartGame;
    [SerializeField] private GameObject EndGame;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject Logo1;
    [SerializeField] private GameObject Logo2;
    [SerializeField] private GameObject Bye;
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        SetExitLogo(false);
        audioMixer.SetFloat("Volume", 5.0f);
        //Music: Bensound.com/free-music-for-videos
    }

    public void OnEndGame()
    {
        SetExitLogo(true);

        delay(1000);
        Application.Quit();
    }

    private void SetExitLogo(bool status)
    {
        Bye.SetActive(status);
        Logo2.SetActive(status);

        Author.SetActive(!status);
        StartGame.SetActive(!status);
        Settings.SetActive(!status);
        EndGame.SetActive(!status);
        Logo1.SetActive(!status);
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("GameScene1");
    }

    public void OnSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    void Update()
    {

    }

    private IEnumerator delay(float x)
    {
        yield return new WaitForSeconds(x);
    }
}
