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
    public ItemSpawner ItemSpawner;

    [HideInInspector] public PlayerController Player;
    [HideInInspector] public List<EnemyController> Enemies;

    private List<UnitScript> unitScripts;
    private PlayerController playerController;

    private void Start()
    {
        unitScripts = Units.GetComponentsInChildren<UnitScript>().ToList();
        Player = Units.GetComponentInChildren<PlayerController>();
        Enemies = Units.GetComponentsInChildren<EnemyController>().ToList();
        
        Player.SetUp(Obstacles);
        Enemies.ForEach(x => x.SetUp(Player,Camp));
        bool isPlayer;
        UnitScript campUnit = Camp.GetComponent<UnitScript>();
        foreach(UnitScript unit in unitScripts)
        {
            isPlayer = unit.TryGetComponent(out playerController);
            unit.SetUp(
                isPlayer,
                isPlayer || unit == campUnit
                );
        }
        unitScripts.ForEach(x => x.SetUp(x.TryGetComponent(out playerController), true));
        EnemySpawner.SetUp(Player, Units, Camp, this);
    }

    public IEnumerator BossSlain()
    {
        yield return new WaitForEndOfFrame();
        ItemSpawner.Spawn();
    }
}