using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Archer : Player
{
    [SerializeField] Bullet arrow;

    float maxTime = 1;
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
        range = arrow.bulletSO.speed * arrow.bulletSO.lifeTime;
    }

    protected override void Update()
    {
        base.Update();
        if(timeGoing) currentTime += Time.deltaTime;
        if (maxTime <= currentTime)
        {
            TurnManager.Instance.SomeoneIsMoving = false;
            timeGoing = false;
            currentTime = 0;
        }
    }

    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        RayCastTargets(range);
        var target = FindNearObject();

        if (target.magnitude != 0)
        {
            attackRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
            skillRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
        }
        else
        {
            attackRange.size = new Vector2(range / 2, 1);
            skillRange.size = new Vector2(range / 2, 1);
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
        if (angle.x < 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (angle.x > 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(AnimTimingAttack());
        WaitSec(1.5f);
    }

    public override IEnumerator AnimTimingAttack()
    {
        while (isAttack) yield return null;
        cameraMove.MovetoTarget(Shoot(angle, stat.ATK).transform); 
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        animationManager.ShotBow();
        this.angle = angle;
        if (angle.x < 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (angle.x > 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        WaitSec(2f);
    }

    public override IEnumerator AnimTimingSkill()
    {
        while (isSkill) yield return null;
        cameraMove.MovetoTarget(Shoot(angle, stat.SATK).transform);
        yield return new WaitForSeconds(0.2f);
        Shoot(angle, stat.SATK);
    }

    Bullet Shoot(Vector2 angle, int damage)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        EffectManager.Instance.InstantiateEffect_P(Effect.BowShoot, (Vector2)attackRange.transform.position - angle, new Vector2(angle.x, -angle.y));
        var bullet = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, zAngle-180));
        bullet.SetDamage(damage);
        rigid.velocity = ((angle.normalized * 0.5f) * 25) / (1 + stat.WEIGHT * 0.1f);

        return bullet;
    }

    void WaitSec(float sec)
    {
        TurnManager.Instance.SomeoneIsMoving = true;
        timeGoing = true;
        maxTime = sec;
        currentTime = 0;
    }
}
