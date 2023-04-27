using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy
{
    public override void StatReset()
    {
        stat.MaxHP = 99999;
        stat.MaxSP = 11111;
        stat.ATK = 4;
        stat.SATK = 4;
        stat.CriPer = 5;
        stat.CriDmg = 1.5f;
        stat.WEIGHT = 5;

        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 20;
        passiveCool = 5;
        currentPassiveCool = 0;
        base.StatReset();
        gameObject.SetActive(false);
    }
}
