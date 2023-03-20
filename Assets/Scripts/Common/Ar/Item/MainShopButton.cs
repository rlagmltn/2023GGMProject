using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainShopButton : MonoBehaviour
{
    private ItemSO Item;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI aboutText;
    [SerializeField] private Image ItemFrameImage;

    public void UpdateAboutUI()
    {
        aboutText.text = Item.itemExplain;
        ItemFrameImage.sprite = Item.itemIcon;
        ItemFrameImage.color = new Color(1, 1, 1, 1);
    }

    public void UpdateUI()
    {
        itemIcon.sprite = Item.itemIcon;
        NameText.SetText(Item.itemName);
        PriceText.SetText(Item.itemPrice.ToString());
    }

    public void SetItemSO(ItemSO item)
    {
        Item = item;
    }

    public ItemSO GetItemSO()
    {
        return Item;
    }

}
