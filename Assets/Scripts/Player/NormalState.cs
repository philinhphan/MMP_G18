using UnityEngine;

public class NormalState : PlayerStateBase
{
    

    private bool isJumping = false;
    private bool hasReleasedJump = false;

    public NormalState(PlayerMovement playerMovement, CharacterController2D characterController) 
        : base(playerMovement, characterController) { }


    public override void Update()
    {
        player.animator.SetFloat("speed", Mathf.Abs(player.horizontalMove));


        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            player.animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonUp("Jump"))
        {
            hasReleasedJump = true;
        }
    }

    public override void FixedUpdate()
    {
        controller.Move(player.horizontalMove * Time.fixedDeltaTime, isJumping, hasReleasedJump, false);

        if (controller.GetIsGrounded() && !isJumping && player.animator.GetBool("isJumping"))
        {
            player.animator.SetBool("isJumping", false);
        }

        isJumping = false;
        hasReleasedJump = false;
    }
}