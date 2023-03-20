using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuySlot : MonoBehaviour
{
    public ItemSO item;
    [SerializeField] Image itemImage;
    [SerializeField] Sprite iconSprite;
    [SerializeField] Text nameText;
    [SerializeField] Text priceText;
    [SerializeField] Text infoText;

    private void Start()
    {
        itemImage.sprite = item.itemIcon;
        nameText.text = string.Format(item.itemName);
        priceText.text = string.Format(item.itemPrice.ToString());
        infoText.text = item.itemExplain;
    }

}
