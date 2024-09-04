using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    #region public Fields
    public Queue<Arrow> arrowPool;
    public GameObject ArrowPrefab;
    #endregion

    #region private Fields
    private int arrowCount = 5;
    #endregion

    private void Start()
    {
        arrowPool = new Queue<Arrow>();
        SetPool(arrowCount);
    }
    /// <summary>
    /// ȭ���� �����ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    public Arrow CreateArrow()
    {
        var newArrow = Instantiate(ArrowPrefab, transform).GetComponent<Arrow>();
        newArrow.gameObject.SetActive(false);
        return newArrow;
    }
    /// <summary>
    /// ���۽� Ǯ�ȿ� ȭ���� �����ϴ� �޼���
    /// </summary>
    /// <param name="count">������ ȭ���� ����</param>
    public void SetPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            arrowPool.Enqueue(CreateArrow());
        }
    }

    /// <summary>
    /// ȭ���� Ǯ���� ������ �޼���(������� ������ ��ȯ)
    /// </summary>
    /// <returns></returns>
    public Arrow GetArrow()
    {
        if (arrowPool.Count > 0)
        {
            var arrow = arrowPool.Dequeue();
            arrow.transform.SetParent(null);
            arrow.gameObject.SetActive(true);
            return arrow;
        }
        else
        {
            var newArrow = CreateArrow();
            newArrow.gameObject.SetActive(true);
            newArrow.transform.SetParent(null);
            return newArrow;
        }
    }
    /// <summary>
    /// ȭ���� Ǯ������ ��ȯ�ϴ� �޼���
    /// </summary>
    /// <param name="arrow">Ǯ������ ���� ȭ��</param>
    public void ReturnArrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        arrow.transform.SetParent(transform);
        arrowPool.Enqueue(arrow);
    }

}
