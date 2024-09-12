using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct STPos
{
    public int x;
    public int z;

    public STPos(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}

public class CItemDrag : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    Transform[] gridPoints;
    Camera cameraShop;

    List<STPos> pos = new List<STPos>();

    [SerializeField]
    int nMaxCount;

    bool isCanDrop = false;
    #endregion

    void Awake()
    {
        cameraShop = GameObject.Find("ShopCamera").GetComponent<Camera>();
    }

    void OnMouseDown()
    {
        transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    }

    void OnMouseDrag()
    {
        float distance = cameraShop.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = cameraShop.ScreenToWorldPoint(mousePos);

        objPos.y = 0.5f;

        transform.position = objPos;

        int activeCellCount = 0;

        for (int i = 0; i < gridPoints.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(gridPoints[i].position, Vector3.down, out hit, float.MaxValue, LayerMask.GetMask("Cell")))
            {
                Debug.DrawRay(gridPoints[i].position, Vector3.down * hit.distance, Color.red);

                if (hit.transform.parent.TryGetComponent<CellInfo>(out CellInfo cellinfo))
                {
                    STPos cellPos = new STPos(cellinfo.x, cellinfo.z);

                    // TODO : cellPos를 CellManager에 메서드로 보내고 해당 좌표 cell이 0인지 1인지를 가져오는 로직
                    if (CellManager.Instance.CheckItemActive(cellPos.x, cellPos.z))
                    {
                        activeCellCount++;
                    }
                }
            }
        }

        if (activeCellCount == nMaxCount)
        {
            isCanDrop = true;
        }

        else
        {
            isCanDrop = false;
        }
    }

    void OnMouseUp()
    {
        transform.rotation = Quaternion.Euler(-45.0f, 0.0f, 0.0f);

        Debug.Log(isCanDrop);
    }
}
