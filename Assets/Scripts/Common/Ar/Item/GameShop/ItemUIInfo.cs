using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUIInfo : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> Stat_Texts;
    [SerializeField] private List<Transform> AllImages;

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

    }

    void AllTransformActiveSelf(bool isActive)
    {
        for(int num = 0; num < AllImages.Count; num++)
            AllImages[num].gameObject.SetActive(isActive);
    }
}
