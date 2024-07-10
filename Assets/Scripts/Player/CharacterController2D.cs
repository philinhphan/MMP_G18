using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private bool hasAirControl = true;

    private Rigidbody2D m_Rigidbody2D;
    
    private float normalSpeed = 400f;
    private float flapForce = 20f;
    private float jumpForce = 800f;
    private bool isGrounded;
    
    private bool isFacingRight = true;
    private bool m_IsFlappyBirdMode = false;

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Move(float move, bool jump, bool flap)
    {

        if (jump && flap)
        {
            Debug.LogError("jump and flap paramter are not allowed to be true simultanously");
            return;
        }

        //Only control the player if grounded is true or airControl is turned on
        if (isGrounded || hasAirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * normalSpeed, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = targetVelocity;
            
            if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight)) Flip();
        }
       
        if (isGrounded && jump)
        {
            // Add a vertical force to the player.
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            isGrounded = false;
        }

        if (flap)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, flapForce);
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