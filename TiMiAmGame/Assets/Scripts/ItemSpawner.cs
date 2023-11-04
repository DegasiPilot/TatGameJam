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

    public void Spawn()
    {
        direction = new Vector2(
            Random.Range(0, 1),
            Random.Range(-1, 1)
            );
        offset = Random.Range(MinOffset, MaxOffset);
        Instantiate(Item, direction * offset, Quaternion.identity);
    }
}