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
        this.angle = angle;
    }

    public override void AnimTimingAttack()
    {
        Shoot(angle);
        WaitSec(1f);
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
        WaitSec(2.5f);
    }

    void Shoot(Vector2 angle)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, zAngle-180));
        rigid.velocity = ((angle.normalized * 0.5f) * 25) / (1 + stat.WEIGHT * 0.1f);
        TurnManager.Instance.SomeoneIsMoving = true;
        cameraMove.MovetoTarget(bullet.transform);
        if (angle.x > 0) sprite.flipX = false;
        else if (angle.x < 0) sprite.flipX = true;
    }

    IEnumerator WaitSec(float sec)
    {
        yield return new WaitForSeconds(sec);
        TurnManager.Instance.SomeoneIsMoving = false;
    }
}
