using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : Shop
{
    [SerializeField] private ArSOArray arInven;
    [SerializeField] private ArSOArray arHolder;
    [SerializeField] private ItemDBSO itemInven;

    public void TakeAr()
    {
        var newAr = false;
        while(!newAr)
        {
            var rand = Random.Range(0, arHolder.list.Count);
            if (arHolder.list[rand] == null) continue;

            Debug.Log(arHolder.list[rand].name);
            arInven.list.Add(arHolder.list[rand]);
            arHolder.list.RemoveAt(rand);
            newAr = true;
        }
    }

    public void TakeItem()
    {
        itemInven.AddItemsInItems(GetRandomItems(1));
    }

    public void TakeHeal()
    {
        foreach(ArSO ar in arInven.list)
        {
            ar.ArData.so.surviveStats.currentHP = Mathf.Clamp(ar.ArData.so.surviveStats.currentHP + 
                (ar.ArData.so.surviveStats.MaxHP / 10 * 3), 0, ar.ArData.so.surviveStats.MaxHP);
        }
    }
}
