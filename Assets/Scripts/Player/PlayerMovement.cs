using UnityEngine;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;

public class PlayerMovement : MonoBehaviour
{ 
    
    [SerializeField] private CharacterController2D characterController;

    [SerializeField] private Vector3 startingPosition = new Vector3(-16, -7, 0);

    [SerializeField] public InputHandler inputHandler;

    [HideInInspector] public float horizontalMove;

    [HideInInspector] public Rigidbody2D rb;

    private Dictionary<PlayerState, PlayerStateBase> states;
    private PlayerStateBase currentState;
    private CheckpointSystem checkpointSystem;

    private void Start()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) ResetPosition();
    }

    private void ResetPosition()
    {
        GameObject activeCheckpoint = CheckpointSystem.GetActiveCheckpoint();
        Vector3 checkpointPosition;
        bool isFlappyCheckpoint;

        // StartingPosition Case - No Checkpoints collected
        if (activeCheckpoint != null)
        {
            checkpointPosition = activeCheckpoint.transform.position;
            isFlappyCheckpoint = activeCheckpoint.GetComponent<CheckpointSystem>().isFlappyCheckpoint;
        }
        else
        {
            checkpointPosition = startingPosition;
            isFlappyCheckpoint = false;
        }

        // Reset position to active checkpoint
        transform.position = checkpointPosition;

        // Set PlayerState according to type of checkpoint
        if (isFlappyCheckpoint && currentState is NormalState)
        {
            SwitchState(PlayerState.FlappyBird);
        }
        else if (!isFlappyCheckpoint && currentState is FlappyBirdState)
        {
            SwitchState(PlayerState.Normal);
        }
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