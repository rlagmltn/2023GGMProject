using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Reward : Shop
{
    [SerializeField] private ItemDBSO itemInven;
     
    [SerializeField] private Transform NomalWinPanel;
    [SerializeField] private Transform TakeArPannel;
    [SerializeField] private Transform TakeItemPannel;
    [SerializeField] private Transform TakeHealPannel;

    [SerializeField] private Transform BossWinPanel;
    //[SerializeField] private Transform TakeArPannel;
    //[SerializeField] private Transform TakeItemPannel;
    //[SerializeField] private Transform TakeHealPannel;

    public void TakeAr()
    {
        var newAr = ArInventoryManager.Instance.HolderToInven(Random.Range(0, ArInventoryManager.Instance.HolderList.Count));

        TakeArPannel.gameObject.SetActive(true);
        TakeArPannel.GetChild(0).GetComponent<Image>().sprite = BackGroundHolder.Instance.BackGround(newAr.characterInfo.rarity);
        TakeArPannel.GetChild(1).GetComponent<Image>().sprite = newAr.characterInfo.Image;
        TakeArPannel.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(newAr.characterInfo.Name);

        NomalWinPanel.gameObject.SetActive(false);
    }

    public void TakeItem()
    {
        var newItem = GetRandomItems(1);

        itemInven.AddItemsInItems(newItem);

        TakeItemPannel.gameObject.SetActive(true);
        TakeItemPannel.GetChild(0).GetComponent<Image>().sprite = BackGroundHolder.Instance.BackGround(newItem[0].itemRarity);
        TakeItemPannel.GetChild(1).GetComponent<Image>().sprite = newItem[0].itemIcon;
        TakeItemPannel.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(newItem[0].itemName);
        TakeItemPannel.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(newItem[0].itemExplain);

        NomalWinPanel.gameObject.SetActive(false);
    }

    public void TakeHeal()
    {
        TakeHealPannel.gameObject.SetActive(true);

        foreach (ArSO ar in ArInventoryManager.Instance.InvenList)
        {
            ar.ArData.so.surviveStats.currentHP = Mathf.Clamp(ar.ArData.so.surviveStats.currentHP + 
                (ar.ArData.so.surviveStats.MaxHP / 10 * 3), 0, ar.ArData.so.surviveStats.MaxHP);
        }

        NomalWinPanel.gameObject.SetActive(false);
    }

    public void StageClear() //이거 나중에 버튼눌렀을때 실행되도록 바꿔야함
    {
        if(!Global.isEventBattle)
        {
            Global.EnterStage.IsCleared = true;
            SceneManager.LoadScene("Stage1Map");
            return;
        }

        Global.isEventBattle = false;
        SceneMgr.Instance.LoadScene(eSceneName.Event);
        return;
    }
}
