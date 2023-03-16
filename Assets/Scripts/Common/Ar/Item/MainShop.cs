using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShop : MonoSingleton<MainShop>
{
    [SerializeField] MainItemButton[] buttons;
    [SerializeField] int shopItemCount;
    private Shop shop;
    private ItemSO[] shopItems;
    public ItemSO selectItem;

    private void Awake()
    {
        shop = GetComponent<Shop>();
        shopItems = new ItemSO[shopItemCount];
    }

    void Start()
    {
        int count = 0;

        shopItems = shop.GetRandomItems(shopItemCount);

        foreach(ItemSO item in shopItems)
            buttons[count++].SettingInfo(item);
    }

    public void GetItemSO(ItemSO item)
    {
        selectItem = item;
    }
}
