using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverTextManager : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = GetFormattedGameOverText();
    }

    private string GetFormattedGameOverText()
    {
        int numCompletedYears = GameStateManager.GetNumCompletedSeasons() / 4;
        int numRemainingCompletedSeasons = GameStateManager.GetNumCompletedSeasons() % 4;
        return string.Format(
            "You Fought For {0} Years, {1} Seasons",
            numCompletedYears.ToString(),
            numRemainingCompletedSeasons.ToString()
        );
    }
}
