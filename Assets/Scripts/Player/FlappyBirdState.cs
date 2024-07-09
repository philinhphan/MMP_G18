using UnityEngine;

public class FlappyBirdState : PlayerStateBase
{
    private float verticalVelocity = 0f;

    private float horizontalSpeed = 50f;
    private float gravity = -9.81f;
    private float flapForce = 5f;
    private float maxUpwardVelocity = 10f;
    private float maxDownwardVelocity = -15f;

    private float flapCooldown = 0.2f;
    private float lastFlapTime = -1f;

    public FlappyBirdState(PlayerMovement playerMovement, CharacterController2D characterController) 
        : base(playerMovement, characterController) { }

    public override void Enter()
    {
        verticalVelocity = 0f;
        lastFlapTime = -flapCooldown;

        player.animator.SetBool("isJumping", false);
        player.animator.SetBool("isFlappyBird", true);
    }

    public override void Exit()
    {
        player.animator.SetBool("isFlappyBird", false);
        player.animator.SetBool("isFlapping", false);
    }

    public override void Update()
    {
        bool shouldFlap = player.inputHandler.IsJumpPressed() && CanFlap();
        
        if (shouldFlap)
        {
            Flap();
            
        }

        if (!player.inputHandler.IsJumpPressed() && player.animator.GetBool("isFlapping"))
        {
            player.animator.SetBool("isFlapping", false);
        }

        // LINE COMMENT: Use CharacterController2D's Move method
        controller.Move(0, false, shouldFlap);

        //ApplyGravity();
    }

    public override void FixedUpdate()
    {
        bool shouldFlap = player.inputHandler.IsJumpPressed() && CanFlap();
        
        if (shouldFlap)
        {
            Flap();
            player.animator.SetBool("isFlapping", true);
        }

        // Get horizontal input
        float horizontalMove = player.inputHandler.GetHorizontalMovement();

        // Use CharacterController2D's Move method with horizontal movement
        controller.Move(horizontalMove * horizontalSpeed * Time.fixedDeltaTime, false, shouldFlap);

        //ApplyGravity();
    }

    private bool CanFlap()
    {
        return Time.time - lastFlapTime >= flapCooldown;
    }

    private void Flap()
    {
        verticalVelocity = flapForce;
        lastFlapTime = Time.time;

        //player.animator.SetBool("isFlapping", true);
        
        // Add debug log
        //Debug.Log("Flap triggered in FlappyBirdState");
    }

    private void ApplyGravity()
    {
        verticalVelocity += gravity * Time.fixedDeltaTime;
        verticalVelocity = Mathf.Clamp(verticalVelocity, maxDownwardVelocity, maxUpwardVelocity);
    }


    private void UpdateAnimation()
    {
        //player.animator.SetFloat("verticalSpeed", controller.GetVerticalVelocity());
        // Reset the flap trigger to ensure it can be triggered again
        // player.animator.ResetTrigger("flapTrigger");
    }
}