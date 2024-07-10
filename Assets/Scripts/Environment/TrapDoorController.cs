using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapDoorController : MonoBehaviour
{
    private HingeJoint2D hinge;
    private JointMotor2D motor;

    public GameObject trapdoor;

    // Start is called before the first frame update
    void Start()
    {
        hinge = trapdoor.GetComponent<HingeJoint2D>();
        motor = hinge.motor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            motor.motorSpeed *= -1;
            hinge.motor = motor;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            motor.motorSpeed *= -1;
            hinge.motor = motor;
        }
    }



}
