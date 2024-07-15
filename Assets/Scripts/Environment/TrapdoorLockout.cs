using System.Collections;
using UnityEngine;

public class TrapdoorLockout : MonoBehaviour
{
    private Rigidbody2D r2d;
    private HingeJoint2D h2d;
    private Vector3 originalPos;
    private Quaternion originalRot;

    [SerializeField]
    private JointLimitState2D InitialLimitState = JointLimitState2D.UpperLimit;
    
    //private AudioManager audioManager;


    void Start()
    {
        h2d = GetComponent<HingeJoint2D>();
        r2d = GetComponent<Rigidbody2D>();

        gameObject.transform.GetPositionAndRotation(out originalPos, out originalRot);                
        r2d.constraints = RigidbodyConstraints2D.FreezeAll;

        // Initialize AudioManager reference
        /*audioManager = AudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }*/
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //audioManager.PlayTrapdoorSound();
        r2d.constraints = RigidbodyConstraints2D.None;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        StartCoroutine(LockTrapdoor());
    }

    private bool hasStoppedMoving()
    {
        //need to check both Limit and Speed to avoid deadlock in coroutine
        return h2d.limitState == InitialLimitState || h2d.jointSpeed == 0;
    }

    IEnumerator LockTrapdoor()
    {
        yield return new WaitUntil(hasStoppedMoving);
        if(r2d.constraints == RigidbodyConstraints2D.None){
            gameObject.transform.SetPositionAndRotation(originalPos, originalRot);
            r2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
