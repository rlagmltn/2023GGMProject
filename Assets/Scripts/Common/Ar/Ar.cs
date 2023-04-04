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
    public bool isMove;

    protected float minDragPower = 0.4f;
    protected float maxDragPower = 1.5f;
    protected float pushPower;

    protected Transform hpBar;
    protected SpriteRenderer hpImage;

    protected Transform dpBar;
    protected SpriteRenderer dpImage;

    public Rigidbody2D rigid { get; protected set; }
    public Vector2 lastVelocity { get; protected set; }

    public UnityEvent BeforeCrash;
    public UnityEvent AfterCrash;
    public UnityEvent BeforeAttack;
    public UnityEvent AfterAttack;
    public UnityEvent BeforeDefence;
    public UnityEvent AfterDefence;
    public UnityEvent AfterMove;
    public UnityEvent OnOutDie;
    public UnityEvent OnBattleDie;

    protected SpriteRenderer sprite;
    private Animator animator;

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

        AfterCrash.AddListener(() =>
        {
            battleTarget = null;
            CameraMove.Instance.TimeFreeze(1);
            CameraMove.Instance.EffectZoom(0);
        });
    }

    protected virtual void StatReset() // ��ġ �ʱ�ȭ
    {
        isDead = false;
        DeadCheck();
    }

    protected void FixedUpdate()
    {
        if (rigid.velocity.normalized != lastVelocity.normalized && rigid.velocity.magnitude != 0)
        {
            RaycastHit2D[] hit = Physics2D.BoxCastAll(gameObject.transform.position, Vector2.one/2, Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg ,rigid.velocity.normalized, rigid.velocity.magnitude / 2 / (1 + stat.WEIGHT));

            if (hit.Length <= 1) return;

            if(hit[1].collider.GetComponent<Ar>())
            {
                battleTarget = hit[1].collider.transform;
            }
            else
            {
                battleTarget = null;
                CameraMove.Instance.TimeFreeze(1);
                CameraMove.Instance.EffectZoom(0);
            }
        }

        lastVelocity = rigid.velocity;
    }

    protected void Update()
    {
        if (rigid.velocity.magnitude <= 0.1f && isMove)
        {
            isMove = false;
            TurnManager.Instance.SomeoneIsMoving = false;
            AfterMove?.Invoke();
        }
        if (rigid.velocity.x < 0) sprite.flipX = true;
        else if (rigid.velocity.x > 0) sprite.flipX = false;

        if(battleTarget!=null)
        {
            var distance = transform.position - battleTarget.position;
            if(distance.magnitude<slowMagnitude)
            {
                var amount = distance.magnitude / slowMagnitude == float.NaN ? 1 : distance.magnitude / slowMagnitude;
                CameraMove.Instance.EffectZoom(amount);
                if (amount < 0.25f) amount *= amount;
                CameraMove.Instance.TimeFreeze(amount);
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Object"))
        {
            //BeforeCrash?.Invoke();

            //rigid.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
            //CameraMove.Instance.Shake();
            //EffectManager.Instance.InstantiateEffect(0, collision.contacts[0].point, transform.position, collision.contacts[0].point);

            collision.gameObject.SetActive(false);

            //AfterCrash?.Invoke();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Out"))
        {
            Out();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform == battleTarget)
        {
            battleTarget = null;
            CameraMove.Instance.TimeFreeze(1);
            CameraMove.Instance.EffectZoom(0);
        }
    }

    public void Push(Vector2 velo)
    {
        rigid.velocity = velo;
    }

    public bool Hit(int damage)
    {
        if(damage>0 && stat.SP>0)
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
        return DeadCheck();
    }

    protected bool DeadCheck()
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

    protected void Out()
    {
        OnOutDie.Invoke();
        isDead = true;
        TurnManager.Instance.SomeoneIsMoving = false;
        GameManager.Instance.ArDead();
        gameObject.SetActive(false);
    }

    public void DecreaseDP(int val)
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
