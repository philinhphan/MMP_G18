using System.Collections;
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

    [SerializeField]
    private float flashDuration = 1f;
    [SerializeField]
    private float flashIntensity = 3f;

    private float targetIntensity;
    private float lastIntensity;
    private float interval;
    private float timer;
    private bool flashing = false;

    void Update()
    {
        if (!flashing) 
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

    public void FlashLight()
    {
        flashing = true;
        StartCoroutine(FlashLightRoutine());
    }

    IEnumerator FlashLightRoutine()
    {
        float initialLightIntensity = myLight.intensity;
        float time = 0;

        // interpolate light intensity (up and down)
        while (time < flashDuration) {
            time += Time.deltaTime;
            myLight.intensity = Mathf.Lerp(initialLightIntensity, flashIntensity, time/flashDuration);
            yield return null;
        }

        time = 0;
        while (time < flashDuration) {
            time += Time.deltaTime;
            myLight.intensity = Mathf.Lerp(flashIntensity, 0f, time / flashDuration);
            yield return null;
        }
    }

    public float GetFlashDuration()
    {
        return flashDuration;
    }
}
