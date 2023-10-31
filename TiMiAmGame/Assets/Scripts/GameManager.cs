using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Units;
    public EnemySpawner EnemySpawner;
    public GameObject Obstacles;
    public GameObject Camp;

    [HideInInspector] public PlayerController Player;
    [HideInInspector] public List<EnemyController> Enemies;

    private List<UnitScript> unitScripts;
    private PlayerController playerController;

    private void Start()
    {
        Player = Units.GetComponentInChildren<PlayerController>();
        Enemies = Units.GetComponentsInChildren<EnemyController>().ToList();
        unitScripts = Units.GetComponentsInChildren<UnitScript>().ToList();

        Player.SetUp(Obstacles);
        Enemies.ForEach(x => x.SetUp(Player,Camp));
        unitScripts.ForEach(x => x.SetUp(x.TryGetComponent(out playerController)));
        EnemySpawner.SetUp(Player, Units, Camp);
    }
}