using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStageManager : MonoBehaviour
{
    #region static ����
    public static CStageManager Instance { get; private set; }
    #endregion

    #region private ����
    int nLevel = 1;
    int nCurrnetLevel = 1;
    int nStageCount = 0;
    float fEXP = 0.0f;

    bool isClick = false;
    #endregion

    /// <summary>
    /// ���� ��������
    /// </summary>
    public int StageCount
    { 
        get
        {
            return nStageCount;
        }
    }

    /// <summary>
    /// ����ġ
    /// </summary>
    public float EXP
    {
        get
        {
            return fEXP;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => CellManager.Instance != null);

        CellManager.Instance.OnCellClick += AddCellCheck;
    }

    /// <summary>
    /// �������� ���� �� �κ��丮 ĭ �߰� �ڷ�ƾ�� �����Ѵ�.
    /// </summary>
    public void EndStage()
    {
        StartCoroutine(AddCellCountCheck());
    }

    /// <summary>
    /// ���� ��·��� ���� �κ��丮�� ĭ�� �ø���.
    /// </summary>
    /// <returns></returns>
    public IEnumerator AddCellCountCheck()
    {
        for (int i = 0; i < nLevel; i++)
        {
            isClick = false;
            CellManager.Instance.AddCell();
            yield return new WaitUntil(() => isClick);
        }
        yield return null;
    }

    /// <summary>
    /// ĭ�� �߰��ƴٴ°� üũ
    /// </summary>
    private void AddCellCheck()
    {
        isClick = true;
    }

    /// <summary>
    /// �������� �����Ѵ�.
    /// </summary>
    public void StartStage()
    {
        nStageCount++;

        CCreateMapManager.Instance.Init(nStageCount);
    }
}
