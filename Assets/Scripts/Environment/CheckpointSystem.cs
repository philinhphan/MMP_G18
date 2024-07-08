using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{

    [SerializeField]
    Vector3 startingPosition = Vector3.zero;
    Vector3 currentCheckpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = startingPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Checkpoint"))
        {
           currentCheckpoint = other.transform.position;
           Debug.Log("Checkpoint hit, new position: " + currentCheckpoint);
        }
    }

    public Vector3 getCurrentCheckpoint()
    {
        return currentCheckpoint;
    }
}
