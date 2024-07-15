using UnityEngine;

public class FireController : MonoBehaviour
{
    private AudioManager audioManager;
    private bool isPlaying = false;

    void Start()
    {
        audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }
    }

    void Update()
    {
        // This is a placeholder condition. Replace it with actual fire activation logic.
        bool shouldPlayFire = /* Fire activation condition */;

        if (shouldPlayFire && !isPlaying)
        {
            audioManager.PlayFireSound();
            isPlaying = true;
        }
        else if (!shouldPlayFire && isPlaying)
        {
            // audioManager.StopFireSound();
            isPlaying = false;
        }
    }
}