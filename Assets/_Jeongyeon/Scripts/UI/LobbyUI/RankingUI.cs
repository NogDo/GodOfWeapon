using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RankingUI : MonoBehaviour
{
    #region Public Fields
    public GameObject dialogUI; // G키를 누르면 UI가 나타난다는걸 알려주는 UI
    public GameObject rankingUI; // 랭킹판을 나타내는 UI
    public GameObject rankingTitleUIPanel; // 랭킹판의 제목을 나타내는 UI
    public GameObject loadingUI; // 랭킹을 불러오는 동안 나타나는 UI
    [Header("결과를 나타내는 UI")]
    public GameObject[] resultPanel;
    [Header("유저의 이름을 나타내는 UI")]
    public TMP_Text[] nameTexts;
    [Header("유저의 점수를 나타내는 UI")]
    public TMP_Text[] scoreTexts;

    private List<RankData> userRank; // 유저의 랭킹을 저장하는 리스트
    #endregion
    private void Awake()
    {
        userRank = new List<RankData>();
    }
    private void Start()
    {
        FireBaseManager.Instance.OnRank += SetUserRank;

    }
    private void OnDestroy()
    {
        FireBaseManager.Instance.OnRank -= SetUserRank;
    }
    private void Update()
    {
        if (dialogUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (rankingUI.activeSelf == false)
                {
                    SoundManager.Instance.ButtonClickSound();
                }
                dialogUI.SetActive(false);
                rankingUI.SetActive(true);
                loadingUI.SetActive(true);
                FireBaseManager.Instance.GetRankData();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            dialogUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if (dialogUI.activeSelf == true)
            {
                dialogUI.SetActive(false);
            }
            else
            {
                rankingUI.SetActive(false);
                if (userRank.Count > 0)
                {
                    for (int i = 0; i < userRank.Count; i++)
                    {
                        resultPanel[i].SetActive(false);
                    }
                }
                rankingTitleUIPanel.SetActive(false);
            }
        }
    }

    public void SetUserRank()
    {
        userRank = FireBaseManager.Instance.totalRankData;
        userRank.Sort(new Comparison<RankData>((a, b) => a.totalDamage.CompareTo(b.totalDamage)));

        if (userRank.Count > 0)
        {
            for (int i = 0; i < userRank.Count; i++)
            {
                nameTexts[i].text = userRank[i].userName;
                scoreTexts[i].text = userRank[i].totalDamage.ToString();
            }
            loadingUI.SetActive(false);
            rankingTitleUIPanel.SetActive(true);
            for (int i = 0; i < userRank.Count; i++)
            {
                resultPanel[i].SetActive(true);
            }
        }
        else
        {
            loadingUI.SetActive(false);
            rankingTitleUIPanel.SetActive(true);
        }
    }


}

