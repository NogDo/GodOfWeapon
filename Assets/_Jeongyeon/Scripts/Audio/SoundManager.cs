using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [Header("�κ����� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] lobbyAudioClip;
    [Header("������� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] backgrounAudioClip;
    [Header("����Ʈ Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] effectAudioClip;
    [Header("ĳ���� ȿ���� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] characterAudioClip;
    [Header("���� ȿ���� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] weaponAudioClip;

    public AudioClip stageEndClip;

    [Header("���庰 ����� �ҽ�")]
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
        PlayLobbyAudio(0);
        yield return new WaitUntil(() => CStageManager.Instance != null);
        CStageManager.Instance.OnStageEnd += StageEndSound;
    }
    /// <summary>
    /// �κ� ������ ����ϴ� �޼���
    /// </summary>
    /// <param name="index">���� �κ������� ����Ұ��� �ִ� �Ķ����</param>
    public void PlayLobbyAudio(int index)
    {
        StopBackgroundAudio();
        backgroundAudioSource.clip = lobbyAudioClip[index];
        backgroundAudioSource.Play();
    }
    /// <summary>
    /// ��������� ����ϴ� �޼���
    /// </summary>
    /// <param name="index">�������� �Է�</param>
    public void PlayBackgrounAudio(int index)
    {
        StopBackgroundAudio();
        if (index == 10 || index == 20)
        {            
            backgroundAudioSource.clip = backgrounAudioClip[4];
            backgroundAudioSource.Play();
        }
        else 
        {
            if (index < 5)
            {
                backgroundAudioSource.clip = backgrounAudioClip[0];
                backgroundAudioSource.Play();
            }
            else if (index < 9)
            {
                backgroundAudioSource.clip = backgrounAudioClip[1];
                backgroundAudioSource.Play();
            }
            else if (index < 15)
            {
                backgroundAudioSource.clip = backgrounAudioClip[2];
                backgroundAudioSource.Play();
            }
            else if (index < 19)
            {
                backgroundAudioSource.clip = backgrounAudioClip[3];
                backgroundAudioSource.Play();
            }
        }
    }
    public void PlayStageStartAudio()
    {
        StopBackgroundAudio();
        backgroundAudioSource.clip = effectAudioClip[0];
        backgroundAudioSource.Play();
    }
    /// <summary>
    /// ���� ȿ������ ����ϴ� �޼���
    /// </summary>
    /// <param name="index">���� ȿ��������</param>
    public void PlayWeaponAudio(int index)
    {
        effectAudioSource.PlayOneShot(weaponAudioClip[index]);
    }
    /// <summary>
    /// ȿ������ ����ϴ� �޼���
    /// </summary>
    /// <param name="index">���° ȿ��������</param>
    public void PlayEffectAudio(int index)
    { 
        effectAudioSource.PlayOneShot(effectAudioClip[index]);
    }
    /// <summary>
    /// ĳ���� ���� ������� ����ϴ� �޼���
    /// </summary>
    /// <param name="index">���� ȿ��������</param>
    public void PlayCharacterAudio(int index)
    {
        StopCharacterAudio();
        characterAudioSource.clip = characterAudioClip[index];
        characterAudioSource.Play();
    }

    /// <summary>
    /// ��������� ���ߴ� �޼���
    /// </summary>
    public void StopBackgroundAudio()
    {
        if (backgroundAudioSource.clip != null && backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.Stop();
        }
    }
    /// <summary>
    /// ȿ������ ���ߴ� �޼���
    /// </summary>
    public void StopEffectAudio()
    {
        if (effectAudioSource.clip != null && effectAudioSource.isPlaying)
        {
            effectAudioSource.Stop();
        }
    }
    /// <summary>
    /// ĳ���� ���� ������� ���ߴ� �޼���
    /// </summary>
    public void StopCharacterAudio()
    {
        if (characterAudioSource.clip != null && characterAudioSource.isPlaying)
        {
            characterAudioSource.Stop();
        }
    }
    /// <summary>
    /// ���������� ����Ǹ� ������ �޼���
    /// </summary>
    public void StageEndSound()
    {
        StopBackgroundAudio();
        StopCharacterAudio();
        PlayBackgrounAudio(3);
    }

    /// <summary>
    /// ��� ���带 ���ߴ� �޼���
    /// </summary>
    public void StopAllSound()
    {
        StopBackgroundAudio();
        StopEffectAudio();
        StopCharacterAudio();
    }
}
