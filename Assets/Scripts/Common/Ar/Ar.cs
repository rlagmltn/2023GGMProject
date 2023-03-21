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
    }

    protected virtual void StatReset() // ¼öÄ¡ ÃÊ±âÈ­
    {
        stat.HP = stat.MaxHP;
        isDead = false;
        stat.WEIGHT = 1;
        //hpBar.localScale = new Vector3(Mathf.Clamp(HP / MaxHP, 0, 1), 1, 1);
    }

    protected void FixedUpdate()
    {
        lastVelocity = rigid.velocity;
    }

    protected void Update()
    {
        if (rigid.velocity.magnitude <= 0.5f && isMove)
        {
            isMove = false;
            AfterMove?.Invoke();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Object"))
        {
            rigid.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
            CameraMove.Instance.Shake();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Out"))
        {
            Debug.Log("À¸¾ÓÁê±Ý");
            Out();
        }
    }

    public void Push(Vector2 velo)
    {
        rigid.velocity = velo;
    }

    public bool Hit(float damage)
    {
        stat.HP = Mathf.Clamp(stat.HP - damage, 0, stat.MaxHP);
        Debug.Log(name + stat.HP);
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
            GameManager.Instance.ArDead();
            gameObject.SetActive(false);
            return true;
        }
        hpBar.localScale = new Vector3(Mathf.Clamp(stat.HP / stat.MaxHP, 0, 1), 1, 1);
        return false;
    }

    protected void Out()
    {
        OnOutDie.Invoke();
        isDead = true;
        GameManager.Instance.ArDead();
        gameObject.SetActive(false);
    }
}
