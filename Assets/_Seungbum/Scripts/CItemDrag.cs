using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct STPos
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
    #region public ����
    public Camera cameraShop;
    #endregion

    #region private ����
    [SerializeField]
    Transform[] gridPoints;

    List<STPos> pos = new List<STPos>();

    [SerializeField]
    int nMaxCount;
    #endregion

    void Awake()
    {
        
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

                    // TODO : cellPos�� CellManager�� �޼���� ������ �ش� ��ǥ cell�� 0���� 1������ �������� ����
                    //CellManager.Instance.
                }
            }
        }
    }

    void OnMouseUp()
    {
        transform.rotation = Quaternion.Euler(-45.0f, 0.0f, 0.0f);
    }
}
