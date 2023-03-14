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
        AfterMove.AddListener(ShootArrow);
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

    private void ShootArrow()
    {
        //var _slash = Instantiate(arrow, transform.position, Quaternion.Eul);
    }
    private void Skill()
    {

    }
}
