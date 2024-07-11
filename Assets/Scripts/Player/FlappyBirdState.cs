using UnityEngine;

public class FlappyBirdState : PlayerStateBase
{
    private float flapCooldown = .1f;
    private float lastFlapTime = -1f;
    private bool isFlapping = false;

    public FlappyBirdState(PlayerMovement playerMovement, CharacterController2D characterController) 
        : base(playerMovement, characterController) { }

    public override void Enter()
    {
        lastFlapTime = -flapCooldown;

        controller.animator.SetBool("isJumping", false);
        controller.animator.SetBool("isFlappyBird", true);
    }

    public override void Exit()
    {
        controller.animator.SetBool("isFlappyBird", false);
        controller.animator.SetBool("isFlapping", false);
    }

    public override void Update()
    {
        if (Input.GetButtonDown("Jump") && CanFlap())
        {
            isFlapping = true;
            lastFlapTime = Time.time;
            controller.animator.SetBool("isFlapping", true);
        }
    }

    public override void FixedUpdate()
    {
        controller.Move(player.horizontalMove * Time.fixedDeltaTime, false, false, isFlapping);
        
        isFlapping = false;
        controller.animator.SetBool("isFlapping", false);
    }

    private bool CanFlap()
    {
        return Time.time - lastFlapTime >= flapCooldown;
    }
}