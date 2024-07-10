using UnityEngine;

public class FlappyBirdState : PlayerStateBase
{
    private float horizontalMove;
    private float flapCooldown = .2f;
    private float lastFlapTime = -1f;
    private bool flap = false;

    public FlappyBirdState(PlayerMovement playerMovement, CharacterController2D characterController) 
        : base(playerMovement, characterController) { }

    public override void Enter()
    {
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
        horizontalMove = player.inputHandler.GetHorizontalMovement();

        if (player.inputHandler.IsJumpPressed() && CanFlap())
        {
            flap = true;
            lastFlapTime = Time.time;
            player.animator.SetBool("isFlapping", true);
        }
    }

    public override void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false ,flap);
        flap = false;
        player.animator.SetBool("isFlapping", false);
    }

    private bool CanFlap()
    {
        return Time.time - lastFlapTime >= flapCooldown;
    }
}