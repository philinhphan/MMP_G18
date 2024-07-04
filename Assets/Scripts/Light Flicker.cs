using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
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

    [SerializeField]
    private float maxDisplacement = 0.02f;
    private Vector3 targetPosition;
    private Vector3 lastPosition;
    private Vector3 origin;

    private void Start()
    {
        origin = transform.position;
        lastPosition = origin;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            lastIntensity = myLight.intensity;
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            timer = 0;
            interval = Random.Range(0, maxInterval);

            targetPosition = origin + Random.insideUnitSphere * maxDisplacement;
            lastPosition = myLight.transform.position;
        }

        myLight.intensity = Mathf.Lerp(lastIntensity, targetIntensity, timer / interval);
        myLight.transform.position = Vector3.Lerp(lastPosition, targetPosition, timer / interval);
    }
}
