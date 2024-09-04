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

    public static ArrowPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        arrowPool = new Queue<Arrow>();
        SetPool(arrowCount);
    }

    public Arrow CreateArrow()
    {
        var newArrow = Instantiate(ArrowPrefab).GetComponent<Arrow>();
        newArrow.gameObject.SetActive(false);
        newArrow.transform.SetParent(transform);
        return newArrow;
    }

    public void SetPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            arrowPool.Enqueue(CreateArrow());
        }
    }

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

    public void ReturnArrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        arrowPool.Enqueue(arrow);
    }

}
