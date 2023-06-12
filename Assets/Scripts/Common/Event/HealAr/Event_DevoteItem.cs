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
        ClearPannel.gameObject.SetActive(true);
        BackGroundPannel.gameObject.SetActive(true);
    }
}
