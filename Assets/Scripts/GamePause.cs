using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public KeyCode escape = KeyCode.Escape;
  
    [SerializeField] private GameObject Score;
    [SerializeField] private GameObject PauseMenu;
    void Start()
    {
        setVisibility(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(escape))
        {
            Time.timeScale = 0.0f;
            setVisibility(false);
        }
    }

    private void setVisibility(bool visible) { 
        PauseMenu.SetActive(!visible);
        Score.SetActive(visible);
    }
    public void onResumeGame() {
        Time.timeScale = 1.0f;
        setVisibility(true);
    }
    public void OnExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
