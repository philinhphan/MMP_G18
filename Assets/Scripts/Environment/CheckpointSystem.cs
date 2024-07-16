using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public static List<GameObject> checkpointList;

    public bool isFlappyCheckpoint;
    public bool isActive = false;
    public FlapCircleController flapCircleController;

    void Start()
    {
        checkpointList = GameObject.FindGameObjectsWithTag("Checkpoint").ToList();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateCheckpoint();
        }
    }

    private void ActivateCheckpoint()
    {
        DeactivateAllCheckpoints();
        isActive = true;
    }

    private void DeactivateAllCheckpoints()
    {
        foreach (GameObject checkpoint in checkpointList)
        {
            checkpoint.GetComponent<CheckpointSystem>().isActive = false;
        }
    }

    public static GameObject GetActiveCheckpoint()
    {
        GameObject activeCheckpoint = null;

        foreach (GameObject checkpoint in checkpointList)
        {
            if (checkpoint.GetComponent<CheckpointSystem>().isActive == true)
            {
                activeCheckpoint = checkpoint;
            }
        }
        return activeCheckpoint;
    }
}
