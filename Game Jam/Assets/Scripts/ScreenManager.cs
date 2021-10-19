using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    Light lt;
    Color color0 = Color.white;
    Color color1 = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lt.color = Color.Lerp(color1, color0, SanityController.GetSanity() / 100);
    }
}
