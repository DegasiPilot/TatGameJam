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
    public QuestSystem QuestSystem;

    [HideInInspector] public PlayerController Player;
    [HideInInspector] public List<EnemyController> Enemies;

    private List<UnitScript> unitScripts;
    private PlayerController playerController;

    private void Start()
    {
        unitScripts = Units.GetComponentsInChildren<UnitScript>().ToList();
        Player = Units.GetComponentInChildren<PlayerController>();
        Enemies = Units.GetComponentsInChildren<EnemyController>().ToList();

        EventManager.Setup();
        Player.SetUp(Obstacles);
        Enemies.ForEach(x => x.SetUp(Player,Camp));
        UnitScript campUnit = Camp.GetComponent<UnitScript>();
        foreach(UnitScript unit in unitScripts)
        {
            if(playerController == null) unit.TryGetComponent(out playerController);
            unit.SetUp();
        }
        unitScripts.ForEach(x => x.SetUp());
        EnemySpawner.SetUp(Player, Units, Camp, this);
        MenuSettings.Setup();
        QuestSystem.Setup();
        ItemSpawner.Setup();

        EventManager.OnBossSlain.AddListener(BossSlain);

        SetQuest("Защитите лагерь!");
        Time.timeScale = 1;
    }

    public void BossSlain()
    {
        SetQuest("Найдите ингридиент!");
    }

    public void SetQuest(string text)
    {
        QuestSystem.SetQuest(text);
    }
}