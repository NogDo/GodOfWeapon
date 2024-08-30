using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateMapManager : MonoBehaviour
{
    #region static ����
    public static CCreateMapManager Instance { get; private set; }
    #endregion

    #region private ����
    CMap map;

    CMapFloorBuilder floorBuilder;

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
    /// ���� �����Ѵ�.
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
    public void CreateMap(int minX, int maxX, int minZ, int maxZ)
    {
        map = GameObject.Find("Map").GetComponent<CMap>();

        map.SetFloorPart(minX, maxX, minZ, maxZ);

        map.transform.Rotate(new Vector3(0.0f, 45.0f, 0.0f));

        isCreateMap = true;
    }
}