using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public CharacterController2D characterController;
    public float speed = 40f;

    [HideInInspector]
    public InputHandler inputHandler;

    private Dictionary<PlayerState, PlayerStateBase> states;
    private PlayerStateBase currentState;
    private CheckpointSystem checkpointSystem;

    private void Awake()
    {
        inputHandler = new InputHandler();
        checkpointSystem = GetComponent<CheckpointSystem>();
        InitializeStates();
    }

    private void InitializeStates()
    {
        states = new Dictionary<PlayerState, PlayerStateBase>
        {
            { PlayerState.Normal, new NormalState(this, characterController) },
            { PlayerState.FlappyBird, new FlappyBirdState(this, characterController) }
        };

        currentState = states[PlayerState.Normal];
        currentState.Enter();
    }

    private void Update()
    {
        currentState.Update();
        // Update animator parameters
        animator.SetBool("isFlappyBird", currentState is FlappyBirdState);
        
        // Use the new method from CharacterController2D
        animator.SetFloat("verticalSpeed", characterController.GetVerticalVelocity());

        // Trigger flap animation in FlappyBird mode
        if (currentState is FlappyBirdState && inputHandler.IsJumpPressed())
        {
            animator.SetTrigger("flap");
        }

    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = checkpointSystem.getCurrentCheckpoint();
        // Ensure we're in Normal state when resetting position TODO
        SwitchState(PlayerState.Normal);
    }

    public void SwitchState(PlayerState newState)
    {
        if (states.TryGetValue(newState, out PlayerStateBase state))
        {
            currentState.Exit();
            currentState = state;
            currentState.Enter();

            // Toggle FlappyBird mode in CharacterController2D
            characterController.ToggleFlappyBirdMode(newState == PlayerState.FlappyBird);
        }
        else
        {
            Debug.LogError($"State {newState} not found!");
        }
    }

    public void OnLanding()
    {
        currentState.OnLanding();
    }
}