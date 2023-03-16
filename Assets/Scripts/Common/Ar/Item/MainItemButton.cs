using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainItemButton : MonoBehaviour
{
    private Image itemIcon;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemCost;
    private ItemSO item;

    public void SettingInfo(ItemSO item)
    {
        this.item = item;
        itemIcon = transform.Find("ItemImage").GetComponent<Image>();
        itemName = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        itemCost = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();

        itemIcon.sprite = item.itemIcon;
        itemName.SetText(item.itemName);
        itemCost.SetText(item.itemPrice.ToString());
    }

    public ItemSO GetItemSO()
    {
        return item;
    }

}
