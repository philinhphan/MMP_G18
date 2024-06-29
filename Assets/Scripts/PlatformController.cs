using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public Transform startPoint;
    public Transform endPoint;

    private Vector3 startPosition;
    private Vector3 endPosition;
    
    public Vector3 nextPosition;

    public float speed = 2f;

    private float width;
    private float height;


    // Start is called before the first frame update
    void Start() {

        width = transform.localScale.x;
        height = transform.localScale.y;

        startPosition = new Vector3(startPoint.position.x + width/2, startPoint.position.y);
        endPosition = new Vector3(endPoint.position.x - width/2, endPoint.position.y);

        nextPosition = endPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position == endPosition) {
            nextPosition = startPosition;
        } else if (transform.position == startPosition) {
            nextPosition = endPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.parent = null;
        }
    }
}
