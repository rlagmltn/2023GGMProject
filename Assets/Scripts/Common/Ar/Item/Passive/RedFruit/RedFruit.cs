using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFruit : ItemInfo
{
    [SerializeField] private int HealAmount;

    public override void Passive()
    {
        player.Heal(HealAmount, player);
    }
}
