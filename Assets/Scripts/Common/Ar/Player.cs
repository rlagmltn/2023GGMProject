using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ar
{
    public UnityEvent MouseUp;

    private float dragAngle;
    private Vector2 dis;

    private float power;
    private Vector2 angle;

    protected override void StatReset()
    {
        MaxHP = 100;
        ATK = 10;
        minDragPower = 1.25f;
        maxDragPower = 6;
        pushPower = 2;
        base.StatReset();
    }

    protected void OnMouseDrag()
    {
        power = Mathf.Clamp(Vector2.Distance(transform.position, Util.Instance.mousePosition) * 4, minDragPower, maxDragPower);
        if (power <= minDragPower)
        {
            line.transform.localScale = defaultScale;
            return;
        }
        dis = Util.Instance.mousePosition - transform.position;
        dragAngle = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg;
        line.transform.localScale = new Vector2(power, 0.5f);
        line.transform.rotation = Quaternion.Euler(0, 0, dragAngle);
    }

    protected void OnMouseUp()
    {
        line.transform.localScale = defaultScale;
        if (power <= minDragPower) return;

        /*
        TurnManager.Instance.AddTrun();
        Debug.Log(TurnManager.Turn);
        */

        angle = transform.position - Util.Instance.mousePosition;
        angle /= angle.magnitude;

        rigid.velocity = (angle * (power * pushPower));
        MouseUp?.Invoke(); // �߻� ���� �ߵ��ϴ� Ʈ����
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.CompareTag("Enemy"))
        {
            BeforeCrash?.Invoke(); //�浹 ���� �ߵ��ϴ� Ʈ����
            //BattleManager.Instance.CrashSet(this, collision.contacts[0].normal);
        }
    }
}
