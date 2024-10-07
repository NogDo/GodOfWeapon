using Firebase;
using Firebase.Auth;
using Firebase.Database;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FireBaseManager : MonoBehaviour
{
    public static FireBaseManager Instance { get; private set; }

    public FirebaseApp App { get; private set; } // ���̾�̽� �⺻ ��(�⺻ ��ɵ�)
    public FirebaseAuth Auth { get; private set; } // ���� (�α���) ��� ����
    public FirebaseDatabase DB { get; private set; } // �����ͺ��̽� ��� ����

    // ���̾�̽� ���� �ʱ�ȭ �Ǿ� ��� �������� ����
    public bool IsInitialized { get; private set; } = false;

    public event Action OnInit; // ���̾�̽��� �ʱ�ȭ�Ǹ� ȣ��
    public event Action OnRank; // ��ŷ�� �ҷ����� ȣ��

    public UserData userData; // ���� ������
    public JsonData jsonData; // Json ������
    public RankData rankData; // ��ŷ ������

    public List<RankData> totalRankData;

    public DatabaseReference usersRef; // �����ͺ��̽� ���۷���

    private string userName;
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
            userName = userData.userName;
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
            var jsonHash = jsonDataValues.Child("hash");
            if (jsonHash.Exists)
            {
                string hashValue = jsonHash.GetValue(false).ToString();

                if (hashValue != hash)
                {
                    var jsonValue = jsonDataValues.Child("json");
                    if (jsonValue.Exists)
                    {
                        string value = jsonValue.GetValue(false).ToString();
                        switch (num)
                        {
                            case 0:
                                DataManager.Instance.weaponJson = value;
                                DataManager.Instance.LoadWeapon();
                                break;
                            case 1:
                                DataManager.Instance.itemJson = value;
                                DataManager.Instance.LoadItem();
                                break;
                        }
                    }
                }
                else
                {
                }
            }
        }

    }
    /// <summary>
    /// ���̾�̽��� ȸ���� ����ϴ� �޼���
    /// </summary>
    /// <param name="email">���̵�</param>
    /// <param name="name">�̸�</param>
    /// <param name="pw">��й�ȣ</param>
    public async void CreateID(string email, string name, string pw)
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

    /// <summary>
    /// ���� �����ͺ��̽��� ��ŷ�� ��ϵǾ� �ִ��� Ȯ���ϰ� ������ ���� ������ ��ŷ�� ����ϴ� �޼���
    /// </summary>
    /// <param name="totalValue">�� ����</param>
    /// <param name="stage">������ ��</param>
    public async void CheckRank(int totalValue, int stage)
    {

        usersRef = DB.GetReference($"rank/{userName}");

        DataSnapshot rankDataValues = await usersRef.GetValueAsync();

        if (rankDataValues.Exists)
        {
            var rankValue = rankDataValues.Child("totalDamage");
            if (rankValue.Exists)
            {
                int value = int.Parse(rankValue.GetValue(false).ToString());

                if (value < totalValue)
                {
                    await rankDataValues.Reference.Child("totalDamage").SetValueAsync(totalValue);
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            CreateRank(userName, totalValue, stage);
        }
    }
    /// <summary>
    /// �����ͺ��̽��� ��ŷ�� ����ϴ� �޼���
    /// </summary>
    /// <param name="name">����ϴ� ��� �̸�</param>
    /// <param name="value">�� ������</param>
    public async void CreateRank(string name, int value, int stage)
    {
        try
        {
            usersRef = DB.GetReference($"rank/{name}");

            RankData rankData = new RankData(name, value, stage);

            string rankDatas = JsonConvert.SerializeObject(rankData);

            await usersRef.SetRawJsonValueAsync(rankDatas);

            this.rankData = rankData;
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e.Message);
        }
    }
    /// <summary>
    /// ��ũ ������ �ҷ����� �޼���
    /// </summary>
    public async void GetRankData()
    {
        try
        {
            usersRef = DB.GetReference("rank");
            DataSnapshot rankDatas = await usersRef.GetValueAsync();

            if (rankDatas.Exists)
            {
                foreach (DataSnapshot data in rankDatas.Children)
                {
                    string json = data.GetRawJsonValue();
                    RankData rankData = JsonConvert.DeserializeObject<RankData>(json);
                    totalRankData.Add(rankData);
                    if (totalRankData.Count > 10)
                    {
                        break;
                    }
                }
                OnRank?.Invoke();
            }
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e.Message);
        }

    }
}
