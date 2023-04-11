using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainShopButton : MonoBehaviour
{
    private MainShopItem m_Item;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI aboutText;
    [SerializeField] private Image ItemFrameImage;

    public void UpdateAboutUI()
    {
        aboutText.text = m_Item.Item.itemExplain;
        ItemFrameImage.sprite = m_Item.Item.itemIcon;
        ItemFrameImage.color = new Color(1, 1, 1, 1);

        MainShop.Instance.SetSelcetSO(m_Item);
    }

    public void UpdateUI()
    {
        itemIcon.sprite = m_Item.Item.itemIcon;
        NameText.SetText(m_Item.Item.itemName);
        PriceText.SetText(m_Item.Item.itemPrice.ToString());

        var btn = this.gameObject.GetComponent<Button>();
        btn.interactable = true;
        if (m_Item.isBuy) btn.interactable = false;
    }

    public void SetItemSO(MainShopItem item)
    {
        m_Item = item;
    }

    public MainShopItem GetItemSO()
    {
        return m_Item;
    }

}
