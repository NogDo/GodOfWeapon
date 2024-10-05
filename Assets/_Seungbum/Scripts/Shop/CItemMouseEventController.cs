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
    #region private 변수
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
    /// 아이템 중앙 포지션 값
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
    /// 아이템을 마우스로 집는다.
    /// </summary>
    void Grab()
    {
        if (!isInInventory)
        {
            int price = (itemStats == null) ? weaponStats.Weapon.price : itemStats.Item.price;

            // 보유한 돈이 가격보다 적다면 그랩을 할 수 없음
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

        // 원래 있던 칸 없애기
        if (prevCellPos.Count > 0)
        {
            CellManager.Instance.ResetItem(prevCellPos);
        }

        // 띄워져있던 UI 끄기
        ActiveUIPanel(false);

        isGrab = true;
    }

    /// <summary>
    /// 인벤토리 Cell에 아이템이 들어갈 수 있는지 판단
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

        // 그리드의 개수와 Cell과 충돌한 Ray의 개수가 같다면 놓을 수 있다고 판단.
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
    /// 아이템이 놓일 곳을 미리 보여준다.
    /// </summary>
    void Preview()
    {
        // 첫번째 그리드의 Ray가 Cell과 충돌했을 경우, 놓을 곳의 아이템을 미리 보여준다.
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
    /// 아이템 Grab을 종료한다.
    /// </summary>
    void ReleaseGrab()
    {
        // 인벤토리에 들어갈 수 있는 상태
        if (isCanDrop)
        {
            Vector3 pos = tfCell.position;

            EquipItem(pos, nRotateCount);
        }
        // 인벤토리에 들어갈 수 없는 상태
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
    /// 아이템을 인벤토리에 장착한다.
    /// </summary>
    /// <param name="position">아이템이 위치할 포지션 값</param>
    /// <param name="rotationCount">회전값</param>
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

        // 아이템일 경우
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
        // 무기일 경우
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
    /// 아이템을 조합하기 위해 아이템을 클릭했을 때 실행될 메서드
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
    /// 조합이 성공 또는 취소 될때 까지 기다리고 Cell의 하이라이트를 종료한다.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitCombine()
    {
        CellManager.Instance.HighlightCell(prevCellPos, true);

        yield return new WaitUntil(() => !UIManager.Instance.canCombine);

        CellManager.Instance.HighlightCell(prevCellPos, false);
    }

    /// <summary>
    /// 회전 카운트 수를 증가시킨다.
    /// </summary>
    public void IncreaseRotationCount()
    {
        nRotateCount = (nRotateCount + 1 < 4) ? nRotateCount + 1 : 0;
    }

    /// <summary>
    /// 아이템의 인덱스를 설정한다.
    /// </summary>
    /// <param name="index">상점 인덱스</param>
    public void SetIndex(int index)
    {
        nIndex = index;
    }

    /// <summary>
    /// 아이템 판매 버튼을 눌렀을 때 호출될 메서드
    /// </summary>
    public void SellItem()
    {
        int price = (itemStats == null) ? Mathf.RoundToInt(weaponStats.Weapon.price * 0.8f) : Mathf.RoundToInt(itemStats.Item.price * 0.8f);
        CStageManager.Instance.IncreaseMoney(price);

        ItemCellReset();
    }

    /// <summary>
    /// 아이템이 있었던 공간의 Cell을 Reset한다.
    /// </summary>
    public void ItemCellReset()
    {
        if (prevCellPos.Count > 0)
        {
            CellManager.Instance.ResetItem(prevCellPos);
        }
    }

    /// <summary>
    /// 아이템을 강화 했을 때 호출될 메서드
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
    /// 아아템, 무기 정보를 나타내는 UI Panel을 활성화 / 비활성화 한다.
    /// </summary>
    /// <param name="active">활성화 여부</param>
    /// <param name="UILock">추가 UI 고정인지 확인</param>
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
    /// 추가 UI를 계속 활성화한다.
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