using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public List<EnemyController> Enemies;

    private void Start()
    {
        Player.GetComponent<PlayerController>().SetUp();
        Enemies.ForEach(x => x.SetUp(Player));
    }
}
