using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletSO bulletSO;

    private SpriteRenderer spriteSize;
    private BoxCollider2D collide;
    private CameraMove cameraMove;

    public Ar summoner { get; set; }
    public int damage { get; set; }

    private void OnEnable()
    {
        SetSO();
    }

    protected virtual void SetSO()
    {
        if (collide == null) collide = GetComponent<BoxCollider2D>();
        if (spriteSize == null) spriteSize = GetComponent<SpriteRenderer>();
        if (cameraMove == null) cameraMove = FindObjectOfType<CameraMove>();
        gameObject.name = bulletSO.name;
        damage = bulletSO.damage;
        collide.size = new Vector2(bulletSO.width, bulletSO.height);    
        spriteSize.size = new Vector2(bulletSO.width, bulletSO.height);
        Destroy(gameObject, bulletSO.lifeTime);
        // lifeTime��ŭ ��ٷȴٰ� Ǯ��
    }

    private void Update()
    {
        transform.Translate(Vector2.right * bulletSO.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
            // Ǯ���Ǵ� �ڵ�
            AfterCrush();
        }
        if (collision.CompareTag("Player") && bulletSO.teamType != TeamType.Player)
        {
            DamageToAr(collision);
            //�÷��̾�� �� �ҷ��� ��������ŭ�� ���ظ� ��
            AfterCrush();
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            DamageToAr(collision);
            //���ʹ̿��� �� �ҷ��� ��������ŭ�� ���ظ� ��
            AfterCrush();
        }
    }

    public void DamageToAr(Collider2D collision)
    {
        BattleManager.Instance.SettingAr(collision.GetComponent<Ar>(), damage);
        EffectManager.Instance.InstantiateEffect(Effect.HIT, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
        EffectManager.Instance.InstantiateEffect(Effect.CRASH, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
        EffectManager.Instance.InstantiateEffect(Effect.Flash, collision.ClosestPoint(transform.position), transform.position, collision.ClosestPoint(transform.position));
        AfterCrush();
    }

    protected virtual void AfterCrush()
    {
        Debug.Log("ȭ��ε�");
        cameraMove.Shake();
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
