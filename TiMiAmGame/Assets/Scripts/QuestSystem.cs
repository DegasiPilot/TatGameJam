using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    public Text QuestText;
    public Text TimeRemainText;

    public float SecondsForItemQuest;
    private float timeRemain;

    public void Setup()
    {
        EventManager.OnBossSlain.AddListener(StartItemQuest);
    }

    public void StartItemQuest()
    {
        StartCoroutine(DestroyItemTimer());
    }

    private IEnumerator DestroyItemTimer()
    {
        timeRemain = SecondsForItemQuest;
        while (timeRemain > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemain -= 1;
            SetTimeRemain(timeRemain);
        }
        EventManager.Lose("Не успел подобрать ингридиент!");
        Destroy(gameObject);
    }

    public void SetTimeRemain(float time)
    {
        TimeRemainText.text = $"Осталось времени: {time}";
    }

    public void SetQuest(string text)
    {
        QuestText.text = text;
    }
}
