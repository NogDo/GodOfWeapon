using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBaseManager : MonoBehaviour
{
    public static FireBaseManager Instance { get; private set; }

    public FirebaseApp App { get; private set; } // ���̾�̽� �⺻ ��(�⺻ ��ɵ�)
    public FirebaseAuth Auth { get; private set; } // ���� (�α���) ��� ����
    public FirebaseDatabase DB { get; private set; } // �����ͺ��̽� ��� ����

    // ���̾�̽� ���� �ʱ�ȭ �Ǿ� ��� �������� ����
    public bool IsInitialized { get; private set; } = false;

    public event Action onInit; // ���̾�̽��� �ʱ�ȭ�Ǹ� ȣ��

    public UserData userData;
    public DatabaseReference usersRef;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        InitializeAsync();
    }
    //async Ű���带 ���� �񵿱� ���α׷���
    private async void InitializeAsync()
    {
        DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (status == DependencyStatus.Available)
        {
            // ���̾�̽� �ʱ�ȭ ����
            App = FirebaseApp.DefaultInstance;
            Auth = FirebaseAuth.DefaultInstance;
            DB = FirebaseDatabase.DefaultInstance;
            IsInitialized = true;
            onInit?.Invoke();
            print($"���̾�̽� �ʱ�ȭ ����!");
        }
        else
        {
            // ���̾�̽� �ʱ�ȭ ����
            Debug.LogWarning($"���̾�� �ʱ�ȭ ����: {status}");
        }

    }
    /// <summary>
    /// ���̾�̽��� �α����� �ϴ� �޼���
    /// </summary>
    /// <param name="email">���̵�</param>
    /// <param name="pw">��й�ȣ</param>
    /// <param name="callback">�ݹ�</param>
    public async void Login(string email, string pw, Action<FirebaseUser> callback = null)
    {
        var result = await Auth.SignInWithEmailAndPasswordAsync(email, pw);

        usersRef = DB.GetReference($"users/{result.User.UserId}");

        DataSnapshot userDataValues = await usersRef.GetValueAsync();

        if (userDataValues.Exists)
        {
            string json = userDataValues.GetRawJsonValue();
            userData = JsonConvert.DeserializeObject<UserData>(json);
            FBPanelManager.Instance.SuccessLogin();
        }
        else
        {
            FBPanelManager.Instance.FailLogin();
        }
    }
    public async void Create(string email, string name, string pw)
    {
        try
        {
            var result = await Auth.CreateUserWithEmailAndPasswordAsync(email, pw);

            usersRef = DB.GetReference($"users/{result.User.UserId}");

            UserData userData = new UserData(result.User.UserId, name);

            string userDataJson = JsonConvert.SerializeObject(userData);

            await usersRef.SetRawJsonValueAsync(userDataJson);

            this.userData = userData;
            FBPanelManager.Instance.SuccessCreate();
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e.Message);
            FBPanelManager.Instance.FailCreate("ȸ������ ����");
        }
    }
}
