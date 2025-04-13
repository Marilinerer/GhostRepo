using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public List<AudioClip> Music;
    public List<AudioClip> SFX;

    private void Start()
    {
        musicSource.clip = Music[0];
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFXByIndex(int index)
    {
        if (index >= 0 && index < SFX.Count)
        {
            sfxSource.PlayOneShot(SFX[index]);
        }
    }

}
