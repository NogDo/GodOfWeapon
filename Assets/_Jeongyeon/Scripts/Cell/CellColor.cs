using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellColor : MonoBehaviour
{
    public Material[] materials;
    public Material[] materials_Single;

    private MeshRenderer myMeshRenderer;
    private Material myMaterial;
    private GameObject item;
    private void Start()
    {
        myMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        myMaterial = myMeshRenderer.material;
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
