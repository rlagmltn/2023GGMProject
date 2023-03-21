using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

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

    private Shop shop;
    private ItemSO[] shopItems;
    private int pageNum = 1;


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
        aboutText.text = "";
    }

    void PageUpdate()
    {
        for (int num = buttons.Count * (pageNum - 1); num < buttons.Count * pageNum; num++)
        {
            buttons[(num % 3)].GetComponent<MainShopButton>().SetItemSO(shopItems[num]);
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

    private void RemoveButtonEvents(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    private void AddButtonEvents(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}
