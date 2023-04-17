using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHealEvent : _Event
{
    [SerializeField] private ArSOList ArList;
    [SerializeField] private int HealPercent;

    public List<ArSO> Ars;

    public override void EventStart()
    {
        GetIsTakeArs();
    }

    public override void Reward()
    {
        HealAll(HealPercent);
    }

    void GetIsTakeArs()
    {
        foreach(ArSO ar in ArList.list)
        {
            if (!ar.isInGameTake) continue;
            Ars.Add(ar);
        }
    }

    void HealAll(int HealPercent)
    {
        float percent = (float)HealPercent / 100;
        float HP = 0f;

        foreach (ArSO Ar in Ars)
        {
            HP = Ar.surviveStats.currentHP;
            HP += Ar.surviveStats.MaxHP * percent;
            HP = Mathf.Clamp(HP, 0, Ar.surviveStats.MaxHP);
            Ar.surviveStats.currentHP = HP;
        }
    }
}
