using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPlayer : MonoBehaviour
{
   AudioSource f_MyAudioSource;

   public AudioClip StaticClip;
   public AudioClip GameOverClip;
   public AudioClip Footsteps;

   public float baseVolume = 1f;

   bool f_ToggleChange;
   bool f_ToggleLow;
   bool f_ToggleOver;
    // Start is called before the first frame update
    void Start()
    {
      f_MyAudioSource = GetComponent<AudioSource>();
      f_ToggleChange = true;
      f_ToggleOver = false;
      f_ToggleLow = false;
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
      if (GameStateManager.GetGameState() == GameState.FadingIn && f_ToggleChange == true)
      {
          f_MyAudioSource.clip = Footsteps;
          f_MyAudioSource.volume = baseVolume * 0.7f;
          f_MyAudioSource.Play();
          f_ToggleChange = false;
      }
      if (GameStateManager.GetGameState() == GameState.Play && f_ToggleChange == false)
      {
          f_MyAudioSource.Stop();
          f_ToggleChange = false;
      }
      if (GameStateManager.GetGameState() == GameState.Play && SanityController.GetSanity() <= 35  && f_ToggleLow == false)
      {
          f_MyAudioSource.clip = StaticClip;
          f_MyAudioSource.volume = baseVolume * 0.5f;
          f_MyAudioSource.Play();
          f_ToggleLow = true;
          StartCoroutine (FXPlayer.StartFade(f_MyAudioSource, 1.0f, baseVolume));
      }
      if ((GameStateManager.GetGameState() == GameState.Play && SanityController.GetSanity() > 35 && f_ToggleLow == true) || (GameStateManager.GetGameState() == GameState.GameOver && f_ToggleOver == false))
      {
         //StartCoroutine (FXPlayer.StartFade(f_MyAudioSource, 5.0f, 0));
         f_MyAudioSource.Stop();
         f_ToggleLow = false;
      }
      if (GameStateManager.GetGameState() == GameState.GameOver && f_ToggleOver == false)
      {
         f_MyAudioSource.clip = GameOverClip;
         f_MyAudioSource.volume = baseVolume * 0.3f;
         f_MyAudioSource.Play();
         f_ToggleOver = true;
      }
   }
}
