using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject instru1;
    public GameObject WASD;
    public GameObject instru2;
    public GameObject instru3;
    public GameObject button;
    public GameObject good;
    public GameObject lives;
    private TutorialCar tutorialCar;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        tutorialCar = FindObjectOfType<TutorialCar>().GetComponent<TutorialCar>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            WASD.SetActive(false);
        }

        if (tutorialCar.partOneDone)
        {
            StartCoroutine(Delay());
            instru1.SetActive(false);
            instru2.SetActive(true);
        }

        if (tutorialCar.partTwoDone)
        {
            StartCoroutine(Delay());
            instru2.SetActive(false);
            instru3.SetActive(true);
        }

        if (tutorialCar.partThreeDone)
        {
            StartCoroutine(Delay());
            instru3.SetActive(false);
            good.SetActive(true);
            button.SetActive(true);
            lives.SetActive(true);
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
    }
}
