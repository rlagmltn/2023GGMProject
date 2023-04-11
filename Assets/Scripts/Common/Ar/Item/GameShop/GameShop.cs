using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameShop : Shop
{
    public ItemSO[] items;
    public GameObject itemSlotObj;

    [SerializeField] private Transform ContentTransform;
    [SerializeField] int itemCount = 5;

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
            var tempObj = Instantiate(itemSlotObj, ContentTransform);
            tempObj.GetComponent<ShopBuySlot>().Generate();
            tempObj.GetComponent<ShopBuySlot>().SetItem(items[i]);
            tempObj.GetComponent<Button>().onClick.RemoveAllListeners();
            tempObj.GetComponent<Button>().onClick.AddListener(tempObj.GetComponent<ShopBuySlot>().Buy);
        }
    }
}
