using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShop : Shop
{
    public ItemSO[] items;
    public GameObject itemSlotObj;

    [SerializeField] private Transform ContentTransform;
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
        ShopBuySlot[] trash = FindObjectsOfType<ShopBuySlot>();

        for(int num = 0; num < trash.Length; num++) Destroy(trash[num]);
    }

    void ShowItemContent()
    {
        for (int i = 0; i < itemCount; i++)
        {
            var tempObj = itemSlotObj;
            itemSlotObj.GetComponent<ShopBuySlot>().SetItem(items[i]);
            tempObj = Instantiate(tempObj, ContentTransform);
            Debug.Log(itemIndex);
        }
    }
}
