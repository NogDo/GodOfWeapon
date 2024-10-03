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
    /// �ܻ� �̹����� �������� �ִ� �޼���
    /// </summary>
    /// <param name="smrs">������ ����ϴ� ��Ų��޽�������</param>
    /// <param name="count">�ܻ��� ������ ����</param>
    /// <param name="remainTime">��ȯ�ð��� ����</param>
    public void Setup(SkinnedMeshRenderer[] smrs, int count, float remainTime)
    {
        this.smrs = smrs;
        afterImageCount = count;
        remainingTime = remainTime;
        interval = remainingTime / (float)afterImageCount;
        CreateImageClone();
    }

    /// <summary>
    /// ������ ��ŭ�� �ܻ��� �����ϴ� �޼���
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
    ///  �ܻ��� �����ϴ� �ڷ�ƾ
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
