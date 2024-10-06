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
    #region private ����
    private List<ItemData> itemDatas;
    private List<WeaponData> weaponDatas;
    private List<EnemyStats> enemyStatsDatas;

    private string wPath; // ���� ������ ���
    private string iPath; // ������ ������ ���
    #endregion
    #region Public ����
    [HideInInspector] public string weaponJson; // ���� json�����͸� ��� ����
    [HideInInspector] public string itemJson; // ������ json�����͸� ��� ����
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
        LoadWeapon();// �׽�Ʈ������ �־�� ����ȯ�� �Ǹ� ���� ��!
        LoadItem(); // �׽�Ʈ������ �־�� ����ȯ�� �Ǹ� ���� ��!
        LoadEnemy();
    }
    private void Start()
    {
        //FireBaseManager.Instance.OnInit += CompareJsonData; // ���̾�̽� �ʱ�ȭ�� �Ϸ�Ǹ� Json�����͸� ���ϴ� �޼��带 ȣ��
        // FireBaseManager.Instance.OnInit += SetJsonInDatabase;
    }

    public void CompareJsonData()
    {
        FireBaseManager.Instance.CompareJson(HashHelper.CreateHash(weaponJson), 0);
        FireBaseManager.Instance.CompareJson(HashHelper.CreateHash(itemJson), 1);

    }

    /// <summary>
    /// ���̽��� �����ͺ��̽��� �ִ� �޼��� path�� �� �ִ��Ŀ� ���� ��ġ�� �޶��� ���� !!
    /// </summary>
    public void SetJsonInDatabase()
    {
        string path = $"{Application.streamingAssetsPath}/Weapons_Data.json";
        string json = File.ReadAllText(path);
        FireBaseManager.Instance.CreateJson(HashHelper.CreateHash(json), json);
    }
    /// <summary>
    /// �ҷ��� ������ ������ �ְ� �����͸� �����ϴ� �޼���
    /// </summary>
    public void LoadItem()
    {
        List<ItemData> itemData = JsonConvert.DeserializeObject<List<ItemData>>(itemJson);
        this.itemDatas.AddRange(itemData);
        File.WriteAllText(iPath, itemJson);
    }
    /// <summary>
    /// �ҷ��� ���� ������ �ְ� �����͸� �����ϴ� �޼���
    /// </summary>
    public void LoadWeapon()
    {
        List<WeaponData> WeaponData = JsonConvert.DeserializeObject<List<WeaponData>>(weaponJson);
        this.weaponDatas.AddRange(WeaponData);
        File.WriteAllText(wPath, weaponJson);
    }

    /// <summary>
    /// �� ���� ������ �ҷ��´�.
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
            Debug.LogError("�������� ���ٱ���!");
            return null;
        }

        else
        {
            return itemDatas[uid - 1];
        }
    }
    /// <summary>
    /// ������ �̸����� ������ �����͸� �ҷ����� �޼���
    /// </summary>
    /// <param name="name">������ �̸�</param>
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
        Debug.LogError("�������� ���ٱ���!");
        return null;
    }
    /// <summary>
    /// ������ uid�� ���� �����͸� �ҷ����� �޼���
    /// </summary>
    /// <param name="uid">���� uid</param>
    /// <returns></returns>
    public WeaponData GetWeaponData(int uid)
    {

        if (uid - 1 >= weaponDatas.Count)
        {
            Debug.LogError("�������� ���ٱ���!");
            return null;
        }

        else
        {
            return new WeaponData(weaponDatas[uid - 1]);
        }
    }

    /// <summary>
    /// ������ �̸����� ���� �����͸� �ҷ����� �޼���
    /// </summary>
    /// <param name="name">���� �̸�</param>
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
    /// �� ������ �����´�. (ID�� Ž��)
    /// </summary>
    /// <param name="uid">�ش� ���� ���� ���� ��ȣ</param>
    /// <returns></returns>
    public EnemyStats GetEnemyStatsData(int uid)
    {
        if (uid - 1 >= enemyStatsDatas.Count)
        {
            Debug.LogError("�������� ���ٱ���!");
            return null;
        }

        else
        {
            return enemyStatsDatas[uid - 1];
        }
    }

    /// <summary>
    /// �� ������ �����´�. (Name���� Ž��)
    /// </summary>
    /// <param name="name">�ش� �� �̸�</param>
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
    #region public ����
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