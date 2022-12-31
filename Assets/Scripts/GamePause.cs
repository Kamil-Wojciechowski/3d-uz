using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private KeyCode escape = KeyCode.Escape;
    [SerializeField] private GameObject Score;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject healthText;
    void Start()
    {
        Time.timeScale = 1.0f;
        SetVisibility(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(escape))
        {
            Time.timeScale = 0.0f;
            SetVisibility(false);
        }
    }

    private void SetVisibility(bool visible) { 
        PauseMenu.SetActive(!visible);
        Score.SetActive(visible);
        healthText.SetActive(visible);
    }
    public void OnResumeGame() {
        Time.timeScale = 1.0f;
        SetVisibility(true);
    }
    public void OnExitGame()
    {
        SceneManager.LoadScene("MainMenu");
        PhotonNetwork.Disconnect();
    }
}
