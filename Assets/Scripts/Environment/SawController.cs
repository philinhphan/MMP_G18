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

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (moveVertical)
        {
            float playerY = player.position.y;
            float clampedY = Mathf.Clamp(playerY, minPoint.position.y, maxPoint.position.y);
            float newY = Mathf.Lerp(currentPosition.y, clampedY, movementSpeed * Time.deltaTime);

            transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);
        } else
        {
            float playerX = player.position.x;
            float clampedX = Mathf.Clamp(playerX, minPoint.position.x, maxPoint.position.x);
            float newX = Mathf.Lerp(currentPosition.x, clampedX, movementSpeed * Time.deltaTime);

            transform.position = new Vector3(newX, currentPosition.y, currentPosition.z);
        }
        
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.back);
    }
}
