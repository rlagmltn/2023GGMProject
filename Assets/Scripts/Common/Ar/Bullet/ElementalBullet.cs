using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalBullet : Bullet
{
    protected override void SetSO()
    {
        summoner = FindObjectOfType<Elementalist>();
        base.SetSO();
    }

    protected override void AfterCrush()
    {
        base.AfterCrush();
    }
}
