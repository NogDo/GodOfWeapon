using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    #region private ����
    [SerializeField]
    UIFadeControl fadeControl;
    #endregion

    public void ChangeGameScene()
    {
        fadeControl.StartMainFade();
        SoundManager.Instance.PlayLobbyAudio(1);
    }

    public void GameExit()
    {
        // Application.Quit(); // ���� ����ȣ�� �޼��� ��, �����Ϳ����� �۵����� �ʰ� ����� ���¿����� �۵�
         // ���� ����ȣ�� �޼��� ��, ����� ���¿����� �۵����� �ʰ� �����Ϳ����� �۵�
    }
}
