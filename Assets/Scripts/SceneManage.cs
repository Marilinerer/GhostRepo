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
    //private AudioManager audioManager;

    void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
        gameOverPanel.SetActive(false);
        //audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
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
            gameOverPanel.SetActive(true);
        }
    }

    public void Pause()
    {
        //audioManager.PlaySFXByIndex(7);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);

    }
    public void Resume()
    {
        //audioManager.PlaySFXByIndex(8);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ChangeSceneWithDelay(string sceneName)
    {
        print("SceneChangeWDelay");
        StartCoroutine(Delay(sceneName));
    }

    public IEnumerator Delay(string sceneName)
    {

        yield return new WaitForSecondsRealtime(0.5f);

        //audioManager.PlaySFXByIndex(7);
        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReloadSceneWithDelay()
    {
        print("SceneReloadWDelay");
        StartCoroutine(Delaye());
    }

    public IEnumerator Delaye()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        //audioManager.PlaySFXByIndex(8);
        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isPaused = false;
        gameOverPanel.SetActive(false);
    }
}
