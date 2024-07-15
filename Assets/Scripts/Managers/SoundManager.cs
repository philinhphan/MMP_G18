using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundObject;

    public AudioClip jumpClip;
    public AudioClip FlapSound;
    public AudioClip DieSound;
    public AudioClip LaserSound;

    public Transform playerTransform;
    
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip audioClip)
    {
        PlayClip(audioClip, 1);
    }
    public void PlayClip(AudioClip audioClip, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float length = audioSource.clip.length;

        Destroy(audioSource.gameObject, length);
    }

    

    public GameObject PlayInterruptableClip(AudioClip audioClip)
    {
        return PlayInterruptableClip(audioClip, transform.position);
    }

    public GameObject PlayInterruptableClip(AudioClip audioClip, float volume)
    {
        return PlayInterruptableClip(audioClip, transform.position, volume);
    }


    public GameObject PlayInterruptableClip(AudioClip audioClip, Vector3 position)
    {
        return PlayInterruptableClip(audioClip, position, 1);
    }
    
    public GameObject PlayInterruptableClip(AudioClip audioClip, Vector3 position, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        return audioSource.gameObject;
    }

    // adapted from PlayClipAtPoint()
    public GameObject PlayLocalClip(AudioClip audioClip, Vector3 position, float minDistance, float maxDistance, float clipVolume)
    {
            AudioSource audioSource = Instantiate(soundObject, position, Quaternion.identity);
            audioSource.clip = audioClip;
            audioSource.spatialBlend = 1f; // Make the sound fully 3D
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.volume = clipVolume;
            audioSource.Play();

            return audioSource.gameObject;
    }
}
