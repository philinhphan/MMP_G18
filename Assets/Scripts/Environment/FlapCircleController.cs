using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapCircleController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 20;

    //private AudioManager audioManager;

    void Start()
    {
        // Initialize AudioManager reference
        /*audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.back);
    }

    public void ResetFlapCircle()
    {
        transform.rotation = new Quaternion(0,0,-0.356330365f,0.934360087f);
        //audioManager.PlayFlapCircleSound();
    }

}
