using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortalController : MonoBehaviour
{
    // [SerializeField]
    private string nextSceneName = "EndScreen";
    private UITimer timer;
    private string savedTimeString;

    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<UITimer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (timer != null)
        {
            // stop and save timer
            timer.playing = false;
            savedTimeString = timer.timerText.text;
        }
        SceneManager.LoadScene(nextSceneName);
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("lastTime", savedTimeString);
    }

}
