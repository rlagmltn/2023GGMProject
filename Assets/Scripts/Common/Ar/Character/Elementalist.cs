using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elementalist : Player
{
    private Vector2 angle;

    protected override void Start()
    {
        base.Start();
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

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        StartCoroutine(Slash(angle));
        this.angle = angle;
    }

    protected override void Passive()
    {
        stat.SP = (int)so.surviveStats.MaxShield;
        DeadCheck();
    }

    private IEnumerator Slash(Vector2 angle)
    {
        rigid.velocity = -((angle.normalized * 1.5f) * pushPower) / (1 + stat.WEIGHT * 0.1f);

        yield return new WaitForSeconds(0.5f);
        AnimSkillStart();
    }

    public override void AnimTimingSkill()
    {

    }
}
