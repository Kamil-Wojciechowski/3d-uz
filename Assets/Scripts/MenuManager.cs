using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject Author;
    public GameObject StartGame;
    public GameObject EndGame;
    public GameObject Settings;
    public GameObject LoadGame;
    public GameObject Logo1;
    public GameObject Logo2;
    public GameObject Bye;


    void Start()
    {
        Bye.SetActive(false);
        Logo2.SetActive(false);
        LoadGame.GetComponent<Button>().interactable = false;
    }

    public void OnEndGame()
    {
        Bye.SetActive(true);
        Logo2.SetActive(true);

        Author.SetActive(false);
        StartGame.SetActive(false);
        Settings.SetActive(false);
        EndGame.SetActive(false);
        LoadGame.SetActive(false);
        Logo1.SetActive(false);

        delay(1000);
        Application.Quit();
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
