using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Image ItemImage;
    private ItemSO Item;
    private int ButtonNum;

    internal void UpdateImage()
    {
        if (Item.itemName == "EMPTY")
        {
            ItemImage.sprite = null;
            return;
        }

        ItemImage.sprite = Item.itemIcon;
    }

    internal void SetItem(ItemSO Item, int num)
    {
        this.Item = Item;
        ButtonNum = num;
        UpdateImage();
    }

    internal ItemSO GetItem()
    {
        return Item;
    }

    internal void EquipItemToCharacter()
    {
        GameShop_Inventory.Instance.InventoryClear(ButtonNum);
    }

    internal int GetNum()
    {
        return ButtonNum;
    }
}
