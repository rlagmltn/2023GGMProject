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
            ItemImage.sprite = null;
            return;
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

    internal int GetNum()
    {
        return ButtonNum;
    }
}
