using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemySkillInfo")]
public class CEnemySkillInfo : ScriptableObject
{
    public string SkillName;
    public float Attack;
}