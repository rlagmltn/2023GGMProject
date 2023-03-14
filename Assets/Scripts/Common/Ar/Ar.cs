using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ar : MonoBehaviour
{
    public float MaxHP { get; set; }
    public float HP { get; set; }
    public float ATK { get; set; }
    public float DEF { get; set; }
    public float WEIGHT { get; set; }

    public Sprite arSprite { get; set; }

    public bool isDead { get; set; }

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

    protected virtual void StatReset() // ºˆƒ° √ ±‚»≠
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
            Debug.Log("¿∏æ”¡Í±›");
            Out();
        }
    }

    public void Push(Vector2 velo)
    {
        rigid.velocity = velo;
    }

    public bool Hit(float damage)
    {
        HP -= damage;
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
