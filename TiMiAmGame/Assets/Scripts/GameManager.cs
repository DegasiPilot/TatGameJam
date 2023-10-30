using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Units;

    [HideInInspector] public PlayerController Player;
    [HideInInspector] public List<EnemyController> Enemies;

    private List<UnitScript> unitScripts;

    private void Start()
    {
        Player = Units.GetComponentInChildren<PlayerController>();
        Enemies = Units.GetComponentsInChildren<EnemyController>().ToList();
        unitScripts = Units.GetComponentsInChildren<UnitScript>().ToList();

        Player.SetUp();
        Enemies.ForEach(x => x.SetUp(Player));
        unitScripts.ForEach(x => x.SetUp());
    }
}
