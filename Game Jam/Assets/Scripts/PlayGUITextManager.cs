using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayGUITextManager : MonoBehaviour
{
    private static PlayGUITextManager instance;

    private TextMeshProUGUI seasonCounterText;

    private void Awake()
    {
        instance = this;
        seasonCounterText = GetComponent<TextMeshProUGUI>();
        FlashYear();
    }

    public static void FlashYear()
    {
        instance.seasonCounterText.text = instance.GetFormattedSeasonCounterText();
        instance.StartCoroutine(instance.FadeInThenFadeOutText());
    }

    private string GetFormattedSeasonCounterText()
    {
        int numCompletedYears = GameStateManager.GetNumCompletedSeasons() / 4;
        return string.Format(
            "Year {0}",
            numCompletedYears.ToString()
        );
    }

    private IEnumerator FadeInThenFadeOutText()
    {
        while (seasonCounterText.color.a < 1.0f)
        {
            seasonCounterText.color = new Color(
                seasonCounterText.color.r,
                seasonCounterText.color.g,
                seasonCounterText.color.b,
                seasonCounterText.color.a + (Time.deltaTime * 10)
            );
            yield return new WaitForSeconds(Time.deltaTime * 30);
        }
        yield return StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        while (seasonCounterText.color.a > 0.0f)
        {
            seasonCounterText.color = new Color(
                seasonCounterText.color.r,
                seasonCounterText.color.g,
                seasonCounterText.color.b,
                seasonCounterText.color.a - (Time.deltaTime * 10)
            );
            yield return new WaitForSeconds(Time.deltaTime * 30);
        }
    }
}
