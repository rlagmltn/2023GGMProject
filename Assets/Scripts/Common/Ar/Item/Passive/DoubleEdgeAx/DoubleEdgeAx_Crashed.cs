using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEdgeAx_Crashed : ItemInfo
{
    public override void Passive()
    {
        GetNearEnemy();
    }

    void GetNearEnemy()
    {
        float Distance = 100000;
        Transform trans = null;

        Collider2D[] hit = Physics2D.OverlapCircleAll(player.transform.position, 10000);

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Enemy>()) continue;

            if (Vector2.Distance(player.transform.position, hit[num].transform.position) > Distance) continue;
            Distance = Vector2.Distance(player.transform.position, hit[num].transform.position);
            trans = hit[num].transform;
        }

        if (trans == null) return;
        player.AxList.Add(trans);
        //애프터 무브에서 클리어해주면됨
        trans.GetComponent<Enemy>().stat.WEIGHT = trans.GetComponent<Enemy>().stat.WEIGHT / 10f * 9f;
    }
}
