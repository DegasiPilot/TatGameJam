using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameMenuSettings : MonoBehaviour
{
    public AudioMixer am;
    public static bool GameIsPaused = false;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject pauseMenu;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public Text LoseCauseText;
    public Text QuestText;
    public Text TimeRemainText;

    public void Opensettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainnMenuScene");
    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;  
    }

    public void Pause()
    {   
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ValueMusic(float slidermus)
    {
        am.SetFloat("FoneMusic", slidermus);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnWin()
    {
        WinPanel.SetActive(true);
    }

    public void OnLose(string loseCause)
    {
        LosePanel.SetActive(true);
        LoseCauseText.text = loseCause;
    }

    public void SetTimeRemain(float time)
    {
        if (time == 0)
        {
            TimeRemainText.enabled = false;
        }
        else
        {
            TimeRemainText.text = $"Осталось времени: {time}";
        }
    }

    public void SetQuest(string text)
    {
        QuestText.text = text;
    }
}