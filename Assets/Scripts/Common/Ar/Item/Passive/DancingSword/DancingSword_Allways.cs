using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingSword_Allways : ItemInfo
{
    [SerializeField] private float Distance = 0f;

    public override void Passive()
    {
        //원래 빈거임;
    }

    private void Update()
    {
        GetNearEnemyDamage();
    }

    void GetNearEnemyDamage()
    {
        if (!player._isMove) return;

        //이펙트는 누군가 해줘야함 칼돌아가는거?

        Collider2D[] hit = Physics2D.OverlapCircleAll(player.transform.position, Distance);

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Enemy>()) continue;
            if (hit[num].GetComponent<Enemy>().isDancingSword_Damage) continue;

            hit[num].GetComponent<Enemy>().Hit(3);
            hit[num].GetComponent<Enemy>().isDancingSword_Damage = true;
        }
    }
}
