using UnityEngine;

public class FlappyBirdState : PlayerStateBase
{
    private float verticalVelocity = 0f;
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
        player.animator.SetBool("isFlappyBird", true);
        verticalVelocity = 0f;
        lastFlapTime = -flapCooldown;
    }

    public override void Exit()
    {
        player.animator.SetBool("isFlappyBird", false);
    }

    public override void Update()
    {
        if (player.inputHandler.IsJumpPressed() && CanFlap())
        {
            Flap();
        }

        ApplyGravity();
        UpdateAnimation();
    }

    public override void FixedUpdate()
    {
        MoveVertically();
    }

    private bool CanFlap()
    {
        return Time.time - lastFlapTime >= flapCooldown;
    }

    private void Flap()
    {
        verticalVelocity = Mathf.Min(verticalVelocity + flapForce, maxUpwardVelocity);
        lastFlapTime = Time.time;
        player.animator.SetTrigger("flap");
    }

    private void ApplyGravity()
    {
        verticalVelocity += gravity * Time.deltaTime;
        verticalVelocity = Mathf.Max(verticalVelocity, maxDownwardVelocity);
    }

    private void MoveVertically()
    {
        Vector3 movement = Vector3.up * verticalVelocity * Time.fixedDeltaTime;
        controller.Move(0, false, false);
        player.transform.Translate(movement);
    }

    private void UpdateAnimation()
    {
        player.animator.SetFloat("verticalSpeed", verticalVelocity);
    }
}