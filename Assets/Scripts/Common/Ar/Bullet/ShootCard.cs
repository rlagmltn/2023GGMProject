using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCard : Bullet
{
    protected override void SetSO()
    {
        summoner = FindObjectOfType<Gambler>();
        base.SetSO();
    }
}
