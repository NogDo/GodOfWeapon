using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   
    public void ChangeGameScene()
    {
        SceneManager.LoadSceneAsync("MapCreate");
        SoundManager.Instance.PlayBackgrounAudio(1);
    }

    public void GameExit()
    {
        // Application.Quit(); // 게임 종료호출 메서드 단, 에디터에서는 작동하지 않고 빌드된 상태에서만 작동
         // 게임 종료호출 메서드 단, 빌드된 상태에서는 작동하지 않고 에디터에서만 작동
    }
}
