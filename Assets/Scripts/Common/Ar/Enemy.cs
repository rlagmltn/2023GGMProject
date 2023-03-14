using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ar
{
    protected override void StatReset()
    {
        MaxHP = 100;
        ATK = 10;
        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 15;
        base.StatReset();
        Debug.Log(HP);
    }

    protected override void Start()
    {
        base.Start();
        StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (!collision.transform.CompareTag("Object"))
        {
            BattleManager.Instance.SettingAr(this);
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
}
