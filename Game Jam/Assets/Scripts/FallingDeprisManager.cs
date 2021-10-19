using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDeprisManager : MonoBehaviour
{
    private static string LEAVES_KEY = "Leaves";
    private static string SNOWFLAKES_KEY = "Snowflakes";
    private static string RAINDROPS_KEY = "Raindrops";

    private ParticleSystem leavesParticleSystem;
    private ParticleSystem snowflakesParticleSystem;
    private ParticleSystem raindropsParticleSystem;

    private static FallingDeprisManager instance;

    private void Awake()
    {
        instance = this;

        leavesParticleSystem = GameObject.FindWithTag(LEAVES_KEY).GetComponent<ParticleSystem>();
        snowflakesParticleSystem = GameObject.FindWithTag(SNOWFLAKES_KEY).GetComponent<ParticleSystem>();
        raindropsParticleSystem = GameObject.FindWithTag(RAINDROPS_KEY).GetComponent<ParticleSystem>();
    }

    public static void TransistionIntoSeason(Season season)
    {
        switch (season)
        {
            case Season.Summer:
                instance.raindropsParticleSystem.Stop();
                //FadeOutParticleSystem(instance.raindropsParticleSystem);
                break;
            case Season.Fall:
                instance.leavesParticleSystem.Play();
                //FadeInParticleSystem(instance.leavesParticleSystem);
                break;
            case Season.Winter:
                instance.leavesParticleSystem.Stop();
                instance.snowflakesParticleSystem.Play();
                //FadeOutParticleSystem(instance.leavesParticleSystem);
                //FadeInParticleSystem(instance.snowflakesParticleSystem);
                break;
            default:
                instance.snowflakesParticleSystem.Stop();
                instance.raindropsParticleSystem.Play();
                //FadeOutParticleSystem(instance.snowflakesParticleSystem);
                //FadeInParticleSystem(instance.raindropsParticleSystem);
                break;
        }
    }

    /**
    public static IEnumerator FadeOutParticleSystem(ParticleSystem particleSystem)
    {
        while (particleSystem.colorOverLifetime.color.color.a > 0.0f)
        {
            particleSystem.colorOverLifetime.color.color = new Color(
                particleSystem.colorOverLifetime.color.color.r,
                particleSystem.colorOverLifetime.color.color.g,
                particleSystem.colorOverLifetime.color.color.b,
                particleSystem.colorOverLifetime.color.color.a - Time.deltaTime
            );
            yield return new WaitForSeconds(Time.deltaTime * 5);
        }
    }

    public static IEnumerator FadeInParticleSystem(ParticleSystem particleSystem)
    {
        while (particleSystem.colorOverLifetime.color.color.a < 1.0f)
        {
            particleSystem.colorOverLifetime.color.color = new Color(
                particleSystem.colorOverLifetime.color.color.r,
                particleSystem.colorOverLifetime.color.color.g,
                particleSystem.colorOverLifetime.color.color.b,
                particleSystem.colorOverLifetime.color.color.a + Time.deltaTime
            );
            yield return new WaitForSeconds(Time.deltaTime * 5);
        }
    }
    */
}
