using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [Header("로비음악 클립을 담아두는 배열")]
    public AudioClip[] lobbyAudioClip;
    [Header("배경음악 클립을 담아두는 배열")]
    public AudioClip[] backgroundAudioClip;
    [Header("이펙트 클립을 담아두는 배열")]
    public AudioClip[] effectAudioClip;
    [Header("캐릭터 효과음 클립을 담아두는 배열")]
    public AudioClip[] characterAudioClip;
    [Header("무기 효과음 클립을 담아두는 배열")]
    public AudioClip[] weaponAudioClip;

    [Header("스테이지 종료 클립")]
    public AudioClip stageEndClip;
    public AudioClip gameOverClip;

    [Header("UI관련 클립")]
    public AudioClip buttonClickClip;

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
        PlayLobbyAudio(0);
        yield return new WaitUntil(() => CStageManager.Instance != null);
        CStageManager.Instance.OnStageEnd += StageEndSound;
    }
    /// <summary>
    /// 로비 음악을 재생하는 메서드
    /// </summary>
    /// <param name="index">무슨 로비음악을 재생할건지 넣는 파라미터</param>
    public void PlayLobbyAudio(int index)
    {
        StopBackgroundAudio();
        backgroundAudioSource.clip = lobbyAudioClip[index];
        backgroundAudioSource.Play();
    }
    /// <summary>
    /// 배경음악을 재생하는 메서드
    /// </summary>
    /// <param name="index">스테이지 입력</param>
    public void PlayBackgrounAudio(int index)
    {
        StopBackgroundAudio();
        if (index == 10)
        {
            backgroundAudioSource.clip = backgroundAudioClip[4];
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
            else if (index < 9)
            {
                backgroundAudioSource.clip = backgroundAudioClip[1];
                backgroundAudioSource.Play();
            }
            else if (index < 15)
            {
                backgroundAudioSource.clip = backgroundAudioClip[2];
                backgroundAudioSource.Play();
            }
            else if (index < 19)
            {
                backgroundAudioSource.clip = backgroundAudioClip[3];
                backgroundAudioSource.Play();
            }
        }
    }

    /// <summary>
    /// 스테이지 시작시 발판이 올라오는 소리를 재생하는 메서드
    /// </summary>
    public void PlayStageStartAudio()
    {
        StopBackgroundAudio();
        backgroundAudioSource.clip = effectAudioClip[0];
        backgroundAudioSource.Play();
    }
    /// <summary>
    /// 무기 효과음을 재생하는 메서드
    /// </summary>
    /// <param name="index">무슨 효과음인지</param>
    public void PlayWeaponAudio(int index)
    {
        effectAudioSource.PlayOneShot(weaponAudioClip[index]);
    }
    /// <summary>
    /// 효과음을 재생하는 메서드
    /// </summary>
    /// <param name="index">몇번째 효과음인지</param>
    public void PlayEffectAudio(int index)
    {
        effectAudioSource.PlayOneShot(effectAudioClip[index]);
    }
    /// <summary>
    /// 캐릭터 관련 오디오를 재생하는 메서드
    /// </summary>
    /// <param name="index">무슨 효과음인지</param>
    public void PlayCharacterAudio(int index)
    {
        StopCharacterAudio();
        characterAudioSource.clip = characterAudioClip[index];
        characterAudioSource.Play();
    }

    /// <summary>
    /// 배경음악을 멈추는 메서드
    /// </summary>
    public void StopBackgroundAudio()
    {
        if (backgroundAudioSource.clip != null && backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.Stop();
        }
    }
    /// <summary>
    /// 효과음을 멈추는 메서드
    /// </summary>
    public void StopEffectAudio()
    {
        if (effectAudioSource.clip != null && effectAudioSource.isPlaying)
        {
            effectAudioSource.Stop();
        }
    }
    /// <summary>
    /// 캐릭터 관련 오디오를 멈추는 메서드
    /// </summary>
    public void StopCharacterAudio()
    {
        if (characterAudioSource.clip != null && characterAudioSource.isPlaying)
        {
            characterAudioSource.Stop();
        }
    }
    /// <summary>
    /// 스테이지가 종료되면 나오는 메서드
    /// </summary>
    public void StageEndSound()
    {
        StopBackgroundAudio();
        StopCharacterAudio();
        if (CStageManager.Instance.StageCount < 20)
        {
            backgroundAudioSource.clip = stageEndClip;
            backgroundAudioSource.Play();
        }
    }

    public void ButtonClickSound()
    {
        effectAudioSource.PlayOneShot(buttonClickClip);
    }
    /// <summary>
    /// 플레이어가 죽었을때 나는 소리를 재생하는 메서드
    /// </summary>
    public void PlayerDie()
    {
        StopBackgroundAudio();
        StopCharacterAudio();
        backgroundAudioSource.clip = gameOverClip;
        backgroundAudioSource.Play();
    }
    /// <summary>
    /// 모든 사운드를 멈추는 메서드
    /// </summary>
    public void StopAllSound()
    {
        StopBackgroundAudio();
        StopEffectAudio();
        StopCharacterAudio();
    }

    public void EatFoodSound()
    {
        effectAudioSource.PlayOneShot(characterAudioClip[1]);
    }
    /// <summary>
    /// 조합을 했을때 나오는 소리를 재생하는 메서드
    /// </summary>
    public void CombineSound()
    {
        effectAudioSource.PlayOneShot(effectAudioClip[1]);
    }
    /// <summary>
    /// 물건을 팔거나 재굴림할때 나오는 소리를 재생하는 메서드
    /// </summary>
    public void DeleteSound()
    {
        effectAudioSource.PlayOneShot(effectAudioClip[2]);
    }
    /// <summary>
    /// 셀을 추가할때 소리를 재생하는 메서드
    /// </summary>
    public void AddCellSound()
    {
        effectAudioSource.PlayOneShot(effectAudioClip[3]);
    }
    /// <summary>
    /// 아이템을 집거나 셀에 놓았을때 나오는 소리를 재생하는 메서드
    /// </summary>
    public void ItemOnCellSound()
    {
        effectAudioSource.PlayOneShot(effectAudioClip[4]);
    }
}
