using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShop : Shop
{
    public ItemSO[] items;
    public GameObject itemSlotObj;
    public Dictionary<int, ShopBuySlot> itemSlotObjs;
    [SerializeField] int itemCount = 5;
    private int itemIndex = 0;

    void Start()
    {
        Init();
        items = GetRandomItems(itemCount);
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
            var tempObj = itemSlotObj;
            itemSlotObj.GetComponent<ShopBuySlot>().item = items[i];
            itemSlotObj.GetComponent<ShopBuySlot>().Init();
            tempObj = Instantiate(tempObj, transform);
            itemSlotObjs.Add(itemCount++, tempObj.GetComponent<ShopBuySlot>());
        }
    }
}
