using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ar : MonoBehaviour
{
    public Stat stat { get; set; }

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

    protected virtual void Start()
    {
        stat = new Stat();
        rigid = GetComponent<Rigidbody2D>();
        hpBar = transform.GetChild(1).GetChild(0);
        hpImage = hpBar.GetComponentInChildren<SpriteRenderer>();
        dpBar = transform.GetChild(2).GetChild(0);
        dpImage = dpBar.GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void StatReset() // 수치 초기화
    {
        stat.HP = stat.MaxHP;
        stat.DP = stat.MaxDP;
        isDead = false;
        DeadCheck();
    }

    protected void FixedUpdate()
    {
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
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Object"))
        {
            BeforeCrash?.Invoke();

            rigid.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
            CameraMove.Instance.Shake();
            EffectManager.Instance.InstantiateEffect(0, collision.contacts[0].point, transform.position, collision.contacts[0].point);

            AfterCrash?.Invoke();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Out"))
        {
            Out();
        }
    }

    public void Push(Vector2 velo)
    {
        rigid.velocity = velo;
    }

    public bool Hit(int damage)
    {
        if(damage>0 && stat.DP>0)
        {
            stat.DP -= damage;
            if (stat.DP < 0)
            {
                damage = -stat.DP;
                stat.DP = 0;
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
        if (stat.MaxDP != 0) dpBar.localScale = new Vector3(Mathf.Clamp((float)stat.DP / stat.MaxDP, 0, 1), 1, 1); else dpBar.localScale = new Vector3(0, 1, 1);
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
        stat.DP = Mathf.Max(0, stat.DP - val);
    }
}
