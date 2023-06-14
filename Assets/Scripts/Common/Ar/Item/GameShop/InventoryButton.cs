using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Image ItemImage;
    public ItemSO Item;
    public int ButtonNum;

    internal void UpdateImage()
    {
        if (Item.itemName == "EMPTY")
        {
            ItemImage.color = Color.clear;
            return;
        }
        else
        {
            ItemImage.color = Color.white;
        }

        ItemImage = transform.GetChild(0).GetComponent<Image>();
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

    internal void Map_EquipItemToCharacter()
    {
        Map_Inventory.Instance.InventoryClear(ButtonNum);
    }

    internal int GetNum()
    {
        return ButtonNum;
    }

    internal void ButtonClicked()
    {
        Map_Inventory_Info.Instance.SetItem(Item);
        Event_DevoteItem.Instance.SetItem(Item, ButtonNum);
        Event_EnhanceItem_2.Instance.SetItem(Item, ButtonNum);
    }
}
