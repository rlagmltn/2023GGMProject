using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior : Enemy
{
    private Ar lastAr = null;

    protected override void Start()
    {
        base.Start();
        BeforeAttack.AddListener(PassiveDP);
    }

    protected override void StatReset()
    {
        stat.MaxHP = 14;
        stat.MaxDP = 2;
        stat.ATK = 4;
        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 20;
        passiveCool = 5;
        currentPassiveCool = 0;
        base.StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.CompareTag("Player"))
        {
            lastAr = collision.gameObject.GetComponent<Ar>();
        }
    }

    private void PassiveDP()
    {
        if(lastAr != null && currentPassiveCool == 0)
        {
            Debug.Log("¹æ±ï»ç¿ë");
            currentPassiveCool = passiveCool;
            lastAr.DecreaseDP(1);
            lastAr = null;
        }
    }
}
