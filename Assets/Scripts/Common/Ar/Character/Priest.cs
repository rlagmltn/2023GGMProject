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
    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        if (targets.Length > 1)
        {
            var a = Vector2.Distance(transform.position, targets[1].point);
            skillRange.size = new Vector2(a / 2, 1);
        }
        else
        {
            skillRange.size = new Vector2(moveDrag / 2, 1);
        }
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
        rigid.velocity = -(angle * power) * pushPower / (1 + stat.WEIGHT * 0.1f);
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
