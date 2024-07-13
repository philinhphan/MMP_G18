using UnityEngine;

public class FlapCircleSensorController : MonoBehaviour
{
    [SerializeField]
    private GameObject flapCircle;

    void OnTriggerEnter2D()
    {
        flapCircle.GetComponent<FlapCircleController>().ResetFlapCircle();
    }
}
