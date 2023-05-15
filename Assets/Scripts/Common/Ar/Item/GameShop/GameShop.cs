using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameShop : Shop
{
    public static GameShop Instance;

    public ItemSO[] items;
    public GameObject itemSlotObj;

    [SerializeField] private Transform ContentTransform;
    [SerializeField] int itemCount = 5;

    [SerializeField] private Transform purchasePannel;
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button CancleButton;
    [SerializeField] private Transform BackGroundPannel;

    private ItemSO Item;
    private Transform Item_transform;

    void Start()
    {
        Instance = this;
        Init();
        items = GetRandomItems(itemCount);
        ShowItemContent();
    }

    void Init()
    {
        ShopBuySlot[] trash = FindObjectsOfType<ShopBuySlot>();

        for(int num = 0; num < trash.Length; num++) Destroy(trash[num]);
    }

    void ShowItemContent()
    {
        for (int i = 0; i < itemCount; i++)
        {
            var tempObj = Instantiate(itemSlotObj, ContentTransform);
            tempObj.GetComponent<ShopBuySlot>().Generate();
            tempObj.GetComponent<ShopBuySlot>().SetItem(items[i]);
            tempObj.GetComponent<Button>().onClick.RemoveAllListeners();
            tempObj.GetComponent<Button>().onClick.AddListener(tempObj.GetComponent<ShopBuySlot>().SelectedItem);
        }

        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(Buy);
        CancleButton.onClick.RemoveAllListeners();
        CancleButton.onClick.AddListener(() => ActivePurchasePopUp(false));
    }

    internal void SelectedItem(ItemSO S_Item, Transform trans)
    {
        Item = S_Item;
        Item_transform = trans;

        ActivePurchasePopUp(true);
    }

    private void ActivePurchasePopUp(bool isActive)
    {
        purchasePannel.gameObject.SetActive(isActive);
        BackGroundPannel.gameObject.SetActive(isActive);

        if (!isActive) return;

        purchaseText.text = $"{Item.itemName}을 {Item.itemPrice}에 구매하시겠습니까?";
    }

    private void Buy()
    {
        if (!GameShop_Inventory.Instance.CanAddItem()) return;
        GameShop_Inventory.Instance.SetItem(Item);
        Item_transform.gameObject.SetActive(false);

        ActivePurchasePopUp(false);
    }
}
