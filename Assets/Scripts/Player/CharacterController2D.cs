using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private bool hasAirControl = true;

    /* VALUES SET IN INSPECTOR */
    [SerializeField] private float normalSpeed;
    [SerializeField] private float flapForce;
    [SerializeField] private float jumpForce;
    /* END */

    private Rigidbody2D rb;
    
    private bool isGrounded;
    private bool isFacingRight = true;
    private bool m_IsFlappyBirdMode = false;
    private int collisionCounter = 0;

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            collisionCounter += 1;
            if (!isGrounded && collisionCounter >= 1) isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            collisionCounter -= 1;
            if (isGrounded && collisionCounter == 0) isGrounded = false;
        }
    }

    public void Move(float move, bool isJumping, bool hasReleasedJump, bool isFlapping)
    {

        // Player cannot be jumping and flapping at the same time
        if (isJumping && isFlapping)
        {
            Debug.LogError("jump and flap paramter are not allowed to be true simultanously");
            return;
        }

        //Only control the player if grounded is true or airControl is turned on
        if (isGrounded || hasAirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * normalSpeed, rb.velocity.y);
            rb.velocity = targetVelocity;
            
            if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight)) Flip();
        }


        // Normal Jump on jump input and when grounded
        if (isGrounded && isJumping)
        { 
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.5f);
        }

        // Jump Cut
        if (hasReleasedJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Normal Flap
        if (isFlapping)
        {
            rb.velocity = new Vector2(rb.velocity.x, flapForce);
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
}