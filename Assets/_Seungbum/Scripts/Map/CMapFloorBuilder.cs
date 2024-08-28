using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapFloorBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private 변수
    CMapPart mapPart;

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

    float fFloorWidth = 4.0f;
    float fFloorHeight = 4.0f;

    int nBasicFloorPercent = 75;
    int nSpecFloorPercent = 20;
    int nTrapPercent = 5;

    int nMinX;
    int nMaxX;
    int nMinZ;
    int nMaxZ;
    #endregion

    public void CreateMapPart(int minX, int maxX, int minZ, int maxZ)
    {
        nMinX = minX;
        nMaxX = maxX;
        nMinZ = minZ;
        nMaxZ = maxZ;

        GameObject mapPart = new GameObject("FloorMapPart");
        mapPart.AddComponent<CMapPart>();
        mapPart.transform.SetParent(transform);

        this.mapPart = mapPart.GetComponent<CMapPart>();

        BuildFloor();
        BuildDownWall();
        BuildUpWall();
        BuildDeco();
    }

    public CMapPart GetMapPart()
    {
        return mapPart;
    }

    public void BuildFloor()
    {
        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = nMinZ; j < nMaxZ; j++)
            {
                if (i == 0 && j == 0)
                {
                    mapPart.AddPart(oStartFloor, Vector3.zero, Vector3.zero);
                    continue;
                }

                int randFloorType = Random.Range(0, nBasicFloorPercent + nSpecFloorPercent + nTrapPercent);
                int randFloor = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, 0.0f, j * fFloorHeight);

                if (randFloorType < nBasicFloorPercent)
                {
                    randFloor = Random.Range(0, oBasicFloors.Length);

                    mapPart.AddPart(oBasicFloors[randFloor], pos, Vector3.zero);
                }

                else if (randFloorType < nBasicFloorPercent + nSpecFloorPercent)
                {
                    randFloor = Random.Range(0, oSpecFloors.Length);

                    mapPart.AddPart(oSpecFloors[randFloor], pos, Vector3.zero);
                }

                else
                {
                    randFloor = Random.Range(0, oTraps.Length);

                    mapPart.AddPart(oTraps[randFloor], pos, Vector3.zero);
                }
            }
        }
    }

    public void BuildDownWall()
    {

    }

    public void BuildUpWall()
    {

    }

    public void BuildDeco()
    {
        for (int i = nMinX; i < nMaxX; i++)
        {
            int randFence = Random.Range(0, oFence.Length);

            Vector3 pos1 = new Vector3(i * fFloorWidth, 0.0f, nMinZ * fFloorHeight);
            Vector3 pos2 = new Vector3(i * fFloorWidth, 0.0f, nMaxZ * fFloorHeight);

            mapPart.AddPart(oFence[randFence], pos1, Vector3.zero);
            mapPart.AddPart(oFence[randFence], pos2, Vector3.zero);
        }

        for (int i = nMinZ; i < nMaxZ; i++)
        {
            int randFence = Random.Range(0, oFence.Length);

            Vector3 pos1 = new Vector3(nMinX * fFloorWidth, 0.0f, i * fFloorHeight);
            Vector3 pos2 = new Vector3(nMaxX * fFloorWidth, 0.0f, i * fFloorHeight);

            Vector3 rot = new Vector3(0.0f, -90.0f, 0.0f);

            mapPart.AddPart(oFence[randFence], pos1, rot);
            mapPart.AddPart(oFence[randFence], pos2, rot);
        }

        mapPart.AddPart(oFenceEdge, new Vector3(nMinX * fFloorWidth, 0.0f, nMinZ * fFloorHeight), Vector3.zero);
        mapPart.AddPart(oFenceEdge, new Vector3(nMaxX * fFloorWidth, 0.0f, nMinZ * fFloorHeight), Vector3.zero);
        mapPart.AddPart(oFenceEdge, new Vector3(nMinX * fFloorWidth, 0.0f, nMaxZ * fFloorHeight), Vector3.zero);
        mapPart.AddPart(oFenceEdge, new Vector3(nMaxX * fFloorWidth, 0.0f, nMaxZ * fFloorHeight), Vector3.zero);
    }
}