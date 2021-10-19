using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeableScreenManager : MonoBehaviour
{
    private static FadeableScreenManager instance;

    private RawImage fadeableImage;
    private bool doneFadingIn;
    private bool doneFadingOut;

    private void Awake()
    {
        instance = this;
        fadeableImage = GetComponent<RawImage>();
        fadeableImage.color = new Color(
            fadeableImage.color.r,
            fadeableImage.color.g,
            fadeableImage.color.b,
            1.0f
        );
        StartFadeIn();
    }

    private static void StartFadeIn()
    {
        instance.StartCoroutine(instance.FadeIn());
    }

    private IEnumerator FadeIn()
    {
        doneFadingIn = false;
        while (fadeableImage.color.a > 0.0)
        {
            fadeableImage.color = new Color(
                fadeableImage.color.r,
                fadeableImage.color.g,
                fadeableImage.color.b,
                fadeableImage.color.a - Time.deltaTime
            );
            yield return new WaitForSeconds(Time.deltaTime * 5);
        }
        doneFadingIn = true;
    }

    public static void StartFadeOut()
    {
        instance.StartCoroutine(instance.FadeOut());
    }

    private IEnumerator FadeOut()
    {
        doneFadingOut = false;
        while (fadeableImage.color.a < 1.0)
        {
            fadeableImage.color = new Color(
                fadeableImage.color.r,
                fadeableImage.color.g,
                fadeableImage.color.b,
                fadeableImage.color.a + Time.deltaTime
            );
            yield return new WaitForSeconds(Time.deltaTime * 5);
        }
        doneFadingOut = true;
    }

    public static bool IsDoneFadingIn()
    {
        return instance.doneFadingIn;
    }

    public static bool IsDoneFadingOut()
    {
        return instance.doneFadingOut;
    }
}
