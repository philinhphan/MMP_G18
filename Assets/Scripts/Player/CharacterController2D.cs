using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private bool hasAirControl = true;
    [SerializeField] private Vector3 startingPosition = new Vector3(-16, -7, 0);

    /* VALUES SET IN INSPECTOR */
    [SerializeField] private float normalSpeed;
    [SerializeField] private float flapForce;
    [SerializeField] private float jumpForce;
    /* END */

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;

    private Dissolve dissolve;

    private bool isGrounded;
    private bool hasTriggeredJump = false;
    private bool isFacingRight = true;
    private bool m_IsFlappyBirdMode = false;

    private int collisionCounter = 0;

    private float coyoteTime = 0.1f;
    private float coyoteCounter;
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    private float deathDelay = 1f;

    private Vector3 checkpointPosition;
    private bool isFlappyCheckpoint;

    private bool isDying;
    private bool isWalkSoundPlaying = false;

    private bool isMovementLocked = false;
    private GameObject activeFlapSoundSource;

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dissolve = GetComponent<Dissolve>();

        // Initialize AudioManager reference
        /*audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }*/
    }
    private void Update()
    {
        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
        } else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (hasTriggeredJump)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Freeze();
            if (!isDying)
            {
                isDying = true;
                StartCoroutine(Die());
              
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            collisionCounter += 1;
            if (!isGrounded && collisionCounter >= 1)
            {
                isGrounded = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            collisionCounter -= 1;
            if (isGrounded && collisionCounter == 0)
            {
                isGrounded = false;
            }
        }
    }

    public void Move(float move, bool hasTriggeredJump, bool hasReleasedJump, bool hasTriggeredFlap)
    {   
        // Handle FlappyBirdRespawn with player lock
        Vector2 checkpointPosV2 = checkpointPosition;
        if (isMovementLocked && rb.position.Equals(checkpointPosV2))
        {
            if (move > 0 || move < 0 || hasTriggeredFlap) Unfreeze();
        }

        // All other cases
        if (!isMovementLocked)
        {
            if (checkValidInput(hasTriggeredJump, hasTriggeredFlap))
            {
                this.hasTriggeredJump = hasTriggeredJump;
                MoveHorizontal(move);
                Jump(hasReleasedJump);
                Flap(hasTriggeredFlap);
            }
        }
    }

    private bool checkValidInput(bool hasTriggeredJump, bool hasTriggeredFlap)
    {
        bool isValid = true;

        // Player cannot be jumping and flapping at the same time
        bool jumpFlapViolation = hasTriggeredJump && hasTriggeredFlap;

        if (jumpFlapViolation)
        {
            isValid = false;
        }

        return isValid;
    }

    private void MoveHorizontal(float move)
    {
        if (move != 0 && isGrounded && !isWalkSoundPlaying)
        {   
            isWalkSoundPlaying = true;
            StartCoroutine(PlayWalkSound());
        }

        //Only control the player if grounded is true and/ or airControl is turned on
        if (isGrounded || hasAirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * normalSpeed, rb.velocity.y);
            rb.velocity = targetVelocity;

            if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight)) Flip();
        }
    }

    IEnumerator PlayWalkSound()
    {
        SoundManager.instance.PlayClip(SoundManager.instance.walkSound, .4f);
        yield return new WaitForSeconds(UnityEngine.Random.Range(.7f, 1.5f));
        isWalkSoundPlaying = false;
    }

    private void Jump(bool hasReleasedJump)
    {
        // Is on Ground and starts to jump
        if (coyoteCounter > 0f && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0f;
            //audioManager.PlayJumpSound();
            SoundManager.instance.PlayClip(SoundManager.instance.jumpClip, .3f);
        }

        // Is jumping and released the jump button
        if (hasReleasedJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteCounter = 0f;
        }

        
    }

    private void Flap(bool hasTriggeredFlap)
    {
        if (hasTriggeredFlap)
        {
            //audioManager.PlayFlapSound();
            rb.velocity = new Vector2(rb.velocity.x, flapForce);

            // interrupt currently playing sound before playing and saving the next
            if (activeFlapSoundSource != null)
            {
                Destroy(activeFlapSoundSource);
            }

            activeFlapSoundSource = SoundManager.instance.PlayInterruptableClip(SoundManager.instance.FlapSound, .2f);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void ToggleFlappyBirdMode(bool isFlappyBirdMode)
    {
        m_IsFlappyBirdMode = isFlappyBirdMode;
    }

    public IEnumerator Die()
    {
        SoundManager.instance.PlayClip(SoundManager.instance.DieSound, .3f);
        StartCoroutine(dissolve.Vanish(true));
        yield return new WaitForSeconds(deathDelay);
        ResetPosition();
        StartCoroutine(dissolve.Appear(true));
        isDying = false;

        if (!isFlappyCheckpoint)
        {
            Unfreeze();
        }
    }

    public void ResetPosition()
    {
        GameObject activeCheckpoint = CheckpointSystem.GetActiveCheckpoint();
        
        if (activeCheckpoint != null)
        {
            checkpointPosition = activeCheckpoint.transform.position;
            isFlappyCheckpoint = activeCheckpoint.GetComponent<CheckpointSystem>().isFlappyCheckpoint;

            // check if the checkpoint has an upcoming Flap Circle to reset.
            FlapCircleController flapCircleController = activeCheckpoint.GetComponent<CheckpointSystem>().flapCircleController;
            if (flapCircleController != null)
            {
                flapCircleController.ResetFlapCircle();
            }
        }
        // StartingPosition Case - No Checkpoints collected
        else
        {
            checkpointPosition = startingPosition;
            isFlappyCheckpoint = false;
        }

        rb.transform.position = checkpointPosition;
        ResetPlayerState();

    }

    private void ResetPlayerState()
    {
        PlayerMovement movement = GetComponent<PlayerMovement>();

        // Set PlayerState according to type of checkpoint
        if (isFlappyCheckpoint && movement.currentState is NormalState)
        {
            movement.SwitchState(PlayerState.FlappyBird);
        }
        else if (!isFlappyCheckpoint && movement.currentState is FlappyBirdState)
        {
            movement.SwitchState(PlayerState.Normal);
        }

        if (movement.currentState is FlappyBirdState)
        {
            Freeze();
        }
    }

    private void Freeze()
    {
        isMovementLocked = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.velocity = Vector3.zero;
    }

    private void Unfreeze()
    {
        isMovementLocked = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}