using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    [SerializeField]
    private LightFlickerBg lightFlickerScript;

    public void HandlePlayButton()
    {   
        lightFlickerScript.FlashLight();
        StartCoroutine(WaitForFlashThenChangeScene());
    }

    public void HandleQuitButton()
    {
        lightFlickerScript.FlashLight();
        Application.Quit();
    }

        IEnumerator WaitForFlashThenChangeScene()
    {
        yield return new WaitForSeconds(lightFlickerScript.GetFlashDuration() * 2);

        //Disable all canvas elements to simulate fade to black
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        SceneManager.LoadScene(1);
    }

}
