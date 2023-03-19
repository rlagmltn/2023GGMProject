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

    public bool isEnd = true;

    public UnityEvent MouseUp;

    protected float power;
    protected int skillCooltime;

    private QuickSlot slot;

    private Transform rangeContainer;
    private GameObject moveRange;
    private GameObject attackRange;
    private GameObject skillRange;

    [SerializeField] ItemSO[] itemSlots = new ItemSO[3];

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

        foreach(ItemSO item in itemSlots)
        {
            item?.Armed(this);
        }

        base.StatReset();
    }

    //public void DragBegin(JoystickType joystickType)
    //{
    //    switch (joystickType)
    //    {
    //        case JoystickType.Move:
    //            moveRange.SetActive(true);
    //            break;
    //        case JoystickType.Attack:
    //            attackRange.SetActive(true);
    //            break;
    //        case JoystickType.Skill:
    //            skillRange.SetActive(true);
    //            break;
    //        case JoystickType.None:
    //            break;
    //    };
    //}

    public void DragBegin(JoystickType joystickType)
    {
        if (joystickType == JoystickType.None)
        {
            Debug.LogWarning("조이스틱 타입이 NONE임");
            return;
        }

        var Range = joystickType switch
        {
            JoystickType.Move => moveRange,
            JoystickType.Attack => attackRange,
            JoystickType.Skill => skillRange,
            _ => moveRange,
        };

        ActiveRangesAndChangeColor(Range);
    }

    void ActiveRangesAndChangeColor(GameObject obj)
    {
        obj.SetActive(true);

        ChangeColor_A(obj, 0.8f);
    }


    public void Drag(float angle)
    {
        rangeContainer.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void DragEnd(JoystickType joystickType, float charge, Vector2 angle)
    {
        //switch (joystickType)
        //{
        //    case JoystickType.Move:
        //        Move(angle);
        //        break;
        //    case JoystickType.Attack:
        //        Attack(angle);
        //        break;
        //    case JoystickType.Skill:
        //        Skill(angle);
        //        break;
        //    case JoystickType.None:
        //        break;
        //};

        if(joystickType == JoystickType.None)
        {
            Debug.LogWarning("조이스틱 타입이 NONE임");
            return;
        }

        power = Mathf.Clamp(charge, minDragPower, maxDragPower);

        UnityAction action = joystickType switch
        {
            JoystickType.Move => () => Move(angle),
            JoystickType.Attack => () => Attack(angle),
            JoystickType.Skill => () => Skill(angle),
            _ => null,
        };

        action();

        MouseUp?.Invoke(); // 발사 직후 발동하는 트리거
    }

    private void Move(Vector2 angle)
    {
        rigid.velocity = ((angle * power) * pushPower);
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

    public IEnumerator DisableRanges_T()
    {
        isEnd = false;
        GameObject[] Gobj = { moveRange, attackRange, skillRange };
        GameObject S_obj = new GameObject();

        foreach (GameObject obj in Gobj)
            if (obj.activeSelf) S_obj = obj;

        moveRange.SetActive(false);
        attackRange.SetActive(false);

        ChangeColor_A(S_obj, 1f);

        yield return new WaitForSeconds(1f);

        S_obj.SetActive(false);
        isEnd = true;
        StopCoroutine(DisableRanges_T());
    }

    void ChangeColor_A(GameObject obj, float num_A)
    {
        Color color = obj.GetComponent<SpriteRenderer>().color;
        color = new Color(color.r, color.g, color.b, num_A);
        obj.GetComponent<SpriteRenderer>().color = color;
    }
}
