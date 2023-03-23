using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class MainShopItem
{
    //아이템
    public ItemSO Item;
    //구매여부
    public bool isBuy;
}
public class MainShop : MonoSingleton<MainShop>
{
    [SerializeField] private List<Button> buttons;
    //[SerializeField] MainItemButton[] buttons;
    [SerializeField] int shopItemCount;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Image ItemFrameImage;
    [SerializeField] private TextMeshProUGUI aboutText;

    [SerializeField] private List<Image> underImages;
    [SerializeField] private Sprite redSprite;
    [SerializeField] private Sprite commonSprite;

    [SerializeField] private Button purchaseButton;
    [SerializeField] private TextMeshProUGUI purchaseText;

    [SerializeField] private Button PurchaseOKButton;

    public Shop shop;
    public ItemSO[] shopItems;
    public List<MainShopItem> Items;
    public int pageNum = 1;

    public MainShopItem selectSO;

    private void Awake()
    {
        shop = GetComponent<Shop>();
        shopItems = new ItemSO[shopItemCount];
        shopItems = shop.GetRandomItems(shopItemCount);

        foreach (ItemSO Item in shopItems)
        {
            MainShopItem s_Item = new MainShopItem();
            s_Item.Item = Item;
            s_Item.isBuy = false;
            Items.Add(s_Item);
        }

        selectSO = new MainShopItem();
    }

    void Start()
    {
        ButtonInit();
        //Init();
    }

    public void Init()
    {
        PreviewPage();
        ResetMainShop();
        SelectSONullCheck();
    }

    public void ResetMainShop()
    {
        PreviewPage();
        ItemFrameImage.color = new Color(1, 1, 1, 0);
        aboutText.text = "";
    }

    void PageUpdate()
    {
        for (int num = buttons.Count * (pageNum - 1); num < buttons.Count * pageNum; num++)
        {
            buttons[(num % 3)].GetComponent<MainShopButton>().SetItemSO(Items[num]);
            buttons[(num % 3)].interactable = true;
            if (Items[num].isBuy) buttons[(num % 3)].interactable = false;
        }
    }

    private void ButtonInit()
    {
        RemoveButtonEvents(leftButton);
        RemoveButtonEvents(rightButton);

        foreach(Button btn in buttons)
        {
            RemoveButtonEvents(btn);
            AddButtonEvents(btn, () => btn.GetComponent<MainShopButton>().UpdateAboutUI());
        }

        AddButtonEvents(leftButton, PreviewPage);
        AddButtonEvents(rightButton, NextPage);
        AddButtonEvents(PurchaseOKButton, BuyItem);
    }

    private void NextPage()
    {
        pageNum++;
        pageNum = Mathf.Clamp(pageNum, 1, 2);
        PageUpdate();

        SetUnderImages();

        foreach (Button btn in buttons) btn.GetComponent<MainShopButton>().UpdateUI();
    }

    private void PreviewPage()
    {
        pageNum--;
        pageNum = Mathf.Clamp(pageNum, 1, 2);

        PageUpdate();
        SetUnderImages();

        foreach (Button btn in buttons) btn.GetComponent<MainShopButton>().UpdateUI();
    }

    private void SetUnderImages()
    {
        for (int i = 0; i < underImages.Count; i++)
            underImages[i].sprite = commonSprite;

        underImages[(pageNum - 1)].sprite = redSprite;
    }

    public void SetSelcetSO(MainShopItem item)
    {
        selectSO = item;
        PurchaseButtonClick();
        SelectSONullCheck();
    }

    private void SelectSONullCheck()
    {
        purchaseButton.interactable = true;
        if(selectSO == null)
        {
            purchaseButton.interactable = false;
            return;
        }
        else if (selectSO.Item == null)
            purchaseButton.interactable = false;
    }

    private void PurchaseButtonClick()
    {
        purchaseText.text = $"\"{selectSO.Item.name}\" 아이템을 구매하시겠습니까?";
    }

    private void BuyItem()
    {
        //골드가 있는지 조건 체크
        Debug.Log("구매");
        selectSO.isBuy = true;
        selectSO = null;
        foreach (Button btn in buttons) btn.GetComponent<MainShopButton>().UpdateUI();
    }

    private void RemoveButtonEvents(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    private void AddButtonEvents(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}
