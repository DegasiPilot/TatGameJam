using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsConrol : MonoBehaviour
{
    public bool isFullScreen;
    public GameObject menu;
    public GameObject settings;
    public AudioMixer am;



    public void SetIsFullScreen()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    public void OpnMenu()
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }

    public void ValueMusic(float countzvuk)
    {
        am.SetFloat("FoneMusic", countzvuk);
    }
}
