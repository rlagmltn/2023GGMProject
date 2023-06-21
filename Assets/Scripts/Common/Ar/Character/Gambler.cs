using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gambler : Player
{
    [SerializeField] Bullet arrow;

    private Vector2 angle;
    private int card;
    private bool isAttackAble;

    float maxTime = 1;
    float currentTime = 0;
    bool timeGoing = false;

    public override void StatReset()
    {
        isRangeCharacter = true;
        card = 3;
        range = arrow.bulletSO.speed * arrow.bulletSO.lifeTime;
        base.StatReset();
    }
    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        RayCastTargets(range);
        var target = FindNearObject();

        if (target.magnitude != 0)
        {
            attackRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
        }
        else
        {
            attackRange.size = new Vector2(range / 2, 1);
        }
        skillRange.size = new Vector2(1, 1);
    }

    public override void DragEnd(JoystickType joystickType, float charge, Vector2 angle)
    {
        if(joystickType == JoystickType.Attack)
        {
            Passive();
            if (isAttackAble)
                base.DragEnd(joystickType, charge, angle);
        }
        else
            base.DragEnd(joystickType, charge, angle);
    }

    protected override void Update()
    {
        base.Update();
        if (timeGoing) currentTime += Time.deltaTime;
        if (maxTime <= currentTime)
        {
            TurnManager.Instance.SomeoneIsMoving = false;
            timeGoing = false;
            currentTime = 0;
        }
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);

        animationManager.Jab();
    }

    public override IEnumerator AnimTimingSkill()
    {
        card += Random.Range(1, 7);
        card = Mathf.Clamp(card, 0, 6);
        Debug.Log(card);
        return base.AnimTimingSkill();
    }

    protected override void Attack(Vector2 angle)
    {
        base.Attack(angle);
        animationManager.Jab();
        this.angle = angle;
        if (angle.x < 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (angle.x > 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(AnimTimingAttack());
        WaitSec(1f);
    }

    public override IEnumerator AnimTimingAttack()
    {
        while (isAttack) yield return null;
        cameraMove.MovetoTarget(Shoot(angle, stat.ATK).transform);
    }

    Bullet Shoot(Vector2 angle, int damage)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        EffectManager.Instance.InstantiateEffect_P(Effect.BowShoot, (Vector2)attackRange.transform.position - angle, new Vector2(angle.x, -angle.y));
        var bullet = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, zAngle - 180));
        bullet.SetDamage(damage);
        rigid.velocity = ((angle.normalized * 0.5f) * 5) / (1 + stat.WEIGHT * 0.1f);

        return bullet;
    }

    void WaitSec(float sec)
    {
        TurnManager.Instance.SomeoneIsMoving = true;
        timeGoing = true;
        maxTime = sec;
        currentTime = 0;
    }

    protected override void Passive()
    {
        if(card>0)
        {
            card--;
            isAttackAble = true;
        }
        else
        {
            isAttackAble = false;
        }
    }
}
