using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCell : MonoBehaviour
{
    #region Private Fields
    private int x;
    private int z;
    private CellInfo parent;
    #endregion

    private void Awake()
    {
        parent = transform.parent.GetComponent<CellInfo>();
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => parent.isActive);
        x = parent.x;
        z = parent.z;
    }
    private void OnEnable()
    {
        CellManager.Instance.OnCellClick += OnClick;
    }
    private void OnDisable()
    {
        CellManager.Instance.OnCellClick -= OnClick;

    }

    private void OnClick()
    {
        SoundManager.Instance.AddCellSound();
        gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
        CellManager.Instance.Click();
        CellManager.Instance.ResetAddCell();
        CellManager.Instance.GetActiveCell(x, z);
    }

}
