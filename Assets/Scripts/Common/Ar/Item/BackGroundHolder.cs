using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundHolder : MonoSingleton<BackGroundHolder>
{
    [SerializeField] Sprite common;
    [SerializeField] Sprite rare;
    [SerializeField] Sprite epic;
    [SerializeField] Sprite legend;
    [SerializeField] Sprite myth;
    [SerializeField] Sprite nullImage;
    public Sprite NullImage { get { return nullImage; } }

    public Sprite BackGround(ArRarity arRarity)
    {
        Sprite sprite = arRarity switch
        {
            ArRarity.NORMAL => common,
            ArRarity.RARE => rare,
            ArRarity.UNIQUE => epic,
            ArRarity.LEGENDARY => legend,
            _ => common,
        };

        return sprite;
    }

    public Sprite BackGround(ItemRarity itemRarity)
    {
        Sprite sprite = itemRarity switch
        {
            ItemRarity.Common => common,
            ItemRarity.Rare => rare,
            ItemRarity.Epic => epic,
            ItemRarity.Legendary => legend,
            ItemRarity.Legacy => myth,
            _ => common,
        };

        return sprite;
    }
}
