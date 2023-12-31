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
    private GameObject camp;
    private GameManager gameManager;

    public void SetUp(PlayerController player, GameObject Units, GameObject camp, GameManager gameManager)
    {
        Spawns = GetComponentsInChildren<Transform>().Where(x => x!= transform).ToArray();
        this.player = player;
        units = Units;
        this.camp = camp;
        this.gameManager = gameManager;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnBoss());
    }

    public IEnumerator SpawnEnemies()
    {
        int i = 0;
        while (i < SpawnCount)
        {
            Transform spawn = Spawns[Random.Range(0, Spawns.Count())];
            GameObject enemy = Enemies[Random.Range(0, Enemies.Count())];
            enemy = Instantiate(enemy, spawn);
            enemy.GetComponent<EnemyController>().SetUp(player,camp);
            enemy.GetComponent<UnitScript>().SetUp();
            enemy.transform.SetParent(units.transform);
            i++;
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    public IEnumerator SpawnBoss()
    {
        yield return  new WaitForSeconds(BossDelay);
        gameManager.SetQuest("�������� ���� �����!");
        Transform spawn = Spawns[Random.Range(0, Spawns.Count())];
        GameObject boss = Instantiate(Boss, spawn);
        boss.GetComponent<BossScript>().SetUp(player, gameManager);
        boss.GetComponent<UnitScript>().SetUp();
        boss.transform.SetParent(units.transform);
    }
}