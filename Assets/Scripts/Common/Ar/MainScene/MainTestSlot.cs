using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTestSlot : MonoBehaviour
{
    public ArSO arSo { get; set; }
    public ItemSO itemSO { get; set; }
    private Player player;
    public Player Player { get { return player; } }
    private Button button;
    private Image image;
    [SerializeField] Image background;

    public void SetSO(ArSO so)
    {
        arSo = so;
        Init();
        button.onClick.AddListener(SellectPlayer);
        image.sprite = so.characterInfo.Image;
        background.sprite = BackGroundHolder.Instance.BackGround(so.characterInfo.rarity);
    }

    public void SetSO(ItemSO so)
    {
        itemSO = so;
        Init();
        button.onClick.AddListener(ArmedItem);
        image.sprite = so.itemIcon;
        background.sprite = BackGroundHolder.Instance.BackGround(so.itemRarity);
    }

    private void OnEnable()
    {
        if (button == null) Init();
    }

    private void Init()
    {
        button = GetComponent<Button>();
        image = transform.GetChild(0).GetComponent<Image>();
    }

    public void SellectPlayer()
    {
        if(arSo!=null && player==null)
        {
            player = Instantiate(arSo.ArData, null);
            player.Init();
        }
        MainTestModeManager.Instance.SellectPlayer(player);
    }

    private void ArmedItem()
    {
        MainTestModeManager.Instance.ArmedItem(itemSO);
    }

    public void GetAr(ArSO so)
    {
        if (button == null) Init();
        arSo = so;
        image.sprite = arSo.characterInfo.Image;
        background.sprite = BackGroundHolder.Instance.BackGround(so.characterInfo.rarity);
    }

    public void GetItem(ItemSO so)
    {
        if (button == null) Init();
        itemSO = so;
        image.sprite = itemSO.itemIcon;
        background.sprite = BackGroundHolder.Instance.BackGround(so.itemRarity);
    }

    public void UnSetAr()
    {
        MainTestModeManager.Instance.SellectPlayer();
    }

    public void UnSetItem()
    {
        if(itemSO!=null) MainTestModeManager.Instance.UnArmedItem(itemSO);
        itemSO = null;
        image.sprite = BackGroundHolder.Instance.NullImage;
        background.sprite = BackGroundHolder.Instance.BackGround(ItemRarity.Common);
    }
}
