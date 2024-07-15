using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private AudioClip backgroundMusic;

    void Start()
    {
        audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }
        else
        {
            audioManager.PlayBackgroundMusic(backgroundMusic);
        }
    }
}