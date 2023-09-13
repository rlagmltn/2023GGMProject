using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ar : MonoBehaviour
{
    public Stat stat { get; set; }

    public string Name;

    public bool isDead { get; set; }
    public bool isMove { get; set; }

    public float pushPower = 40f;
    protected float minDragPower = 0.4f;
    protected float maxDragPower = 1.5f;

    protected Bar hpBar;

    protected Bar spBar;

    public Rigidbody2D rigid { get; protected set; }
    public Vector2 lastVelocity { get; protected set; }

    [HideInInspector] public UnityEvent StartTurn;
    [HideInInspector] public UnityEvent BeforeCrash;
    [HideInInspector] public UnityEvent AfterCrash;
    [HideInInspector] public UnityEvent BeforeAttack;
    [HideInInspector] public UnityEvent AfterAttack;
    [HideInInspector] public UnityEvent BeforeDefence;
    [HideInInspector] public UnityEvent AfterDefence;
    [HideInInspector] public UnityEvent AfterMove;
    [HideInInspector] public UnityEvent OnOutDie;
    [HideInInspector] public UnityEvent OnBattleDie;
    [HideInInspector] public UnityEvent OnUsedSkill;
    [HideInInspector] public UnityEvent OnCrashed;
    [HideInInspector] public UnityEvent EndTurn;
    [HideInInspector] public UnityEvent Dealed;
    [HideInInspector] public UnityEvent Healed;

    public Ar lastDealed { get; set; }
    public Ar lastHealed { get; set; }

    public bool isRangeCharacter { get; protected set; }

    protected Transform character;
    protected AnimationManager animationManager;
    public CameraMove cameraMove { get; protected set; }

    [SerializeField] protected Transform battleTarget;
    private float slowMagnitude = 5f;

    protected int DamageDrcrease = 0;

    public bool isFuckingRoot = false;

    protected virtual void Start()
    {
        stat = new Stat();
        rigid = GetComponent<Rigidbody2D>();
        hpBar = transform.GetChild(1).GetComponent<Bar>();
        spBar = transform.GetChild(2).GetComponent<Bar>();
        character = transform.GetChild(3);
        animationManager = GetComponent<AnimationManager>();
        cameraMove = FindObjectOfType<CameraMove>();

        AfterCrash.AddListener(InitTImeScale);
        AfterMove.AddListener(InitTImeScale);
        AfterMove.AddListener(TurnOver);
        OnBattleDie.AddListener(InitTImeScale);
        OnOutDie.AddListener(InitTImeScale);
    }

    protected void InitTImeScale()
    {
        battleTarget = null;
        cameraMove.TimeFreeze();
        cameraMove.ApplyCameraSize();
    }

    public virtual void StatReset() // 수치 초기화
    {
        isDead = false;
        DeadCheck();
    }

    protected virtual void FixedUpdate()
    {
        if (rigid.velocity.normalized != lastVelocity.normalized && rigid.velocity.magnitude != 0 && isMove)
        {
            Flip();
            lastVelocity = rigid.velocity;
            Collider2D[] hit = Physics2D.OverlapBoxAll((Vector2)transform.position + lastVelocity/8, new Vector2(0.95f, lastVelocity.magnitude/4), Mathf.Atan2(lastVelocity.y, lastVelocity.x) * Mathf.Rad2Deg + 90);

            foreach(Collider2D col in hit)
            {
                if (col.GetComponent<Ar>() && col.gameObject != gameObject)
                {
                    battleTarget = col.transform;
                    cameraMove.MovetoTarget(battleTarget);
                    break;
                }
            }
        }
        lastVelocity = rigid.velocity;
    }

    protected virtual void Update()
    {
        StopMove();
        BattleEffect();
    }

    private void BattleEffect()
    {
        if (battleTarget != null)
        {
            var distance = Vector2.Distance(transform.position, battleTarget.position);
            if (distance < slowMagnitude)
            {
                var amount = distance / slowMagnitude == float.NaN ? 1 : distance / slowMagnitude;
                cameraMove.TimeFreeze(amount - lastVelocity.magnitude/(pushPower*2));
                cameraMove.ApplyCameraSize(amount * 0.7f);
            }
        }
    }

    private void StopMove()
    {
        if (rigid.velocity.magnitude <= 1.5f && isMove)
        {
            rigid.velocity = Vector2.zero;
            isMove = false;
            AfterMove?.Invoke();
            animationManager.SetState(CharacterState.Idle);
        }
    }

    protected void Flip()
    {
        if (rigid.velocity.x < 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if (rigid.velocity.x > 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Object"))
        {
            rigid.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
            OnCrashed?.Invoke();
            cameraMove.Shake();
            EffectManager.Instance.InstantiateEffect(0, collision.contacts[0].point, transform.position, collision.contacts[0].point);
            BattleManager.Instance.PlayBumpSound();
            InitTImeScale();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Out"))
        {
            stat.HP = 0;
            stat.SP = 0;
            Out();
        }
    }

    public virtual void Push(Vector2 velo)
    {
        rigid.velocity = velo;
    }

    public virtual bool Hit(int damage, Ar dealer)
    {
        damage -= DamageDrcrease;
        EffectManager.Instance.InstantiateFloatDamage(transform.position).DamageText(damage);
        animationManager.Hit();
        if (stat.SP > 0)
        {
            stat.SP -= damage;
            if (stat.SP < 0)
            {
                damage = -stat.SP;
                stat.SP = 0;
            }
            else damage = 0;
        }
        stat.HP = Mathf.Clamp(stat.HP - damage, 0, stat.MaxHP);
        if(dealer!=null)
        {
            dealer.lastDealed = this;
            dealer.Dealed.Invoke();
        }
        return DeadCheck();
    }

    public virtual void Heal(int heal, Ar healer)
    {
        stat.HP = Mathf.Clamp(stat.HP + heal, 0, stat.MaxHP);
        if(healer!=null)
        {
            healer.lastHealed = this;
            healer.Healed.Invoke();
        }
        DeadCheck();
    }

    public virtual bool DeadCheck()
    {
        //ArInfoManager.Instance.ShowBulletInfo(this);
        stat.HP = Mathf.Clamp(stat.HP, 0, stat.MaxHP);
        stat.SP = Mathf.Clamp(stat.SP, 0, stat.MaxSP);
        if (stat.HP <= 0)
        {
            OnBattleDie.Invoke();
            //Pooling();
            isDead = true;
            TurnManager.Instance.SomeoneIsMoving = false;
            GameManager.Instance.ArDead();
            animationManager.Die();
            return true;
        }

        if (stat.SP > 0)
        {
            spBar.gameObject.SetActive(true);
            spBar.GageChange((float)stat.SP / stat.MaxSP);
        }
        else spBar.gameObject.SetActive(false);
        hpBar.GageChange((float)stat.HP / stat.MaxHP);
        return false;
    }

    protected virtual void Out()
    {
        OnOutDie?.Invoke();
        rigid.velocity = lastVelocity/5;
        isMove = false;
        EffectManager.Instance.InstantiateEffect_P(Effect.SUNK, transform.position);
        DeadCheck();
    }

    public virtual void OutDie()
    {
        gameObject.SetActive(false);
    }

    public void DecreaseSP(int val)
    {
        stat.SP = Mathf.Max(0, stat.SP - val);
    }

    protected virtual void TurnOver()
    {
        TurnManager.Instance.SomeoneIsMoving = false;
    }

    public void AnimMoveStart()
    {
        animationManager.Attack();
    }
}
