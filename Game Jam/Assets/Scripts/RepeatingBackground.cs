using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private float mainMenuScrollMultiplier = .5f;
    [SerializeField] private float endX;
    [SerializeField] private float startX;

    private void Update()
    {
        if (
            GameStateManager.GetGameState() == GameState.FadingIn ||
            GameStateManager.GetGameState() == GameState.MainMenu
        )
            transform.Translate(Vector2.left * scrollSpeed * mainMenuScrollMultiplier * Time.deltaTime * PowerupManager.timeMultiplier);
        else if (GameStateManager.GetGameState() == GameState.Play)
        {
            transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime * PowerupManager.timeMultiplier);
        }

        if (transform.position.x <= endX)
        {
            transform.position = new Vector2(startX, transform.position.y);
        }
    }
}
