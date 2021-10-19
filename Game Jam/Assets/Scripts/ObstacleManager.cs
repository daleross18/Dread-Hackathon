using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private static string OBSTACLES_CONTAINER_KEY = "Obstacles";
    private static float MAX_TIME_SINCE_LAST_PATTERN_SPAWN = 6.0f;

    public GameObject[] obstaclePatternPrefabs;

    private GameObject obstaclesContainer;
    private GameObject activeObstaclePattern;
    private GameObject previousObstaclePattern;
    private float timeSinceLastPatternSpawn;

    private void Awake()
    {
        obstaclesContainer = GameObject.FindWithTag(OBSTACLES_CONTAINER_KEY);
        timeSinceLastPatternSpawn = 0.0f;
    }

    private void Update()
    {
        if (GameStateManager.GetGameState() == GameState.Play)
        {
            if (timeSinceLastPatternSpawn >= MAX_TIME_SINCE_LAST_PATTERN_SPAWN)
            {
                SpawnRandomObstaclePattern();
                timeSinceLastPatternSpawn = 0.0f;
            }
            timeSinceLastPatternSpawn += Time.deltaTime * PowerupManager.timeMultiplier;
        }
    }

    private void SpawnRandomObstaclePattern()
    {
        if (previousObstaclePattern != null)
        {
            Destroy(previousObstaclePattern.gameObject);
        }
        previousObstaclePattern = activeObstaclePattern;
        int randomIdx = Random.Range(0, obstaclePatternPrefabs.Length);
        activeObstaclePattern =
            Instantiate(obstaclePatternPrefabs[randomIdx] as GameObject, obstaclesContainer.transform);
    }
}
