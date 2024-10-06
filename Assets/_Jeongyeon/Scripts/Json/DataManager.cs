using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Unity.VisualScripting;
using System;
using Google.MiniJSON;

public class DataManager : MonoBehaviour
{
    #region private 변수
    private List<ItemData> itemDatas;
    private List<WeaponData> weaponDatas;
    private List<EnemyStats> enemyStatsDatas;

    private string wPath; // 무기 데이터 경로
    private string iPath; // 아이템 데이터 경로
    #endregion
    #region Public 변수
    [HideInInspector] public string weaponJson; // 무기 json데이터를 담는 변수
    [HideInInspector] public string itemJson; // 아이템 json데이터를 담는 변수
    #endregion
    public static DataManager Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        weaponDatas = new List<WeaponData>();
        itemDatas = new List<ItemData>();
        enemyStatsDatas = new List<EnemyStats>();

        wPath = $"{Application.streamingAssetsPath}/Weapons_Data.json";
        iPath = $"{Application.streamingAssetsPath}/Items_Data.json";
        weaponJson = File.ReadAllText(wPath);
        itemJson = File.ReadAllText(iPath);
        LoadWeapon();// 테스트를위해 넣어둠 씬전환이 되면 지울 것!
        LoadItem(); // 테스트를위해 넣어둠 씬전환이 되면 지울 것!
        LoadEnemy();
    }
    private void Start()
    {
        //FireBaseManager.Instance.OnInit += CompareJsonData; // 파이어베이스 초기화가 완료되면 Json데이터를 비교하는 메서드를 호출
        // FireBaseManager.Instance.OnInit += SetJsonInDatabase;
    }

    public void CompareJsonData()
    {
        FireBaseManager.Instance.CompareJson(HashHelper.CreateHash(weaponJson), 0);
        FireBaseManager.Instance.CompareJson(HashHelper.CreateHash(itemJson), 1);

    }

    /// <summary>
    /// 제이슨을 데이터베이스에 넣는 메서드 path에 뭘 넣느냐에 따라 위치가 달라짐 조심 !!
    /// </summary>
    public void SetJsonInDatabase()
    {
        string path = $"{Application.streamingAssetsPath}/Weapons_Data.json";
        string json = File.ReadAllText(path);
        FireBaseManager.Instance.CreateJson(HashHelper.CreateHash(json), json);
    }
    /// <summary>
    /// 불러온 아이템 정보를 넣고 데이터를 저장하는 메서드
    /// </summary>
    public void LoadItem()
    {
        List<ItemData> itemData = JsonConvert.DeserializeObject<List<ItemData>>(itemJson);
        this.itemDatas.AddRange(itemData);
        File.WriteAllText(iPath, itemJson);
    }
    /// <summary>
    /// 불러온 무기 정보를 넣고 데이터를 저장하는 메서드
    /// </summary>
    public void LoadWeapon()
    {
        List<WeaponData> WeaponData = JsonConvert.DeserializeObject<List<WeaponData>>(weaponJson);
        this.weaponDatas.AddRange(WeaponData);
        File.WriteAllText(wPath, weaponJson);
    }

    /// <summary>
    /// 적 스텟 정보를 불러온다.
    /// </summary>
    public void LoadEnemy()
    {
        string path = $"{Application.streamingAssetsPath}/Enemys_Data.json";
        string json = File.ReadAllText(path);

        List<EnemyStats> enemyStats = JsonConvert.DeserializeObject<List<EnemyStats>>(json);
        this.enemyStatsDatas.AddRange(enemyStats);
    }


    public ItemData GetItemData(int uid)
    {

        if (uid - 1 >= itemDatas.Count)
        {
            Debug.LogError("아이템이 없다구요!");
            return null;
        }

        else
        {
            return itemDatas[uid - 1];
        }
    }
    /// <summary>
    /// 아이템 이름으로 아이템 데이터를 불러오는 메서드
    /// </summary>
    /// <param name="name">아이템 이름</param>
    /// <returns></returns>
    public ItemData GetItemData(string name)
    {
        foreach (ItemData data in itemDatas)
        {
            if (data.name.Equals(name))
            {
                return data;
            }
        }
        Debug.LogError("아이템이 없다구요!");
        return null;
    }
    /// <summary>
    /// 무기의 uid로 무기 데이터를 불러오는 메서드
    /// </summary>
    /// <param name="uid">무기 uid</param>
    /// <returns></returns>
    public WeaponData GetWeaponData(int uid)
    {

        if (uid - 1 >= weaponDatas.Count)
        {
            Debug.LogError("아이템이 없다구요!");
            return null;
        }

        else
        {
            return new WeaponData(weaponDatas[uid - 1]);
        }
    }

    /// <summary>
    /// 무기의 이름으로 무기 데이터를 불러오는 메서드
    /// </summary>
    /// <param name="name">무기 이름</param>
    /// <returns></returns>
    public WeaponData GetWeaponData(string name)
    {
        foreach (WeaponData data in weaponDatas)
        {
            if (data.weaponName.Equals(name))
            {
                return data;
            }
        }
        return null;
    }

    /// <summary>
    /// 적 정보를 가져온다. (ID로 탐색)
    /// </summary>
    /// <param name="uid">해당 적이 가진 고유 번호</param>
    /// <returns></returns>
    public EnemyStats GetEnemyStatsData(int uid)
    {
        if (uid - 1 >= enemyStatsDatas.Count)
        {
            Debug.LogError("아이템이 없다구요!");
            return null;
        }

        else
        {
            return enemyStatsDatas[uid - 1];
        }
    }

    /// <summary>
    /// 적 정보를 가져온다. (Name으로 탐색)
    /// </summary>
    /// <param name="name">해당 적 이름</param>
    /// <returns></returns>
    public EnemyStats GetEnemyStatsData(string name)
    {
        foreach (EnemyStats data in enemyStatsDatas)
        {
            if (data.Name.Equals(name))
            {
                return data;
            }
        }

        return null;
    }
}

[Serializable]
public class ItemData
{
    public string name;
    public string koreanName;
    public string tooltip;
    public int uid;
    public int price;
    public float hp;
    public float damage;
    public float meleeDamage;
    public float rangeDamage;
    public float criticalRate;
    public float attackSpeed;
    public float moveSpeed;
    public float attackRange;
    public float massValue;
    public float bloodDrain;
    public float defense;
    public float luck;
    public float moneyRate;
    public float expRate;
    public float enemyAmount;
    public int level;
    public bool active;

    public ItemData()
    {

    }
    public ItemData(string name)
    {
        this.name = name;
        koreanName = "";
        tooltip = "";
        uid = 0;
        price = 0;
        hp = 0;
        damage = 0;
        meleeDamage = 0;
        rangeDamage = 0;
        criticalRate = 0;
        attackSpeed = 0;
        moveSpeed = 0;
        attackRange = 0;
        massValue = 0;
        bloodDrain = 0;
        defense = 0;
        luck = 0;
        moneyRate = 0;
        expRate = 0;
        enemyAmount = 0;
        level = 1;
        active = false;
    }

    public ItemData(string name, string koreanName, string tooltip, int uid, int price, float hp, float damage, float meleeDamage, float rangeDamage, float criticalRate,
        float attackSpeed, float moveSpeed, float attackRange, float massValue, float bloodDrain, float defense, float luck, float moneyRate,
        float expRate, float enemyAmount, int level, bool active)
    {
        this.name = name;
        this.koreanName = koreanName;
        this.tooltip = tooltip;
        this.uid = uid;
        this.price = price;
        this.hp = hp;
        this.damage = damage;
        this.meleeDamage = meleeDamage;
        this.rangeDamage = rangeDamage;
        this.criticalRate = criticalRate;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        this.attackRange = attackRange;
        this.massValue = massValue;
        this.bloodDrain = bloodDrain;
        this.defense = defense;
        this.luck = luck;
        this.moneyRate = moneyRate;
        this.expRate = expRate;
        this.enemyAmount = enemyAmount;
        this.level = level;
        this.active = active;
    }
}
public enum Type
{
    LWeapon,
    SWeapon,
    Crossbow
}

[Serializable]
public class WeaponData
{
    #region Public Fields

    public string weaponName;
    public string weaponKoreanName;
    public int uid;
    public int level;
    public int price;
    public float damage;
    public float massValue;
    public float attackSpeed;
    public float attackRange;
    public string tooltip;
    public Type weaponType;
    #endregion

    public WeaponData()
    {

    }
    public WeaponData(WeaponData copy)
    {
        this.weaponName = copy.weaponName;
        this.weaponKoreanName = copy.weaponKoreanName;
        this.uid = copy.uid;
        this.level = copy.level;
        this.price = copy.price;
        this.damage = copy.damage;
        this.massValue = copy.massValue;
        this.attackSpeed = copy.attackSpeed;
        this.attackRange = copy.attackRange;
        this.weaponType = copy.weaponType;
        this.tooltip = copy.tooltip;
    }
}

[System.Serializable]
public class EnemyStats
{
    #region public 변수
    public EAttackType Attacktype;
    public string Name;
    public int ID;
    public float Speed;
    public float MaxHP;
    public float NowHP;
    public float Attack;
    public float AttackCooltime;
    #endregion

    public EnemyStats()
    {

    }

    public EnemyStats(EnemyStats copy)
    {
        Attacktype = copy.Attacktype;
        Name = copy.Name;
        ID = copy.ID;
        Speed = copy.Speed;
        MaxHP = copy.MaxHP;
        NowHP = copy.NowHP;
        Attack = copy.Attack;
        AttackCooltime = copy.AttackCooltime;
    }
}