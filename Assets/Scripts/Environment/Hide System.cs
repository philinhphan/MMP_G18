using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSystem : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float fadeDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other ) {

        if(other.CompareTag("Player"))
        {
            StartCoroutine(FadeAlphaToZero(GetComponent<SpriteRenderer>(), fadeDuration));
        }
    }
 
    IEnumerator FadeAlphaToZero(SpriteRenderer renderer, float duration) {
        Color startColor = renderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        float time = 0;
        while (time < duration) {
            time += Time.deltaTime;
            renderer.color = Color.Lerp(startColor, endColor, time/duration);
            yield return null;  
        }
    }
}
