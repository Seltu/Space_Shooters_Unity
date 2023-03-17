using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource.Play();
        Destroy(gameObject, 1.6f);
    }
}
