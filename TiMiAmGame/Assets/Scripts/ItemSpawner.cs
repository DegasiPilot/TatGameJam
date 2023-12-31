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

    public void Setup()
    {
        EventManager.OnBossSlain.AddListener(Spawn);
    }

    public void Spawn()
    {
        direction = new Vector2(
            Random.value,
            Random.value * Random.Range(-1,1)
            );
        direction = direction == Vector2.zero ? Vector2.right : direction.normalized;
        offset = Random.Range(MinOffset, MaxOffset);
        Instantiate(Item, (Vector2)transform.position + direction * offset, Quaternion.identity).GetComponent<ItemScript>();
    }
}