using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    private Vector2 angle;
    private Transform boxPoint;
    private bool dashing = false;

    protected override void Start()
    {
        base.Start();
        boxPoint = rangeContainer.GetChild(3);
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

    public override void Push(Vector2 velo)
    {
        rigid.velocity = lastVelocity;
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
        stat.SP = (int)so.surviveStats.MaxShield;
        DeadCheck();
    }

    private IEnumerator Slash(Vector2 angle)
    {
        rigid.velocity = -((angle.normalized * 1f) * pushPower) / (1 + stat.WEIGHT * 0.1f);

        yield return new WaitForSeconds(1.3f);
        AnimSkillStart();
    }

    public override void AnimTimingSkill()
    {
        var attackSuccess = false;
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
