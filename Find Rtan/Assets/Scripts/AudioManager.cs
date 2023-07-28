using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgmClip;

    private void Start() {
        audioSource.clip = bgmClip;
        audioSource.Play();
    }   
}
