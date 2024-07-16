using System.Collections;
using UnityEngine;

public class TrapdoorController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    private Rigidbody2D r2d;
    private bool hasUnlocked = false;
    private bool hasSoundPlayed = false;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (r2d.constraints == RigidbodyConstraints2D.None)
        {
            hasUnlocked = true;
        }
        else
        {
            hasUnlocked = false;
        }
    }

    void OnCollisionEnter2D (Collision2D collision2D)
    {
        if (hasUnlocked && !hasSoundPlayed)
        {
            audioSource.Play();
            hasSoundPlayed = true;
        }
    }

    void OnCollisionExit2D (Collision2D collision)
    {
        StartCoroutine(AudioCooldown());
    }

    IEnumerator AudioCooldown()
    {
        yield return new WaitForSeconds(3);
        hasSoundPlayed = false;
    }
}
