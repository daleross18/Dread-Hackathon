using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientPlayer : MonoBehaviour
{
   AudioSource a_MyAudioSource;

   public AudioClip LongTreesClip;
   public AudioClip ShortTreesClip;

   public float baseVolume = 0.7f;

   bool a_ToggleChange;
    // Start is called before the first frame update
    void Start()
    {
      a_MyAudioSource = GetComponent<AudioSource>();
      a_ToggleChange = true;
    }

    // Update is called once per frame
    void Update()
    {
      if (GameStateManager.GetGameState() == GameState.FadingIn && a_ToggleChange == true)
      {
         a_MyAudioSource.Stop();
         a_MyAudioSource.clip = ShortTreesClip;
         a_MyAudioSource.volume = baseVolume;
         a_MyAudioSource.Play();
         a_ToggleChange = false;
      }
      if (GameStateManager.GetGameState() == GameState.GameOver && a_ToggleChange == false)
      {
          a_MyAudioSource.Stop();
          a_MyAudioSource.clip = LongTreesClip;
          a_MyAudioSource.volume = baseVolume * 3.0f;
          a_MyAudioSource.Play();
          a_ToggleChange = true;
      }
    }
}
