using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private float m_FlapVelocity = 20;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilingCheck;

    const float k_GroundedRadius = .2f;
    const float k_CeilingRadius = .2f;
    private bool m_Grounded;
    
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    
    private bool m_FacingRight = true;

    private const float defaultGravityScale = 3f;
    private const float flapGravityScale = 3f;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    // New variables for FlappyBird mode
    private bool m_IsFlappyBirdMode = false; // Added to track the current mode
    [SerializeField] private float m_FlappyBirdGravity = -9.81f; // Gravity for FlappyBird mode
    [SerializeField] private float m_FlappyBirdJumpForce = 5f; // Jump force for FlappyBird mode

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool jump, bool flap)
    {

        if (jump && flap)
        {
            Debug.LogError("jump and flap paramter are not allowed to be true simultanously");
            return;
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }

        if (flap)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_FlapVelocity);
        }

    }

    
    public void MoveFlappyBird(float move, bool flap)
    {
        // Apply gravity
        //m_Rigidbody2D.velocity += Vector2.up * m_FlappyBirdGravity * Time.fixedDeltaTime;


        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // Flip the character if needed
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }

        // Apply jump force if jump button is pressed
       
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    
     public void ToggleFlappyBirdMode(bool isFlappyBirdMode)
     {
        m_IsFlappyBirdMode = isFlappyBirdMode;
        //m_Rigidbody2D.gravityScale = isFlappyBirdMode ? flapGravityScale : defaultGravityScale; // Disable Unity's gravity in FlappyBird mode

        //Debug.Log($"FlappyBird mode toggled: {isFlappyBirdMode}");
    }

    public float GetVerticalVelocity()
    {
        return m_Rigidbody2D.velocity.y;
    }
}