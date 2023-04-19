using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    private HitBox hitbox;

    private Vector2 angle;

    protected override void Start()
    {
        base.Start();
        hitbox = GetComponent<HitBox>();
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
        base.StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
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
        rigid.velocity = -((angle.normalized * 1.5f) * pushPower) / (1 + stat.WEIGHT * 0.1f);

        yield return new WaitForSeconds(0.5f);
        AnimSkillStart();
    }

    public override void AnimTimingSkill()
    {
        var attackSuccess = false;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitbox.Hitbox.transform.position, new Vector2(hitbox.rangeX, hitbox.rangeY), Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg);

        foreach (Collider2D collider in colliders)
        {
            var enemy = collider.GetComponent<Enemy>();
            BattleManager.Instance.SettingAr(enemy, this);
            if (enemy != null)
            {
                attackSuccess = true;
                cameraMove.Shake();
            }
        }
        if (attackSuccess) Passive();
    }
}
