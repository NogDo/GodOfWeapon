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

    public event Action OnInit; // ���̾�̽��� �ʱ�ȭ�Ǹ� ȣ��

    public UserData userData;
    public JsonData jsonData;
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
            OnInit?.Invoke();
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

    /// <summary>
    /// ���̽��� ���ϴ� �޼���
    /// </summary>
    /// <param name="hash">�ؽ��� ��ȯ�� ��</param>
    /// <param name="num">0 = ����, 1 = ������, 2 = ��</param>
    public async void CompareJson(string hash, int num)
    {
        switch (num)
        {
            case 0:
                usersRef = DB.GetReference($"json/weapon");
                break;
            case 1:
                usersRef = DB.GetReference($"json/item");
                break;
        }

        DataSnapshot jsonDataValues = await usersRef.GetValueAsync();

        if (jsonDataValues.Exists)
        {
            string json = jsonDataValues.GetRawJsonValue();
            jsonData = JsonConvert.DeserializeObject<JsonData>(json);
        }
        if (jsonData.hash != hash)
        {
            Debug.Log("�����Ͱ� �ٸ��ڳ�!");
            switch (num)
            {
                case 0:
                    DataManager.Instance.weaponJson = jsonData.json;
                    DataManager.Instance.LoadWeapon();
                    break;
                case 1:
                    DataManager.Instance.itemJson = jsonData.json;
                    DataManager.Instance.LoadItem();
                    break;
            }
        }
        else
        {
            Debug.Log("������ ����");
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

    public async void CreateJson(string hash, string value)
    {
        try
        {
            usersRef = DB.GetReference($"json/weapon");

            JsonData jsonData = new JsonData(hash, value);

            string jsonDatas = JsonConvert.SerializeObject(jsonData);

            await usersRef.SetRawJsonValueAsync(jsonDatas);

            this.jsonData = jsonData;
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e.Message);
        }
    }
}
