using Assets.HeroEditor4D.Common.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : Player
{
    private Vector2 angle;
    private bool dashing = false;
    private CapsuleCollider2D capsule;

    protected override void Start()
    {
        base.Start();
        capsule = GetComponent<CapsuleCollider2D>();
        AfterMove.AddListener(() => { dashing = false; capsule.isTrigger = false; });
        AfterAttack.AddListener(Passive);
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
        base.StatReset();
    }

    protected override void Update()
    {
        base.Update();
        if (dashing && battleTarget != null) InitTImeScale();
    }

    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        RayCastTargets(moveDrag);
        var target = FindNearAr();

        if (target.magnitude != 0)
        {
            skillRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(dashing)
        {
            if(collision.GetComponent<Ar>())
            {
                dashing = false;
                capsule.isTrigger = false;
                BattleManager.Instance.SettingAr(this);
                BattleManager.Instance.SettingAr(collision.GetComponent<Ar>());
            }
        }
        base.OnTriggerEnter2D(collision);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        SuperDash(angle);
        this.angle = angle;
    }

    protected override void Passive()
    {
        stat.SP += 1;
        DeadCheck();
    }

    private void SuperDash(Vector2 angle)
    {
        capsule.isTrigger = true;
        isMove = true;
        dashing = true;
        rigid.velocity = -(angle * power) * pushPower / (1 + stat.WEIGHT * 0.1f);
        EffectManager.Instance.InstantiateEffect_P(Effect.LandingSmoke, transform.position);
        animationManager.SetState(CharacterState.Run);
        Flip();
    }
}
