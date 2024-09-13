using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
    private List<ItemData> itemDatas;
    private List<WeaponData> weaponDatas;
    private WeaponData crossbow;
    private ItemData dice;
    public void Awake()
    {
        crossbow = new WeaponData("",0,0,0,0,0,0,0,0);
        dice = new ItemData("Dice");
        weaponDatas = new List<WeaponData>();
        itemDatas = new List<ItemData>();
        weaponDatas.Add(crossbow);
        itemDatas.Add(dice);
    }
    private void Start()
    {
        Save();
    }

    public void Save()
    {
        string path = $"{Application.streamingAssetsPath}/Items_Data.json";
        List<string> jsonList = new List<string>();

        foreach (ItemData data in itemDatas)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            jsonList.Add(json);
        }

        string finalJson = "[" + string.Join(",", jsonList) + "]";
        File.WriteAllText(path, finalJson);
    }

    public void Load()
    {
        string path = $"{Application.streamingAssetsPath}/Items_Data.json";
        string json = File.ReadAllText(path);
        List<ItemData> itemData = JsonConvert.DeserializeObject<List<ItemData>>(json);
        this.itemDatas.AddRange(itemData);
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

    public WeaponData GetWeaponData(int uid)
    {
        foreach (WeaponData data in weaponDatas)
        {
            if (data.uid == uid)
            {
                return data;
            }
        }
        Debug.LogError("힛또");
        return null;
    }
}
