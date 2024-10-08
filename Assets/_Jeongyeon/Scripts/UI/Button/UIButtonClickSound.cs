using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonClickSound : MonoBehaviour
{
    private Button UIButton;

    private void Awake()
    {
        UIButton = GetComponent<Button>();
    }
    private void Start()
    {
        UIButton.onClick.AddListener(ClickUI);
    }

    private void ClickUI()
    {
        SoundManager.Instance.ButtonClickSound();
    }

    public void NextStage()
    {
        CStageManager.Instance.NextStage();
    }
}
