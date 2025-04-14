using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    public void StartGame()
    {
        audioManager.PlaySFXByIndex(1);
        StartCoroutine(DelaySceneLoad("Game Scene"));
    }

    public void OpenTutorial()
    {
        audioManager.PlaySFXByIndex(1);
        StartCoroutine(DelaySceneLoad("Tutorial"));
    }

    private IEnumerator DelaySceneLoad(string sceneName)
    {
        yield return new WaitForSecondsRealtime(0.5f); // Adjust based on your audio clip length
        SceneManager.LoadScene(sceneName);
    }
}