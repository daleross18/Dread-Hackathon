using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<GameObject> powerups = new List<GameObject>();

    public GameObject spawnPoint;

    private static PowerupManager instance;

    private static float MAX_TIME_SINCE_LAST_POWERUP_SPAWN = 5.0f;
    private float timeSinceLastPowerupSpawn;

    public static float timePowerupLength = 6f;
    private float currentTimePowerupLength;
    public static float timeDecreaseMultiplier = .6f;

    public static float slideTimeMultiplier = 2f;
    private static float currentSlideTimeMultiplier;

    public static float timeMultiplier = 1f;

    public static bool isTimePowerupActivated = false;

    void Awake()
    {
        timeSinceLastPowerupSpawn = MAX_TIME_SINCE_LAST_POWERUP_SPAWN;
        currentTimePowerupLength = timePowerupLength;
        currentSlideTimeMultiplier = slideTimeMultiplier / 2f;
        instance = this;
    }

    void Update()
    {
        // Debug.Log(timeMultiplier);
        /*if (timeSinceLastPowerupSpawn >= MAX_TIME_SINCE_LAST_POWERUP_SPAWN)
        {
            SpawnPowerupByChance();
            timeSinceLastPowerupSpawn = 0f;
        }
        timeSinceLastPowerupSpawn += Time.deltaTime * timeMultiplier;*/

        // Debug.Log(timeMultiplier);

        if (isTimePowerupActivated)
        {
            ObjectMovement.SetSpeedMultiplier(timeMultiplier);
            currentTimePowerupLength -= Time.deltaTime;
            currentSlideTimeMultiplier = slideTimeMultiplier;
        }
        
        if (currentTimePowerupLength < 0)
        {
            IncreaseObjectSpeed();
            currentTimePowerupLength = timePowerupLength;
            isTimePowerupActivated = false;
            currentSlideTimeMultiplier = slideTimeMultiplier / 2f;
        }
    }

    void SpawnPowerupByChance()
    {
        if (Random.value < .25)
        {
            int randomIdx = Random.Range(0, powerups.Count);
            GameObject spawnedPowerup = 
                Instantiate<GameObject>(powerups[randomIdx], spawnPoint.transform.position, Quaternion.identity);
            Destroy(spawnedPowerup, 5f);
        }
    }

    void IncreaseObjectSpeed()
    {
        timeMultiplier *= 1 / timeDecreaseMultiplier;
        ObjectMovement.SetSpeedMultiplier(timeMultiplier);
    }

    public static void SetCurrentTimePowerupLength(float value)
    {
        instance.currentTimePowerupLength = value;
    }

    public static float getSlideTimeMultiplier()
    {
        return currentSlideTimeMultiplier;
    }

    public static void ResetInstance()
    {
        currentSlideTimeMultiplier = slideTimeMultiplier / 2f;
        timeMultiplier = 1.0f;
        instance = null;
    }
}
