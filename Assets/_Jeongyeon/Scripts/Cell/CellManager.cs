using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CellManager : MonoBehaviour
{
    #region Private Fields
    // �� ũ��� ���� ���� ����
    private int height = 12;
    private int width = 16;
    private float cellSize = 0.15f;


    //�� ������ �� �κ��丮�� �ִ� ���� ����
    [SerializeField] GameObject gridPrefab;
    private GameObject[,] gameGrid;
    private int[,] checkGrid;
    private List<Pos> canActiveCell;
    #endregion

    #region Public Fields
    //���� Ȱ��ȭ ���θ� Ȯ���ϴ� ��������Ʈ
    public event Action OnCellClick;
    #endregion

    public static CellManager Instance { get; private set; }

    /// <summary>
    /// Ȱ��ȭ�� ���� ��ǥ���� �����ϴ� ����ü
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
        checkGrid = new int[width, height];
        CreateGrid();
        SetCell();
        canActiveCell = new List<Pos>();
    }

    /// <summary>
    /// �κ��丮�� �� �׸��带 �����ϴ� �޼���
    /// </summary>
    private void CreateGrid()
    {
        gameGrid = new GameObject[width, height];
        if (gameGrid == null)
        {
            Debug.Log("�׸��尡 �����ϴ�.");
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
    }

    /// <summary>
    /// ���۽� Ȱ��ȭ�� ���� �����ϴ� �޼���
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
    /// ���������� ����ǰ� ������ ��ŭ�� ���� ���� �޼���
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
    /// Ȱ��ȭ �� ���� �� �Ʒ� �� �츦 Ȯ���� Ȱ��ȭ ���� ���� ����Ʈ�� �ִ� �޼���
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
    /// Ȱ�� ���ɼ��� �޾ƿ� ���� ���� �޼���
    /// </summary>
    /// <param name="cell">Ȱ��ȭ ���ɼ��� ��Ƶ� ����Ʈ</param>
    private void ShuffleCell(List<Pos> cell)
    {
        for (int i =0; i < 10; i++)
        {
            int j = Random.Range(0, canActiveCell.Count);
            int k = Random.Range(0, canActiveCell.Count);
            SwapCell(cell, k, j);
        }
    }
    /// <summary>
    /// ���� ���� �޼���
    /// </summary>
    /// <param name="cell">�������� ����ִ� ����Ʈ</param>
    /// <param name="i">���� x��ǥ</param>
    /// <param name="j">���� z��ǥ</param>
    private void SwapCell(List<Pos> cell, int i, int j)
    {
        Pos temp = cell[i];
        cell[i] = cell[j];
        cell[j] = temp;
    }

    public void Click()
    {
        OnCellClick?.Invoke();
    }
   
    public void GetActiveCell(int x, int z)
    {
        gameGrid[x,z].transform.GetChild(0).gameObject.SetActive(false);
        gameGrid[x, z].transform.GetChild(1).gameObject.SetActive(true);
        checkGrid[x, z] = 0;
    }
}

