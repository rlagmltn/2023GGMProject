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
        // lifeTime��ŭ ��ٷȴٰ� Ǯ��
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Object"))
        {
            // Ǯ���Ǵ� �ڵ�
        }

        if (collision.CompareTag("Player") && bulletSO.teamType != TeamType.Player)
        {
            //�÷��̾�� �� �ҷ��� ��������ŭ�� ���ظ� ��
            AfterCrush();
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            //���ʹ̿��� �� �ҷ��� ��������ŭ�� ���ظ� ��
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
                    // Ǯ���Ǵ� �ڵ�
                    break;
                case BulletType.Penetrate:
                    // ���� ���� �� ���� ��� ���ư��� �ڵ�
                    break;
                case BulletType.Explosion:
                    // �ֺ� ���鵵 ���ظ� �ְ� Ǯ���Ǵ� �ڵ�
                    break;
            }
        }
        else
        {

        }
    }
}
