using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2D : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    
    private Rigidbody2D rigidBody;
    private Vector3 savedVelocity;
    private bool colliding = false;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        savedVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (!colliding)
        {
            savedVelocity = rigidBody.velocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            colliding = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            colliding = false;
            rigidBody.velocity = savedVelocity;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemyBound"))
        {
           Flip();
           Debug.Log("Bound hit");
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
