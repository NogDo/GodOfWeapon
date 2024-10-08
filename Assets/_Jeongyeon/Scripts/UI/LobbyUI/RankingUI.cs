using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RankingUI : MonoBehaviour
{
    #region Public Fields
    public GameObject dialogUI; // GŰ�� ������ UI�� ��Ÿ���ٴ°� �˷��ִ� UI
    public GameObject rankingUI; // ��ŷ���� ��Ÿ���� UI
    public GameObject rankingTitleUIPanel; // ��ŷ���� ������ ��Ÿ���� UI
    public GameObject loadingUI; // ��ŷ�� �ҷ����� ���� ��Ÿ���� UI
    [Header("����� ��Ÿ���� UI")]
    public GameObject[] resultPanel;
    [Header("������ �̸��� ��Ÿ���� UI")]
    public TMP_Text[] nameTexts;
    [Header("������ ������ ��Ÿ���� UI")]
    public TMP_Text[] scoreTexts;

    private List<RankData> userRank; // ������ ��ŷ�� �����ϴ� ����Ʈ
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

