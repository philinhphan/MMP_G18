using UnityEngine;

public class NormalState : PlayerStateBase
{
    private bool hasReleasedJump = false;
    private bool hasTriggeredJump = false;

    public NormalState(PlayerMovement playerMovement, CharacterController2D characterController) 
        : base(playerMovement, characterController) { }


    public override void Update()
    {
        controller.animator.SetFloat("speed", Mathf.Abs(player.horizontalMove));
        controller.animator.SetFloat("yVelocity", player.rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            hasTriggeredJump = true;
        } else if (Input.GetButtonUp("Jump"))
        {
            hasReleasedJump = true; 
        }

        if (controller.GetIsGrounded() && controller.animator.GetBool("isJumping"))
        {
            controller.animator.SetBool("isJumping", false);
        } else if (!controller.GetIsGrounded() && !controller.animator.GetBool("isJumping"))
        {
            controller.animator.SetBool("isJumping", true);
        }
    }

    public override void FixedUpdate()
    {
        controller.Move(player.horizontalMove * Time.fixedDeltaTime, hasTriggeredJump, hasReleasedJump,  false);

        hasTriggeredJump = false;
        hasReleasedJump = false;
    }
}