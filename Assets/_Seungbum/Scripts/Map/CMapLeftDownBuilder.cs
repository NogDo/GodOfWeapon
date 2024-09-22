using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapLeftDownBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private ����
    CMapPart mapPart;

    [Header("�ٴ� ����")]
    [SerializeField]
    GameObject[] oBasicFloors;
    [SerializeField]
    GameObject[] oSpecFloors;

    [Header("��Ÿ�� ����")]
    [SerializeField]
    GameObject[] oBasicFences;
    [SerializeField]
    GameObject[] oThornFences;

    [Header("�� ����")]
    [SerializeField]
    GameObject oWallProps;

    // ����, ����, ���� ũ��
    float fFloorWidth = 4.0f;
    float fFloorLength = 4.0f;
    float fFloorHeight = 4.0f;

    // �ٴ� ���� Ȯ��
    int nBasicFloorPercent = 75;
    int nSpecFloorPercent = 25;

    // �� ���� Ȯ��
    int nBasicWallPercent = 80;
    int nSpecWallPercent = 20;

    // �� �� ��ǥ��
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

        GameObject mapPart = new GameObject("LeftUpMapPart");
        mapPart.AddComponent<CMapPart>();
        mapPart.transform.SetParent(transform);

        this.mapPart = mapPart.GetComponent<CMapPart>();

        BuildFloor();
        BuildDownWall();
        BuildUpWall();
    }

    public CMapPart GetMapPart()
    {
        return mapPart;
    }

    public void BuildFloor()
    {
        GameObject floor = new GameObject("Floor");
        floor.transform.SetParent(mapPart.transform);

        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = nMinZ; j < nMaxZ; j++)
            {
                int randFloorType = Random.Range(0, nBasicFloorPercent + nSpecFloorPercent);
                int randFloor = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, -3 * fFloorHeight, j * fFloorLength);

                // �⺻ �ٴ� ����
                if (randFloorType < nBasicFloorPercent)
                {
                    randFloor = Random.Range(0, oBasicFloors.Length);

                    mapPart.AddPart(oBasicFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
                // Ư�� �ٴ� ����
                else
                {
                    randFloor = Random.Range(0, oSpecFloors.Length);

                    mapPart.AddPart(oSpecFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
            }
        }
    }

    public void BuildDownWall()
    {

    }

    public void BuildUpWall()
    {
        GameObject upWall = new GameObject("UpWall");
        upWall.transform.SetParent(mapPart.transform);

        // ��ġ�� ��Ÿ�� ����
        int fenceType = Random.Range(0, 2);

        // ��Ÿ�� ����
        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            int randFence = 0;

            Vector3 pos = new Vector3(nMinX * fFloorWidth, -3 * fFloorHeight, i * fFloorLength);
            Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

            // ��Ÿ�� ������ ���� ����
            switch (fenceType)
            {
                case 0:
                    randFence = Random.Range(0, oBasicFences.Length);

                    mapPart.AddPart(oBasicFences[randFence], pos, rot, upWall.transform);
                    break;

                case 1:
                    randFence = Random.Range(0, oThornFences.Length);

                    mapPart.AddPart(oThornFences[randFence], pos, rot, upWall.transform);
                    break;
            }
        }

        // �� ���� ����
        for (int i = nMinX + 1; i <= nMaxX; i++)
        {
            int deco = Random.Range(0, nBasicWallPercent + nSpecWallPercent);

            if (deco >= nBasicWallPercent)
            {
                Vector3 pos = new Vector3(i * fFloorWidth, -3 * fFloorHeight, nMaxZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                mapPart.AddPart(oWallProps, pos, rot, upWall.transform);
            }
        }
    }

    public void DestroyMapPart()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
    }
}
