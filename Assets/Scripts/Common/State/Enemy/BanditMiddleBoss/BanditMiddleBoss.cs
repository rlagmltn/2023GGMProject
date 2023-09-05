using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMiddleBoss : Enemy
{
    public override void StatReset()
    {
        stat.WEIGHT = 5;
        stat.MaxSP = 20;
        stat.MaxHP = 24;
        stat.ATK = 5;
        stat.CriPer = 20;
        stat.CriDmg = 1.5f;
        base.StatReset();
    }

    private void Phase1()
    {
        stat.WEIGHT = 100000;
    }
}
