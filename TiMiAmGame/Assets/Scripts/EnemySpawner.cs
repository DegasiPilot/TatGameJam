using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float SpawnDelay;
    public float SpawnCount;
    public GameObject Boss;
    public float BossDelay;

    private Transform[] Spawns;
    private PlayerController player;
    private GameObject units;

    public void SetUp(PlayerController player, GameObject Units)
    {
        Spawns = GetComponentsInChildren<Transform>().Where(x => x!= transform).ToArray();
        this.player = player;
        units = Units;
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        int i = 0;
        while (i < SpawnCount)
        {
            Transform spawn = Spawns[Random.Range(0, Spawns.Count())];
            GameObject enemy = Enemies[Random.Range(0, Enemies.Count())];
            enemy = Instantiate(enemy, spawn);
            enemy.GetComponent<EnemyController>().SetUp(player);
            enemy.GetComponent<UnitScript>().SetUp();
            enemy.transform.SetParent(units.transform);
            i++;
            yield return new WaitForSeconds(SpawnDelay);
        }
        StartCoroutine(SpawnBoss());
    }

    public IEnumerator SpawnBoss()
    {
        yield return  new WaitForSeconds(BossDelay);
        Transform spawn = Spawns[Random.Range(0, Spawns.Count())];
        GameObject enemy = Instantiate(Boss, spawn);
        enemy.GetComponent<EnemyController>().SetUp(player);
        enemy.GetComponent<UnitScript>().SetUp();
        enemy.transform.SetParent(units.transform);
    }
}
