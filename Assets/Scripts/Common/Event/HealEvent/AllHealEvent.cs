using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllHealEvent : _Event
{
    [SerializeField] private ArSOList ArList;
    [SerializeField] private int HealPercent;
    [SerializeField] private Transform HealPannel;

    [SerializeField] private Button OneHealBtn;
    [SerializeField] private Button AllHealBtn;

    [SerializeField] private Transform BackGroundPannel;
    [SerializeField] private Transform ArInventory;

    private List<ArSO> Ars;
    private bool isHealAll = false;

    public override void EventStart()
    {
        GetIsTakeArs();
        EventInit();
    }

    void EventInit()
    {
        HealPannel.gameObject.SetActive(true);
        ButtonInit();
    }

    void ButtonInit()
    {
        OneHealBtn.onClick.RemoveAllListeners();
        AllHealBtn.onClick.RemoveAllListeners();

        OneHealBtn.onClick.AddListener(Active_InvenPannel_True);
        AllHealBtn.onClick.AddListener(()=>HealAll(HealPercent));
    }

    void GetIsTakeArs()
    {
        Ars = new List<ArSO>();
        foreach (ArSO ar in ArList.list)
        {
            if (!ar.isInGameTake) continue;
            Ars.Add(ar);
        }
    }

    void Active_InvenPannel_True()
    {
        BackGroundPannel.gameObject.SetActive(true);
        ArInventory.gameObject.SetActive(true);
    }

    public void HealFull(ArSO Ar)
    {
        Ar.surviveStats.currentHP = Ar.surviveStats.MaxHP;
        EventManger.Instance.EventClear();
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

        EventManger.Instance.EventClear();
    }
}
