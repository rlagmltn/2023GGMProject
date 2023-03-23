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
    public int MaxHP; // �ִ�ü��
    public int HP; // ���� ü��
    public int MaxDP; // �ִ����
    public int DP; // ���� ����
    public int ATK; // �⺻ ���ݷ�
    public int SATK; // ��ų���ݷ�
    public int WEIGHT; // ����

    public Stat()
    {
        MaxHP = 0;
        HP = 0;
        MaxDP = 0;
        DP = 0;
        ATK = 0;
        SATK = 0;
        WEIGHT = 0;
    }

    // ������ �����ε�
    public static Stat operator +(Stat a, Stat b)
    {
        Stat result = new Stat();
        result.MaxHP = a.MaxHP + b.MaxHP;
        result.HP = a.HP + b.HP;
        result.MaxDP = a.MaxDP + b.MaxDP;
        result.DP = a.DP + b.DP;
        result.ATK = a.ATK + b.ATK;
        result.SATK = a.SATK + b.SATK;
        result.WEIGHT = a.WEIGHT + b.WEIGHT;
        return result;
    }

    public static Stat operator -(Stat a, Stat b)
    {
        Stat result = new Stat();
        result.MaxHP = a.MaxHP - b.MaxHP;
        result.HP = a.HP - b.HP;
        result.MaxDP = a.MaxDP - b.MaxDP;
        result.DP = a.DP - b.DP;
        result.ATK = a.ATK - b.ATK;
        result.SATK = a.SATK - b.SATK;
        result.WEIGHT = a.WEIGHT - b.WEIGHT;
        return result;
    }
}
