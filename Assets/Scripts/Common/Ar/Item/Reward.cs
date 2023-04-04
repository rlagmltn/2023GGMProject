using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : Shop
{
    [SerializeField] private ItemDBSO itemInven;

    public void TakeAr()
    {
        ArInventoryManager.Instance.HolderToInven(Random.Range(0, ArInventoryManager.Instance.HolderList.Count));
    }

    public void TakeItem()
    {
        itemInven.AddItemsInItems(GetRandomItems(1));
    }

    public void TakeHeal()
    {
        foreach(ArSO ar in ArInventoryManager.Instance.InvenList)
        {
            ar.ArData.so.surviveStats.currentHP = Mathf.Clamp(ar.ArData.so.surviveStats.currentHP + 
                (ar.ArData.so.surviveStats.MaxHP / 10 * 3), 0, ar.ArData.so.surviveStats.MaxHP);
        }
    }
}
