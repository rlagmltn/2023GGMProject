using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalGuanlet : ItemInfo
{
    [SerializeField] private int triggerPercent;
    private Transform nearEnemy;

    public override void Passive() //애프터 크러쉬에 넣을것
    {
        StopEnemy_Random();
    }

    void StopEnemy_Random()
    {
        GetNearEnemy();
        int Rnum = Random.Range(0, 101);
        if (Rnum > triggerPercent) return;
        nearEnemy.GetComponent<ArFSM>().SetTurnSkip();
    }

    void GetNearEnemy()
    {
        float Distance = 100000;
        Collider2D[] hit = Physics2D.OverlapCircleAll(player.transform.position, 10000);

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Enemy>()) continue;

            if (Vector2.Distance(transform.position, hit[num].transform.position) > Distance) continue;

            Distance = Vector2.Distance(transform.position, hit[num].transform.position);
            nearEnemy = hit[num].transform;
        }
    }
}
