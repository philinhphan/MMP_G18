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

    private void Awake()
    {
        inputHandler = new InputHandler();
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
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public void SwitchState(PlayerState newState)
    {
        if (states.TryGetValue(newState, out PlayerStateBase state))
        {
            currentState.Exit();
            currentState = state;
            currentState.Enter();
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