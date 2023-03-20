using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShop : Shop
{
    ItemSO[] items;
    GameObject[] itemSlotObjs;
    [SerializeField] int itemCount;

    void Start()
    {
        items = GetRandomItems(itemCount);
        Init();
        ShowItemContent();
    }

    void Init()
    {
        var trash = FindObjectsOfType<ShopBuySlot>();
        foreach (var t in trash)
        {
            Destroy(t.gameObject);
        }
    }

    void ShowItemContent()
    {
        for (int i = 0; i < itemCount; i++)
        {
            ShopBuySlot itemSlot = new ShopBuySlot();
            itemSlot.item = items[i];
            itemSlotObjs[i] = Instantiate(itemSlot.gameObject);
            itemSlotObjs[i].transform.SetParent(this.transform);
        }
    }
}
