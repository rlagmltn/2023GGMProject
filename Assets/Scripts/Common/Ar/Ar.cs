using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ar : MonoBehaviour
{
    public float MaxHP { get; set; }//최대체력
    public float HP { get; set; }//현재 체력
    public float ATK { get; set; }//기본 공격력
    public float SATK { get; set; } //스킬공격력
    public float DEF { get; set; }//방어력
    public float WEIGHT { get; set; }//무게

    public Sprite arSprite { get; set; }

    public bool isDead { get; set; }
    public bool isUsingSkill { get; set; }

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
        rigid = GetComponent<Rigidbody2D>();
        hpBar = transform.GetChild(1).GetChild(0);
        hpImage = hpBar.GetComponentInChildren<SpriteRenderer>();
        arSprite = transform.GetComponent<SpriteRenderer>().sprite;
    }

    protected virtual void StatReset() // 수치 초기화
    {
        HP = MaxHP;
        isDead = false;
        WEIGHT = 1;
        //hpBar.localScale = new Vector3(Mathf.Clamp(HP / MaxHP, 0, 1), 1, 1);
    }

    protected void FixedUpdate()
    {
        lastVelocity = rigid.velocity;
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
            OnOutDie.Invoke();
            Debug.Log("으앙쥬금");
            Out();
        }
    }

    public void Push(Vector2 velo)
    {
        rigid.velocity = velo;
    }

    public bool Hit(float damage)
    {
        HP = Mathf.Clamp(HP - damage, 0, MaxHP);
        Debug.Log(name + HP);
        return DeadCheck();
    }

    protected bool DeadCheck()
    {
        //ArInfoManager.Instance.ShowBulletInfo(this);
        if (HP <= 0)
        {
            OnBattleDie.Invoke();
            //Pooling();
            isDead = true;
            GameManager.Instance.ArDead();
            gameObject.SetActive(false);
            return true;
        }
        hpBar.localScale = new Vector3(Mathf.Clamp(HP / MaxHP, 0, 1), 1, 1);
        return false;
    }

    protected void Out()
    {
        gameObject.SetActive(false);
    }
}
