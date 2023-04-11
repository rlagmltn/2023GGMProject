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

    protected SpriteRenderer sprite;
    private Animator animator;
    protected CameraMove cameraMove;

    [SerializeField] private Transform battleTarget;
    private float slowMagnitude = 5f;

    protected virtual void Start()
    {
        stat = new Stat();
        rigid = GetComponent<Rigidbody2D>();
        hpBar = transform.GetChild(1).GetChild(0);
        hpImage = hpBar.GetComponentInChildren<SpriteRenderer>();
        dpBar = transform.GetChild(2).GetChild(0);
        dpImage = dpBar.GetComponentInChildren<SpriteRenderer>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cameraMove = FindObjectOfType<CameraMove>();

        AfterCrash.AddListener(InitTImeScale);
        AfterMove.AddListener(InitTImeScale);
    }

    void InitTImeScale()
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

    protected void FixedUpdate()
    {
        if (rigid.velocity.normalized != lastVelocity.normalized && rigid.velocity.magnitude != 0)
        {
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

    protected void Update()
    {
        Flip();
        StopMove();
        BattleEffect();
    }

    private void BattleEffect()
    {
        if (battleTarget != null)
        {
            var distance = transform.position - battleTarget.position;
            if (distance.magnitude < slowMagnitude)
            {
                var amount = distance.magnitude / slowMagnitude == float.NaN ? 1 : distance.magnitude / slowMagnitude;
                cameraMove.TimeFreeze(amount);
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
            TurnManager.Instance.SomeoneIsMoving = false;
            AfterMove?.Invoke();
        }
    }

    private void Flip()
    {
        if (rigid.velocity.x < 0) sprite.flipX = true;
        else if (rigid.velocity.x > 0) sprite.flipX = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Object"))
        {
            //BeforeCrash?.Invoke();

            rigid.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
            cameraMove.Shake();
            EffectManager.Instance.InstantiateEffect(0, collision.contacts[0].point, transform.position, collision.contacts[0].point);

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

    public void Push(Vector2 velo)
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

        StartCoroutine(HitColorChange(Color.red));
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
        if (stat.MaxSP != 0) dpBar.localScale = new Vector3(Mathf.Clamp((float)stat.SP / stat.MaxSP, 0, 1), 1, 1); else dpBar.localScale = new Vector3(0, 1, 1);
        hpBar.localScale = new Vector3(Mathf.Clamp((float)stat.HP / stat.MaxHP, 0, 1), 1, 1);
        return false;
    }

    protected virtual void Out()
    {
        OnOutDie?.Invoke();
        isDead = true;
        TurnManager.Instance.SomeoneIsMoving = false;
        GameManager.Instance.ArDead();
        gameObject.SetActive(false);
    }

    private IEnumerator HitColorChange(Color color)
    {
        sprite.color = color;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;
    }

    public void DecreaseSP(int val)
    {
        stat.SP = Mathf.Max(0, stat.SP - val);
    }

    public void AnimAttackStart()
    {
        animator?.SetBool("isAttack", true);
    }

    public void AnimAttackFinish()
    {
        animator?.SetBool("isAttack", false);
    }
}
