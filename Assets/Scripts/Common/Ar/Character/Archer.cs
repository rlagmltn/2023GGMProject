using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Archer : Player
{
    [SerializeField] Bullet arrow;

    private bool isMove;

    protected override void Start()
    {
        base.Start();
        MouseUp.AddListener(() => { isMove = true; });
    }

    protected override void StatReset()
    {
        MaxHP = 100;
        ATK = 10;
        pushPower = 15;
        isMove = false;
        base.StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    private void Update()
    {
        if (rigid.velocity.magnitude <= 0.8f && isMove)
        {
            isMove = false;
            AfterMove?.Invoke();
        }
    }

    protected override void Attack(Vector2 angle)
    {
        Shoot(angle);
    }
    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        Shoot(angle);
        Shoot(angle);
    }
    void Shoot(Vector2 angle)
    {
        var bullet = Instantiate(arrow, transform.position, Quaternion.Euler(angle));
    }
}
