using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Archer : Player
{
    public bool isSkillUse = false;
    void Start()
    {
        base.Start();
    }

    public void DragEnd(float charge, Vector2 angle)
    {
        base.DragEnd(charge, angle);
    }
}
