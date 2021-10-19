using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
   AudioSource m_MyAudioSource;

   public AudioClip MusicClip;

   public float baseVolume = 0.05f;

   private AudioDistortionFilter distortion;
   bool m_ToggleChange;
   bool m_TogglePause;

   [RangeAttribute(0,1)]
   public float distortionLevel = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
      m_MyAudioSource = GetComponent<AudioSource>();
      distortion = gameObject.AddComponent<AudioDistortionFilter>();
      distortion.distortionLevel = distortionLevel;
      m_ToggleChange = true;
      m_TogglePause = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (GameStateManager.GetGameState() == GameState.Play && m_ToggleChange == true)
      {
         m_MyAudioSource.clip = MusicClip;
         m_MyAudioSource.Play();
         m_ToggleChange = false;
      }
      if (GameStateManager.GetGameState() == GameState.PauseMenu && m_TogglePause == false)
      {
         m_MyAudioSource.Pause();
         m_TogglePause = true;
      }

      if (GameStateManager.GetGameState() == GameState.Play && m_TogglePause == true)
      {
         m_MyAudioSource.Play();
         m_TogglePause = false;
      }
      if (GameStateManager.GetGameState() != GameState.Play && m_ToggleChange == false)
      {
         m_MyAudioSource.Stop();
         m_ToggleChange = true;
      }
      distortion.distortionLevel = 1f - SanityController.GetSanity()/100;
      if (SanityController.GetSanity() >= 20) {
         m_MyAudioSource.volume = baseVolume + (0.05f * (SanityController.GetSanity()/100));
      }
      if (SanityController.GetSanity() < 20 && SanityController.GetSanity() > 20) {
         m_MyAudioSource.volume = baseVolume + (0.02f * (SanityController.GetSanity()/100));
      }
      if (SanityController.GetSanity() <= 10) {
         m_MyAudioSource.volume = baseVolume + (0.02f * (SanityController.GetSanity()/100));
      }

    }
}
