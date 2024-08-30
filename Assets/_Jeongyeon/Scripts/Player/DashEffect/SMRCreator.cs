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
    /// �ܻ� �̹����� �������� �ִ� �޼���
    /// </summary>
    /// <param name="smr">������ ����ϴ� ��Ų��޽�������</param>
    /// <param name="count">�ܻ��� ������ ����</param>
    /// <param name="remainTime">��ȯ�ð��� ����</param>
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
    /// �������� �����ؾߵ� �ܻ� �����ϴ� �Լ�
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
    /// ���������� �ƴ��� Ȯ���ϰ� �ƴϸ� �����ϴ� �޼���
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
    ///  �ܻ��� �����ϴ� �ڷ�ƾ
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
