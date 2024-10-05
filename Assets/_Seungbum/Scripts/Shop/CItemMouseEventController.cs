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

public class CItemMouseEventController : MonoBehaviour
{
    #region private ����
    [SerializeField]
    Transform[] gridPoints;
    [SerializeField]
    CItemPreviewContoller itemPreview;

    CItemStats itemStats;
    CWeaponStats weaponStats;
    Transform tfCell;
    Transform tfmouse;
    List<STPos> prevCellPos = new List<STPos>();
    List<STPos> cellPos = new List<STPos>();

    Vector3 v3StartPosition;
    Vector3 v3StartRotation;

    bool isCanDrop = false;
    bool isInInventory = false;
    bool isGrab = false;
    int nRotateCount;
    int nIndex = -1;
    #endregion

    /// <summary>
    /// ������ �߾� ������ ��
    /// </summary>
    public Vector3 MiddlePos
    {
        get
        {
            return transform.GetChild(0).position;
        }
    }

    void Awake()
    {
        itemStats = GetComponent<CItemStats>();
        weaponStats = GetComponent<CWeaponStats>();
    }

    void Start()
    {
        tfmouse = transform.root.GetComponentInChildren<CMouseFollower>().transform;

        transform.position += Vector3.left * GetComponent<BoxCollider>().size.x / 10.0f;

        v3StartRotation = new Vector3(-30.0f, 0.0f, -30.0f);
        v3StartPosition = transform.position;

        nRotateCount = 0;
    }

    void OnMouseDown()
    {
        if (UIManager.Instance.canCombine || CStageManager.Instance.IsAddCell)
        {

        }

        else
        {
            Grab();
        }
    }

    void OnMouseDrag()
    {
        if (UIManager.Instance.canCombine || CStageManager.Instance.IsAddCell)
        {

        }

        else
        {
            CheckGrid();
            Preview();
        }
    }

    void OnMouseUp()
    {
        if (UIManager.Instance.canCombine)
        {
            if (UIManager.Instance.sourceWeapon.Count < 2)
            {
                ClickToCombine(true);
            }
        }

        else
        {
            ReleaseGrab();
        }
    }

    void OnMouseEnter()
    {
        if (UIManager.Instance.canCombine || CStageManager.Instance.IsAddCell)
        {

        }

        else
        {
            if (!isGrab && tfmouse.childCount == 0)
            {
                ActiveUIPanel(true);
            }
        }
    }

    void OnMouseExit()
    {
        if (UIManager.Instance.canCombine || CStageManager.Instance.IsAddCell)
        {

        }

        else
        {
            ActiveUIPanel(false);
        }
    }

    void OnMouseOver()
    {
        if (CStageManager.Instance.IsAddCell)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!UIManager.Instance.ExtraUIOpen)
            {

                if (!isGrab && !isInInventory)
                {
                    CShopManager.Instance.LockItem(nIndex, transform);
                }

                else if (!isGrab && isInInventory)
                {
                    LockExtraUIPanel();
                    UIManager.Instance.SetActiveExtraUI(true);
                }
            }
        }
    }

    /// <summary>
    /// �������� ���콺�� ���´�.
    /// </summary>
    void Grab()
    {
        if (!isInInventory)
        {
            int price = (itemStats == null) ? weaponStats.Weapon.price : itemStats.Item.price;

            // ������ ���� ���ݺ��� ���ٸ� �׷��� �� �� ����
            if (CStageManager.Instance.Money < price)
            {
                return;
            }

            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        nRotateCount = (int)(transform.rotation.eulerAngles.y / 90);

        Vector3 position = transform.position;
        position.y = 0.5f;
        transform.position = position;

        transform.SetParent(tfmouse);

        // ���� �ִ� ĭ ���ֱ�
        if (prevCellPos.Count > 0)
        {
            CellManager.Instance.ResetItem(prevCellPos);
        }

        // ������ִ� UI ����
        ActiveUIPanel(false);

        isGrab = true;
    }

    /// <summary>
    /// �κ��丮 Cell�� �������� �� �� �ִ��� �Ǵ�
    /// </summary>
    void CheckGrid()
    {
        int activeCellCount = 0;
        tfCell = null;
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

        // �׸����� ������ Cell�� �浹�� Ray�� ������ ���ٸ� ���� �� �ִٰ� �Ǵ�.
        if (activeCellCount == gridPoints.Length)
        {
            isCanDrop = true;
        }

        else
        {
            isCanDrop = false;
        }
    }

    /// <summary>
    /// �������� ���� ���� �̸� �����ش�.
    /// </summary>
    void Preview()
    {
        // ù��° �׸����� Ray�� Cell�� �浹���� ���, ���� ���� �������� �̸� �����ش�.
        if (tfCell != null)
        {
            Vector3 pos = tfCell.position;

            switch (nRotateCount)
            {
                case 0:
                    pos.x += 0.0f;
                    pos.z += 0.0f;
                    break;

                case 1:
                    pos.x += 0.0f;
                    pos.z += 0.15f;
                    break;

                case 2:
                    pos.x += 0.15f;
                    pos.z += 0.15f;
                    break;

                case 3:
                    pos.x += 0.15f;
                    pos.z += 0.0f;
                    break;
            }

            itemPreview.SetActive(true);
            itemPreview.SetPreview(isCanDrop, pos);
        }

        else
        {
            itemPreview.SetActive(false);
        }
    }

    /// <summary>
    /// ������ Grab�� �����Ѵ�.
    /// </summary>
    void ReleaseGrab()
    {
        // �κ��丮�� �� �� �ִ� ����
        if (isCanDrop)
        {
            Vector3 pos = tfCell.position;

            EquipItem(pos, nRotateCount);
        }
        // �κ��丮�� �� �� ���� ����
        else
        {
            if (isInInventory)
            {
                transform.rotation = Quaternion.Euler(v3StartRotation);
                transform.position = v3StartPosition;

                if (itemStats != null)
                {
                    CellManager.Instance.SetItem(prevCellPos, itemStats.Item.level);
                }

                else if (weaponStats != null)
                {
                    CellManager.Instance.SetItem(prevCellPos, weaponStats.Weapon.level);
                }
            }

            else
            {
                transform.rotation = Quaternion.Euler(v3StartRotation);
                transform.position = v3StartPosition;
            }
        }

        if (isInInventory)
        {
            transform.SetParent(CShopManager.Instance.tfBuyItems);
        }

        else
        {
            transform.SetParent(CShopManager.Instance.tfNonBuyItems);
        }

        isGrab = false;
        itemPreview.SetActive(false);
    }

    /// <summary>
    /// �������� �κ��丮�� �����Ѵ�.
    /// </summary>
    /// <param name="position">�������� ��ġ�� ������ ��</param>
    /// <param name="rotationCount">ȸ����</param>
    public void EquipItem(Vector3 position, int rotationCount, CellInfo cellinfo = null)
    {
        if (cellinfo != null)
        {
            transform.rotation = Quaternion.Euler(0.0f, 90.0f * rotationCount, 0.0f);
        }

        nRotateCount = rotationCount;

        switch (nRotateCount)
        {
            case 0:
                position.x += 0.0f;
                position.z += 0.0f;
                v3StartRotation = new Vector3(0.0f, 0.0f, 0.0f);
                break;

            case 1:
                position.x += 0.0f;
                position.z += 0.15f;
                v3StartRotation = new Vector3(0.0f, 90.0f, 0.0f);
                break;

            case 2:
                position.x += 0.15f;
                position.z += 0.15f;
                v3StartRotation = new Vector3(0.0f, 180.0f, 0.0f);
                break;

            case 3:
                position.x += 0.15f;
                position.z += 0.0f;
                v3StartRotation = new Vector3(0.0f, 270.0f, 0.0f);
                break;
        }

        transform.position = position;
        v3StartPosition = transform.position;

        if (cellPos.Count == 0)
        {
            int x = (nRotateCount == 0) ? (int)GetComponent<BoxCollider>().size.x : (int)GetComponent<BoxCollider>().size.z;
            int z = (nRotateCount == 0) ? (int)GetComponent<BoxCollider>().size.z : (int)GetComponent<BoxCollider>().size.x;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < z; j++)
                {
                    STPos cell = new STPos(cellinfo.x + i, cellinfo.z + j);
                    cellPos.Add(cell);
                }
            }
        }

        // �������� ���
        if (itemStats != null)
        {
            CellManager.Instance.SetItem(cellPos, itemStats.Item.level);

            if (!isInInventory)
            {
                CellManager.Instance.PlayerInventory.GetItem(itemStats.Item);

                if (nIndex != -1)
                {
                    CStageManager.Instance.DecreaseMoney(itemStats.Item.price);
                    CShopManager.Instance.SetInteractableReRellButton();
                    CShopManager.Instance.InActiveShopCostUI(nIndex);
                }
            }
        }
        // ������ ���
        else if (weaponStats != null)
        {
            CellManager.Instance.SetItem(cellPos, weaponStats.Weapon.level);

            if (!isInInventory)
            {
                CellManager.Instance.PlayerInventory.CreateWeapon(weaponStats);

                if (nIndex != -1)
                {
                    CStageManager.Instance.DecreaseMoney(weaponStats.Weapon.price);
                    CShopManager.Instance.SetInteractableReRellButton();
                    CShopManager.Instance.InActiveShopCostUI(nIndex);
                }
            }
        }

        prevCellPos = cellPos.ToList();

        isInInventory = true;
    }

    /// <summary>
    /// �������� �����ϱ� ���� �������� Ŭ������ �� ����� �޼���
    /// </summary>
    public void ClickToCombine(bool isSource)
    {
        StartCoroutine(WaitCombine());

        if (isSource)
        {
            UIManager.Instance.sourceWeapon.Add(weaponStats);
        }
    }

    /// <summary>
    /// ������ ���� �Ǵ� ��� �ɶ� ���� ��ٸ��� Cell�� ���̶���Ʈ�� �����Ѵ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitCombine()
    {
        CellManager.Instance.HighlightCell(prevCellPos, true);

        yield return new WaitUntil(() => !UIManager.Instance.canCombine);

        CellManager.Instance.HighlightCell(prevCellPos, false);
    }

    /// <summary>
    /// ȸ�� ī��Ʈ ���� ������Ų��.
    /// </summary>
    public void IncreaseRotationCount()
    {
        nRotateCount = (nRotateCount + 1 < 4) ? nRotateCount + 1 : 0;
    }

    /// <summary>
    /// �������� �ε����� �����Ѵ�.
    /// </summary>
    /// <param name="index">���� �ε���</param>
    public void SetIndex(int index)
    {
        nIndex = index;
    }

    /// <summary>
    /// ������ �Ǹ� ��ư�� ������ �� ȣ��� �޼���
    /// </summary>
    public void SellItem()
    {
        int price = (itemStats == null) ? Mathf.RoundToInt(weaponStats.Weapon.price * 0.8f) : Mathf.RoundToInt(itemStats.Item.price * 0.8f);
        CStageManager.Instance.IncreaseMoney(price);

        ItemCellReset();
    }

    /// <summary>
    /// �������� �־��� ������ Cell�� Reset�Ѵ�.
    /// </summary>
    public void ItemCellReset()
    {
        if (prevCellPos.Count > 0)
        {
            CellManager.Instance.ResetItem(prevCellPos);
        }
    }

    /// <summary>
    /// �������� ��ȭ ���� �� ȣ��� �޼���
    /// </summary>
    public void UpgradeItem()
    {
        weaponStats.Init(weaponStats.Weapon.level + 1);

        if (prevCellPos.Count > 0)
        {
            CellManager.Instance.SetItem(prevCellPos, weaponStats.Weapon.level);
        }
    }

    /// <summary>
    /// �ƾ���, ���� ������ ��Ÿ���� UI Panel�� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="active">Ȱ��ȭ ����</param>
    /// <param name="UILock">�߰� UI �������� Ȯ��</param>
    void ActiveUIPanel(bool active)
    {
        if (isInInventory)
        {
            if (UIManager.Instance.ExtraUIOpen)
            {
                return;
            }

            if (itemStats != null)
            {
                UIManager.Instance.ActiveShopItemExtraInfoPanel(itemStats, active);
            }

            if (weaponStats != null)
            {
                UIManager.Instance.ActiveShopWeaponExtraInfoPanel(weaponStats, active);
            }
        }

        else
        {
            if (itemStats != null)
            {
                UIManager.Instance.ActiveShopItemInfoPanel(itemStats, active);
            }

            if (weaponStats != null)
            {
                UIManager.Instance.ActiveShopWeaponInfoPanel(weaponStats, active);
            }
        }
    }

    /// <summary>
    /// �߰� UI�� ��� Ȱ��ȭ�Ѵ�.
    /// </summary>
    void LockExtraUIPanel()
    {
        if (itemStats != null && !UIManager.Instance.shopItemExtraInfo.gameObject.activeSelf)
        {
            UIManager.Instance.ActiveShopItemExtraInfoPanel(itemStats, true);
        }

        if (weaponStats != null && !UIManager.Instance.shopWeaponExtraInfo.gameObject.activeSelf)
        {
            UIManager.Instance.ActiveShopWeaponExtraInfoPanel(weaponStats, true);
        }
    }
}