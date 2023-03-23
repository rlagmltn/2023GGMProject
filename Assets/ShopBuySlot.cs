using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopBuySlot : MonoBehaviour
{
    public ItemSO item;
    [SerializeField] Image itemImage;
    [SerializeField] Sprite iconSprite;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI infoText;

    internal void Init()
    {
        itemImage.sprite = item.itemIcon;
        nameText.text = string.Format(item.itemName);
        priceText.text = string.Format(item.itemPrice.ToString());
        infoText.text = item.itemExplain;
    }
}
