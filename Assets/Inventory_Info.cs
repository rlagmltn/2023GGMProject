using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_Info : MonoSingleton<Inventory_Info>
{
    [SerializeField] private Image ItemImage;
    [SerializeField] private TextMeshProUGUI ItemNameText;
    [SerializeField] private TextMeshProUGUI ItemSummaryText;
    [SerializeField] private Transform DetailImage;
    [SerializeField] private List<Transform> ItemStats;

    [SerializeField] private Sprite StatImage_HP;
    [SerializeField] private Sprite Statimage_SP;
    [SerializeField] private Sprite StatImage_Atk;
    [SerializeField] private Sprite StatImage_Satk;
    [SerializeField] private Sprite StatImage_CriticalPercent;
    [SerializeField] private Sprite StatImage_CriticalDamage;
    [SerializeField] private Sprite StatImage_Weight;

    public List<SpriteAndNum> Stats;

    private ItemSO Item;
    private Stat ItemStat;

    [SerializeField] private ItemSO _ItemSO;

    private void Start()
    {
        Info_ActiveSelf(false);
    }

    public void SetItem(ItemSO _Item)
    {
        if (_Item.itemName == "EMPTY") return;

        Item = _Item;
        ItemStat = Item.stat;

        ReturnImage();
        UpdateUI();
    }

    void UpdateUI()
    {
        Info_ActiveSelf(false);
        Info_ActiveSelf(true);
        StatPannelAcitve();

        ItemImage.sprite = Item.itemIcon;
        ItemNameText.text = Item.itemName;
        ItemSummaryText.text = Item.itemExplain;
    }

    void Info_ActiveSelf(bool isActive)
    {
        ItemImage.gameObject.SetActive(isActive);
        ItemNameText.gameObject.SetActive(isActive);
        ItemSummaryText.gameObject.SetActive(isActive);
        DetailImage.gameObject.SetActive(isActive);

        for (int num = 0; num < ItemStats.Count; num++)
        {
            ItemStats[num].gameObject.SetActive(isActive);
        }
    }

    void StatPannelAcitve()
    {
        for (int num = 0; num < ItemStats.Count; num++)
        {
            ItemStats[num].gameObject.SetActive(false);
        }

        for (int num = 0; num < Stats.Count; num++)
        {
            ItemStats[num].gameObject.SetActive(true);
            ItemStats[num].GetChild(0).GetComponent<Image>().sprite = Stats[num].StatIcon;
            ItemStats[num].GetChild(1).GetComponent<TextMeshProUGUI>().text = ($"+ {Stats[num].StatNum}");
        }
    }

    void ReturnImage()
    {
        Stats.Clear();

        if (ItemStat.ATK != 0)
        {
            SpriteAndNum SAN = new SpriteAndNum();
            SAN.StatIcon = StatImage_Atk;
            SAN.StatNum = ItemStat.ATK;
            Stats.Add(SAN);
        }


        if (ItemStat.SATK != 0)
        {
            SpriteAndNum SAN = new SpriteAndNum();
            SAN.StatIcon = StatImage_Satk;
            SAN.StatNum = ItemStat.SATK;
            Stats.Add(SAN);
        }

        if (ItemStat.MaxSP != 0)
        {
            SpriteAndNum SAN = new SpriteAndNum();
            SAN.StatIcon = Statimage_SP;
            SAN.StatNum = ItemStat.SP;
            Stats.Add(SAN);
        }

        if (ItemStat.MaxHP != 0)
        {
            SpriteAndNum SAN = new SpriteAndNum();
            SAN.StatIcon = StatImage_HP;
            SAN.StatNum = ItemStat.MaxHP;
            Stats.Add(SAN);
        }

        if (ItemStat.CriPer != 0)
        {
            SpriteAndNum SAN = new SpriteAndNum();
            SAN.StatIcon = StatImage_CriticalPercent;
            SAN.StatNum = ItemStat.CriPer;
            Stats.Add(SAN);
        }

        if (ItemStat.CriDmg != 0)
        {
            SpriteAndNum SAN = new SpriteAndNum();
            SAN.StatIcon = StatImage_CriticalDamage;
            SAN.StatNum = (int)ItemStat.CriDmg;
            Stats.Add(SAN);
        }

        if (ItemStat.WEIGHT != 0)
        {
            SpriteAndNum SAN = new SpriteAndNum();
            SAN.StatIcon = StatImage_Weight;
            SAN.StatNum = (int)ItemStat.WEIGHT;
            Stats.Add(SAN);
        }
    }
}
