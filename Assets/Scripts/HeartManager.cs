using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public static int health = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator heartAnimator;

    private void Start()
    {
        health = 3;
    }

    void Update()
    {
        /* foreach (Image img in hearts)
         {
             img.sprite = emptyHeart;
         }

         for (int i = 0; i < health; i++)
         {
             hearts[i].sprite = fullHeart;
         }*/
    }
    public void LoseHeart()
    {
        if (health < 1)
        {
            heartAnimator.SetTrigger("LoseHeart1");
        }

        else if (health < 2)
        {
            heartAnimator.SetTrigger("LoseHeart2");
        }

        else if (health < 3)
        {
            heartAnimator.SetTrigger("LoseHeart3");
        }
    }


}
