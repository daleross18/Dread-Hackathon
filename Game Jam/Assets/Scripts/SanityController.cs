using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityController : MonoBehaviour
{
    public float maxSanity = 100f;
    private float _currentSanity;
    bool _canRegenSanity = true;

    public float sanityIncrementRate = 5f;
    public float sanityRegenPause = 2f;
    public float invincibilityTime = 1f;
    private float currentRegenPause;
    private float targetSanity;

    private bool isInvincible = false;

    private BoxCollider2D boxCollider;

    private static SanityController instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        _currentSanity = maxSanity;
        targetSanity = _currentSanity;

        currentRegenPause = sanityRegenPause;
    }

    void Update()
    {
        // Debug.Log(_currentSanity);
        if (GameStateManager.GetGameState() == GameState.Play)
        {
            if (currentRegenPause <= 0)
            {
                currentRegenPause = sanityRegenPause;
                _canRegenSanity = true;
            }

            if (_currentSanity < maxSanity && _canRegenSanity)
            {
                _currentSanity += sanityIncrementRate * Time.deltaTime;
                _currentSanity = Mathf.Clamp(_currentSanity, 0f, maxSanity);
            }
            else if (!_canRegenSanity)
            {
                _currentSanity = Mathf.SmoothStep(_currentSanity, targetSanity, 25f * Time.deltaTime);
                currentRegenPause -= Time.deltaTime;
            }
            if (_currentSanity <= 1)
            {
                Die();
            }
        }
    }

    void Die()
    {
        _currentSanity = maxSanity; // to get rid of red screen and distorted music
        GameStateManager.SetGameState(GameState.GameOver);
        GameObject.FindWithTag("Player").SetActive(false);
    }

    void incrementSanity(float amount)
    {
        _currentSanity += amount;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (OtherColliderShouldDamagePlayer(other) && !isInvincible)
        {
            targetSanity = _currentSanity - 25f;
            _canRegenSanity = false;
            StartCoroutine(MakePlayerInvincible());
        }
    }

    private bool OtherColliderShouldDamagePlayer(Collider2D other)
    {
        return other.gameObject.CompareTag("Obstacle") ||
            other.gameObject.CompareTag("Monster");
    }

    public static float GetSanity()
    {
        return instance._currentSanity;
    }

    IEnumerator MakePlayerInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }
}
