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

    internal void Buy() 
    {
        //�� �ִ��� Ȯ�� //�������� ���� //�����̰� ���ְ���?
        if (!GameShop_Inventory.Instance.CanAddItem()) return;
        GameShop_Inventory.Instance.SetItem(item as ItemSO);
        Debug.Log("����");
        Destroy(this.gameObject);
    }
}
