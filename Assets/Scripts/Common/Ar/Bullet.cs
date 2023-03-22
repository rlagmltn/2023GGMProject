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
        // lifeTime��ŭ ��ٷȴٰ� Ǯ��
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
            // Ǯ���Ǵ� �ڵ�
        }
        if (collision.CompareTag("Player") && bulletSO.teamType != TeamType.Player)
        {
            BattleManager.Instance.SettingAr(collision.GetComponent<Ar>(), damage);
            EffectManager.Instance.InstantiateEffect(0, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
            AfterCrush();
            //�÷��̾�� �� �ҷ��� ��������ŭ�� ���ظ� ��
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            BattleManager.Instance.SettingAr(collision.GetComponent<Ar>(), damage);
            EffectManager.Instance.InstantiateEffect(0, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
            AfterCrush();
            //���ʹ̿��� �� �ҷ��� ��������ŭ�� ���ظ� ��
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
