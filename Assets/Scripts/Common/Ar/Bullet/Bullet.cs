using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletSO bulletSO;

    private CameraMove cameraMove;

    public Ar summoner { get; set; }
    public int damage { get; set; }

    private void OnEnable()
    {
        SetSO();
    }

    protected virtual void SetSO()
    {
        if (cameraMove == null) cameraMove = FindObjectOfType<CameraMove>();
        gameObject.name = bulletSO.name;
        Destroy(gameObject, bulletSO.lifeTime);
        // lifeTime만큼 기다렸다가 풀링
    }

    public virtual void SetDamage(int damage)
    {
        this.damage = damage;
    }

    protected virtual void Update()
    {
        transform.Translate(Vector2.right * bulletSO.speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Out"))
        {
            Destroy(gameObject);
            return;
        }
        else if (collision.CompareTag("Object"))
        {
            EffectManager.Instance.InstantiateEffect(Effect.HIT, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
            AfterCrush();
            // 풀링되는 코드
        }
        if (collision.CompareTag("Player") && bulletSO.teamType != TeamType.Player)
        {
            DamageToAr(collision);
            //플레이어에게 이 불렛의 데미지만큼의 피해를 줌
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            DamageToAr(collision);
            //에너미에게 이 불렛의 데미지만큼의 피해를 줌
        }
    }

    public void DamageToAr(Collider2D collision)
    {
        if(bulletSO.bulletType!=BulletType.Explosion)
            BattleManager.Instance.SettingAr(collision.GetComponent<Ar>(), this);
        EffectManager.Instance.InstantiateEffect(Effect.HIT, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
        EffectManager.Instance.InstantiateEffect(Effect.CRASH, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
        AfterCrush();
    }

    protected virtual void AfterCrush()
    {
        Debug.Log("화살부딪");
        cameraMove.Shake();
        cameraMove.ResetTarget();
        if (bulletSO.rangeType == RangeType.Range)
        {
            switch (bulletSO.bulletType)
            {
                case BulletType.Nomal:
                    Destroy(gameObject);
                    // 풀링되는 코드
                    break;
                case BulletType.Penetrate:
                    // 벽에 닿을 때 까지 계속 날아가는 코드
                    break;
                case BulletType.Explosion:
                    var hits = Physics2D.OverlapCircleAll(transform.position, bulletSO.range);
                    foreach(Collider2D hit in hits)
                    {
                        BattleManager.Instance.SettingAr(hit.GetComponent<Enemy>(), this);
                    }
                    Destroy(gameObject);
                    // 주변 적들도 피해를 주고 풀링되는 코드
                    break;
            }
        }
        else
        {

        }
    }

    protected BulletSO GetBullet()
    {
        return bulletSO;
    }
}
