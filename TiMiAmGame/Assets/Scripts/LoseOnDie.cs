using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnDie : MonoBehaviour
{
    public string Name;

    private GameManager gameManager;

    public void SetUp(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void OnDestroy()
    {
        try
        {
            gameManager.Lose(Name + " потерял все жизни");
        }
        catch { }
    }
}
