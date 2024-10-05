using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FBCreatePanel : MonoBehaviour
{
    public TMP_InputField idInput;
    public TMP_InputField nameInput;
    public TMP_InputField pwInput;

    public Button createButton;
    public Button returnButton;

    private void Awake()
    {
        createButton.onClick.AddListener(() => OnCreateButtonClikc());
        returnButton.onClick.AddListener(() => OnReturnButtonClick());
    }

    public void OnCreateButtonClikc()
    {
        FireBaseManager.Instance.Create(idInput.text, nameInput.text, pwInput.text);
    }

    public void OnReturnButtonClick()
    {
        FBPanelManager.Instance.PanelOpen<FBLogInPanel>();
    }
}
