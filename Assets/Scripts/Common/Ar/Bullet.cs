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
            collision.GetComponent<Player>().stat.HP -= damage;
            //�÷��̾�� �� �ҷ��� ��������ŭ�� ���ظ� ��
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            collision.GetComponent<Enemy>().stat.HP -= damage;
            //���ʹ̿��� �� �ҷ��� ��������ŭ�� ���ظ� ��
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
