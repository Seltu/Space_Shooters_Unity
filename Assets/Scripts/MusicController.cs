using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    public AudioSource audioSource;

    public void PlayWarning()
    {
        audioSource.clip = audios[0];
        audioSource.Play();
        StartCoroutine(PlayBossMusic(1));
    }

    IEnumerator PlayBossMusic(int boss)
    {
        yield return new WaitForSeconds(5f);
        audioSource.clip = audios[boss];
        //audioSource.PlayScheduled(AudioSettings.dspTime + 5f);
        audioSource.Play();
    }
}
