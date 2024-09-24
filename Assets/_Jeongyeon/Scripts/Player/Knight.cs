using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Character
{
    [SerializeField]
    private CharacterDataSet characterData;
    public CharacterDataSet CharacterData { get { return characterData; } }

    public override void Awake()
    {
        base.Awake();
        inventory.GetItemValues(hp: CharacterData.MaxHp, meleeDamage: CharacterData.MeleeDamage, defense: CharacterData.Defense);
        myData = inventory.myItemData;
        currentHp = myData.hp;
        moveSpeed = CharacterData.MoveSpeed;
    }
    public override void Update()
    {
        if (isGameStart == true)
        {
            base.Update();
        }
    }
    public void OnEnable()
    {
        myData = inventory.myItemData;
        moveSpeed += myData.moveSpeed / 100;
    }
    public override void Hit(float damage)
    {
        float finalDamage = damage - myData.defense / 20;
        if (damage - inventory.myItemData.defense / 20 == 0)
        {
            finalDamage = 1;
        }
        currentHp -= finalDamage;

        CDamageTextPoolManager.Instance.SpawnPlayerText(transform, finalDamage);
    }

    public void StartGame()
    {
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;
        isGameStart = true;
    }

}
