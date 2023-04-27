using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OminousTalisman_AfterCrash : ItemInfo
{
    private Transform nearEnemy;

    public override void Passive()
    {
        GetNearEnemy();
        OminousTalisman_AfterCrash_Play();
    }

    void OminousTalisman_AfterCrash_Play()
    {
        if (player.OminousTalismanDic.ContainsKey(nearEnemy)) player.OminousTalismanDic[nearEnemy] = 2;
        else player.OminousTalismanDic.Add(nearEnemy, 2);
        Debug.Log("어택 실행");
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
