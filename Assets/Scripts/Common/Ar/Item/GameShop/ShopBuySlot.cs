using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopBuySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Sprite iconSprite;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI infoText;

    private int price;
    private ItemSO item;

    internal void Generate()
    {
        item = new ItemSO();
    }

    private void Init()
    {
        gameObject.SetActive(true);
        itemImage.sprite = item.itemIcon;
        nameText.text = string.Format(item.itemName);
        priceText.text = string.Format(item.itemPrice.ToString());
        infoText.text = item.itemExplain;

        price = item.itemPrice;
    }

    internal void SetItem(ItemSO _Item)
    {
        item = _Item;
        Init();
    }

    internal int GetItemPrice()
    {
        return price;
    }

    internal void SelectedItem()
    {
        GameShop.Instance.SelectedItem(item, this.transform);
    }

    internal void Buy() 
    {
        //돈 있는지 확인 //돈없으면 리턴 //상현이가 해주겠지? //에반데
        if (!GoldManager.Instance.RemoveGold(price)) return;
        if (!GameShop_Inventory.Instance.CanAddItem()) return;
        GameShop_Inventory.Instance.SetItem(item as ItemSO);
        gameObject.SetActive(false);
    }
}
