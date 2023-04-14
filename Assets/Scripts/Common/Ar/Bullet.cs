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
        // lifeTime만큼 기다렸다가 풀링
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
            // 풀링되는 코드
            AfterCrush();
        }
        if (collision.CompareTag("Player") && bulletSO.teamType != TeamType.Player)
        {
            DamageToAr(collision);
            //플레이어에게 이 불렛의 데미지만큼의 피해를 줌
            AfterCrush();
        }
        else if (collision.CompareTag("Enemy") && bulletSO.teamType != TeamType.Enemy)
        {
            DamageToAr(collision);
            //에너미에게 이 불렛의 데미지만큼의 피해를 줌
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
        Debug.Log("화살부딪");
        cameraMove.Shake();
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
