using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Event_EnhanceItem_2 : MonoSingleton<Event_EnhanceItem_2>
{
    [SerializeField] private Image ItemImage;
    [SerializeField] private TextMeshProUGUI ItemNameText;
    [SerializeField] private Transform DetailText;
    [SerializeField] private Button EnhanceButton;

    [SerializeField] private Transform BackGroundPannel;
    [SerializeField] private Transform ClearPannel;

    [SerializeField] private ItemDBSO InventorySO;
    [SerializeField] private ItemSO EmptySO;
    [SerializeField] private ArSOList ArList;

    private ItemSO Item;
    private int ItemNum;

    private void Start()
    {
        Info_ActiveSelf(false);
        ButtonInit();
    }

    void ButtonInit()
    {
        EnhanceButton.onClick.RemoveAllListeners();
        EnhanceButton.onClick.AddListener(DevoteButtonClicked);
    }

    public void SetItem(ItemSO _Item, int num)
    {
        if (_Item.itemName == "EMPTY") return;

        Item = _Item;
        ItemNum = num;

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
        EnhanceButton.gameObject.SetActive(isActive);
    }

    void DevoteButtonClicked()
    {
        DevoteItem();

        ClearPannel.gameObject.SetActive(true);
        BackGroundPannel.gameObject.SetActive(true);
    }

    void DevoteItem()
    {
        InventorySO.items[ItemNum] = EmptySO;
    }
}
