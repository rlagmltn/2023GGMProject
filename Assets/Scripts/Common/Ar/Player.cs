using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ar
{
    public int ar_id;
    public string ar_name;
    public bool isSellected;

    public UnityEvent MouseUp;

    private float power;

    public Player()
    {
        ar_id = -1;
        ar_name = "";
    }

    public Player(ItemObj itemObj)
    {
        ar_id = itemObj.itemData.ar_id;
        ar_name = itemObj.itemData.ar_name;
    }

    protected override void Start()
    {
        base.Start();
        StatReset();
    }

    protected override void StatReset()
    {
        base.StatReset();
        MaxHP = 100;
        ATK = 10;
        minDragPower = 0.4f;
        maxDragPower = 1.5f;
        pushPower = 15;
    }

    public void Drag()
    {
        /*power = Mathf.Clamp(charge * 4, minDragPower, maxDragPower);
        if (power <= minDragPower)
        {
            line.transform.localScale = defaultScale;
            return;
        }
        line.transform.localScale = new Vector2(power, 0.5f);
        line.transform.rotation = Quaternion.Euler(0, 0, dragAngle);*/
    }

    public void DragEnd(float charge, Vector2 angle)
    {
        power = Mathf.Clamp(charge, minDragPower, maxDragPower);
        if (power <= minDragPower) return;

        Debug.Log(power);
        rigid.velocity = (angle * power)*pushPower;
        MouseUp?.Invoke(); // 발사 직후 발동하는 트리거
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        /*if (collision.transform.CompareTag("Enemy"))
        {
            BeforeCrash?.Invoke(); //충돌 직전 발동하는 트리거
            //BattleManager.Instance.CrashSet(this, collision.contacts[0].normal);
        }*/
    }
}
