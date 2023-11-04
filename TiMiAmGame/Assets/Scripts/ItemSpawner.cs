using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject Item;
    public float MinOffset;
    public float MaxOffset;

    private Vector2 direction;
    private float offset;

    public void Spawn(GameManager gameManager)
    {
        direction = new Vector2(
            Random.Range(0, 1),
            Random.Range(-1, 1)
            );
        offset = Random.Range(MinOffset, MaxOffset);
        ItemScript item = Instantiate(Item, (Vector2)transform.position + direction * offset, Quaternion.identity).GetComponent<ItemScript>();
        item.SetUp(gameManager);
    }
}
