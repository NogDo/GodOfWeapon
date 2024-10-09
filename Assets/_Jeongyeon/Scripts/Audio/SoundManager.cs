using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [Header("�κ����� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] lobbyAudioClip;
    [Header("������� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] backgroundAudioClip;
    [Header("����Ʈ Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] effectAudioClip;
    [Header("ĳ���� ȿ���� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] characterAudioClip;
    [Header("�� ���� ȿ���� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] lWeaponAudioClip;
    [Header("���� ȿ���� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] cWeaponAudioClip;
    [Header("ª�� ���� ȿ���� Ŭ���� ��Ƶδ� �迭")]
    public AudioClip[] sWeaponAudioClip;

    [Header("�������� ���� Ŭ��")]
    public AudioClip stageEndClip;
    public AudioClip gameOverClip;

    [Header("UI���� Ŭ��")]
    public AudioClip buttonClickClip;

    [Header("���庰 ����� �ҽ�")]
    public AudioSource backgroundAudioSource;
    public AudioSource effectAudioSource;
    public AudioSource characterAudioSource;
    public AudioSource weaponAudioSourece;
    
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
        backgroundAudioSource.loop = false;
        if (index == 10)
        {
            backgroundAudioSource.clip = backgroundAudioClip[4];
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }
        else if (index == 20)
        {
            backgroundAudioSource.clip = backgroundAudioClip[5];
            backgroundAudioSource.Play();
        }
        else
        {
            if (index < 5)
            {
                backgroundAudioSource.clip = backgroundAudioClip[0];
                backgroundAudioSource.Play();
            }
            else if (index < 10)
            {
                backgroundAudioSource.clip = backgroundAudioClip[1];
                backgroundAudioSource.Play();
            }
            else if (index < 16)
            {
                backgroundAudioSource.clip = backgroundAudioClip[2];
                backgroundAudioSource.Play();
            }
            else if (index < 20)
            {
                backgroundAudioSource.clip = backgroundAudioClip[3];
                backgroundAudioSource.Play();
            }
        }
    }

    /// <summary>
    /// �������� ���۽� ������ �ö���� �Ҹ��� ����ϴ� �޼���
    /// </summary>
    public void PlayStageStartAudio()
    {
        StopBackgroundAudio();
        backgroundAudioSource.clip = effectAudioClip[0];
        backgroundAudioSource.Play();
    }
    /// <summary>
    /// �� ���� ȿ������ ����ϴ� �޼���
    /// </summary>
    /// <param name="index">Ư�� ����ȿ����</param>
    public void PlayLWeaponAudio(int index)
    {
        weaponAudioSourece.PlayOneShot(lWeaponAudioClip[index]);
    }
    /// <summary>
    /// ª�� ���� ȿ������ ����ϴ� �޼���
    /// </summary>
    /// <param name="index">Ư�� ����ȿ����</param>
    public void PlaySWeaponAudio(int index)
    {
        weaponAudioSourece.PlayOneShot(sWeaponAudioClip[index]);
    }
    /// <summary>
    /// ���� ȿ������ ����ϴ� �޼���
    /// </summary>
    /// <param name="index">ȿ���� ����</param>
    public void PlayCWeaponAudio(int index)
    {
        weaponAudioSourece.PlayOneShot(cWeaponAudioClip[index]);
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
        characterAudioSource.PlayOneShot(characterAudioClip[index]);
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
        if (CStageManager.Instance.StageCount < 20)
        {
            backgroundAudioSource.clip = stageEndClip;
            backgroundAudioSource.loop = false;
            backgroundAudioSource.Play();
        }
    }

    public void ButtonClickSound()
    {
        effectAudioSource.PlayOneShot(buttonClickClip);
    }
    /// <summary>
    /// �÷��̾ �׾����� ���� �Ҹ��� ����ϴ� �޼���
    /// </summary>
    public void PlayerDie()
    {
        StopBackgroundAudio();
        StopCharacterAudio();
        backgroundAudioSource.clip = gameOverClip;
        backgroundAudioSource.loop = false;
        backgroundAudioSource.Play();
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
