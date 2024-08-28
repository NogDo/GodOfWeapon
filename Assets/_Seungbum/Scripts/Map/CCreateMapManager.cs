using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateMapManager : MonoBehaviour
{
    #region static 변수
    public static CCreateMapManager Instance { get; private set; }
    #endregion

    #region private 변수
    [Header("바닥 관련")]
    [SerializeField]
    GameObject oStartFloor;
    [SerializeField]
    GameObject[] oBasicFloors;
    [SerializeField]
    GameObject[] oSpecFloors;
    [SerializeField]
    GameObject[] oTraps;

    [Header("울타리 관련")]
    [SerializeField]
    GameObject[] oFence;
    [SerializeField]
    GameObject oFenceEdge;



    Transform tfMapFloor;
    Transform tfMapLeftUp;
    Transform tfMapLeftDown;
    Transform tfMapRightUp;
    Transform tfMapRightDown;

    float fFloorWidth = 4.0f;
    float fFloorHeight = 4.0f;

    int nBasicFloorPercent = 75;
    int nSpecFloorPercent = 20;
    int nTrapPercent = 5;
    #endregion

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CreateMap(-1, 5, -1, 10);
    }

    /// <summary>
    /// 맵을 생성한다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void CreateMap(int minX, int maxX, int minZ, int maxZ)
    {
        GameObject map = GameObject.Find("Map");

        tfMapFloor = map.transform.GetChild(0);
        tfMapLeftUp = map.transform.GetChild(1);
        tfMapLeftDown = map.transform.GetChild(2);
        tfMapRightUp = map.transform.GetChild(3);
        tfMapRightDown = map.transform.GetChild(4);

        for (int i = minX; i < maxX; i++)
        {
            for (int j = minZ; j < maxZ; j++)
            {
                CreateFloor(i, j);
            }
        }

        CreateFence(minX, maxX, minZ, maxZ);

        tfMapFloor.transform.Rotate(new Vector3(0.0f, 45.0f, 0.0f));
    }

    /// <summary>
    /// 바닥을 생성한다.
    /// </summary>
    /// <param name="posX">X 위치</param>
    /// <param name="posZ">Z 위치</param>
    void CreateFloor(int posX, int posZ)
    {
        if (posX == 0 && posZ == 0)
        {
            Instantiate(oStartFloor, Vector3.zero, Quaternion.identity, tfMapFloor.transform);

            return;
        }

        int randFloorType = Random.Range(0, 100);
        int randFloor = 0;

        if (randFloorType < nBasicFloorPercent)
        {
            randFloor = Random.Range(0, oBasicFloors.Length);

            Instantiate
                (
                    oBasicFloors[randFloor],
                    new Vector3(posX * fFloorWidth, 0.0f, posZ * fFloorHeight),
                    Quaternion.identity,
                    tfMapFloor.transform
                );
        }

        else if (randFloorType < nBasicFloorPercent + nSpecFloorPercent)
        {
            randFloor = Random.Range(0, oSpecFloors.Length);

            Instantiate
                (
                    oSpecFloors[randFloor],
                    new Vector3(posX * fFloorWidth, 0.0f, posZ * fFloorHeight),
                    Quaternion.identity,
                    tfMapFloor.transform
                );
        }

        else
        {
            randFloor = Random.Range(0, oTraps.Length);

            Instantiate
                (
                    oTraps[randFloor],
                    new Vector3(posX * fFloorWidth, 0.0f, posZ * fFloorHeight),
                    Quaternion.identity,
                    tfMapFloor.transform
                );
        }
    }

    /// <summary>
    /// 울타리를 생성한다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    void CreateFence(int minX, int maxX, int minZ, int maxZ)
    {
        for (int i = minX; i < maxX; i++)
        {
            int randFence = Random.Range(0, oFence.Length);

            Instantiate
                (
                    oFence[randFence],
                    new Vector3(i * fFloorWidth, 0.0f, minZ * fFloorHeight),
                    Quaternion.identity,
                    tfMapFloor.transform
                );

            Instantiate
                (
                    oFence[randFence],
                    new Vector3(i * fFloorWidth, 0.0f, maxZ * fFloorHeight),
                    Quaternion.identity,
                    tfMapFloor.transform
                );
        }

        for (int i = minZ; i < maxZ; i++)
        {
            int randFence = Random.Range(0, oFence.Length);

            Instantiate
                (
                    oFence[randFence],
                    new Vector3(minX * fFloorWidth, 0.0f, i * fFloorHeight),
                    Quaternion.Euler(0.0f, -90.0f, 0.0f),
                    tfMapFloor.transform
                );

            Instantiate
                (
                    oFence[randFence],
                    new Vector3(maxX * fFloorWidth, 0.0f, i * fFloorHeight),
                    Quaternion.Euler(0.0f, -90.0f, 0.0f),
                    tfMapFloor.transform
                );
        }

        Instantiate
               (
                   oFenceEdge,
                   new Vector3(minX * fFloorWidth, 0.0f, minZ * fFloorHeight),
                   Quaternion.identity,
                   tfMapFloor.transform
               );

        Instantiate
               (
                   oFenceEdge,
                   new Vector3(maxX * fFloorWidth, 0.0f, minZ * fFloorHeight),
                   Quaternion.identity,
                   tfMapFloor.transform
               );

        Instantiate
               (
                   oFenceEdge,
                   new Vector3(minX * fFloorWidth, 0.0f, maxZ * fFloorHeight),
                   Quaternion.identity,
                   tfMapFloor.transform
               );

        Instantiate
               (
                   oFenceEdge,
                   new Vector3(maxX * fFloorWidth, 0.0f, maxZ * fFloorHeight),
                   Quaternion.identity,
                   tfMapFloor.transform
               );
    }


    void CreateLeftUpWall()
    {

    }
}