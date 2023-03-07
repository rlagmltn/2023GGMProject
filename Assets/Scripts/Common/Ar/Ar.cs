using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ar : MonoBehaviour
{
    public float MaxHP { get; set; }
    public float HP { get; set; }
    public float ATK { get; set; }
    public Sprite arSprite { get; set; }

    protected float minDragPower = 0.4f;
    protected float maxDragPower = 1.5f;
    protected float pushPower;

    protected Vector2 defaultScale = new Vector2(0.5f, 0.5f);
    protected GameObject line;
    protected Transform hpBar;
    protected SpriteRenderer hpImage;

    public Rigidbody2D rigid { get; protected set; }
    public Vector2 lastVelocity { get; protected set; }

    public UnityEvent BeforeCrash;
    public UnityEvent AfterCrash;
    public UnityEvent BeforeBattle;
    public UnityEvent AfterBattle;
    public UnityEvent BeforeAttack;
    public UnityEvent AfterAttack;
    public UnityEvent BeforeDefence;
    public UnityEvent AfterDefence;
    public UnityEvent OnOutDie;
    public UnityEvent OnBattleDie;

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        line = transform.GetChild(0).gameObject;
        hpBar = transform.GetChild(1).GetChild(0);
        hpImage = hpBar.GetComponentInChildren<SpriteRenderer>();
        arSprite = transform.GetComponent<SpriteRenderer>().sprite;
    }

    protected virtual void StatReset() // 수치 초기화
    {
        HP = MaxHP;
        //hpBar.localScale = new Vector3(Mathf.Clamp(HP / MaxHP, 0, 1), 1, 1);
    }

    /*protected void OnMouseDown()
    {
        ArInfoManager.Instance.ShowBulletInfo(this);
    }*/

    protected void FixedUpdate()
    {
        lastVelocity = rigid.velocity;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Out"))
        {
            OnOutDie.Invoke();
            //Pooling();
        }
        else if (collision.transform.CompareTag("Object"))
        {
            rigid.velocity = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal) * pushPower;
            AfterCrash?.Invoke();
        }
    }

    public void BattleFinish()
    {
        Debug.Log(name + HP);
        if (!DeadCheck())
            AfterCrash?.Invoke(); //충돌 직후 발동하는 트리거
    }

    public void Hit(Vector2 velo)
    {
        rigid.velocity = velo;
    }

    protected bool DeadCheck()
    {
        //ArInfoManager.Instance.ShowBulletInfo(this);
        if (HP <= 0)
        {
            OnBattleDie.Invoke();
            if (HP <= 0)
            {
                //Pooling();
                return true;
            }
        }
        hpBar.localScale = new Vector3(Mathf.Clamp(HP / MaxHP, 0, 1), 1, 1);
        return false;
    }
}
