using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Bullet
{
    protected override void SetSO()
    {
        summoner = FindObjectOfType<BanditMiddleBoss>();
        damage = 3;
        base.SetSO();
    }
}
