using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    public Transform player;
    public Transform minPoint;
    public Transform maxPoint;

    public float movementSpeed = 2f;
    public float rotationSpeed = 300f;

    public bool moveVertical = true;

    private AudioManager audioManager;

    void Start()
    {
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
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition;

        if (moveVertical)
        {
            float playerY = player.position.y;
            float clampedY = Mathf.Clamp(playerY, minPoint.position.y, maxPoint.position.y);
            newPosition.y = Mathf.Lerp(currentPosition.y, clampedY, movementSpeed * Time.deltaTime);
        }
        else
        {
            float playerX = player.position.x;
            float clampedX = Mathf.Clamp(playerX, minPoint.position.x, maxPoint.position.x);
            newPosition.x = Mathf.Lerp(currentPosition.x, clampedX, movementSpeed * Time.deltaTime);
        }

        if (newPosition != currentPosition)
        {
            if (audioManager != null)
            {
                audioManager.PlaySawSound(transform.position, player);
            }
        }

        transform.position = newPosition;
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.back);
    }
}
