using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public float SeconsBeforDestroy;

    private GameManager gameManager;

    public void SetUp(GameManager gameManager)
    {
        this.gameManager = gameManager;
        StartCoroutine(DestroyTimer());
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

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(SeconsBeforDestroy);
        Destroy(gameObject);
        gameManager.Lose("Не успел подобрать ингридиент!");
    }
}
