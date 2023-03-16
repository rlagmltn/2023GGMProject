using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuySlot : MonoBehaviour
{
    Stat UpgradingStat;

    [SerializeField] Sprite iconSprite;
    [SerializeField] Text nameText;
    [SerializeField] Text priceText;
    [SerializeField] Text infoText;

    public Sprite icon;
    public string Itemname;
    [SerializeField] int itemPrice;
    [SerializeField] string itemInfo;

    private void Start()
    {
        UpgradingStat = new Stat();
        iconSprite = icon;
        priceText = GetComponentInChildren<Text>();
        iconSprite = GetComponentInChildren<Sprite>();
    }
}
