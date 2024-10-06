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

    public FirebaseApp App { get; private set; } // 파이어베이스 기본 앱(기본 기능들)
    public FirebaseAuth Auth { get; private set; } // 인증 (로그인) 기능 전용
    public FirebaseDatabase DB { get; private set; } // 데이터베이스 기능 전용

    // 파이어베이스 앱이 초기화 되어 사용 가능한지 여부
    public bool IsInitialized { get; private set; } = false;

    public event Action OnInit; // 파이어베이스가 초기화되면 호출

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
    //async 키워드를 통해 비동기 프로그래밍
    private async void InitializeAsync()
    {
        DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (status == DependencyStatus.Available)
        {
            // 파이어베이스 초기화 성공
            App = FirebaseApp.DefaultInstance;
            Auth = FirebaseAuth.DefaultInstance;
            DB = FirebaseDatabase.DefaultInstance;
            IsInitialized = true;
            OnInit?.Invoke();
            print($"파이어베이스 초기화 성공!");
        }
        else
        {
            // 파이어베이스 초기화 실패
            Debug.LogWarning($"파이어베스 초기화 실패: {status}");
        }

    }
    /// <summary>
    /// 파이어베이스에 로그인을 하는 메서드
    /// </summary>
    /// <param name="email">아이디</param>
    /// <param name="pw">비밀번호</param>
    /// <param name="callback">콜백</param>
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
    /// 제이슨을 비교하는 메서드
    /// </summary>
    /// <param name="hash">해쉬로 변환된 값</param>
    /// <param name="num">0 = 무기, 1 = 아이템, 2 = 적</param>
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
            Debug.Log("데이터가 다르자나!");
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
            Debug.Log("데이터 힛또");
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
            FBPanelManager.Instance.FailCreate("회원가입 실패");
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
