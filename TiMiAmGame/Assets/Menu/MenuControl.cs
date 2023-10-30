using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpnSett()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
