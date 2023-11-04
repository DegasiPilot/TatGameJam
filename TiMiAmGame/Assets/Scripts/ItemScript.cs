using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public float SeconsBeforDestroy;

    private GameManager gameManager;
    private float timeRemain;

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
        timeRemain = SeconsBeforDestroy;
        while(timeRemain > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemain -= 1;
            gameManager.SetTimeRemain(timeRemain);
        }
        Destroy(gameObject);
        gameManager.Lose("�� ����� ��������� ����������!");
    }
}
