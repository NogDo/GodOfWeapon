using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellColor : MonoBehaviour
{
    #region Public Fields
    public Material[] materials;
    public Material[] materials_Single;
    #endregion

    #region Private Fields
    private MeshRenderer myMeshRenderer;
    private CellInfo parent;

    private int x;
    private int z;
    #endregion

    private void Awake()
    {
        parent = transform.parent.GetComponent<CellInfo>();
        myMeshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => parent.isActive);
        x = parent.x;
        z = parent.z;
    }

    public void ChangeCellColor(int level)
    {
        switch (level)
        {
            case 1:
                myMeshRenderer.material = materials[0];
                break;
            case 2:
                myMeshRenderer.material = materials[1];
                break;
            case 3:
                myMeshRenderer.material = materials[2];
                break;
            case 4:
                myMeshRenderer.material = materials[3];
                break;
            case 5:
                myMeshRenderer.material = materials[5];
                break;

        }
    }

    public void ChangeCellColor_Single(int level)
    {
        switch (level)
        {
            case 1:
                myMeshRenderer.material = materials_Single[0];
                break;
            case 2:
                myMeshRenderer.material = materials_Single[1];
                break;
            case 3:
                myMeshRenderer.material = materials_Single[2];
                break;
            case 4:
                myMeshRenderer.material = materials_Single[3];
                break;
        }

    }

    public void ResetColor()
    {
        myMeshRenderer.material = materials[4];
    }

}
