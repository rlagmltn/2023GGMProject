using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Archer : Player
{
    void Start()
    {
        base.Start();
    }

    public void DragEnd(JoystickType joystickType, float charge, Vector2 angle)
    {
        base.DragEnd(joystickType, charge, angle);
    }
    public override void Attack(Vector2 angle)
    {

    }
    public override void Skill(Vector2 angle)
    {

    }
}
