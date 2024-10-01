using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStageManager : MonoBehaviour
{
    #region static 변수
    public static CStageManager Instance { get; private set; }
    #endregion

    #region private 변수
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
    /// 현재 스테이지
    /// </summary>
    public int StageCount
    { 
        get
        {
            return nStageCount;
        }
    }

    /// <summary>
    /// 경험치
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
    /// 스테이지 종류 후 인벤토리 칸 추가 코루틴을 실행한다.
    /// </summary>
    public void EndStage()
    {
        StartCoroutine(AddCellCountCheck());
    }

    /// <summary>
    /// 레벨 상승량에 따라 인벤토리의 칸을 늘린다.
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
    /// 칸이 추가됐다는걸 체크
    /// </summary>
    private void AddCellCheck()
    {
        isClick = true;
    }

    /// <summary>
    /// 스테이지를 시작한다.
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
    /// 다음 스테이지를 시작한다.
    /// </summary>
    public void NextStage()
    {
        nStageCount++;

        CCreateMapManager.Instance.AddLine();
        CShopManager.Instance.InActiveShop();

        tfCharacter.position = new Vector3(2.0f, 0.0f, 2.0f);
    }

    /// <summary>
    /// 상점을 시작한다.
    /// </summary>
    public void StartShop()
    {
        CShopManager.Instance.ActiveShop();
    }
}
