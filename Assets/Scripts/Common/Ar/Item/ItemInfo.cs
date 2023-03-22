using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemPassiveType:int
{
    None,
    BeforeCrash,
    AfterCrash,
    BeforeAttack,
    AfterAttack,
    BeforeDefence,
    AfterDefence,
    AfterMove,
    OnOutDie,
    OnBattleDie,
    MouseUp,
    Alway
}

public class ItemInfo : MonoBehaviour
{
    [System.NonSerialized]
    public Player player;

    public virtual void Passive() { }

    public void GetPlayer(Player player = null)
    {
        this.player = player;
    }
}

[System.Serializable]
public class Stat
{
    public float MaxHP; // 최대체력
    public float HP; // 현재 체력
    public float ATK; // 기본 공격력
    public float SATK; // 스킬공격력
    public float DEF; // 방어력
    public float WEIGHT; // 무게

    public Stat()
    {
        MaxHP = 0;
        HP = 0;
        ATK = 0;
        SATK = 0;
        DEF = 0;
        WEIGHT = 0;
    }

    // 연산자 오버로딩
    public static Stat operator +(Stat a, Stat b)
    {
        Stat result = new Stat();
        result.MaxHP = a.MaxHP + b.MaxHP;
        result.HP = a.HP + b.HP;
        result.ATK = a.ATK + b.ATK;
        result.SATK = a.SATK + b.SATK;
        result.DEF = a.DEF + b.DEF;
        result.WEIGHT = a.WEIGHT + b.WEIGHT;
        return result;
    }

    public static Stat operator -(Stat a, Stat b)
    {
        Stat result = new Stat();
        result.MaxHP = a.MaxHP - b.MaxHP;
        result.HP = a.HP - b.HP;
        result.ATK = a.ATK - b.ATK;
        result.SATK = a.SATK - b.SATK;
        result.DEF = a.DEF - b.DEF;
        result.WEIGHT = a.WEIGHT - b.WEIGHT;
        return result;
    }
}
