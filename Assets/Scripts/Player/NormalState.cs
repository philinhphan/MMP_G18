using UnityEngine;

public class NormalState : PlayerStateBase
{
    private float horizontalMove = 0f;
    private bool jump = false;

    public NormalState(PlayerMovement playerMovement, CharacterController2D characterController) 
        : base(playerMovement, characterController) { }

    public override void Enter()
    {
        
        
    }

    public override void Update()
    {
        horizontalMove = player.inputHandler.GetHorizontalMovement() * player.speed;
        player.animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (player.inputHandler.IsJumpPressed())
        {
            jump = true;
            player.animator.SetBool("isJumping", true);
        }
    }

    public override void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public override void OnLanding()
    {
        player.animator.SetBool("isJumping", false);
    }
}