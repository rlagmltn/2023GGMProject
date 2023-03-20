using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainShop : MonoSingleton<MainShop>
{
    [SerializeField] private List<Button> buttons;
    //[SerializeField] MainItemButton[] buttons;
    [SerializeField] int shopItemCount;
    private Shop shop;
    private ItemSO[] shopItems;
    public ItemSO selectItem;
    public int pageNum;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Image ItemFrameImage;

    private void Awake()
    {
        shop = GetComponent<Shop>();
        shopItems = new ItemSO[shopItemCount];
        shopItems = shop.GetRandomItems(shopItemCount);
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        PageUpdate();
        ButtonInit();
        PreviewPage();
        ItemFrameImage.color = new Color(1, 1, 1, 0);
    }

    void PageUpdate()
    {
        for (int num = buttons.Count * (pageNum - 1); num < buttons.Count * pageNum; num++)
        {
            buttons[(num % 3)].GetComponent<MainShopButton>().SetItemSO(shopItems[num]);
        }
    }

    public void GetItemSO(ItemSO item)
    {
        selectItem = item;
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
    }

    private void NextPage()
    {
        pageNum++;
        pageNum = Mathf.Clamp(pageNum, 1, 2);
        PageUpdate();

        foreach (Button btn in buttons) btn.GetComponent<MainShopButton>().UpdateUI();
    }

    private void PreviewPage()
    {
        pageNum--;
        pageNum = Mathf.Clamp(pageNum, 1, 2);
        PageUpdate();

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
