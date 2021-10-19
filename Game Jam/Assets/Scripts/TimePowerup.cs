using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        // spawn particles??

        if (PowerupManager.isTimePowerupActivated == false)
            PowerupManager.timeMultiplier *= PowerupManager.timeDecreaseMultiplier;
        PowerupManager.isTimePowerupActivated = true;
        PowerupManager.SetCurrentTimePowerupLength(PowerupManager.timePowerupLength);
        
        Destroy(gameObject);
    }

    

}
