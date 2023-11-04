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
    public GameMenuSettings MenuSettings;

    [HideInInspector] public PlayerController Player;
    [HideInInspector] public List<EnemyController> Enemies;

    private List<UnitScript> unitScripts;
    private List<LoseOnDie> loseScripts;
    private PlayerController playerController;
    private GameObject finalItem;

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
            unit.SetUp(isPlayer);
        }
        unitScripts.ForEach(x => x.SetUp(x.TryGetComponent(out playerController)));
        loseScripts = Units.GetComponentsInChildren<LoseOnDie>().ToList();
        loseScripts.ForEach(x => x.SetUp(this));
        EnemySpawner.SetUp(Player, Units, Camp, this);
        Time.timeScale = 1;
    }

    public IEnumerator BossSlain()
    {
        yield return new WaitForEndOfFrame();
        ItemSpawner.Spawn(this);
    }

    public void Win()
    {
        MenuSettings.OnWin();
        Time.timeScale = 0;
    }

    public void Lose()
    {
        MenuSettings.OnLose();
        Time.timeScale = 0;
    }
}