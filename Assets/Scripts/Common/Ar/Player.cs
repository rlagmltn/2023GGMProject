using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ar
{
    public ArSO so;

    public bool isSellected { get; set; }
    public int currentCooltime { get; set; }
    public bool isRangeCharacter { get; protected set; }

    public bool isEnd = true;

    [HideInInspector] public UnityEvent MouseUp;

    protected float power;
    protected int skillCooltime;

    public float Power { get { return power; } }
    public float PushPower { get { return pushPower; } }

    public CircleCollider2D Collide { get; set; }

    private QuickSlot slot;

    private Transform rangeContainer;
    private SpriteRenderer moveRange;
    private SpriteRenderer attackRange;
    private SpriteRenderer skillRange;

    private PlayerController playerController;

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
        moveRange = rangeContainer.GetChild(0).GetComponent<SpriteRenderer>();
        attackRange = rangeContainer.GetChild(1).GetComponent<SpriteRenderer>();
        skillRange = rangeContainer.GetChild(2).GetComponent<SpriteRenderer>();
        playerController = FindObjectOfType<PlayerController>();
        DisableRanges();

        MouseUp.AddListener(() => { isMove = true; });
    
        StatReset();

        gameObject.SetActive(false);
    }

    public override void StatReset()
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

        foreach(ItemSO item in itemSlots)
        {
            item?.Armed(this);
        }

        base.StatReset();
    }

    public void DragBegin(JoystickType joystickType)
    {
        if (joystickType == JoystickType.None)
        {
            Debug.LogWarning("���̽�ƽ Ÿ���� NONE��");
            return;
        }

        var Range = joystickType switch
        {
            JoystickType.Move => moveRange.gameObject,
            JoystickType.Attack => attackRange.gameObject,
            JoystickType.Skill => skillRange.gameObject,
            _ => moveRange.gameObject,
        };

        ActiveRangesAndChangeColor(Range);
    }

    void ActiveRangesAndChangeColor(GameObject obj)
    {
        obj.SetActive(true);
        ChangeColor_A(obj, 0.8f);
    }


    public void Drag(float angle, float dis)
    {
        rangeContainer.rotation = Quaternion.Euler(0, 0, angle+180);
        moveRange.size = new Vector2((dis * 2), 1);
        attackRange.size = new Vector2((dis * 2), 1);
    }

    public void DragEnd(JoystickType joystickType, float charge, Vector2 angle)
    {
        if(joystickType == JoystickType.None)
        {
            return;
        }

        power = Mathf.Clamp(charge, minDragPower, maxDragPower);

        UnityAction action = joystickType switch
        {
            JoystickType.Move => () => Move(angle),
            JoystickType.Attack => () => Attack(-angle),
            JoystickType.Skill => () => Skill(-angle),
            _ => null,
        };

        action();
    }

    private void Move(Vector2 angle)
    {
        MouseUp?.Invoke();
        TurnManager.Instance.SomeoneIsMoving = true;
        rigid.velocity = ((angle.normalized * power) * pushPower)/(1+stat.WEIGHT*0.1f);
        EffectManager.Instance.InstantiateEffect_P(Effect.DASH, transform.position, new Vector2(-angle.x, angle.y));
    }

    protected virtual void Attack(Vector2 angle)
    {
        AnimAttackStart();
    }
    protected virtual void Skill(Vector2 angle)
    {
        currentCooltime = skillCooltime;
    }
    protected virtual void Passive() { }

    public virtual void AnimTimingSkill() { }

    public void DisableRanges()
    {
        moveRange.gameObject.SetActive(false);
        attackRange.gameObject.SetActive(false);
        skillRange.gameObject.SetActive(false);
    }

    public GameObject ActiveRange()
    {
        if (moveRange.gameObject.activeSelf) return moveRange.gameObject;
        else if (attackRange.gameObject.activeSelf) return attackRange.gameObject;
        else if (skillRange.gameObject.activeSelf) return skillRange.gameObject;
        else return null;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (!collision.transform.CompareTag("Object"))
        {
            BattleManager.Instance.SettingAr(this);
            cameraMove.Shake();
            EffectManager.Instance.InstantiateEffect(0, collision.contacts[0].point, transform.position, collision.contacts[0].point);
        }
    }

    private void OnMouseDown()
    {
        if (!TurnManager.Instance.IsPlayerTurn || isDead || TurnManager.Instance.SomeoneIsMoving) return;
        playerController?.SellectPlayer(slot);
    }

    public void Connect(QuickSlot slot)
    {
        this.slot = slot;
        OnBattleDie.AddListener(()=>this.slot.SetSlotActive(false));
        OnOutDie.AddListener(()=>this.slot.SetSlotActive(false));
        slot.SkillReady(true);
        Collide = GetComponent<CircleCollider2D>();
    }

    public void CountCooltime()
    {
        if (currentCooltime > 0)
        {
            currentCooltime--;
            slot?.SkillReady(false);
        }
        if (currentCooltime == 0)
        {
            slot?.SkillReady(true);
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

    protected override void Out()
    {
        DeadSave();
        base.Out();
    }

    public override bool DeadCheck()
    {
        DeadSave();
        return base.DeadCheck();
    }

    private void DeadSave()
    {
        slot?.SetHPBar((float)stat.HP / stat.MaxHP);
        slot?.SetSPBar((float)stat.SP / stat.MaxSP);
        if (stat.HP <= 0) TakeAllStat();
    }

    public void TakeAllStat()
    {
        so.surviveStats.currentHP = stat.HP;
        so.attackStats.currentAtk = stat.ATK;
        so.attackStats.currentSkillAtk = stat.SATK;
        so.criticalStats.currentCritalPer = stat.CriPer;
        so.criticalStats.currentCriticalDamage = stat.CriDmg;
        so.surviveStats.currentWeight = stat.WEIGHT;
        if(!isDead) so.surviveStats.currentShield = so.surviveStats.MaxShield;
    }
}
