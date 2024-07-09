using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickerBg : MonoBehaviour
{
    [SerializeField]
    private Light2D myLight;

    [SerializeField, Min(0)]
    private float maxInterval = 1f;

    [SerializeField, Min(0)]
    private float minIntensity = 0.5f;

    [SerializeField, Min(0)]
    private float maxIntensity = 1f;
    private float targetIntensity;
    private float lastIntensity;
    private float interval;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            lastIntensity = myLight.intensity;
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            timer = 0;
            interval = Random.Range(0, maxInterval);
        }

        myLight.intensity = Mathf.Lerp(lastIntensity, targetIntensity, timer / interval);
    }
}
