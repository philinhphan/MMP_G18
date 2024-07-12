using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserController : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float interval = 10f;

    [SerializeField]
    private bool startActive = true;
    private bool isActive;
    private float timer;
    private Light2D l2d;
    private BoxCollider2D c2d;

    void Start()
    {
        isActive = startActive;
        l2d = GetComponent<Light2D>();
        c2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            c2d.enabled = isActive;
            l2d.enabled = isActive;
            isActive = !isActive;
            
            timer = 0;
        }
    }
}
