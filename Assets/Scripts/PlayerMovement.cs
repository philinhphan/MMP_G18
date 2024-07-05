using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour { 

    public Animator animator;

    public CharacterController2D characterController;
    public float speed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    public Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = new Vector3(-18, -7, 0);
    }

    // Update is called once per frame
    void Update()
    {

        

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }
    }

    public void onLanding() {
        animator.SetBool("isJumping", false);
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
        transform.position = startingPosition;
    }
}
