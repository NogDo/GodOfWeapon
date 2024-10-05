using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FBPanelManager : MonoBehaviour
{
   public enum PanelType
   {
       LogIn,
       Create,
       Active,
   }

    #region Public Fields
    public FBLogInPanel login; // �α��� �г�
    public FBCreatePanel create; // ȸ������ �г�
    public ActivePanel active; // �������� ������ ���� ��ư �г�
    public GameObject logInResult; // ��� ���� �г�
    public GameObject creatResult; // ��� ���� �г�
    #endregion

    #region Private Fields
    private Dictionary<PanelType, MonoBehaviour> panels;
    #endregion

    public static FBPanelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        panels = new()
        {
            { PanelType.LogIn, login },
            { PanelType.Create, create },
            { PanelType.Active, active }
        };
        
    }

    private void Start()
    {
        StartCoroutine(startPanel());
    }

    private IEnumerator startPanel()
    {
        yield return new WaitForSeconds(1.0f);
        PanelOpen(PanelType.LogIn);
    }
    /// <summary>
    /// �ش��ϴ� �г��� Ȱ��ȭ ��Ű��, �������� ��Ȱ��ȭ ��Ų��.
    /// </summary>
    /// <param name="type"> �ش� �г�</param>
    /// <returns></returns>
    public GameObject PanelOpen(PanelType type)
    {
        GameObject panel = null;

        foreach (var row in panels)
        {
            bool isMatch = type == row.Key;
            if (isMatch)
            {
                panel = row.Value.gameObject;
            }
            row.Value.gameObject.SetActive(isMatch);
        }
        return panel;
    }

    /// <summary>
    /// �ش��ϴ� �г��� Ȱ��ȭ ��Ű��, �������� ��Ȱ��ȭ ��Ų��.
    /// </summary>
    /// <typeparam name="T">Ÿ��</typeparam>
    /// <returns></returns>
    public T PanelOpen<T>() where T : MonoBehaviour
    {
        T panel = null;

        foreach (var row in panels)
        {
            bool isMatch = typeof(T) == row.Value.GetType();
            if (isMatch)
            {
                panel = row.Value as T;
            }
            row.Value.gameObject.SetActive(isMatch);
        }
        return panel;
    }

    public void SuccessLogin()
    {
        StartCoroutine(loginSuccess());
    }

    public void FailLogin()
    {
        logInResult.GetComponentInChildren<TextMeshProUGUI>().text = "ID/Password ����";
        logInResult.GetComponentInChildren<Button>().gameObject.SetActive(true);
        logInResult.SetActive(true);
    }

    public void SuccessCreate()
    {
        StartCoroutine(CreateSuccess());
    }
    public void FailCreate(string content) 
    {
        creatResult.GetComponentInChildren<TextMeshProUGUI>().text = content;
        creatResult.GetComponentInChildren<Button>().gameObject.SetActive(true);
        creatResult.SetActive(true);
    }
    private IEnumerator loginSuccess()
    {
        logInResult.GetComponentInChildren<TextMeshProUGUI>().text = "�α��� ����!";
        logInResult.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        logInResult.SetActive(false);
        PanelOpen<ActivePanel>();
    }

    private IEnumerator CreateSuccess()
    {
        creatResult.GetComponentInChildren<TextMeshProUGUI>().text = "ȸ������ ����!";
        creatResult.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        creatResult.SetActive(false);
        PanelOpen<FBLogInPanel>();
    }

}
