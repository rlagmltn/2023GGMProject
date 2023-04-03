using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ar
{
    protected int currentPassiveCool;
    protected int passiveCool;

    protected override void StatReset()
    {
        isDead = false;
        stat.HP = stat.MaxHP;
        stat.SP = stat.MaxSP;
        base.StatReset();
    }

    public float GetPower()
    {
        return maxDragPower * pushPower;
    }

    protected override void Start()
    {
        stat = new Stat();
        base.Start();
        StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (!collision.transform.CompareTag("Object"))
        {
            BattleManager.Instance.SettingAr(this);
            CameraMove.Instance.Shake();
            EffectManager.Instance.InstantiateEffect(0, collision.contacts[0].point, transform.position, collision.contacts[0].point);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Attack"))
        {
            if (collision.transform.IsChildOf(transform)) return;
            var attacker = collision.transform.parent.GetComponent<Ar>();
            BattleManager.Instance.SettingAr(this, attacker);
        }
    }

    public void PassiveCoolDown()
    {
        currentPassiveCool = Mathf.Max(0, currentPassiveCool - 1);
    }
}
