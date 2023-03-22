using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletSO bulletSO;

    private SpriteRenderer spriteSize;
    private CircleCollider2D collide;
    public float damage { get; set; }

    private void OnEnable()
    {
        SetSO();
    }

    private void SetSO()
    {
        if (collide == null) collide = GetComponent<CircleCollider2D>();
        if (spriteSize == null) spriteSize = GetComponent<SpriteRenderer>();
        gameObject.name = bulletSO.name;
        damage = bulletSO.damage;
        collide.radius = bulletSO.radius;
        spriteSize.size = new Vector2(bulletSO.radius * 2, bulletSO.radius * 2);
        Destroy(gameObject, bulletSO.lifeTime);
        // lifeTime만큼 기다렸다가 풀링
    }

    private void Update()
    {
        transform.Translate(Vector2.right * bulletSO.speed * Time.deltaTime);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            EffectManager.Instance.InstantiateEffect(0, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
            AfterCrush();
            // 풀링되는 코드
        }
        if (collision.CompareTag("Player") && bulletSO.teamType != TeamType.Player)
        {
            BattleManager.Instance.SettingAr(collision.GetComponent<Ar>(), damage);
            EffectManager.Instance.InstantiateEffect(0, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
            AfterCrush();
            //플레이어에게 이 불렛의 데미지만큼의 피해를 줌
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            BattleManager.Instance.SettingAr(collision.GetComponent<Ar>(), damage);
            EffectManager.Instance.InstantiateEffect(0, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
            AfterCrush();
            //에너미에게 이 불렛의 데미지만큼의 피해를 줌
        }
    }

    private void AfterCrush()
    {
        CameraMove.Instance.Shake();
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
                    // 주변 적들도 피해를 주고 풀링되는 코드
                    break;
            }
        }
        else
        {

        }
    }
}
