using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Unity.VisualScripting;

public class DataManager : MonoBehaviour
{
    private List<ItemData> itemDatas;
    private List<WeaponData> weaponDatas;
    private WeaponData crossbow;
    private ItemData dice;

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
        LoadItem();
        LoadWeapon();
    }
    private void Start()
    {

    }

    public void LoadItem()
    {
        string path = $"{Application.streamingAssetsPath}/Items_Data.json";
        string json = File.ReadAllText(path);
        List<ItemData> itemData = JsonConvert.DeserializeObject<List<ItemData>>(json);
        this.itemDatas.AddRange(itemData);
    }
    public void LoadWeapon()
    {
        string path = $"{Application.streamingAssetsPath}/Weapons_Data.json";
        string json = File.ReadAllText(path);
        List<WeaponData> WeaponData = JsonConvert.DeserializeObject<List<WeaponData>>(json);
        this.weaponDatas.AddRange(WeaponData);
    }


    public ItemData GetItemData(int uid)
    {
        foreach (ItemData data in itemDatas)
        {
            if (data.uid == uid)
            {
                return data;
            }
        }
        Debug.LogError("아이템이 없다구요!");
        return null;
    }
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

    public WeaponData GetWeaponData(int uid)
    {
        foreach (WeaponData data in weaponDatas)
        {
            if (data.uid == uid)
            {
                return data;
            }
        }
        return null;
    }
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
}
