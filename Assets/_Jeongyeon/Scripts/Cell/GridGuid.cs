using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGuid : MonoBehaviour
{
    private int height =  13;
    private int width = 15;
    private float cellSize = 0.155f;

    [SerializeField] GameObject gridPrefab;
    private GameObject[,] gameGrid;

    private void Start()
    {
        CreateGrid();
        SetCell();
    }
    private void CreateGrid()
    {
        gameGrid = new GameObject[width, height];
        if (gameGrid == null)
        {
            Debug.Log("그리드가 없습니다.");
            return;
        }
        gameGrid = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                gameGrid[x, z] = Instantiate(gridPrefab,transform);
                gameGrid[x, z].transform.localPosition = new Vector3(x * cellSize, 0, z * cellSize);
                gameGrid[x, z].gameObject.name = $"Grid Space(x:{x.ToString()} z: {z.ToString()})";
            }
        }
    }

    private void SetCell()
    {
        for (int x = 4; x < 11; x++)
        {
            for (int z = 5; z < 8; z++)
            {
                gameGrid[x, z].transform.GetChild(0).gameObject.SetActive(false);
                gameGrid[x, z].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

}

