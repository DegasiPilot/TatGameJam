using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private GameManager gameManager;

    public void SetUp(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeItem();
            Destroy(gameObject);
            gameManager.Win();
        }
    }
}
