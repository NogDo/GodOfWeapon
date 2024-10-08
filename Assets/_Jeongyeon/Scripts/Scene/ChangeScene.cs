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
        // Application.Quit(); // ���� ����ȣ�� �޼��� ��, �����Ϳ����� �۵����� �ʰ� ����� ���¿����� �۵�
         // ���� ����ȣ�� �޼��� ��, ����� ���¿����� �۵����� �ʰ� �����Ϳ����� �۵�
    }
}
