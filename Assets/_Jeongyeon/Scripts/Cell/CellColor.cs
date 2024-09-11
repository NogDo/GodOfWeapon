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
    private Material myMaterial;
    private GameObject item;
    private CellInfo parent;

    private int x;
    private int z;
    #endregion

    private void Awake()
    {
        parent = transform.parent.GetComponent<CellInfo>();
        myMeshRenderer = GetComponentInChildren<MeshRenderer>();
        myMaterial = myMeshRenderer.material;
    }
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => parent.isActive);
        x = parent.x;
        z = parent.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item") && myMeshRenderer.material == myMaterial)
        {
            item = other.gameObject;
            float itemXSize = other.gameObject.GetComponent<BoxCollider>().size.x;
            float itemZSize = other.gameObject.GetComponent<BoxCollider>().size.z;
            int level = Random.Range(0, 3);
            if (itemXSize+ itemZSize > 2)
            {
                switch (level)
                {
                    case 0:
                        myMeshRenderer.material = materials[0];
                        break;
                    case 1:
                        myMeshRenderer.material = materials[1];
                        break;
                    case 2:
                        myMeshRenderer.material = materials[2];
                        break;
                    case 3:
                        myMeshRenderer.material = materials[3];
                        break;
                }
            }
            else
            {
                switch (level)
                {
                    case 0:
                        myMeshRenderer.material = materials_Single[0];
                        break;
                    case 1:
                        myMeshRenderer.material = materials_Single[1];
                        break;
                    case 2:
                        myMeshRenderer.material = materials_Single[2];
                        break;
                    case 3:
                        myMeshRenderer.material = materials_Single[3];
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == item && myMeshRenderer.material != myMaterial)
        {
            myMeshRenderer.material = myMaterial;
        }
    }
}
