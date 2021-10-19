using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private static string MIDDLE_BACKGROUND_DEFAULT_KEY =
        "MiddleBackgroundDefault";
    private static string MIDDLE_BACKGROUND_WINTER_KEY =
        "MiddleBackgroundWinter";
    private static string FAR_BACKGROUND_SUMMER_KEY =
        "FarBackgroundSummer";
    private static string FAR_BACKGROUND_FALL_KEY =
        "FarBackgroundFall";
    private static string FAR_BACKGROUND_WINTER_KEY =
        "FarBackgroundWinter";
    private static string FAR_BACKGROUND_SPRING_KEY =
        "FarBackgroundSpring";

    private SpriteRenderer middleBackgroundDefaultSprite1;
    private SpriteRenderer middleBackgroundDefaultSprite2;

    private SpriteRenderer middleBackgroundWinterSprite1;
    private SpriteRenderer middleBackgroundWinterSprite2;

    private SpriteRenderer farBackgroundSummerSprite1;
    private SpriteRenderer farBackgroundSummerSprite2;

    private SpriteRenderer farBackgroundFallSprite1;
    private SpriteRenderer farBackgroundFallSprite2;

    private SpriteRenderer farBackgroundWinterSprite1;
    private SpriteRenderer farBackgroundWinterSprite2;

    private SpriteRenderer farBackgroundSpringSprite1;
    private SpriteRenderer farBackgroundSpringSprite2;


    private static BackgroundManager instance;
    
    private void Awake()
    {
        instance = this;

        middleBackgroundDefaultSprite1 =
            GameObject.FindWithTag(MIDDLE_BACKGROUND_DEFAULT_KEY + "1").GetComponent<SpriteRenderer>();
        middleBackgroundDefaultSprite2 =
            GameObject.FindWithTag(MIDDLE_BACKGROUND_DEFAULT_KEY + "2").GetComponent<SpriteRenderer>();

        middleBackgroundWinterSprite1 =
            GameObject.FindWithTag(MIDDLE_BACKGROUND_WINTER_KEY + "1").GetComponent<SpriteRenderer>();
        middleBackgroundWinterSprite2 =
            GameObject.FindWithTag(MIDDLE_BACKGROUND_WINTER_KEY + "2").GetComponent<SpriteRenderer>();

        farBackgroundSummerSprite1 =
            GameObject.FindWithTag(FAR_BACKGROUND_SUMMER_KEY + "1").GetComponent<SpriteRenderer>();
        farBackgroundSummerSprite2 =
            GameObject.FindWithTag(FAR_BACKGROUND_SUMMER_KEY + "2").GetComponent<SpriteRenderer>();

        farBackgroundFallSprite1 =
            GameObject.FindWithTag(FAR_BACKGROUND_FALL_KEY + "1").GetComponent<SpriteRenderer>();
        farBackgroundFallSprite2 =
            GameObject.FindWithTag(FAR_BACKGROUND_FALL_KEY + "2").GetComponent<SpriteRenderer>();

        farBackgroundWinterSprite1 =
            GameObject.FindWithTag(FAR_BACKGROUND_WINTER_KEY + "1").GetComponent<SpriteRenderer>();
        farBackgroundWinterSprite2 =
            GameObject.FindWithTag(FAR_BACKGROUND_WINTER_KEY + "2").GetComponent<SpriteRenderer>();

        farBackgroundSpringSprite1 =
            GameObject.FindWithTag(FAR_BACKGROUND_SPRING_KEY + "1").GetComponent<SpriteRenderer>();
        farBackgroundSpringSprite2 =
            GameObject.FindWithTag(FAR_BACKGROUND_SPRING_KEY + "2").GetComponent<SpriteRenderer>();
    }

    public static void TransistionIntoSeason(Season season)
    {
        switch (season)
        {
            case Season.Summer:
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.farBackgroundSpringSprite1));
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.farBackgroundSpringSprite2));

                instance.StartCoroutine(FadeInSpriteRenderer(instance.farBackgroundSummerSprite1));
                instance.StartCoroutine(FadeInSpriteRenderer(instance.farBackgroundSummerSprite2));

                break;
            case Season.Fall:
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.farBackgroundSummerSprite1));
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.farBackgroundSummerSprite2));

                instance.StartCoroutine(FadeOutSpriteRenderer(instance.middleBackgroundDefaultSprite1));
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.middleBackgroundDefaultSprite2));

                instance.StartCoroutine(FadeInSpriteRenderer(instance.farBackgroundFallSprite1));
                instance.StartCoroutine(FadeInSpriteRenderer(instance.farBackgroundFallSprite2));

                instance.StartCoroutine(FadeInSpriteRenderer(instance.middleBackgroundWinterSprite1));
                instance.StartCoroutine(FadeInSpriteRenderer(instance.middleBackgroundWinterSprite2));

                break;
            case Season.Winter:
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.farBackgroundFallSprite1));
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.farBackgroundFallSprite2));

                instance.StartCoroutine(FadeInSpriteRenderer(instance.farBackgroundWinterSprite1));
                instance.StartCoroutine(FadeInSpriteRenderer(instance.farBackgroundWinterSprite2));

                break;
            default:
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.farBackgroundWinterSprite1));
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.farBackgroundWinterSprite2));

                instance.StartCoroutine(FadeOutSpriteRenderer(instance.middleBackgroundWinterSprite1));
                instance.StartCoroutine(FadeOutSpriteRenderer(instance.middleBackgroundWinterSprite2));

                instance.StartCoroutine(FadeInSpriteRenderer(instance.farBackgroundSpringSprite1));
                instance.StartCoroutine(FadeInSpriteRenderer(instance.farBackgroundSpringSprite2));


                instance.StartCoroutine(FadeInSpriteRenderer(instance.middleBackgroundDefaultSprite1));
                instance.StartCoroutine(FadeInSpriteRenderer(instance.middleBackgroundDefaultSprite2));

                break;
        }
    }

    public static IEnumerator FadeOutSpriteRenderer(SpriteRenderer spriteRenderer)
    {
        while (spriteRenderer.color.a > 0.0f)
        {
            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                spriteRenderer.color.a - Time.deltaTime
            );
            yield return new WaitForSeconds(Time.deltaTime * 5);
        }
    }

    public static IEnumerator FadeInSpriteRenderer(SpriteRenderer spriteRenderer)
    {
        while (spriteRenderer.color.a < 1.0f)
        {
            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                spriteRenderer.color.a + Time.deltaTime
            );
            yield return new WaitForSeconds(Time.deltaTime * 5);
        }
    }
}
