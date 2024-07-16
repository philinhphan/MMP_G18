using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    
    [SerializeField] private CharacterController2D characterController;

    [SerializeField] public InputHandler inputHandler;

    [HideInInspector] public float horizontalMove;

    [HideInInspector] public Rigidbody2D rb;

    public Dictionary<PlayerState, PlayerStateBase> states;
    public PlayerStateBase currentState;
    private CheckpointSystem checkpointSystem;

    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputHandler = new InputHandler();
        checkpointSystem = GetComponent<CheckpointSystem>();
        InitializeStates();
        rb = GetComponent<Rigidbody2D>();
}

    private void Update()
    {
        horizontalMove = inputHandler.GetHorizontalMovement();
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
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
}