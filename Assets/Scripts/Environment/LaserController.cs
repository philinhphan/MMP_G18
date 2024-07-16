using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LaserController : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float interval = 10f;

    [SerializeField]
    private bool startActive = true;

    private float minSoundDistance = .1f;
    private float maxSoundDistance = 30f;
    private float soundVolume = .3f;
    
    private bool isActive;
    private float timer;
    private Light2D l2d;
    private BoxCollider2D c2d;
    private GameObject activeLaserSoundClip;

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

            //play sound (at laser position) or destroy it, depending on laser state
            if (isActive)
            {
               activeLaserSoundClip = SoundManager.instance.PlayLocalClip(SoundManager.instance.LaserSound, transform.position, minSoundDistance, maxSoundDistance, soundVolume);
            }
            else
            {
                if (activeLaserSoundClip != null)
                {
                    Destroy(activeLaserSoundClip);
                }
            }


            isActive = !isActive;
            timer = 0;
        }
    }
}
