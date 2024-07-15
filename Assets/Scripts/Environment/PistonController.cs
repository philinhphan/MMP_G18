using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PistonController : MonoBehaviour {

    public Transform startPoint;
    public Transform endPoint;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 nextPosition;

    public float speed = 2f;

    private float width;
    private float height;

    public bool isReversed = false;

    private AudioManager audioManager;


    // Start is called before the first frame update
    void Start() {

        width = transform.localScale.x;
        height = transform.localScale.y;
        
    	
        if (endPoint.position.x - startPoint.position.x < width) {
            // Makes vertical platforms connect with center to limitPoints
            startPosition = new Vector3(startPoint.position.x, startPoint.position.y);
            endPosition = new Vector3(endPoint.position.x, endPoint.position.y);
        } 
        else {
            // Makes horizontal or diagonal platforms connect with edge to limitPoints
            startPosition = new Vector3(startPoint.position.x + width/2, startPoint.position.y);
            endPosition = new Vector3(endPoint.position.x - width/2, endPoint.position.y);
        }
        
        if (!isReversed) {
            nextPosition = endPosition;
        } else {
            nextPosition = startPosition;
        }

        // Initialize AudioManager reference
        audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 previousPosition = transform.position;

        if (transform.position == endPosition)
        {
            nextPosition = startPosition;
            audioManager.PlayPistonSound();
        }
        else if (transform.position == startPosition)
        {
            nextPosition = endPosition;
            audioManager.PlayPistonSound();
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        if (transform.position != previousPosition)
        {
            audioManager.PlayPistonSound();
        }
    }
}
