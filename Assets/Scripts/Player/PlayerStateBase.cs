using UnityEngine;

public abstract class PlayerStateBase
{
    protected PlayerMovement player;
    protected CharacterController2D controller;

    public PlayerStateBase(PlayerMovement playerMovement, CharacterController2D characterController)
    {
        player = playerMovement;
        controller = characterController;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public abstract void Update();
    public abstract void FixedUpdate();
    public virtual void OnLanding() { }
}