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
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip audioClip)
    {
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.Play();

        float length = audioSource.clip.length;

        Destroy(audioSource.gameObject, length);
    }

    public GameObject PlayInterruptableClip(AudioClip audioClip)
    {
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.Play();

        return audioSource.gameObject;
    }
}
