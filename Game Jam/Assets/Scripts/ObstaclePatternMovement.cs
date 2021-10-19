using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePatternMovement : MonoBehaviour
{
    private static float SPEED = 10.0f;

    void Update()
    {
        transform.Translate(Vector3.left * SPEED * Time.deltaTime);
    }
}
