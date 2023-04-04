using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ArSO", menuName = "SO/ArSO")]
public class ArSO : ScriptableObject
{
    public CharacterInfo characterInfo;

    public AttackStat attackStats;

    public CriticalStat criticalStats;

    public SurviveStat surviveStats;

    public Skill skill;

    public bool isTake = false;
    public bool isUse = false;

    public Player ArData;

    [Multiline(5)]
    public string Summary;

    public void ResetAll()
    {
        characterInfo.Reset();
        attackStats.Reset();
        criticalStats.Reset();
        surviveStats.Reset();
        skill.Reset();
    }
}

[System.Serializable]
public class CharacterInfo
{
    [SerializeField] private Sprite image;
    public Sprite Image => image;

    [SerializeField] private string nameString;
    public string Name => nameString;

    [SerializeField] private ArRarity Rarity;
    public ArRarity rarity => Rarity;

    [SerializeField] private ArClasses arClass;
    public ArClasses ArClass => arClass;
     
    public int Level;

    public void Reset()
    {
        Level = 1;
    }
}

[System.Serializable]
public class AttackStat
{
    [SerializeField] private float baseAtk;
    public float BaseAtk => baseAtk;

    public float currentAtk;

    [SerializeField] private float baseSkillAtk;
    public float BaseSkillAtk => baseSkillAtk;

    public float currentSkillAtk;

    public void Reset()
    {
        currentAtk = baseAtk;
        currentSkillAtk = baseSkillAtk;
    }
}

[System.Serializable]
public class CriticalStat
{
    [SerializeField] private float baseCriticalPer;
    public float BaseCriticalPer => baseCriticalPer;

    public float currentCritalPer;

    [SerializeField] private float baseCriticalDamage;
    public float BaseCriticalDamage => baseCriticalDamage;

    public float currentCriticalDamage;

    public void Reset()
    {
        currentCritalPer = baseCriticalPer;
        currentCriticalDamage = baseCriticalDamage;
    }
}

[System.Serializable]
public class SurviveStat
{
    [SerializeField] private float baseHP;
    public float BaseHP => baseHP;

    public float MaxHP;

    public float currentHP;

    [SerializeField] private float baseShield;
    public float BaseShield => baseShield;

    public float MaxShield;

    public float currentShield;

    [SerializeField] private float baseWeight;
    public float BaseWeight => baseWeight;

    public float currentWeight;

    public void Reset()
    {
        MaxHP = baseHP;
        currentHP = baseHP;
        MaxShield = baseShield;
        currentShield = baseShield;
        currentWeight = baseWeight;
    }
}

[System.Serializable]
public class Skill
{
    [SerializeField] private Sprite skillImage;
    public Sprite SkillImage => skillImage;

    [SerializeField] private string skillName;
    public string SkillName => skillName;

    [SerializeField] private int skillCoolTime;
    public int SkillCoolTime => skillCoolTime;

    public int MaxSkillCoolTime;

    public int currentSkillCoolTime;

    public string SkillSummary;

    public void Reset()
    {
        MaxSkillCoolTime = skillCoolTime;
        currentSkillCoolTime = skillCoolTime;
    }
}
