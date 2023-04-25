using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Archer : Player
{
    [SerializeField] Bullet arrow;

    float maxTime = 0;
    float currentTime = 0;
    bool timeGoing = false;

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

    protected override void Update()
    {
        base.Update();
        if(timeGoing) currentTime += Time.deltaTime;
        if (maxTime <= currentTime)
        {
            TurnManager.Instance.SomeoneIsMoving = false;
            timeGoing = false;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void Attack(Vector2 angle)
    {
        base.Attack(angle);
        this.angle = angle;
        StartCoroutine(AnimTimingAttack());
        WaitSec(1.5f);
    }

    public override IEnumerator AnimTimingAttack()
    {
        while (isAttack) yield return null;
        Shoot(angle);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        animationManager.ShotBow();
        this.angle = angle;
        WaitSec(2f);
    }

    public override IEnumerator AnimTimingSkill()
    {
        while (isSkill) yield return null;
        Shoot(angle);
        yield return new WaitForSeconds(0.2f);
        Shoot(angle);
    }

    void Shoot(Vector2 angle)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, zAngle-180));
        rigid.velocity = ((angle.normalized * 0.5f) * 25) / (1 + stat.WEIGHT * 0.1f);
        cameraMove.MovetoTarget(bullet.transform);
        if (angle.x < 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if (angle.x > 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
    }

    void WaitSec(float sec)
    {
        TurnManager.Instance.SomeoneIsMoving = true;
        timeGoing = true;
        maxTime = sec;
        currentTime = 0;
    }
}
