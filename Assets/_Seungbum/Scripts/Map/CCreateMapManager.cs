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
    /// �� ������ �Ѵ�.
    /// </summary>
    /// <param name="stage">��������</param>
    public void Init(int stage)
    {
        int size = CStageManager.Instance.StageCount + 10;

        int width = Random.Range(size / 2 - 2, size - 2);
        int height = size - width;
        Debug.Log($"width : {width}, height : {height}");

        int minX = (width % 2 == 0) ? width / 2 - 1 : width / 2;
        int maxX = width - minX - 1;
        int minZ = (height % 2 == 0) ? height / 2 - 1 : height / 2;
        int maxZ = height - minZ - 1;
        Debug.Log($"minX : {-minX}, maxX : {maxX}, minZ : {-minZ}, maxZ : {maxZ}");

        SetMapSize(-minX, maxX, -minZ, maxZ);
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

        map.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        isCreateMap = true;
    }
}