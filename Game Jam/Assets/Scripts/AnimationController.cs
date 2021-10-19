using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    public float smoothBlend = 0.1f;
    private float runSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("runSpeed", runSpeed * PowerupManager.timeMultiplier, smoothBlend, Time.deltaTime);

        if (GameStateManager.GetGameState() == GameState.Play)
            animator.SetBool("GameStart", true);

        if (Input.GetKeyDown(KeyCode.Space) && PlayerController.GetExtraJumps() > 0 
            && GameStateManager.GetGameState() == GameState.Play)
            animator.SetTrigger("jumpPressed");

        /*if (Input.GetKeyDown(KeyCode.S) && PlayerController.isRolling)
        {
            animator.SetTrigger("rollPressed");
        }*/

        if (PowerupManager.isTimePowerupActivated)
            animator.speed = .5f;
        else
            animator.speed = 1f;
    }
}
