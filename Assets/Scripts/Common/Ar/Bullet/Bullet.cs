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
        // lifeTime��ŭ ��ٷȴٰ� Ǯ��
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
            // Ǯ���Ǵ� �ڵ�
        }
        if (collision.CompareTag("Player") && bulletSO.teamType != TeamType.Player)
        {
            DamageToAr(collision);
            //�÷��̾�� �� �ҷ��� ��������ŭ�� ���ظ� ��
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            DamageToAr(collision);
            //���ʹ̿��� �� �ҷ��� ��������ŭ�� ���ظ� ��
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
        Debug.Log("ȭ��ε�");
        cameraMove.Shake();
        cameraMove.ResetTarget();
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
                    var hits = Physics2D.OverlapCircleAll(transform.position, bulletSO.range);
                    foreach(Collider2D hit in hits)
                    {
                        BattleManager.Instance.SettingAr(hit.GetComponent<Enemy>(), this);
                    }
                    Destroy(gameObject);
                    // �ֺ� ���鵵 ���ظ� �ְ� Ǯ���Ǵ� �ڵ�
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
