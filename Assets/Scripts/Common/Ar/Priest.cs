using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Player
{
    protected override void Start()
    {
        base.Start();
        StatReset();
    }

    protected override void StatReset()
    {
        stat.MaxHP = 100;
        stat.ATK = 10;
        stat.SATK = -20;
        pushPower = 15;
        isRangeCharacter = false;
        skillCooltime = 7;
        currentCooltime = 0;
        base.StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void Attack(Vector2 angle)
    {
        base.Attack(angle);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        rigid.velocity = (angle * power) * pushPower;
        isUsingSkill = true;
    }
}
