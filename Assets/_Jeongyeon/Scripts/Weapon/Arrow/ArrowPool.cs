using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    #region public Fields
    public Queue<GameObject> arrowPool;
    public GameObject ArrowPrefab;
    #endregion

    #region private Fields
    private int arrowCount = 5;
    #endregion

    private void Start()
    {
        arrowPool = new Queue<GameObject>();
        CreateArrowPool(ArrowPrefab);
    }

    public void CreateArrowPool(GameObject arrowPrefab)
    {
        Instantiate(arrowPrefab).SetActive(false);
        arrowPool.Enqueue(arrowPrefab);
    }

    public void SetPool()
    {
        
    }

    public void GetArrow()
    {
        if (arrowPool.Count > 0)
        {
            arrowPool.Dequeue();
        }
        else
        {
            arrowPool.Enqueue(Instantiate(ArrowPrefab));
            arrowPool.Dequeue().SetActive(true);
        }
    }

    public void ReturnArrow(GameObject arrow)
    {
        arrow.SetActive(false);
        arrowPool.Enqueue(arrow);
    }

}
