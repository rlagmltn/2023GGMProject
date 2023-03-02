using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ar
{
    protected override void StatReset()
    {
        MaxHP = 100;
        ATK = 10;
        minDragPower = 1.25f;
        maxDragPower = 6;
        pushPower = 2;
        base.StatReset();
    }
}
