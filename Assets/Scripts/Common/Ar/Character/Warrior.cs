using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    private HitBox hitbox;

    protected override void Start()
    {
        base.Start();
        hitbox = GetComponent<HitBox>();
    }

    protected override void StatReset()
    {
        MaxHP = 100;
        ATK = 10;
        pushPower = 15;
        isRangeCharacter = true;
        skillCooltime = 7;
        currentCooltime = 0;
        base.StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        Super_Hyper_Ultimate_Miracle_Ultimate_Warrior_Slash(Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg);
    }

    private void Super_Hyper_Ultimate_Miracle_Ultimate_Warrior_Slash(float angle)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitbox.Hitbox.transform.position, new Vector2(hitbox.rangeX, hitbox.rangeY), angle);

        foreach (Collider2D collider in colliders)
        {
            BattleManager.Instance.SettingAr(collider.GetComponent<Enemy>(), this);
        }
    }
}
