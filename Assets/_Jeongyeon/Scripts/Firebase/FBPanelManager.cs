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
    public FBLogInPanel login; // 로그인 패널
    public FBCreatePanel create; // 회원가입 패널
    public ActivePanel active; // 게임으로 진행을 위한 버튼 패널
    public GameObject logInResult; // 결과 성공 패널
    public GameObject creatResult; // 결과 실패 패널
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
    /// 해당하는 패널을 활성화 시키고, 나머지는 비활성화 시킨다.
    /// </summary>
    /// <param name="type"> 해당 패널</param>
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
    /// 해당하는 패널을 활성화 시키고, 나머지는 비활성화 시킨다.
    /// </summary>
    /// <typeparam name="T">타입</typeparam>
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
        logInResult.GetComponentInChildren<TextMeshProUGUI>().text = "ID/Password 오류";
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
        logInResult.GetComponentInChildren<TextMeshProUGUI>().text = "로그인 성공!";
        logInResult.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        logInResult.SetActive(false);
        PanelOpen<ActivePanel>();
    }

    private IEnumerator CreateSuccess()
    {
        creatResult.GetComponentInChildren<TextMeshProUGUI>().text = "회원가입 성공!";
        creatResult.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        creatResult.SetActive(false);
        PanelOpen<FBLogInPanel>();
    }

}
