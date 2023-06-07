using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class Warrior : Player
{
    private Vector2 angle;
    private Transform boxPoint;
    private Transform skillBox;
    private bool dashing = false;

    protected override void Start()
    {
        base.Start();
        boxPoint = rangeContainer.GetChild(3);
        skillBox = skillRange.transform.GetChild(0);
        range = 7f;
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

        RayCastTargets(range);
        var target = FindNearObject();

        if (target.magnitude != 0)
        {
            skillRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
            skillBox.localPosition = new Vector2(Vector2.Distance(transform.position, target) / 2 + 1, 0);
        }
        else
        {
            skillRange.size = new Vector2(range/2, 1);
            skillBox.localPosition = new Vector2(range/2 + 1, 0);
        }
    }

    public override void Push(Vector2 velo)
    {
        if (dashing)
            rigid.velocity = Vector2.zero;
        else
            base.Push(velo);    
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        dashing = true;
        StartCoroutine(Slash(angle));
        this.angle = angle;
    }

    protected override void Passive()
    {
        stat.SP += Mathf.RoundToInt(so.surviveStats.MaxShield / 2);
        DeadCheck();
    }

    private IEnumerator Slash(Vector2 angle)
    {
        isMove = true;
        rigid.velocity = -((angle.normalized * 1f) * pushPower) / (1 + stat.WEIGHT * 0.1f);
        EffectManager.Instance.InstantiateEffect_P(Effect.LandingSmoke, transform.position);
        animationManager.SetState(CharacterState.Run);
        Flip();

        yield return new WaitForSeconds(0.8f);
        
        if(isSkill)
            animationManager.Attack();
    }

    public override IEnumerator AnimTimingSkill()
    {
        while (isSkill) yield return null;
        var attackSuccess = false;
        EffectManager.Instance.InstantiateEffect_P(Effect.WARRIOR, boxPoint.position, angle);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxPoint.position, new Vector2(4.2f, 2.2f), rangeContainer.rotation.z);

        foreach (Collider2D collider in colliders)
        {
            var enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Push(-angle.normalized * 5);
                BattleManager.Instance.SettingAr(enemy, this);
                attackSuccess = true;
            }
        }
        if (attackSuccess)
        {
            Passive();
            cameraMove.Shake();
        }
        dashing = false;
    }
}
