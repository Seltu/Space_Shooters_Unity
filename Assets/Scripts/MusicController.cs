using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    [SerializeField] private GameObject levelManager;
    public AudioSource audioSource;

    public void PlayWarning()
    {
        audioSource.clip = audios[0];
        audioSource.Play();
        StartCoroutine(PlayBossMusic(levelManager.GetComponent<LevelManager>().level + 1));
    }

    IEnumerator PlayBossMusic(int boss)
    {
        yield return new WaitForSeconds(5f);
        audioSource.clip = audios[boss];
        //audioSource.PlayScheduled(AudioSettings.dspTime + 5f);
        audioSource.Play();
    }

    public void BackToBasic()
    {
        audioSource.clip = audios[4];
        audioSource.Play();
    }
}
