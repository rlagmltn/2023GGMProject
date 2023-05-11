using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Player
{
    [SerializeField] Bullet kunai;
    private Vector2 angle;

    protected override void Start()
    {
        base.Start();
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
        base.StatReset();
        Passive();
        range = kunai.bulletSO.speed * kunai.bulletSO.lifeTime;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        RayCastTargets(range);
        var target = FindNearObject();

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
        this.angle = angle;
        animationManager.Throw();
    }

    public override IEnumerator AnimTimingSkill()
    {
        while (isSkill) yield return null;
        EffectManager.Instance.InstantiateEffect_P(Effect.KuaniShoot, (Vector2)attackRange.transform.position - angle, new Vector2(angle.x, -angle.y));
        Shoot(angle);
    }

    protected override void Passive()
    {
        stat.CriDmg *= 2.5f;
    }

    void Shoot(Vector2 angle)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(kunai, transform.position, Quaternion.Euler(0, 0, zAngle - 180));
        cameraMove.MovetoTarget(bullet.transform);
        cameraMove.Shake();
    }
}
