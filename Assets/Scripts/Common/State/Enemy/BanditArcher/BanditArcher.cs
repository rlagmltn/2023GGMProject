using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher : Enemy
{
    private float atkRange;

    protected override void Start()
    {
        base.Start();
    }

    public override void StatReset()
    {
        stat.MaxHP = 14;
        stat.MaxSP = 2;
        stat.ATK = 4;
        stat.SATK = 4;
        stat.CriPer = 5;
        stat.CriDmg = 1.5f;
        stat.WEIGHT = 1;

        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 20;
        atkRange = 6f;
        base.StatReset();
    }


}
