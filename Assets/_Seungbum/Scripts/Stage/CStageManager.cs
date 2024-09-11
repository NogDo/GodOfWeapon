using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStageManager : MonoBehaviour
{
    int level = 3;
    bool isClick = false;
    public static CStageManager Instance { get; private set; }


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
        CellManager.Instance.OnCellClick += AddCellCheck;
        Invoke("StartStage", 3f);   
    }

    public void StartStage()
    {
        StartCoroutine(AddCellCountCheck());
    }
    public IEnumerator AddCellCountCheck()
    {
        for (int i = 0; i < level; i++)
        {
            isClick = false;
            CellManager.Instance.AddCell();
            yield return new WaitUntil(() => isClick);
        }
        yield return null;
    }

    private void AddCellCheck()
    {
        isClick = true;
        
    }
}
