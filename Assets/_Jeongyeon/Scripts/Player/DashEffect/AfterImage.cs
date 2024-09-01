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
    /// 잔상의 메테리얼을 원본 메테리얼과 같게 복사하는 메서드
    /// </summary>
    /// <param name="material">원본 메테리얼</param>
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
    /// 잔상을 생성하는 메서드
    /// </summary>
    /// <param name ="position">오브젝트의 현재위치<param name= "rot">로테이션값<param name="time">생성시간</param>
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
    /// 잔상의 컬러값을 0으로 바꾸는 코루틴
    /// </summary>
    /// <param name="time">색이 변하는 시간값</param>
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
