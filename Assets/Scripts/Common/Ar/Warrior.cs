using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    [SerializeField] Bullet slash;

    private bool isMove;

    protected override void Start()
    {
        base.Start();
        AfterMove.AddListener(Super_Hyper_Ultimate_Miracle_Ultimate_Warrior_Slash);
        MouseUp.AddListener(() => { isMove = true; });
    }

    protected override void StatReset()
    {
        base.StatReset();
        MaxHP = 100;
        ATK = 10;
        pushPower = 15;
        isMove = false;
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
            AfterMove.Invoke();
        }
    }

    private void Super_Hyper_Ultimate_Miracle_Ultimate_Warrior_Slash()
    {
        var _slash = Instantiate(slash, null);
        _slash.transform.position = transform.position;
    }
}
