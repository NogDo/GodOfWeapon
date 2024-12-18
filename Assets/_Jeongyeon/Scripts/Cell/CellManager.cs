using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CellManager : MonoBehaviour
{
    #region Private Fields
    // 셀 크기와 높이 설정 변수
    private int height = 12;
    private int width = 16;
    private float cellSize = 0.15f;
    private PlayerInventory playerInventory;

    //셀 프리팹 및 인벤토리에 있는 관련 변수
    [SerializeField] GameObject gridPrefab;
    private GameObject[,] gameGrid;
    private List<Pos> canActiveCell;
    #endregion

    #region Public Fields
    // 셀이 클릭이 되었을때 호출되는 이벤트
    public event Action OnCellClick;
    public CellInfo weaponInstancePostion;
    public CellInfo itemInstancePostion;
    //셀의 활성화 상태를 저장하는 배열
    public int[,] checkGrid;
    #endregion

    /// <summary>
    /// 활성화된 셀의 좌표값을 저장하는 구조체
    /// </summary>
    public struct Pos
    {
        public int x;
        public int z;
        public Pos(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
    }
    public static CellManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        checkGrid = new int[width, height];
        CreateGrid();
        SetCell();
        canActiveCell = new List<Pos>();
    }
    /// <summary>
    /// 게임이 재시작될때 셀을 비우는 메서드
    /// </summary>
    public void ReStart()
    {
        ResetAllCell();
        CreateGrid();
        SetCell();
        canActiveCell = new List<Pos>();
    }
    public void Init(PlayerInventory inventory)
    {
        playerInventory = inventory;
    }

    public PlayerInventory PlayerInventory
    {
        get
        {
            return playerInventory;
        }
    }

    /// <summary>
    /// 인벤토리에 셀 그리드를 생성하는 메서드
    /// </summary>
    private void CreateGrid()
    {
        gameGrid = new GameObject[width, height];
        if (gameGrid == null)
        {
            return;
        }
        gameGrid = new GameObject[width, height];
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                gameGrid[x, z] = Instantiate(gridPrefab, transform);
                gameGrid[x, z].GetComponent<CellInfo>().Init(x, z);
                gameGrid[x, z].transform.localPosition = new Vector3(x * cellSize, 0, z * cellSize);
                gameGrid[x, z].gameObject.name = $"Grid Space(x:{x.ToString()} z: {z.ToString()})";
                checkGrid[x, z] = -1;
            }
        }
        weaponInstancePostion = gameGrid[5, 6].GetComponent<CellInfo>();
        itemInstancePostion = gameGrid[5, 5].GetComponent<CellInfo>();
    }

    /// <summary>
    /// 시작시 활성화된 셀을 설정하는 메서드
    /// </summary>
    private void SetCell()
    {
        for (int x = 5; x < 11; x++)
        {
            for (int z = 5; z < 8; z++)
            {
                gameGrid[x, z].transform.GetChild(0).gameObject.SetActive(false);
                gameGrid[x, z].transform.GetChild(1).gameObject.SetActive(true);
                checkGrid[x, z] = 0;
            }
        }
    }

    /// <summary>
    /// 스테이지가 종료되고 레벨업 만큼의 셀을 더할 메서드
    /// </summary>
    public void AddCell()
    {
        ActiveCheck();
        ShuffleCell(canActiveCell);
        for (int j = 0; j < 5; j++)
        {
            int x = canActiveCell[j].x;
            int z = canActiveCell[j].z;
            gameGrid[x, z].transform.GetChild(0).gameObject.SetActive(false);
            gameGrid[x, z].transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 활성화 된 셀중 위 아래 좌 우를 확인해 활성화 가능 셀을 리스트에 넣는 메서드
    /// </summary>
    private void ActiveCheck()
    {
        canActiveCell.Clear();
        int[] dx = { 0, 0, 1, -1 };
        int[] dz = { 1, -1, 0, 0 };
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (checkGrid[x, z] == 0 || checkGrid[x, z] == 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int nx = x + dx[i];
                        int nz = z + dz[i];
                        if (nx < 0 || nx >= width || nz < 0 || nz >= height) continue;
                        if (checkGrid[nx, nz] == -1)
                        {
                            canActiveCell.Add(new Pos(nx, nz));
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 활성 가능셀을 받아와 셀을 섞는 메서드
    /// </summary>
    /// <param name="cell">활성화 가능셀만 모아둔 리스트</param>
    private void ShuffleCell(List<Pos> cell)
    {
        for (int i = 0; i < 10; i++)
        {
            int j = Random.Range(0, canActiveCell.Count);
            int k = Random.Range(0, canActiveCell.Count);
            SwapCell(cell, k, j);
        }
    }
    /// <summary>
    /// 셀을 섞는 메서드
    /// </summary>
    /// <param name="cell">섞을셀이 담겨있는 리스트</param>
    /// <param name="i">셀의 x좌표</param>
    /// <param name="j">셀의 z좌표</param>
    private void SwapCell(List<Pos> cell, int i, int j)
    {
        Pos temp = cell[i];
        cell[i] = cell[j];
        cell[j] = temp;
    }

    /// <summary>
    /// 클릭한 셀을 활성화하는 메서드
    /// </summary>
    /// <param name="x">셀의 x값</param>
    /// <param name="z">셀의 z값</param>
    public void GetActiveCell(int x, int z)
    {
        gameGrid[x, z].transform.GetChild(0).gameObject.SetActive(false);
        gameGrid[x, z].transform.GetChild(1).gameObject.SetActive(true);
        checkGrid[x, z] = 0;
    }
    /// <summary>
    /// 랜덤으로 뽑은 5개를 리셋하는 메서드
    /// </summary>
    public void ResetAddCell()
    {
        for (int j = 0; j < 5; j++)
        {
            int x = canActiveCell[j].x;
            int z = canActiveCell[j].z;
            gameGrid[x, z].transform.GetChild(0).gameObject.SetActive(true);
        }
        canActiveCell.Clear();
    }
    /// <summary>
    /// 클릭이 됬을경우 실행되는 메서드
    /// </summary>
    public void Click()
    {
        OnCellClick?.Invoke();
    }

    /// <summary>
    /// 활성화된 자린지 아닌지 확인하는 메서드
    /// </summary>
    /// <param name="x">셀의 x좌표</param>
    /// <param name="z">셀의 z좌표</param>
    /// <returns></returns>
    public bool CheckItemActive(int x, int z)
    {
        if (checkGrid[x, z] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 셀의 색상값을 변경하는 메서드
    /// </summary>
    /// <param name="pos">위치값을 저장하는 Struct</param>
    /// <param name="level">색상을 결정하는 변수값</param>
    public void SetItem(List<STPos> pos, int level)
    {
        if (pos.Count > 1)
        {
            for (int i = 0; i < pos.Count; i++)
            {
                gameGrid[pos[i].x, pos[i].z].transform.GetChild(1).GetComponent<CellColor>().ChangeCellColor(level);
                checkGrid[pos[i].x, pos[i].z] = 1;
            }
        }
        else
        {
            gameGrid[pos[0].x, pos[0].z].transform.GetChild(1).GetComponent<CellColor>().ChangeCellColor_Single(level);
            checkGrid[pos[0].x, pos[0].z] = 1;
        }
    }

    /// <summary>
    /// 아이템의 셀색상값을 리셋하는 메서드
    /// </summary>
    /// <param name="pos">리셋해야 하는 셀들의 좌표값</param>
    public void ResetItem(List<STPos> pos)
    {
        for (int i = 0; i < pos.Count; i++)
        {
            gameGrid[pos[i].x, pos[i].z].transform.GetChild(1).GetComponent<CellColor>().ResetColor();
            checkGrid[pos[i].x, pos[i].z] = 0;
        }
    }

    /// <summary>
    /// 셀의 하이라트를 적용하는 메서드
    /// </summary>
    /// <param name="pos">적용해야 할 셀의 좌표값</param>
    /// <param name="active">활성화 여부</param>
    public void HighlightCell(List<STPos> pos, bool active)
    {
        if (active == true)
        {
            for (int i = 0; i < pos.Count; i++)
            {
                gameGrid[pos[i].x, pos[i].z].transform.GetChild(1).GetComponent<CellColor>().Highlight();
            }
        }
        else
        {
            for (int i = 0; i < pos.Count; i++)
            {
                gameGrid[pos[i].x, pos[i].z].transform.GetChild(1).GetComponent<CellColor>().ResetHighlight();
            }
        }
    }
    /// <summary>
    /// 게임중 총얻은 셀의 갯수를 확인하는 메서드
    /// </summary>
    /// <returns></returns>
    public int TotalCell()
    {
        int CellCount = 0;
        for (int i = 0; i < checkGrid.GetLength(0); i++)
        {
            for (int j = 0; j < checkGrid.GetLength(1); j++)
            {
                if (checkGrid[i, j] != -1)
                {
                    CellCount++;
                }
            }
        }
        return CellCount;
    }
    public void ResetAllCell()
    {
        for (int i = 0; i < gameGrid.GetLength(0); i++)
        {
            for (int j = 0; j<gameGrid.GetLength(1); j++)
            {
                Destroy(gameGrid[i, j]);
            }
        }
        
    }
}

