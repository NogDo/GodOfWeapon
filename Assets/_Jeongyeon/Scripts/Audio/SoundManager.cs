using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClip[] backgrounAudioClip;


    public AudioSource backgroundAudioSource;
    public AudioSource effectAudioSource;
    public AudioSource playerAudioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        PlayBackgrounAudio(0);
    }

    public void PlayBackgrounAudio(int index)
    {
        if(backgroundAudioSource != null && backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.Stop();
        }
        backgroundAudioSource.clip = backgrounAudioClip[index];
        backgroundAudioSource.Play();
    }
}
