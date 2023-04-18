using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUIInfo : MonoSingleton<ItemUIInfo>
{
    [SerializeField] private List<TextMeshProUGUI> Stat_Texts;
    [SerializeField] private List<Transform> AllImages;
    [SerializeField] private Image _Image;

    private void Awake()
    {
        AllTransformActiveSelf(false);
    }

    internal void UpdateUI(ItemSO Item)
    {
        if(Item.itemName == "EMPTY")
        {
            AllTransformActiveSelf(false);
            return;
        }
        AllTransformActiveSelf(true);

        Stat_Texts[0].text = Item.stat.MaxHP.ToString();
        Stat_Texts[1].text = Item.stat.MaxSP.ToString();
        Stat_Texts[2].text = Item.stat.ATK.ToString();
        Stat_Texts[3].text = Item.stat.SATK.ToString();
        Stat_Texts[4].text = Item.stat.CriPer.ToString();
        Stat_Texts[5].text = Item.stat.CriDmg.ToString();
        Stat_Texts[6].text = Item.stat.WEIGHT.ToString();
        Stat_Texts[7].text = Item.itemName;
        Stat_Texts[8].text = Item.itemExplain;
        _Image.sprite = Item.itemIcon;
    }

    internal void UpdateUI_AR(ArSO ar)
    {
        AllTransformActiveSelf(true);

        _Image.sprite = ar.characterInfo.Image;
        Stat_Texts[0].text = ar.surviveStats.MaxHP.ToString();
        Stat_Texts[1].text = ar.surviveStats.MaxShield.ToString();
        Stat_Texts[2].text = ar.attackStats.currentAtk.ToString();
        Stat_Texts[3].text = ar.attackStats.currentSkillAtk.ToString();
        Stat_Texts[4].text = ar.criticalStats.currentCriticalPer.ToString();
        Stat_Texts[5].text = ar.criticalStats.currentCriticalDamage.ToString();
        Stat_Texts[6].text = ar.surviveStats.currentWeight.ToString();
        Stat_Texts[7].text = ar.characterInfo.Name;
        Stat_Texts[8].text = ar.Summary;
    }

    void AllTransformActiveSelf(bool isActive)
    {
        for(int num = 0; num < AllImages.Count; num++)
            AllImages[num].gameObject.SetActive(isActive);
    }
}
