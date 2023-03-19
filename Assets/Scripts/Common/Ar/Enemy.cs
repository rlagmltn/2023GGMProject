using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ar
{
    protected override void StatReset()
    {
        stat.MaxHP = 100;
        stat.ATK = 10;
        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 5;
        isDead = false;
        base.StatReset();
        Debug.Log(stat.HP);
    }

    public float GetPower()
    {
        float dragPower = Random.Range(minDragPower, maxDragPower);
        return dragPower * pushPower;
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
