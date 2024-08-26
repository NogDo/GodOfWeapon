using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateMapManager : MonoBehaviour
{
    #region static ����
    public static CCreateMapManager Instance { get; private set; }
    #endregion

    #region public ����

    #endregion

    #region private ����
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
    /// �ٴ��� �����Ѵ�.
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
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