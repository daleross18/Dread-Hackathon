using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangmanMovement : MonoBehaviour
{
    private int swingSpeed;
    private bool goingForward;

    private void Awake()
    {
        if (Random.Range(0, 2) == 1)
        {
            // make hangman face opposite direction 50% of the time
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        if (Random.Range(0, 2) == 1)
        {
            // make hangman sway forward first 50% of the time
            goingForward = true;
        }
        swingSpeed = Random.Range(5, 10);
    }

    private void Update()
    {
        if (goingForward)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * swingSpeed);
            if (transform.rotation.z > 0.05f)
            {
                goingForward = false;
            }
        }
        else
        {
            transform.Rotate(Vector3.back * Time.deltaTime * swingSpeed);
            if (transform.rotation.z < -0.05f)
            {
                goingForward = true;
            }
        }
    }
}
