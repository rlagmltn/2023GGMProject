using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTestSlot : MonoBehaviour
{
    public ArSO arSo { get; set; }
    public ItemSO itemSO { get; set; }
    private Player player;
    private Button button;
    private Image image;

    public void SetSO(ArSO so)
    {
        arSo = so;
        Init();
        button.onClick.AddListener(SellectPlayer);
        image.sprite = so.characterInfo.Image;
        player = Instantiate(so.ArData, null);
    }

    public void SetSO(ItemSO so)
    {
        itemSO = so;
        Init();
        button.onClick.AddListener(ArmedItem);
        image.sprite = so.itemIcon;
    }

    private void Start()
    {
        if (button == null) Init();
    }

    private void Init()
    {
        button = GetComponent<Button>();
        image = transform.GetChild(0).GetComponent<Image>();
    }

    private void SellectPlayer()
    {
        MainTestModeManager.Instance.SellectPlayer(player);
    }

    private void ArmedItem()
    {
        MainTestModeManager.Instance.ArmedItem(itemSO);
    }

    public void GetAr(ArSO so)
    {
        arSo = so;
        image.sprite = arSo.characterInfo.Image;
    }

    public void GetItem(ItemSO so)
    {
        itemSO = so;
        image.sprite = itemSO.itemIcon;
    }

    public void UnSetAr()
    {
        arSo = null;
        image.sprite = null;
        MainTestModeManager.Instance.SellectPlayer();
    }

    public void UnSetItem()
    {
        itemSO = null;
        image.sprite = null;
    }
}
