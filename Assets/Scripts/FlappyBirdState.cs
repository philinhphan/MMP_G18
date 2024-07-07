using UnityEngine;

public class FlappyBirdState : PlayerStateBase
{
    private float verticalVelocity = 0f;
    private float gravity = -9.81f;
    private float flapForce = 5f;

    public FlappyBirdState(PlayerMovement playerMovement, CharacterController2D characterController) 
        : base(playerMovement, characterController) { }

    public override void Enter()
    {
        player.animator.SetBool("isFlappyBird", true);
        verticalVelocity = 0f;
    }

    public override void Update()
    {
        if (player.inputHandler.IsJumpPressed())
        {
            Flap();
        }

        verticalVelocity += gravity * Time.deltaTime;
        player.animator.SetFloat("verticalSpeed", verticalVelocity);
    }

    public override void FixedUpdate()
    {
        controller.Move(0, false, false);
        player.transform.Translate(Vector3.up * verticalVelocity * Time.fixedDeltaTime);
    }

    private void Flap()
    {
        verticalVelocity = flapForce;
        player.animator.SetTrigger("flap");
    }
}