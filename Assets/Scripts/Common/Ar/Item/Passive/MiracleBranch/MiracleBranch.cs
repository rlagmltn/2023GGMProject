using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiracleBranch : ItemInfo
{
    [SerializeField] GuidedArrow arrow;

    private Transform nearEnemy;
    private float Distance;
    private float deg;
    Vector3 targetPos;

    public override void Passive()
    {
        StartCoroutine(Shoot_Croutine());
    }

    private IEnumerator Shoot_Croutine()
    {
        GetNearEnemy();
        Shoot();
        yield return new WaitForSeconds(0.1f);
        GetNearEnemy();
        Shoot();
    }

    void Shoot()
    {
        var _bullet = Instantiate(arrow, player.transform.position, Quaternion.Euler(new Vector3(0,0,deg)));
    }

    void GetNearEnemy()
    {
        Distance = 100000;
        Collider2D[] hit = Physics2D.OverlapCircleAll(player.transform.position, 10000);

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Enemy>()) continue;

            if (Vector2.Distance(transform.position, hit[num].transform.position) > Distance) continue;

            Distance = Vector2.Distance(transform.position, hit[num].transform.position);
            nearEnemy = hit[num].transform;
        }

        targetPos = (nearEnemy.position - transform.position).normalized;
        var effectAngle = (nearEnemy.position - transform.position);
        EffectManager.Instance.InstantiateEffect_P(Effect.MiracleBranch, (Vector2)transform.position, new Vector2(-effectAngle.x, effectAngle.y));
        deg = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
    }
}
