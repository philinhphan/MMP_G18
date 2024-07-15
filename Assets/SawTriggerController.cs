using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTriggerController : MonoBehaviour
{
    [SerializeField]
    private GameObject saw;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (saw != null)
            saw.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (saw != null)
            saw.SetActive(false);
    }
}
