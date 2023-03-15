using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ar
{
    public int ar_id;
    public string ar_name;
    public Sprite ar_sprite;
    public bool isSellected;
    public int currentCooltime { get; set; }
    public bool isRangeCharacter { get; protected set; }

    public UnityEvent MouseUp;

    protected float power;
    protected int skillCooltime;

    private QuickSlot slot;

    private Transform rangeContainer;
    private GameObject moveRange;
    private GameObject attackRange;
    private GameObject skillRange;

    [SerializeField] ItemInfo[] itemSlots = new ItemInfo[3];

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
        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 15;

        foreach(ItemInfo item in itemSlots)
        {
            item?.Armed(this);
        }

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
        MouseUp?.Invoke(); // �߻� ���� �ߵ��ϴ� Ʈ����
    }
    protected virtual void Attack(Vector2 angle)
    {

    }
    protected virtual void Skill(Vector2 angle)
    {
        currentCooltime = skillCooltime;
        CameraMove.Instance.Shake();
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
        if (!collision.transform.CompareTag("Object"))
        {
            BattleManager.Instance.SettingAr(this);
            CameraMove.Instance.Shake();
        }
    }

    private void OnMouseDown()
    {
        PlayerController.Instance.SellectPlayer(slot);
    }

    public void Connect(QuickSlot slot)
    {
        this.slot = slot;
    }

    public void CountCooltime()
    {
        if (currentCooltime > 0)
            currentCooltime--;
    }
}
