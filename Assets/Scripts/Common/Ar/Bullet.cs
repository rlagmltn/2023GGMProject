using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletSO bulletSO;

    private CircleCollider2D collide;
    private float damage;

    private void OnEnable()
    {
        SetSO();
    }

    private void SetSO()
    {
        collide = GetComponent<CircleCollider2D>();
        gameObject.name = bulletSO.name;
        damage = bulletSO.damage;
        collide.radius = bulletSO.radius;
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
            //플레이어에게 이 불렛의 데미지만큼의 피해를 줌
            AfterCrush();
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            //에너미에게 이 불렛의 데미지만큼의 피해를 줌
            AfterCrush();
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
