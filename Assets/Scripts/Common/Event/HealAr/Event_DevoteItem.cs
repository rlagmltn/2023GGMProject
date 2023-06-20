using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Event_DevoteItem : MonoSingleton<Event_DevoteItem>
{
    [SerializeField] private Image ItemImage;
    [SerializeField] private TextMeshProUGUI ItemNameText;
    [SerializeField] private Transform DetailText;
    [SerializeField] private Button DevoteButton;

    [SerializeField] private Transform BackGroundPannel;
    [SerializeField] private Transform ClearPannel;

    [SerializeField] private ItemDBSO InventorySO;
    [SerializeField] private ItemSO EmptySO;
    [SerializeField] private ArSOList ArList;
    [SerializeField] private float healPercent;

    private ItemSO Item;
    private int ItemNum;

    private void Start()
    {
        Info_ActiveSelf(false);
        ButtonInit();
    }

    void ButtonInit()
    {
        DevoteButton.onClick.RemoveAllListeners();
        DevoteButton.onClick.AddListener(DevoteButtonClicked);
    }

    public void SetItem(ItemSO _Item, int num)
    {
        if (_Item.itemName == "EMPTY") return;

        Item = _Item;
        ItemNum = num;

        //Debug.Log($"{Item.itemName}, {ItemNum}");

        UpdateUI();
    }

    void UpdateUI()
    {
        Info_ActiveSelf(false);
        Info_ActiveSelf(true);

        ItemImage.sprite = Item.itemIcon;
        ItemNameText.text = Item.itemName;
    }

    void Info_ActiveSelf(bool isActive)
    {
        ItemImage.gameObject.SetActive(isActive);
        DetailText.gameObject.SetActive(isActive);
        ItemNameText.gameObject.SetActive(isActive);
        DevoteButton.gameObject.SetActive(isActive);
    }

    void DevoteButtonClicked()
    {
        DevoteItem();

        ClearPannel.gameObject.SetActive(true);
        BackGroundPannel.gameObject.SetActive(true);
        HealPercent(healPercent);
    }

    void DevoteItem()
    {
        InventorySO.items[ItemNum] = EmptySO;
    }

    void HealPercent(float percent)
    {
        for(int i = 0; i < ArList.list.Count; i++)
        {
            if (!ArList.list[i].isInGameTake) continue;
            if (ArList.list[i].isDead) continue;
            ArList.list[i].surviveStats.currentHP = (int)((float)ArList.list[i].surviveStats.MaxHP * (percent / 100f));
        }
    }
}
