using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FBLogInPanel : MonoBehaviour
{
    public TMP_InputField idInput;
    public TMP_InputField pwInput;

    public Button loginButton;
    public Button CreateButton;
    

    private void Awake()
    {
        loginButton.onClick.AddListener(() => OnLoginButtonClikc());
        CreateButton.onClick.AddListener(() => OnCreateButtonClikc());
    }
    
    public void OnLoginButtonClikc()
    {
        FireBaseManager.Instance.Login(idInput.text, pwInput.text);
    }

    public void OnCreateButtonClikc()
    {
        FBPanelManager.Instance.PanelOpen<FBCreatePanel>();
    }
}
