using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ar
{
    public ArSO so;

    public bool isSellected;
    public int currentCooltime { get; set; }
    public bool isRangeCharacter { get; protected set; }

    public bool isEnd = true;

    public UnityEvent MouseUp;

    protected float power;
    protected int skillCooltime;

    public float Power { get { return power; } }
    public float PushPower { get { return pushPower; } }

    private QuickSlot slot;

    private Transform rangeContainer;
    private GameObject moveRange;
    private GameObject attackRange;
    private GameObject skillRange;
    private GameObject skillActived;

    private MiniAr miniPlayer;

    [SerializeField] ItemSO[] itemSlots = new ItemSO[3];

    public Player()
    {

    }

    public Player(ArSO arSO)
    {
        gameObject.name = arSO.characterInfo.Name;
        sprite.sprite = arSO.characterInfo.Image;
    }

    protected override void Start()
    {
        base.Start();

        rangeContainer = transform.GetChild(0);
        moveRange = rangeContainer.GetChild(0).gameObject;
        attackRange = rangeContainer.GetChild(1).gameObject;
        skillRange = rangeContainer.GetChild(2).gameObject;
        skillActived = transform.GetChild(3).gameObject;
        DisableRanges();

        MouseUp.AddListener(() => { isMove = true; });

        StatReset();
    }

    protected override void StatReset()
    {
        stat.MaxHP = (int)so.surviveStats.MaxHP;
        stat.HP = (int)so.surviveStats.currentHP;
        stat.MaxSP = (int)so.surviveStats.MaxShield;
        stat.SP = (int)so.surviveStats.currentShield;
        stat.ATK = (int)so.attackStats.currentAtk;
        stat.SATK = (int)so.attackStats.currentSkillAtk;
        stat.CriPer = (int)so.criticalStats.currentCritalPer;
        stat.CriDmg = (int)so.criticalStats.currentCriticalDamage;
        stat.WEIGHT = (int)so.surviveStats.currentWeight;
        skillCooltime = so.skill.MaxSkillCoolTime;
        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 22;

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

        miniPlayer.ShowRange(obj);

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
    }

    private void Move(Vector2 angle)
    {
        MouseUp?.Invoke();
        TurnManager.Instance.SomeoneIsMoving = true;
        rigid.velocity = ((angle.normalized * power) * pushPower)/(1+stat.WEIGHT*0.1f);
    }

    protected virtual void Attack(Vector2 angle)
    {
        AnimAttackStart();
    }
    protected virtual void Skill(Vector2 angle)
    {
        AnimAttackStart();
        currentCooltime = skillCooltime;
        skillActived.SetActive(false);
        CameraMove.Instance.Shake();
    }

    public void DisableRanges()
    {
        moveRange.SetActive(false);
        attackRange.SetActive(false);
        skillRange.SetActive(false);
        miniPlayer.DisableRange();
    }

    public GameObject ActiveRange()
    {
        if (moveRange.activeSelf) return moveRange;
        else if (attackRange.activeSelf) return attackRange;
        else if (skillRange.activeSelf) return skillRange;
        else return null;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (!collision.transform.CompareTag("Object"))
        {
            BattleManager.Instance.SettingAr(this);
            CameraMove.Instance.Shake();
            EffectManager.Instance.InstantiateEffect(0, collision.contacts[0].point, transform.position, collision.contacts[0].point);
        }
    }

    private void OnMouseDown()
    {
        if (!TurnManager.Instance.IsPlayerTurn || isDead || TurnManager.Instance.SomeoneIsMoving) return;
        PlayerController.Instance.SellectPlayer(slot);
    }

    public void Connect(QuickSlot slot)
    {
        this.slot = slot;
        OnBattleDie.AddListener(()=>this.slot.SetSlotActive(false));
        OnOutDie.AddListener(()=>this.slot.SetSlotActive(false));
        slot.SkillReady(true);
    }

    public void CountCooltime()
    {
        if (currentCooltime > 0)
        {
            currentCooltime--;
            slot.SkillReady(false);
        }
        if (currentCooltime == 0)
        {
            skillActived.SetActive(true);
            slot.SkillReady(true);
        }
    }

    public IEnumerator DisableRanges_T()
    {
        isEnd = false;
        /*GameObject[] Gobj = { moveRange, attackRange, skillRange };
        GameObject S_obj = new GameObject();

        foreach (GameObject obj in Gobj)
            if (obj.activeSelf) S_obj = obj;

        moveRange.SetActive(false);
        attackRange.SetActive(false);

        ChangeColor_A(S_obj, 1f);*/

        DisableRanges();

        yield return new WaitForSeconds(0.3f);

        //S_obj.SetActive(false);
        isEnd = true;
        StopCoroutine(DisableRanges_T());
    }

    void ChangeColor_A(GameObject obj, float num_A)
    {
        Color color = obj.GetComponent<SpriteRenderer>().color;
        color = new Color(color.r, color.g, color.b, num_A);
        obj.GetComponent<SpriteRenderer>().color = color;
    }

    public void SetMini(MiniAr mini)
    {
        miniPlayer = mini;
    }

    protected override bool DeadCheck()
    {
        slot.SetHPBar((float)stat.HP / stat.MaxHP);
        slot.SetSPBar((float)stat.SP / stat.MaxSP);
        return base.DeadCheck();
    }
}
