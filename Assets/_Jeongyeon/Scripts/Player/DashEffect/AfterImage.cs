using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    #region Public Fields
    public Mesh Mesh { get{ return meshFilter.mesh; } }
    #endregion

    #region Private Fields
    private Material aiMaterial;
    private MeshFilter meshFilter;
    private Coroutine fadeOut;
    private float originAlpha;
    private Coroutine fOCoroutine = null;
    #endregion

    /// <summary>
    /// �ܻ��� ���׸����� ���� ���׸���� ���� �����ϴ� �޼���
    /// </summary>
    /// <param name="material">���� ���׸���</param>
    public void Init(Material material)
    {
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        aiMaterial = new Material(material);
        originAlpha = aiMaterial.color.a;
        meshRenderer.material = aiMaterial;
        meshFilter = gameObject.AddComponent<MeshFilter>();

        gameObject.SetActive(false);
    }

    /// <summary>
    /// �ܻ��� �����ϴ� �޼���
    /// </summary>
    /// <param name ="position">������Ʈ�� ������ġ<param name= "rot">�����̼ǰ�<param name="time">�����ð�</param>
    public void Create(Vector3 position,Quaternion rot, float time)
    {
        if (fOCoroutine == null)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = position;
            gameObject.transform.rotation = rot;

            meshFilter.mesh = Mesh;
            fOCoroutine = StartCoroutine(FadeOut(time));
        }
    }

    /// <summary>
    /// �ܻ��� �÷����� 0���� �ٲٴ� �ڷ�ƾ
    /// </summary>
    /// <param name="time">���� ���ϴ� �ð���</param>
    /// <returns></returns>
    private IEnumerator FadeOut(float time)
    {
        while (time > 0f)
        {
            float alpha = originAlpha / 4 * time;
            aiMaterial.color = new Color(0.3f, 0.3f, 0.3f, alpha);
            time -= Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        fOCoroutine = null;
    }
}
