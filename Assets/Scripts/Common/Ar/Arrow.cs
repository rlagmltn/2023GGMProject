using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    protected override void SetSO()
    {
        summoner = FindObjectOfType<Archer>();
        base.SetSO();
    }
}
