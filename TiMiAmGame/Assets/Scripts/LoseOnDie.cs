using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnDie : MonoBehaviour
{
    private GameManager gameManager;

    public void SetUp(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void OnDestroy()
    {
        gameManager.Lose();
    }
}
