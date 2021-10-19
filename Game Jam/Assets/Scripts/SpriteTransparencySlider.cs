using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTransparencySlider : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            1 - (1 * (SanityController.GetSanity() / 100))
        );
    }
}
