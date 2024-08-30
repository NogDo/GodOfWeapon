using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMRCreator : MonoBehaviour
{
    [SerializeField]
    private Material afterImageMaterial;

    #region Public Fields
    #endregion

    #region Private Fields
    private SkinnedMeshRenderer smr;
    private AfterImage[] afterImages;

    private int afterImageCount;
    private int currentIndex;
    private float remainingTime;
    private float interval;
    private Coroutine createCoroutine = null;

    bool isCreating = false;
    #endregion

    /// <summary>
    /// 잔상 이미지의 설정값을 넣는 메서드
    /// </summary>
    /// <param name="smr">생성을 담당하는 스킨드메쉬랜더러</param>
    /// <param name="count">잔상의 갯수를 설정</param>
    /// <param name="remainTime">소환시간을 설정</param>
    public void Setup(SkinnedMeshRenderer smr, int count, float remainTime)
    {
        this.smr = smr;
        afterImageCount = count;
        remainingTime = remainTime;
        interval = remainingTime/ (float)afterImageCount;
        Debug.Log($"AfterImage Created");
        CreateImageClone();
    }

    /// <summary>
    /// 설정값을 생성해야될 잔상에 적용하는 함수
    /// </summary>
    private void CreateImageClone()
    {
       
        afterImages = new AfterImage[afterImageCount];
        for (int i = 0; i < afterImages.Length; i++)
        {
            GameObject obj = new GameObject();
            afterImages[i] = obj.AddComponent<AfterImage>();
            afterImages[i].Init(afterImageMaterial);
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
                createCoroutine = StartCoroutine(CreateImage());
            }
        }
    }
    /// <summary>
    ///  잔상을 생성하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateImage()
    {
        float time = 0;
        while (isCreating)
        {
            time += Time.deltaTime;
            if (time >= interval)
            {
                smr.BakeMesh(afterImages[currentIndex].Mesh);
                afterImages[currentIndex].Create(transform.position, transform.rotation, remainingTime);
                currentIndex = (currentIndex + 1) % afterImageCount;
                time -= interval;
            }
            yield return null;
        }
        createCoroutine = null;
    }


}
