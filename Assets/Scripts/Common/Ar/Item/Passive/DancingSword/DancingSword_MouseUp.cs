using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingSword_MouseUp : ItemInfo
{
    public override void Passive()
    {
        player._isMove = true;
    }

    void GetNearEnemyDamage()
    {
        if (!player._isMove) return;

        //이펙트는 누군가 해줘야함 칼돌아가는거?

        Collider2D[] hit = Physics2D.OverlapCircleAll(player.transform.position, 10000);

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Enemy>()) continue;

            hit[num].GetComponent<Enemy>().isDancingSword_Damage = false;
        }
    }
}
