using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Archer : Player
{
    [SerializeField] Bullet arrow;

    protected override void Start()
    {
        base.Start();
    }

    protected override void StatReset()
    {
        stat.MaxHP = 100;
        stat.ATK = 50;
        pushPower = 15;
        isMove = false;
        isRangeCharacter = true;
        base.StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
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
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        Debug.Log("½µ");
        var bullet = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, zAngle-180));
        CameraMove.Instance.MovetoTarget(bullet.transform);
    }
}
