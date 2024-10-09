using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    #region private 변수
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
        // Application.Quit(); // 게임 종료호출 메서드 단, 에디터에서는 작동하지 않고 빌드된 상태에서만 작동
         // 게임 종료호출 메서드 단, 빌드된 상태에서는 작동하지 않고 에디터에서만 작동
    }
}
