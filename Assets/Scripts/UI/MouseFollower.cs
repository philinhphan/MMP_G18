using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MouseFollower : MonoBehaviour
{

    [SerializeField]
    private float followSpeed = 2f;
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        
        Vector3 newPosition = Vector3.Lerp(transform.position, mousePosition, followSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
}
