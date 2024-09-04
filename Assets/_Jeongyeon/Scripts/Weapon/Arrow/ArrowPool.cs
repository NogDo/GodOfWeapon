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
    /// 화살을 생성하는 메서드
    /// </summary>
    /// <returns></returns>
    public Arrow CreateArrow()
    {
        var newArrow = Instantiate(ArrowPrefab, transform).GetComponent<Arrow>();
        newArrow.gameObject.SetActive(false);
        return newArrow;
    }
    /// <summary>
    /// 시작시 풀안에 화살을 생성하는 메서드
    /// </summary>
    /// <param name="count">생성할 화살의 갯수</param>
    public void SetPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            arrowPool.Enqueue(CreateArrow());
        }
    }

    /// <summary>
    /// 화살을 풀에서 꺼내는 메서드(없을경우 생성후 반환)
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
    /// 화살을 풀안으로 반환하는 메서드
    /// </summary>
    /// <param name="arrow">풀안으로 들어올 화살</param>
    public void ReturnArrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        arrow.transform.SetParent(transform);
        arrowPool.Enqueue(arrow);
    }

}
