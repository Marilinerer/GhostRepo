using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject gameOverPanel;
    private AudioManager audioManager;
    private bool gameOver = false;

    void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
        gameOverPanel.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (HeartManager.health <= 0)
        {
            Time.timeScale = 0;
            if (!gameOver)
            {
                audioManager.PlaySFXByIndex(3); // game over sfx
                gameOver = true;
            }
            gameOverPanel.SetActive(true);
        }
    }

    public void Pause()
    {
        audioManager.PlaySFXByIndex(13); // pause sfx
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);

    }
    public void Resume()
    {
        audioManager.PlaySFXByIndex(12); // resume sfx
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ChangeSceneWithDelay(string sceneName)
    {
        print("SceneChangeWDelay");
        audioManager.PlaySFXByIndex(13); // home sfx
        StartCoroutine(Delay(sceneName));
    }

    public IEnumerator Delay(string sceneName)
    {

        yield return new WaitForSecondsRealtime(0.5f);

        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReloadSceneWithDelay()
    {
        print("SceneReloadWDelay");
        audioManager.PlaySFXByIndex(12); // restart sfx
        StartCoroutine(Delaye());
    }

    public IEnumerator Delaye()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isPaused = false;
        gameOverPanel.SetActive(false);
    }
}
