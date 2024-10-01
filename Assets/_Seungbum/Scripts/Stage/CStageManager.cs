using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStageManager : MonoBehaviour
{
    #region static ����
    public static CStageManager Instance { get; private set; }
    #endregion

    #region private ����
    [SerializeField]
    GameObject oStartMap;
    Transform tfCharacter;

    int nLevel = 1;
    int nCurrentLevel = 1;
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
        for (int i = 0; i < nCurrentLevel + nLevel; i++)
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
    /// ���������� �����Ѵ�.
    /// </summary>
    public void StartStage()
    {
        nStageCount = 1;
        tfCharacter = FindObjectOfType<Character>().transform;

        oStartMap.SetActive(false);
        CCreateMapManager.Instance.Init();
        CShopManager.Instance.InActiveShop();
        Camera.main.GetComponentInParent<CharacterCamera>().InCreaseCameraCount();

        tfCharacter.position = new Vector3(2.0f, 0.0f, 2.0f);
    }

    /// <summary>
    /// ���� ���������� �����Ѵ�.
    /// </summary>
    public void NextStage()
    {
        nStageCount++;

        CCreateMapManager.Instance.AddLine();
        CShopManager.Instance.InActiveShop();

        tfCharacter.position = new Vector3(2.0f, 0.0f, 2.0f);
    }

    /// <summary>
    /// ������ �����Ѵ�.
    /// </summary>
    public void StartShop()
    {
        CShopManager.Instance.ActiveShop();
    }
}
