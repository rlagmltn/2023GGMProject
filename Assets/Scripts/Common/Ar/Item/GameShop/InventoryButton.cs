using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Image ItemImage;
    private ItemSO Item;

    internal void UpdateImage()
    {
        if (Item.itemName == "EMPTY") return;

        ItemImage.sprite = Item.itemIcon;
    }

    internal void SetItem(ItemSO Item)
    {
        this.Item = Item;
        UpdateImage();
    }

    internal ItemSO GetItem()
    {
        return Item;
    }
}
