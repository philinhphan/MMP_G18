using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText;

    [SerializeField]
    private LightFlickerBg lightFlickerScript;
    private string lastTimeString;

    void Start()
    {
        timerText.text = lastTimeString;
    }


    void OnEnable()
    {
        lastTimeString = PlayerPrefs.GetString("lastTime");
    }

    public void HandlePlayAgain()
    {
        lightFlickerScript.FlashLight();
        StartCoroutine(WaitForFlashThenChangeScene(1));
    }

    public void HandleMainMenu()
    {
        lightFlickerScript.FlashLight();
        StartCoroutine(WaitForFlashThenChangeScene(0));
    }


    IEnumerator WaitForFlashThenChangeScene(int sceneIndex)
    {
        yield return new WaitForSeconds(lightFlickerScript.GetFlashDuration() * 2);

        //Disable all canvas elements to simulate fade to black
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        SceneManager.LoadScene(sceneIndex);
    }

}
