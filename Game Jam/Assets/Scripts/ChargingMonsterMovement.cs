using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingMonsterMovement : MonoBehaviour
{
    public float chargingMonsterMovementMultiplier = 0.4f;
    private float speed = ObjectMovement.GetSpeed();

    void Update()
    {
        if (
            GameStateManager.GetGameState() != GameState.GameOver &&
            GameStateManager.GetGameState() != GameState.FadingOut
        )
        {
            transform.Translate(Vector3.left * speed * chargingMonsterMovementMultiplier * Time.deltaTime);
        }
    }
}
