using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private static float speed;

    private static float baseSpeed = 7.0f;

    private int prevSeasonsCompleted;
    private int currSeasonsCompleted;

    public static float speedIncreasePerSeason = .08f;

    private static ObjectMovement instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currSeasonsCompleted = GameStateManager.GetNumCompletedSeasons();
        prevSeasonsCompleted = currSeasonsCompleted;
        SetSpeedMultiplier(PowerupManager.timeMultiplier);
    }

    void Update()
    {
        currSeasonsCompleted = GameStateManager.GetNumCompletedSeasons();
        if (currSeasonsCompleted > prevSeasonsCompleted)
        {
            PowerupManager.timeMultiplier += speedIncreasePerSeason;
            PowerupManager.timeMultiplier = Mathf.Clamp(PowerupManager.timeMultiplier, .8f, 2.5f);
            SetSpeedMultiplier(PowerupManager.timeMultiplier);
            prevSeasonsCompleted = currSeasonsCompleted;
        }
        if (
            GameStateManager.GetGameState() != GameState.GameOver &&
            GameStateManager.GetGameState() != GameState.FadingOut
        )
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    public static void SetSpeedMultiplier(float value)
    {
        // Debug.Log("test");
        speed = baseSpeed * value;
    }

    public static float GetSpeed()
    {
        return speed;
    }
}
