using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour { 

    public CharacterController2D characterController;
    public float speed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    public Vector3 startingPosition;

    private CheckpointSystem checkpointSystem;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = new Vector3(-18, -7, 0);
        checkpointSystem = GetComponent<CheckpointSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        characterController.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            resetPosition();
        }
    }

    private void resetPosition()
    {
        // transform.position = startingPosition;
        transform.position = checkpointSystem.getCurrentCheckpoint();
    }
}
