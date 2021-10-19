using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
   AudioSource h_MyAudioSource;

   public AudioClip HitClip;

   public float invincibilityTime = 1f;
   private bool isInvincible = false;

   public float baseVolume = 0.5f;


   private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
      h_MyAudioSource = GetComponent<AudioSource>();

      boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

     void OnTriggerEnter2D(Collider2D other)
     {
         if (OtherColliderShouldDamagePlayer(other) && !isInvincible)
         {
            h_MyAudioSource.volume = baseVolume;
            h_MyAudioSource.clip = HitClip;
            h_MyAudioSource.Play();
            StartCoroutine(MakePlayerInvincible());
         }
     }

      private bool OtherColliderShouldDamagePlayer(Collider2D other)
      {
          return other.gameObject.CompareTag("Obstacle") ||
              other.gameObject.CompareTag("Monster");
      }

       IEnumerator MakePlayerInvincible()
       {
           isInvincible = true;
           yield return new WaitForSeconds(invincibilityTime);
           isInvincible = false;
       }
}
