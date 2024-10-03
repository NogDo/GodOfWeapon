using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMRCreator : MonoBehaviour
{
    [SerializeField]
    private Material afterImageMaterial;

    #region Public Fields
    public Transform parentTransform;
    #endregion

    #region Private Fields
    private SkinnedMeshRenderer[] smrs;
    private AfterImage[][] afterImages;

    private int afterImageCount;
    private int currentIndex;
    private float remainingTime;
    private float interval;
    private Coroutine[] createCoroutine = null;

    bool isCreating = false;
    #endregion

    /// <summary>
    /// 잔상 이미지의 설정값을 넣는 메서드
    /// </summary>
    /// <param name="smrs">생성을 담당하는 스킨드메쉬랜더러</param>
    /// <param name="count">잔상의 갯수를 설정</param>
    /// <param name="remainTime">소환시간을 설정</param>
    public void Setup(SkinnedMeshRenderer[] smrs, int count, float remainTime)
    {
        this.smrs = smrs;
        afterImageCount = count;
        remainingTime = remainTime;
        interval = remainingTime / (float)afterImageCount;
        CreateImageClone();
    }

    /// <summary>
    /// 설정값 만큼의 잔상을 생성하는 메서드
    /// </summary>
    private void CreateImageClone()
    {
        afterImages = new AfterImage[smrs.Length][]; 

        for (int i = 0; i < smrs.Length; i++)
        {
            afterImages[i] = new AfterImage[afterImageCount];
            for (int j = 0; j < afterImages[i].Length; j++)
            {
                GameObject obj = new GameObject();
                afterImages[i][j] = obj.AddComponent<AfterImage>();
                afterImages[i][j].Init(afterImageMaterial);
            }
        }
    }

    /// <summary>
    /// 생성중인지 아닌지 확인하고 아니면 생성하는 메서드
    /// </summary>
    /// <param name="creating"></param>
    public void Create(bool creating)
    {
        isCreating = creating;
        if (creating)
        {
            if (createCoroutine == null)
            {
                createCoroutine = new Coroutine[smrs.Length];
                for (int i = 0; i < smrs.Length; i++)
                {
                    createCoroutine[i] = StartCoroutine(CreateImage(i));
                }
            }
        }
        else
        {
            if (createCoroutine != null)
            {
                for (int i = 0; i < createCoroutine.Length; i++)
                {
                    if (createCoroutine[i] != null)
                    {
                        StopCoroutine(createCoroutine[i]);
                        createCoroutine[i] = null;
                    }
                }
                createCoroutine = null;
            }
        }
    }
    /// <summary>
    ///  잔상을 생성하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateImage(int index)
    {
        float time = 0;
        while (isCreating)
        {
            time += Time.deltaTime;
            if (time >= interval)
            {
                smrs[index].BakeMesh(afterImages[index][currentIndex].Mesh);
                afterImages[index][currentIndex].Create(parentTransform.parent.position, parentTransform.rotation, remainingTime);
                currentIndex = (currentIndex + 1) % afterImageCount;
                time -= interval;
            }
            yield return null;
        }
        createCoroutine = null;
    }


}
