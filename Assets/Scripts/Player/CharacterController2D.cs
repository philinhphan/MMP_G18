using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private bool hasAirControl = true;

    private Rigidbody2D rb;
    
    private float normalSpeed = 400f;
    private float flapForce = 15f;
    private float jumpForce = 12f;
    private bool isGrounded;
    
    private bool isFacingRight = true;
    private bool m_IsFlappyBirdMode = false;

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
            isGrounded = true;
            Debug.Log("Trigger enter, collision with: " + collision.name);
            Debug.Log("isGrounded =" + isGrounded);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("Trigger exit, collision with: " + collision.name);
            isGrounded = false;
            Debug.Log("isGrounded =" + isGrounded);
        }
    }

    public void Move(float move, bool isJumping, bool hasReleasedJump, bool isFlapping)
    {

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

        if (isGrounded && isJumping)
        { 
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.5f);
        }

        if (hasReleasedJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

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