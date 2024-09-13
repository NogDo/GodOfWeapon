using System.Linq;
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

    Transform tfCell;
    Transform tfmouse;
    List<STPos> prevCellPos = new List<STPos>();
    List<STPos> cellPos = new List<STPos>();

    Vector3 v3StartPosition;
    Vector3 v3StartRotation;

    bool isCanDrop = false;
    bool isInInventory = false;
    int nRotateCount;
    #endregion

    void Awake()
    {
        tfmouse = FindObjectOfType<CMouseFollower>().transform;

        transform.position += Vector3.left * GetComponent<BoxCollider>().size.x / 10.0f;

        v3StartRotation = new Vector3(-30.0f, 0.0f, 0.0f);
        v3StartPosition = transform.position;

        nRotateCount = 0;
    }

    void OnMouseDown()
    {
        if (!isInInventory)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        nRotateCount = (int)(transform.rotation.eulerAngles.y / 90);

        Vector3 position = transform.position;
        position.y = 0.5f;
        transform.position = position;

        transform.SetParent(tfmouse);

        // 원래 있던 칸 없애기
        if (prevCellPos.Count > 0)
        {
            CellManager.Instance.ResetItem(prevCellPos);
        }
    }

    void OnMouseDrag()
    {
        // 인벤토리 cell에 들어갈 수 있는지 판단
        int activeCellCount = 0;
        cellPos.Clear();

        for (int i = 0; i < gridPoints.Length; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(gridPoints[i].position, Vector3.down, out hit, float.MaxValue, LayerMask.GetMask("Cell")))
            {
                Debug.DrawRay(gridPoints[i].position, Vector3.down * hit.distance, Color.red);

                if (hit.transform.parent.TryGetComponent<CellInfo>(out CellInfo cellinfo))
                {
                    STPos cellPos = new STPos(cellinfo.x, cellinfo.z);
                    this.cellPos.Add(cellPos);

                    if (CellManager.Instance.CheckItemActive(cellPos.x, cellPos.z))
                    {
                        activeCellCount++;
                    }

                    if (i == 0)
                    {
                        tfCell = hit.transform;
                    }
                }
            }
        }

        if (activeCellCount == gridPoints.Length)
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
        transform.SetParent(null);

        if (isCanDrop)
        {
            Vector3 pos = tfCell.position;

            switch (nRotateCount)
            {
                case 0:
                    pos.x += 0.0f;
                    pos.z += 0.0f;
                    v3StartRotation = new Vector3(0.0f, 0.0f, 0.0f);
                    break;

                case 1:
                    pos.x += 0.0f;
                    pos.z += 0.15f;
                    v3StartRotation = new Vector3(0.0f, 90.0f, 0.0f);
                    break;

                case 2:
                    pos.x += 0.15f;
                    pos.z += 0.15f;
                    v3StartRotation = new Vector3(0.0f, 180.0f, 0.0f);
                    break;

                case 3:
                    pos.x += 0.15f;
                    pos.z += 0.0f;
                    v3StartRotation = new Vector3(0.0f, 270.0f, 0.0f);
                    break;
            }

            transform.position = pos;
            v3StartPosition = transform.position;

            CellManager.Instance.SetItem(cellPos, 2);

            prevCellPos = cellPos.ToList();

            isInInventory = true;
        }

        else
        {
            if (isInInventory)
            {
                transform.rotation = Quaternion.Euler(v3StartRotation);
                transform.position = v3StartPosition;

                CellManager.Instance.SetItem(prevCellPos, 2);
            }

            else
            {
                transform.rotation = Quaternion.Euler(v3StartRotation);
                transform.position = v3StartPosition;
            }
        }
    }

    /// <summary>
    /// 회전 카운트 수를 증가시킨다.
    /// </summary>
    public void IncreaseRotationCount()
    {
        nRotateCount = (nRotateCount + 1 < 4) ? nRotateCount + 1 : 0;
    }
}