using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/CharacterInfo")]
public class CharacterDataSet : ScriptableObject
{
    [SerializeField]
    private string playerName;
    public string PlayerName { get { return name; } }

    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField]
    private float maxHp;
    public float MaxHp { get { return maxHp; } }
    [SerializeField]
    private float damage;
    public float Damage { get { return damage; } }
    [SerializeField]
    private float meleeDamage;
    public float MeleeDamage { get { return meleeDamage; } }
    [SerializeField]
    private float rangeDamage;
    public float RangeDamage { get { return rangeDamage; } }
    [SerializeField]
    private float criticalRate;
    public float CriticalRate { get { return criticalRate; } }
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }
    [SerializeField]
    private float attackRange;
    public float AttackRange { get { return attackRange; } }
    [SerializeField]
    private float massValue;
    public float MassValue { get { return massValue; } }
    [SerializeField]
    private float bloodDrain;
    public float BloodDrain { get { return bloodDrain; } }
    [SerializeField]
    private float defense;
    public float Defense { get { return defense; } }
    [SerializeField]
    private float luck;
    public float Luck { get { return luck; } }
    [SerializeField]
    private float moneyRate;
    public float MoneyRate { get { return moneyRate; } }
    [SerializeField]
    private float expRate;
    public float ExpRate { get { return expRate; } }



}
