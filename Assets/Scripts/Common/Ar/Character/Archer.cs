using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Archer : Player
{
    [SerializeField] Bullet arrow;

    private Vector2 angle;

    protected override void Start()
    {
        base.Start();
    }

    public override void StatReset()
    {
        isRangeCharacter = true;
        base.StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void Attack(Vector2 angle)
    {
        base.Attack(angle);
        Shoot(angle);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        AnimSkillStart();
        this.angle = angle;
    }

    public override void AnimTimingSkill()
    {
        Shoot(angle);
    }

    void Shoot(Vector2 angle)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, zAngle-180));
        rigid.velocity = ((angle.normalized * 0.5f) * pushPower) / (1 + stat.WEIGHT * 0.1f);
        cameraMove.MovetoTarget(bullet.transform);
        cameraMove.Shake();
    }
}
