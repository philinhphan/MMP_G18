using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip flapSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip trapdoorSound;
    [SerializeField] private AudioClip fireSound;   // TODO: integrate
    [SerializeField] private AudioClip laserSound;
    [SerializeField] private AudioClip sawSound;
    [SerializeField] private AudioClip pistonSound;
    [SerializeField] private AudioClip flapCircleSound;
    [SerializeField] private AudioClip ambienceSound;

    [SerializeField] [Range(0f, 1f)] private float walkVolume = 1f;
    [SerializeField] [Range(0f, 1f)] private float jumpVolume = 1f;
    [SerializeField] [Range(0f, 1f)] private float flapVolume = 1f;
    [SerializeField] [Range(0f, 1f)] private float deathVolume = 1f;
    [SerializeField] [Range(0f, 1f)] private float trapdoorVolume = 1f;
    [SerializeField] [Range(0f, 1f)] private float fireVolume = 1f;
    [SerializeField] [Range(0f, 1f)] private float laserVolume = 1f;
    [SerializeField] [Range(0f, 1f)] private float sawVolume = 0.3f; // Set this lower for the saw
    [SerializeField] [Range(0f, 1f)] private float pistonVolume = 1f;
    [SerializeField] [Range(0f, 1f)] private float flapCircleVolume = 1f;

    [SerializeField] private float sawDistance = 10f;

    [SerializeField] private AudioSource fxSource;

    [SerializeField] private AudioSource musicSource;



    public float Volume
    {
        get { return fxSource != null ? fxSource.volume : 0f; }
        set
        {
            if (fxSource != null)
            {
                fxSource.volume = Mathf.Clamp01(value);
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Ensure we have two AudioSource components
            if (fxSource == null)
            {
                fxSource = gameObject.AddComponent<AudioSource>();
            }
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true; // Set music to loop
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClip(AudioClip clip, float volumeMultiplier = 1f)
    {
        if (clip != null && fxSource != null)
        {
            fxSource.PlayOneShot(clip, fxSource.volume * volumeMultiplier);
        }
        else
        {
            Debug.LogWarning("AudioClip or AudioSource is null");
        }
    }

    // For distance-based volume
    public void PlayClipAtPoint(AudioClip clip, Vector3 position, float maxDistance, float volumeMultiplier = 1f)
    {
        if (clip != null && fxSource != null)
        {
            GameObject audioSource = new GameObject("One shot audio");
            audioSource.transform.position = position;
            AudioSource source = audioSource.AddComponent<AudioSource>();
            source.clip = clip;
            source.spatialBlend = 1f; // Make the sound fully 3D
            source.maxDistance = maxDistance;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.Play();
            float duration = clip.length;
            source.volume = fxSource.volume * volumeMultiplier;
            Destroy(audioSource, duration);
        }
        else
        {
            Debug.LogWarning("AudioClip or AudioSource is null");
        }
    }

    public void PlayWalkSound()
    {
        PlayClip(walkSound, walkVolume);
    }

    public void PlayJumpSound()
    {
        PlayClip(jumpSound, jumpVolume);
    }

    public void PlayFlapSound()
    {
        PlayClip(flapSound, flapVolume);
    }

    public void PlayDeathSound()
    {
        PlayClip(deathSound, deathVolume);
    }

    public void PlayTrapdoorSound()
    {
        PlayClip(trapdoorSound, trapdoorVolume);
    }

    public void PlayFireSound()
    {
        PlayClip(fireSound, fireVolume);
    }

    public void PlayLaserSound()
    {
        PlayClip(laserSound, laserVolume);
    }

    public void PlaySawSound(Vector3 position, Transform playerTransform)
    {
        if (sawSound != null && fxSource != null)
        {
            float distance = Vector3.Distance(position, playerTransform.position);
            float maxDistance = sawDistance; 
            float volume = Mathf.Clamp01(1 - (distance / maxDistance)) * sawVolume;
            
            AudioSource.PlayClipAtPoint(sawSound, position, fxSource.volume * volume);
        }
        else
        {
            Debug.LogWarning("Saw sound or AudioSource is null");
        }
    }

    public void PlayPistonSound()
    {
        PlayClip(pistonSound, pistonVolume);
    }

    public void PlayFlapCircleSound()
    {
        PlayClip(flapCircleSound, flapCircleVolume);
    }

    public void PlayAmbienceSound()
    {
        PlayClip(ambienceSound);
    }

    public void PlayBackgroundMusic(AudioClip musicClip, bool fadeIn = true, float fadeDuration = 2f)
    {
        if (musicClip != null && musicSource != null)
        {
            musicSource.clip = musicClip;
            if (fadeIn)
            {
                FadeInMusic(fadeDuration);
            }
            else
            {
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Music clip or AudioSource is null");
        }
    }

    public void SetEffectsVolume(float volume)
    {
        fxSource.volume = Mathf.Clamp01(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public float GetEffectsVolume()
    {
        return fxSource.volume;
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public void FadeInMusic(float duration)
    {
        StartCoroutine(FadeInMusicCoroutine(duration));
    }

    private IEnumerator FadeInMusicCoroutine(float duration)
    {
        float startVolume = 0f;
        musicSource.volume = startVolume;
        musicSource.Play();

        while (musicSource.volume < 1f)
        {
            musicSource.volume += Time.deltaTime / duration;
            yield return null;
        }

        musicSource.volume = 1f;
    }
}
