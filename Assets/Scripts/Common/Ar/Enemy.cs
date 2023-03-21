using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ar
{
    protected override void StatReset()
    {
        isDead = false;
        base.StatReset();
        Debug.Log(stat.HP);
    }

    public float GetPower()
    {
        return maxDragPower * pushPower;
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
}
