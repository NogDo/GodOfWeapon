using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateMapManager : MonoBehaviour
{
    public struct STMapSize
    {
        public int minX;
        public int maxX;
        public int minZ;
        public int maxZ;

        public STMapSize(int minX, int maxX, int minZ, int maxZ)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minZ = minZ;
            this.maxZ = maxZ;
        }
    }

    #region static ����
    public static CCreateMapManager Instance { get; private set; }
    #endregion

    #region private ����
    CMap map;

    STMapSize mapSize;

    bool isCreateMap = false;
    int nStartWidth;
    int nStartHeight;
    #endregion

    /// <summary>
    /// ���� �ϼ� �ƴ���.
    /// </summary>
    public bool IsCreateMap
    {
        get
        {
            return isCreateMap;
        }
    }

    /// <summary>
    /// �� ũ��
    /// </summary>
    public STMapSize MapSize

    {
        get
        {
            return mapSize;
        }
    }

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// �ʱ� ũ�⸦ ���� �� ���� �����Ѵ�.
    /// </summary>
    /// <param name="stage">��������</param>
    public void Init()
    {
        transform.position = Vector3.zero;

        nStartWidth = Random.Range(5, 11);
        nStartHeight = 15 - nStartWidth;

        int minX = (nStartWidth % 2 == 0) ? nStartWidth / 2 - 1 : nStartWidth / 2;
        int maxX = nStartWidth - minX - 1;
        int minZ = (nStartHeight % 2 == 0) ? nStartHeight / 2 - 1 : nStartHeight / 2;
        int maxZ = nStartHeight - minZ - 1;

        SetMapSize(-minX, maxX, -minZ, maxZ);
        CreateMap();
    }

    /// <summary>
    /// �ʿ� ������ �Ǵ� �������� �߰��� �� ���� �����Ѵ�.
    /// </summary>
    public void AddLine()
    {
        // �ִ� ������ 10 x 10
        if (nStartWidth >= 10 && nStartHeight >= 10)
        {
            SetMapSize(mapSize.minX, mapSize.maxX, mapSize.minZ, mapSize.maxZ);
        }

        else
        {
            int percent = Random.Range(0, 2);

            if (percent == 0)
            {
                if (nStartWidth + 1 >= 11)
                {
                    nStartHeight++;
                    SetMapSize(mapSize.minX, mapSize.maxX, mapSize.minZ, mapSize.maxZ + 1);
                }

                else
                {
                    nStartWidth++;
                    SetMapSize(mapSize.minX, mapSize.maxX + 1, mapSize.minZ, mapSize.maxZ);
                }
            }

            else
            {
                if (nStartHeight + 1 >= 11)
                {
                    nStartWidth++;
                    SetMapSize(mapSize.minX, mapSize.maxX + 1, mapSize.minZ, mapSize.maxZ);
                }

                else
                {
                    nStartHeight++;
                    SetMapSize(mapSize.minX, mapSize.maxX, mapSize.minZ, mapSize.maxZ + 1);
                }
            }
        }

        CreateMap();
    }

    /// <summary>
    /// �� ũ�⸦ �����Ѵ�.
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
    public void SetMapSize(int minX, int maxX, int minZ, int maxZ)
    {
        mapSize = new STMapSize(minX, maxX, minZ, maxZ);
    }

    /// <summary>
    /// ���� �����Ѵ�.
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
    public void CreateMap()
    {
        map = GameObject.Find("Map").GetComponent<CMap>();

        map.SetFloorPart(mapSize.minX, mapSize.maxX, mapSize.minZ, mapSize.maxZ);
        map.SetLeftUpPart(mapSize.minX - 4, mapSize.minX - 1, mapSize.minZ - 3, mapSize.maxZ + 4);
        map.SetLeftDownPart(mapSize.minX, mapSize.maxX, mapSize.minZ - 4, mapSize.minZ);
        map.SetRighUpPart(mapSize.minX, mapSize.maxX, mapSize.maxZ, mapSize.maxZ + 3);
        map.SetRighDownPart(mapSize.maxX, mapSize.maxX + 4, mapSize.minZ - 2, mapSize.maxZ + 3);

        map.transform.position = new Vector3(0.0f, -0.2f, 0.0f);

        isCreateMap = true;
    }

    /// <summary>
    /// �����ߴ� ���� �ı��Ѵ�.
    /// </summary>
    public void DestroyMap()
    {
        map.DestoryMapPart();
    }
}