using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Player
{
    [SerializeField] private Bullet fireBall;

    private float explosion;
    private bool isUsingSkill;
    private Enemy target;
    private float finalDamage;

    private Vector2 angle;

    protected override void Start()
    {
        base.Start();
        Passive();
        AfterMove.AddListener(() => { isUsingSkill = false; });
        AfterMove.AddListener(Explosion);
    }

    public override void StatReset()
    {
        isRangeCharacter = true;
        explosion = 2f;
        base.StatReset();
        range = fireBall.bulletSO.speed * fireBall.bulletSO.lifeTime;
    }
    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        
        if (joystickType == JoystickType.Attack)
            RayCastTargets(range);
        else
            RayCastTargets(moveDrag);

        var target = FindNearObject();

        if (target.magnitude != 0)
        {
            attackRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
            skillRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
        }
        else
        {
            attackRange.size = new Vector2(range / 2, 1);
            skillRange.size = new Vector2(moveDrag / 2, 1);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.collider.CompareTag("Enemy") && isUsingSkill)
        {
            target = collision.gameObject.GetComponent<Enemy>();
            var effect = EffectManager.Instance.InstantiateEffect_P(Effect.MadRing, target.transform.position);
            effect.transform.SetParent(target.transform);
            
            isUsingSkill = false;
        }
    }

    protected override void Attack(Vector2 angle)
    {
        base.Attack(angle);
        this.angle = angle;
        if (angle.x < 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (angle.x > 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(AnimTimingAttack());
    }

    public override IEnumerator AnimTimingAttack()
    {
        while (isAttack) yield return null;
        cameraMove.MovetoTarget(Shoot(angle, stat.ATK).transform);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        isMove = true;
        isUsingSkill = true;
        rigid.velocity = -(angle * power) * pushPower / (1 + stat.WEIGHT * 0.1f);
        EffectManager.Instance.InstantiateEffect_P(Effect.DASH, transform.position, new Vector2(-angle.x, angle.y));
        if (angle.x < 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (angle.x > 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    Bullet Shoot(Vector2 angle, int damage)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        EffectManager.Instance.InstantiateEffect_P(Effect.BowShoot, (Vector2)attackRange.transform.position - angle, new Vector2(angle.x, -angle.y));
        var bullet = Instantiate(fireBall, transform.position, Quaternion.Euler(0, 0, zAngle - 180));
        bullet.SetDamage(damage);

        return bullet;
    }

    protected override void Passive()
    {
        finalDamage = 1.5f;
    }

    private void Explosion()
    {
        if (target == null) return;

        var hits = Physics2D.OverlapCircleAll(target.transform.position, explosion);
        int deal = Mathf.RoundToInt(stat.SATK * finalDamage);
        EffectManager.Instance.InstantiateEffect_P(Effect.ElementExplode, target.transform.position);
        foreach (Collider2D hit in hits)
        {
            BattleManager.Instance.SettingAr(hit.GetComponent<Enemy>(), deal);
        }

        target = null;
    }
}
