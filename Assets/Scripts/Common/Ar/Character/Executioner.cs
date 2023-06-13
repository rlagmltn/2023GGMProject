using Assets.HeroEditor4D.Common.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executioner : Player
{
    [SerializeField] private Bullet chain;
    private Vector2 angle;

    protected override void Start()
    {
        base.Start();
        range = (maxDragPower * pushPower) / (1 + stat.WEIGHT * 0.1f) / 4;
        Passive();
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
        base.StatReset();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        RayCastTargets(range);
        var target = FindNearAr();

        if (target.magnitude != 0)
        {
            skillRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
        }
        else
        {
            skillRange.size = new Vector2(range / 2, 1);
        }
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        if (angle.x < 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (angle.x > 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        animationManager.Attack();
        this.angle = angle;
    }

    public override IEnumerator AnimTimingSkill()
    {
        while (isSkill) yield return null;
        cameraMove.MovetoTarget(Shoot(angle, stat.SATK).transform);
    }

    protected override void Passive()
    {
        BeforeAttack.AddListener(() => lastVelocity *= 1.1f);
    }

    private Bullet Shoot(Vector2 angle, int damage)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        EffectManager.Instance.InstantiateEffect_P(Effect.BowShoot, (Vector2)attackRange.transform.position - angle, new Vector2(angle.x, -angle.y));
        var bullet = Instantiate(chain, transform.position, Quaternion.Euler(0, 0, zAngle - 180));
        bullet.SetDamage(damage);

        return bullet;
    }

    public void Grap(Ar target)
    {
        StartCoroutine(GrapCo(target));
    }

    private IEnumerator GrapCo(Ar target)
    {
        var dis = Vector2.Distance(transform.position, target.transform.position);
        var moveVec = transform.position - target.transform.position;
        var col = target.GetComponent<CapsuleCollider2D>();
        col.isTrigger = true;
        while (dis>1.5f)
        {
            target.transform.position += moveVec * Time.deltaTime;
            dis = Vector2.Distance(transform.position, target.transform.position);
            yield return null;
        }
        col.isTrigger = false;
    }
}
