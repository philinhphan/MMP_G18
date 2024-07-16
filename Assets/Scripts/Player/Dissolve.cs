using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{

    [SerializeField] private float _dissolveTime = 0.5f;
    
    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;

    private int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");


    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _materials = new Material [_spriteRenderers.Length];

        for (int i = 0; i < _spriteRenderers.Length; i++) _materials [i] = _spriteRenderers[i].material;
    }

    private void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            StartCoroutine(Vanish(true));
        }

        if (Input.GetKeyDown("b"))
        {
            StartCoroutine(Appear(true));
        }
    }

    public IEnumerator Vanish(bool dissolve)
    {
        float elapsedTime = 0;

        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedDissolve = Mathf.Lerp(0, 1.1f, (elapsedTime / _dissolveTime));

            for (int i = 0; i < _materials.Length; i++)
            {
                if (dissolve)
                _materials[i].SetFloat(_dissolveAmount, lerpedDissolve);
            }

            yield return null;
        }
    }

    public IEnumerator Appear(bool dissolve)
    {
        float elapsedTime = 0;

        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(1.1f, 0, (elapsedTime / _dissolveTime));

            for (int i = 0; i < _materials.Length; i++) 
            {
                if (dissolve)
                    _materials[i].SetFloat(_dissolveAmount, lerpedDissolve);
            }

            yield return null;
        }
    }
}
