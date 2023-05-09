using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : Shop
{
    [SerializeField] private ItemDBSO itemInven;
     
    [SerializeField] private Transform TakeArPannel;
    [SerializeField] private Transform TakeItemPannel;
    [SerializeField] private Transform TakeHealPannel;

    public void TakeAr()
    {
        ArInventoryManager.Instance.HolderToInven(Random.Range(0, ArInventoryManager.Instance.HolderList.Count));
        StageClear();
    }

    public void TakeItem()
    {
        itemInven.AddItemsInItems(GetRandomItems(1));
        StageClear();
    }

    public void TakeHeal()
    {
        foreach(ArSO ar in ArInventoryManager.Instance.InvenList)
        {
            ar.ArData.so.surviveStats.currentHP = Mathf.Clamp(ar.ArData.so.surviveStats.currentHP + 
                (ar.ArData.so.surviveStats.MaxHP / 10 * 3), 0, ar.ArData.so.surviveStats.MaxHP);
        }
        StageClear();
    }

    void StageClear() //이거 나중에 버튼눌렀을때 실행되도록 바꿔야함
    {
        if(!Global.isEventBattle)
        {
            Global.EnterStage.IsCleared = true;
            SceneMgr.Instance.LoadScene(eSceneName.Map);
            return;
        }

        Global.isEventBattle = false;
        SceneMgr.Instance.LoadScene(eSceneName.Event);
        return;
    }
}
