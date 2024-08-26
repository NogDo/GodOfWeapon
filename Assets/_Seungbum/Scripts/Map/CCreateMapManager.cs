using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateMapManager : MonoBehaviour
{
    #region static 변수
    public static CCreateMapManager Instance { get; private set; }
    #endregion

    #region public 변수

    #endregion

    #region private 변수
    [SerializeField]
    GameObject oElevator;
    [SerializeField]
    GameObject oSpikeFloor;
    [SerializeField]
    GameObject oSpike;
    [SerializeField]
    GameObject[] oFloors;

    GameObject tfMapParent;

    float fFloorWidth = 4.0f;
    float fFloorHeight = 4.0f;
    #endregion

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CreateFloor(-1, 3, -1, 3);
    }

    /// <summary>
    /// 바닥을 생성한다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void CreateFloor(int minX, int maxX, int minZ, int maxZ)
    {
        tfMapParent = GameObject.Find("Map");

        for (int i = minX; i < maxX; i++)
        {
            for (int j = minZ; j < maxZ; j++)
            {
                if (i == 0 && j == 0)
                {
                    Instantiate(oElevator, Vector3.zero, Quaternion.identity, tfMapParent.transform);

                    continue;
                }

                int randFloor = Random.Range(0, oFloors.Length);
                Instantiate(oFloors[randFloor], new Vector3(i * fFloorWidth, 0.0f, j * fFloorHeight), Quaternion.identity, tfMapParent.transform);
            }
        }

        tfMapParent.transform.Rotate(new Vector3(0.0f, 45.0f, 0.0f));
    }
}