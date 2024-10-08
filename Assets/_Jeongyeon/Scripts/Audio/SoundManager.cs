using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("사운드 클립을 담아두는 배열")]
    public AudioClip[] backgrounAudioClip;
    public AudioClip[] effectAudioClip;
    public AudioClip[] characterAudioClip;

    [Header("사운드별 오디오 소스")]
    public AudioSource backgroundAudioSource;
    public AudioSource effectAudioSource;
    public AudioSource characterAudioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        
    }

    private IEnumerator Start()
    {
        PlayBackgrounAudio(0);
        yield return new WaitUntil(() => CStageManager.Instance != null);
        CStageManager.Instance.OnStageEnd += StageEndSound;
    }

    public void PlayBackgrounAudio(int index)
    {
        StopBackgroundAudio();
        backgroundAudioSource.clip = backgrounAudioClip[index];
        backgroundAudioSource.Play();
    }

    public void PlayEffectAudio(int index)
    {
        StopEffectAudio();
        effectAudioSource.clip = effectAudioClip[index];
        effectAudioSource.Play();
    }

    public void PlayCharacterAudio(int index)
    {
        StopCharacterAudio();
        characterAudioSource.clip = characterAudioClip[index];
        characterAudioSource.Play();
    }


    public void StopBackgroundAudio()
    {
        if (backgroundAudioSource.clip != null && backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.Stop();
        }
    }

    public void StopEffectAudio()
    {
        if (effectAudioSource.clip != null && effectAudioSource.isPlaying)
        {
            effectAudioSource.Stop();
        }
    }

    public void StopCharacterAudio()
    {
        if (characterAudioSource.clip != null && characterAudioSource.isPlaying)
        {
            characterAudioSource.Stop();
        }
    }

    public void StageEndSound()
    {
        StopBackgroundAudio();
        StopCharacterAudio();
        StopCharacterAudio();
        PlayBackgrounAudio(3);

    }
}
