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
    GameObject oStartFloor;
    [SerializeField]
    GameObject[] oBasicFloors;
    [SerializeField]
    GameObject[] oSpecFloors;
    [SerializeField]
    GameObject[] oTraps;

    GameObject tfMapParent;

    float fFloorWidth = 4.0f;
    float fFloorHeight = 4.0f;

    int nBasicFloorPercent = 70;
    int nSpecFloorPercent = 20;
    int nTrapPercent = 10;
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
                    Instantiate(oStartFloor, Vector3.zero, Quaternion.identity, tfMapParent.transform);

                    continue;
                }

                int randFloorType = Random.Range(0, 100);
                int randFloor = 0;

                if (randFloorType < nBasicFloorPercent)
                {
                    randFloor = Random.Range(0, oBasicFloors.Length);

                    Instantiate
                        (
                            oBasicFloors[randFloor], 
                            new Vector3(i * fFloorWidth, 0.0f, j * fFloorHeight), 
                            Quaternion.identity, 
                            tfMapParent.transform
                        );
                }

                else if (randFloorType < nBasicFloorPercent + nSpecFloorPercent)
                {
                    randFloor = Random.Range(0, oSpecFloors.Length);

                    Instantiate
                        (
                            oSpecFloors[randFloor],
                            new Vector3(i * fFloorWidth, 0.0f, j * fFloorHeight),
                            Quaternion.identity,
                            tfMapParent.transform
                        );
                }

                else
                {
                    randFloor = Random.Range(0, oTraps.Length);

                    Instantiate
                        (
                            oTraps[randFloor],
                            new Vector3(i * fFloorWidth, 0.0f, j * fFloorHeight),
                            Quaternion.identity,
                            tfMapParent.transform
                        );
                }

                
            }
        }

        tfMapParent.transform.Rotate(new Vector3(0.0f, 45.0f, 0.0f));
    }
}