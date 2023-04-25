using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ar : MonoBehaviour
{
    public Stat stat { get; set; }

    public string Name;

    public bool isDead { get; set; }
    public bool isUsingSkill { get; set; }
    public bool isMove { get; set; }

    protected float minDragPower = 0.4f;
    protected float maxDragPower = 1.5f;
    protected float pushPower;

    protected Transform hpBar;
    protected SpriteRenderer hpImage;

    protected Transform dpBar;
    protected SpriteRenderer dpImage;

    public Rigidbody2D rigid { get; protected set; }
    public Vector2 lastVelocity { get; protected set; }

    [HideInInspector] public UnityEvent BeforeCrash;
    [HideInInspector] public UnityEvent AfterCrash;
    [HideInInspector] public UnityEvent BeforeAttack;
    [HideInInspector] public UnityEvent AfterAttack;
    [HideInInspector] public UnityEvent BeforeDefence;
    [HideInInspector] public UnityEvent AfterDefence;
    [HideInInspector] public UnityEvent AfterMove;
    [HideInInspector] public UnityEvent OnOutDie;
    [HideInInspector] public UnityEvent OnBattleDie;

    protected Transform character;
    protected AnimationManager animationManager;
    protected CameraMove cameraMove;

    [SerializeField] protected Transform battleTarget;
    private float slowMagnitude = 5f;

    protected virtual void Start()
    {
        stat = new Stat();
        rigid = GetComponent<Rigidbody2D>();
        hpBar = transform.GetChild(1);
        hpImage = hpBar.GetChild(0).GetComponent<SpriteRenderer>();
        dpBar = transform.GetChild(2);
        dpImage = dpBar.GetChild(0).GetComponent<SpriteRenderer>();
        character = transform.GetChild(3);
        animationManager = GetComponent<AnimationManager>();
        cameraMove = FindObjectOfType<CameraMove>();

        AfterCrash.AddListener(InitTImeScale);
        AfterMove.AddListener(InitTImeScale);
        AfterMove.AddListener(TurnOver);
    }

    protected void InitTImeScale()
    {
        battleTarget = null;
        cameraMove.TimeFreeze();
        cameraMove.ApplyCameraSize();
    }

    public virtual void StatReset() // 수치 초기화
    {
        pushPower = 40;
        isDead = false;
        DeadCheck();
    }

    protected virtual void FixedUpdate()
    {
        if (rigid.velocity.normalized != lastVelocity.normalized && rigid.velocity.magnitude != 0)
        {
            Flip();
            lastVelocity = rigid.velocity;
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, lastVelocity.normalized, lastVelocity.magnitude / 4);
            Debug.DrawRay(transform.position, lastVelocity / 4, Color.red, 3f);

            if (hit.Length <= 1) return;

            if(hit[1].collider.GetComponent<Ar>()&&hit[1].collider.gameObject!=gameObject)
            {
                battleTarget = hit[1].collider.transform;
                cameraMove.MovetoTarget(battleTarget);
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
                cameraMove.TimeFreeze(amount - lastVelocity.magnitude/(pushPower*3));
                cameraMove.ApplyCameraSize(amount);
            }
        }
    }

    private void StopMove()
    {
        if (rigid.velocity.magnitude <= 0.2f && isMove)
        {
            rigid.velocity = Vector2.zero;
            isMove = false;
            AfterMove?.Invoke();
        }
    }

    private void Flip()
    {
        if (rigid.velocity.x < 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if (rigid.velocity.x > 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Object"))
        {
            //BeforeCrash?.Invoke();

            rigid.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
            cameraMove.Shake();
            EffectManager.Instance.InstantiateEffect(0, collision.contacts[0].point, transform.position, collision.contacts[0].point);
            InitTImeScale();
            //AfterCrash?.Invoke();
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

    public virtual bool Hit(int damage)
    {
        EffectManager.Instance.InstantiateFloatDamage(transform.position).DamageText(damage);
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
        stat.HP -= damage;
        HpMaxCheck();
        return DeadCheck();
    }

    public void HpMaxCheck()
    {
        stat.HP = Mathf.Clamp(stat.HP, 0, stat.MaxHP);
    }

    public virtual bool DeadCheck()
    {
        //ArInfoManager.Instance.ShowBulletInfo(this);
        if (stat.HP <= 0)
        {
            OnBattleDie.Invoke();
            //Pooling();
            isDead = true;
            TurnManager.Instance.SomeoneIsMoving = false;
            GameManager.Instance.ArDead();
            gameObject.SetActive(false);
            return true;
        }

        if (stat.SP > 0)
        {
            dpBar.gameObject.SetActive(true);
            dpImage.size = new Vector2((float)stat.SP / stat.MaxSP * 2, 2);
        }
        else dpBar.gameObject.SetActive(false);
        hpImage.size = new Vector2((float)stat.HP / stat.MaxHP * 2, 2);
        return false;
    }

    protected virtual void Out()
    {
        OnOutDie?.Invoke();
        isDead = true;
        rigid.velocity = lastVelocity/5;
        isMove = false;
        TurnManager.Instance.SomeoneIsMoving = false;
        GameManager.Instance.ArDead();
        EffectManager.Instance.InstantiateEffect_P(Effect.SUNK, transform.position, Vector2.zero);
        animationManager.Die();
    }

    public void OutDie()
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
