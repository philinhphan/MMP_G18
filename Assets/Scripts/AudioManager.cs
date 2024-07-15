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

    public void PlayClip(AudioClip clip)
    {
        if (clip != null && fxSource != null)
        {
            fxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioClip or AudioSource is null");
        }
    }

    public void PlayWalkSound()
{
    PlayClip(walkSound);
}

public void PlayJumpSound()
{
    PlayClip(jumpSound);
}

public void PlayFlapSound()
{
    PlayClip(flapSound);
}

public void PlayDeathSound()
{
    PlayClip(deathSound);
}

public void PlayTrapdoorSound()
{
    PlayClip(trapdoorSound);
}

public void PlayFireSound()
{
    PlayClip(fireSound);
}

public void PlayLaserSound()
{
    PlayClip(laserSound);
}

public void PlaySawSound()
{
    PlayClip(sawSound);
}

public void PlayPistonSound()
{
    PlayClip(pistonSound);
}

public void PlayFlapCircleSound()
{
    PlayClip(flapCircleSound);
}

public void PlayAmbienceSound()
{
    PlayClip(ambienceSound);
}

public void PlayBackgroundMusic(AudioClip musicClip)
{
    if (musicClip != null && musicSource != null)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }
    else
    {
        Debug.LogWarning("Music clip or AudioSource is null");
    }
}

    
}
