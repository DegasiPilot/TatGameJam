using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<string> OnLose;
    public static UnityEvent OnBossSlain;
    public static UnityEvent OnWin;
    public static UnityEvent OnSetTimeRemain;

    public static void Setup()
    {
        OnLose = new UnityEvent<string>();
        OnBossSlain = new UnityEvent();
        OnWin = new UnityEvent();
    }

    public static void Lose(string cause)
    {
        OnLose.Invoke(cause);
        Time.timeScale = 0;
    }

    public static void Win()
    {
        OnWin.Invoke();
        Time.timeScale = 0;
    }
}
