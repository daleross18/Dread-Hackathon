using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingMonsterManager : MonoBehaviour
{
    private static string CHARGING_MONSTERS_CONTAINER_KEY = "ChargingMonsters";
    private static float MAX_TIME_SINCE_LAST_MONSTER_SPAWN = 10.0f;
    private static float MIN_X = -30f;
    private static float SHORT_SPAWN_Y = 1f;
    private static float MEDIUM_SPAWN_Y = 2.5f;
    private static float TALL_SPAWN_Y = 4f;

    public GameObject chargingMonsterPrefab;

    private GameObject chargingMonsterContainer;
    private GameObject activeChargingMonster;
    private float timeSinceLastMonsterSpawn;

    private void Awake()
    {
        chargingMonsterContainer = GameObject.FindWithTag(CHARGING_MONSTERS_CONTAINER_KEY);
        timeSinceLastMonsterSpawn = MAX_TIME_SINCE_LAST_MONSTER_SPAWN;
    }

    private void Update()
    {
        if (GameStateManager.GetGameState() == GameState.Play)
        {
            DespawnMonsterIfNecessary();
            SpawnMonsterIfNecessary();
        }
    }

    private void DespawnMonsterIfNecessary()
    {
        bool shouldDespawnMonster =
            activeChargingMonster != null && activeChargingMonster.transform.position.x < MIN_X;
        if (shouldDespawnMonster)
        {
            Destroy(activeChargingMonster.gameObject);
            activeChargingMonster = null;
        }
    }

    private void SpawnMonsterIfNecessary()
    {
        if (SanityController.GetSanity() > 75)
        {
            return;
        }
        if (timeSinceLastMonsterSpawn >= MAX_TIME_SINCE_LAST_MONSTER_SPAWN)
        {
            SpawnMonsterAtRandomPosition();
            timeSinceLastMonsterSpawn = 0.0f;
        }
        timeSinceLastMonsterSpawn += Time.deltaTime * PowerupManager.timeMultiplier;
    }

    private void SpawnMonsterAtRandomPosition()
    {
        if (activeChargingMonster != null)
        {
            Destroy(activeChargingMonster.gameObject);
        }
        activeChargingMonster =
            Instantiate(chargingMonsterPrefab as GameObject, chargingMonsterContainer.transform);
        int randomIdx = Random.Range(0, 3);
        if (randomIdx == 0)
        {
            activeChargingMonster.transform.position =
                new Vector2(activeChargingMonster.transform.position.x, SHORT_SPAWN_Y);
        }
        else if (randomIdx == 1)
        {
            activeChargingMonster.transform.position =
                new Vector2(activeChargingMonster.transform.position.x, MEDIUM_SPAWN_Y);
        }
        else
        {
            activeChargingMonster.transform.position =
                new Vector2(activeChargingMonster.transform.position.x, TALL_SPAWN_Y);
        }
    }
}
