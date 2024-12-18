using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartScreenManager : MonoBehaviour
{
    public float startDelay = 3f;
    public void StartGame()
    {
        StartCoroutine(DelayedStart());

    }

    IEnumerator DelayedStart()
    {

        yield return new WaitForSeconds(startDelay);

        // Load the main game scene
        SceneManager.LoadScene("__Scene_0");
    }
    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
