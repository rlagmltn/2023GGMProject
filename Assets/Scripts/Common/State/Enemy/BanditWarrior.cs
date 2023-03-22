using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior : Enemy
{
    protected override void StatReset()
    {
        stat.MaxHP = 14;
        stat.MaxDEF = 2;
        stat.ATK = 4;
        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 20;
        base.StatReset();
    }
}
