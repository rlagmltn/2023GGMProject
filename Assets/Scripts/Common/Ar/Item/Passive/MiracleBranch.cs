using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiracleBranch : ItemInfo
{
    [SerializeField] GuidedArrow arrow;

    private Transform nearEnemy;
    private float Distance;
    Vector3 targetPos;

    public override void Passive()
    {
        GetNearEnemy();
        StartCoroutine(Shoot_Croutine());
    }

    private IEnumerator Shoot_Croutine()
    {
        Shoot();
        yield return new WaitForSeconds(0.5f);
        Shoot();
    }

    void Shoot()
    {
        var _bullet = Instantiate(arrow, player.transform.position, Quaternion.Euler(targetPos));
    }

    void GetNearEnemy()
    {
        Distance = 10000;
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 10000);

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Enemy>()) continue;

            if (Vector2.Distance(transform.position, hit[num].transform.position) > Distance) continue;

            Distance = Vector2.Distance(transform.position, hit[num].transform.position);
            nearEnemy = hit[num].transform;
        }

        targetPos = (nearEnemy.position - transform.position).normalized;
    }
}
