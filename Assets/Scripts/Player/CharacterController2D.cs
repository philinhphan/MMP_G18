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
    private bool hasTriggeredJump;
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

    private bool isMovementLocked = false;

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dissolve = GetComponent<Dissolve>();
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
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            isMovementLocked = true;
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

    public void Move(float move, bool hasTriggeredJump, bool hasReleasedJump, bool isFlapping)
    {
        if (!isMovementLocked)
        {
            this.hasTriggeredJump = hasTriggeredJump;

            // Player cannot be jumping and flapping at the same time
            if (hasTriggeredJump && isFlapping)
            {
                Debug.LogError("jump and flap paramter are not allowed to be true simultanously");
                return;
            }

            //Only control the player if grounded is true and/ or airControl is turned on
            if (isGrounded || hasAirControl)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * normalSpeed, rb.velocity.y);
                rb.velocity = targetVelocity;

                if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight)) Flip();
            }

            // Is on Ground and starts to jump
            if (coyoteCounter > 0f && jumpBufferCounter > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpBufferCounter = 0f;
            }

            // Is jumping and released the jump button
            if (hasReleasedJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                coyoteCounter = 0f;
            }

            // Is flapping
            if (isFlapping)
            {
                rb.velocity = new Vector2(rb.velocity.x, flapForce);
            }
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
        StartCoroutine(dissolve.Vanish(true));
        yield return new WaitForSeconds(deathDelay);
        ResetPosition();
        StartCoroutine(dissolve.Appear(true));
        isMovementLocked = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isDying = false;
    }

    public void ResetPosition()
    {
        GameObject activeCheckpoint = CheckpointSystem.GetActiveCheckpoint();
        
        bool isFlappyCheckpoint;

        // StartingPosition Case - No Checkpoints collected
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
    }

    
}