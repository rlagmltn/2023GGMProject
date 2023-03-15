using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletSO bulletSO;

    private CircleCollider2D collide;
    public float damage { get; set; }

    private void OnEnable()
    {
        SetSO();
    }

    private void SetSO()
    {
        if (collide == null) collide = GetComponent<CircleCollider2D>();
        gameObject.name = bulletSO.name;
        damage = bulletSO.damage;
        collide.radius = bulletSO.radius;
        Destroy(gameObject, bulletSO.lifeTime);
        // lifeTime만큼 기다렸다가 풀링
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Object"))
        {
            // 풀링되는 코드
        }

        if (collision.CompareTag("Player") && bulletSO.teamType != TeamType.Player)
        {
            collision.GetComponent<Player>().stat.HP -= damage;
            //플레이어에게 이 불렛의 데미지만큼의 피해를 줌
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            collision.GetComponent<Enemy>().stat.HP -= damage;
            //에너미에게 이 불렛의 데미지만큼의 피해를 줌
        }
    }

    private void AfterCrush()
    {
        if (bulletSO.rangeType == RangeType.Range)
        {
            switch (bulletSO.bulletType)
            {
                case BulletType.Nomal:
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
