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

    private QuickSlot slot;

    private Transform rangeContainer;
    private GameObject moveRange;
    private GameObject attackRange;
    private GameObject skillRange;

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

        rangeContainer = transform.GetChild(0);
        moveRange = rangeContainer.GetChild(0).gameObject;
        attackRange = rangeContainer.GetChild(1).gameObject;
        skillRange = rangeContainer.GetChild(2).gameObject;
        DisableRanges();

        StatReset();
    }

    protected override void StatReset()
    {
        MaxHP = 100;
        ATK = 10;
        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 15;
        base.StatReset();
    }

    public void DragBegin(JoystickType joystickType)
    {
        switch (joystickType)
        {
            case JoystickType.Move:
                moveRange.SetActive(true);
                break;
            case JoystickType.Attack:
                attackRange.SetActive(true);
                break;
            case JoystickType.Skill:
                skillRange.SetActive(true);
                break;
            case JoystickType.None:
                break;
        };
    }

    public void Drag(float angle)
    {
        rangeContainer.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void DragEnd(JoystickType joystickType, float charge, Vector2 angle)
    {
        power = Mathf.Clamp(charge, minDragPower, maxDragPower);
        switch (joystickType)
        {
            case JoystickType.Move:
                rigid.velocity = ((angle * power) * pushPower);
                break;
            case JoystickType.Attack:
                Attack(angle);
                break;
            case JoystickType.Skill:
                Skill(angle);
                break;
            case JoystickType.None:
                break;
        };
        MouseUp?.Invoke(); // 발사 직후 발동하는 트리거
    }
    public virtual void Attack(Vector2 angle)
    {

    }
    public virtual void Skill(Vector2 angle)
    {

    }

    public void DisableRanges()
    {
        moveRange.SetActive(false);
        attackRange.SetActive(false);
        skillRange.SetActive(false);
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

    private void OnMouseDown()
    {
        PlayerController.Instance.SellectPlayer(slot);
    }

    public void Connect(QuickSlot slot)
    {
        this.slot = slot;
    }
}
