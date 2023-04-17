using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Player
{
    protected override void Start()
    {
        base.Start();
        AfterMove.AddListener(()=> { isUsingSkill = false; });
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
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
        rigid.velocity = -(angle * power) * pushPower;
        isUsingSkill = true;
        cameraMove.Shake();
        Passive();
    }

    protected override void Passive()
    {
        stat.HP += stat.ATK;
        HpMaxCheck();
    }
}
