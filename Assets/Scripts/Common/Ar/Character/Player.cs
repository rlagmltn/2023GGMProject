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

    protected Transform rangeContainer;
    private SpriteRenderer moveRange;
    private SpriteRenderer attackRange;
    private SpriteRenderer skillRange;
    protected Collider2D skillCollider;

    public List<ItemInfo> ItemInfos; //지워야함

    public List<TypeAndInfo> TAI;

    public Dictionary<ItemPassiveType, ItemInfo> ItemInfoDic = new Dictionary<ItemPassiveType, ItemInfo>(); //테스트용

    protected PlayerController playerController;
    protected bool isAttack = false;
    protected bool isSkill = false;

    [SerializeField] ItemSO[] itemSlots = new ItemSO[3];

    public Player()
    {

    }

    public Player(ArSO arSO)
    {
        gameObject.name = arSO.characterInfo.Name;
    }

    protected override void Start()
    {
        base.Start();

        rangeContainer = transform.GetChild(0);
        moveRange = rangeContainer.GetChild(0).GetComponent<SpriteRenderer>();
        attackRange = rangeContainer.GetChild(1).GetComponent<SpriteRenderer>();
        skillRange = rangeContainer.GetChild(2).GetComponent<SpriteRenderer>();
        skillCollider = skillRange.GetComponent<Collider2D>();
        playerController = FindObjectOfType<PlayerController>();
        DisableRanges();

        for (int num = 0; num < itemSlots.Length; num++)
        {
            if (so.E_Item.itmeSO[num] == null) continue;
            itemSlots[num] = so.E_Item.itmeSO[num];

            for (int j = 0; j < so.E_Item.itmeSO[num].TAI.Count; j++)
            {
                TypeAndInfo _TAI = new TypeAndInfo();
                _TAI.itemPassiveType = so.E_Item.itmeSO[num].TAI[j].itemPassiveType;
                _TAI.Info = Instantiate(so.E_Item.itmeSO[num].TAI[j].Info, this.transform);
                TAI.Add(_TAI);
            }
        }

        MouseUp.AddListener(() => { isMove = true; });
    
        StatReset();

        gameObject.SetActive(false);
    }

    public override void StatReset()
    {
        Armed();
        stat.MaxHP = (int)so.surviveStats.MaxHP;
        stat.HP = (int)so.surviveStats.currentHP;
        stat.MaxSP = (int)so.surviveStats.MaxShield;
        stat.SP = (int)so.surviveStats.currentShield;
        stat.ATK = (int)so.attackStats.currentAtk;
        stat.SATK = (int)so.attackStats.currentSkillAtk;
        stat.CriPer = (int)so.criticalStats.currentCriticalPer;
        stat.CriDmg = (int)so.criticalStats.currentCriticalDamage;
        stat.WEIGHT = (int)so.surviveStats.currentWeight;
        skillCooltime = so.skill.MaxSkillCoolTime;
        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        base.StatReset();
    }

    void Armed()
    {

        for (int num = 0; num < itemSlots.Length; num++)
        {
            if (itemSlots[num] == null) continue;
            stat += itemSlots[num].stat;
            skillCooltime -= itemSlots[num].SkillCoolDown; //이거 언암드에도 해줘야함
        }

        for(int num = 0; num < TAI.Count; num++)
        {
            switch(TAI[num].itemPassiveType)
            {
                case ItemPassiveType.BeforeCrash:
                    BeforeCrash.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.AfterCrash:
                    AfterCrash.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.BeforeAttack:
                    BeforeAttack.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.AfterAttack:
                    AfterAttack.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.BeforeDefence:
                    BeforeDefence.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.AfterDefence:
                    AfterDefence.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.AfterMove:
                    AfterMove.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.OnOutDie:
                    OnOutDie.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.OnBattleDie:
                    OnBattleDie.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.MouseUp:
                    MouseUp.AddListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.Alway:


                    break;
                default:
                    Debug.LogError("아이템 타입이 정해지지 않았습니다!");
                    break;
            }
            TAI[num].Info.GetPlayer(this);
        }
    }

    protected override void FixedUpdate()
    {
        if (!TurnManager.Instance.IsPlayerTurn) return;
        base.FixedUpdate();
    }

    public void DragBegin(JoystickType joystickType)
    {
        if (joystickType == JoystickType.None)
        {
            Debug.LogWarning("조이스틱 타입이 NONE임");
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
        cameraMove.MovetoTarget(gameObject.transform);

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
        Debug.Log(rigid.velocity.magnitude);
        EffectManager.Instance.InstantiateEffect_P(Effect.DASH, transform.position, new Vector2(-angle.x, angle.y));
    }

    protected virtual void Passive() { }

    protected virtual void Attack(Vector2 angle) 
    {
        isAttack = true;
        animationManager.Attack();
    }
    public virtual IEnumerator AnimTimingAttack() { yield return null; }

    protected virtual void Skill(Vector2 angle)
    {
        isSkill = true;
        currentCooltime = skillCooltime;
        StartCoroutine(AnimTimingSkill());
    }
    public virtual IEnumerator AnimTimingSkill() { yield return null; }

    public void AnimFinish()
    {
        isAttack = false;
        isSkill = false;
    }

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
        if(playerController.IsBatchMode)
        {
            Collide.enabled = false;
            slot.background.color = Color.yellow;
            cameraMove.isBatchmode = true;
            cameraMove.isDragmode = false;
        }
        else if(!TurnManager.Instance.IsPlayerTurn || isDead || TurnManager.Instance.SomeoneIsMoving || TurnManager.Instance.IsUsedAllTurn()) return;
        playerController?.SellectPlayer(slot);
    }

    private void OnMouseDrag()
    {
        if (!playerController.IsBatchMode) return;
        transform.position = Util.Instance.mousePosition;
    }

    private void OnMouseUp()
    {
        if (!playerController.IsBatchMode) return;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0), 0.01f, LayerMask.GetMask("Batch"));
        if (ray.collider != null && !ray.collider.CompareTag("UI"))
        {
            slot.isBatched = true;
            Collide.enabled = true;
            slot.background.color = Color.gray;
        }
        else
        {
            playerController.BatchCount--;
            playerController.ChangeBatchCount();
            playerController.quickSlotHolder.Remove(slot);
            slot.isBatched = false;
            gameObject.SetActive(false);
            slot.background.color = Color.black;
        }
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
        so.surviveStats.currentWeight = stat.WEIGHT;
        if(!isDead) so.surviveStats.currentShield = so.surviveStats.MaxShield;
    }
}
