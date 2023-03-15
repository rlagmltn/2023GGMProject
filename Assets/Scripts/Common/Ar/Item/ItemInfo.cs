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
}

public class ItemInfo : MonoBehaviour
{
    public Sprite itemIcon;
    public ItemPassiveType itemType;
    public Stat stat;
    private Player armedPlayer;

    public virtual void Passive(){  }

    public void Armed(Player player)
    {
        player.stat += stat;
        switch(itemType)
        {
            case ItemPassiveType.BeforeCrash:
                player.BeforeCrash.AddListener(Passive);
                break;
            case ItemPassiveType.AfterCrash:
                player.AfterCrash.AddListener(Passive);
                break;
            case ItemPassiveType.BeforeAttack:
                player.BeforeAttack.AddListener(Passive);
                break;
            case ItemPassiveType.AfterAttack:
                player.AfterAttack.AddListener(Passive);
                break;
            case ItemPassiveType.BeforeDefence:
                player.BeforeDefence.AddListener(Passive);
                break;
            case ItemPassiveType.AfterDefence:
                player.AfterDefence.AddListener(Passive);
                break;
            case ItemPassiveType.AfterMove:
                player.AfterMove.AddListener(Passive);
                break;
            case ItemPassiveType.OnOutDie:
                player.OnOutDie.AddListener(Passive);
                break;
            case ItemPassiveType.OnBattleDie:
                player.OnBattleDie.AddListener(Passive);
                break;
            case ItemPassiveType.MouseUp:
                player.MouseUp.AddListener(Passive);
                break;
            default:
                Debug.LogError("아이템 타입이 정해지지 않았습니다!");
                break;
        }
        armedPlayer = player;
    }

    public void UnArmed()
    {
        armedPlayer.stat -= stat;
        switch (itemType)
        {
            case ItemPassiveType.BeforeCrash:
                armedPlayer.BeforeCrash.RemoveListener(Passive);
                break;
            case ItemPassiveType.AfterCrash:
                armedPlayer.AfterCrash.RemoveListener(Passive);
                break;
            case ItemPassiveType.BeforeAttack:
                armedPlayer.BeforeAttack.RemoveListener(Passive);
                break;
            case ItemPassiveType.AfterAttack:
                armedPlayer.AfterAttack.RemoveListener(Passive);
                break;
            case ItemPassiveType.BeforeDefence:
                armedPlayer.BeforeDefence.RemoveListener(Passive);
                break;
            case ItemPassiveType.AfterDefence:
                armedPlayer.AfterDefence.RemoveListener(Passive);
                break;
            case ItemPassiveType.AfterMove:
                armedPlayer.AfterMove.RemoveListener(Passive);
                break;
            case ItemPassiveType.OnOutDie:
                armedPlayer.OnOutDie.RemoveListener(Passive);
                break;
            case ItemPassiveType.OnBattleDie:
                armedPlayer.OnBattleDie.RemoveListener(Passive);
                break;
            case ItemPassiveType.MouseUp:
                armedPlayer.MouseUp.RemoveListener(Passive);
                break;
            default:
                Debug.LogError("아이템 타입이 정해지지 않았습니다!");
                break;
        }
        armedPlayer = null;
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
