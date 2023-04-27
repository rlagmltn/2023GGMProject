using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickySlime_AfterCrash : ItemInfo
{
    private float OwnPower;
    private float DecreasePower;
    Transform nearEnemy;

    public override void Passive() //애프터 크러쉬에 넣을것
    {
        GetNearEnemy();
        StickySlime_Play();
    }

    void StickySlime_Play()
    {
        //니얼 에너미한테 파워 낮추기
        //그리고 니얼 애너미를 player의 transform 리스트에 넣어줬다가 속도를 원래대로 돌려주며 리스트 클리어

        OwnPower = nearEnemy.GetComponent<Ar>().pushPower;
        nearEnemy.GetComponent<Ar>().pushPower = OwnPower / 10f * 7f;

        player.StickySlimeList.Add(nearEnemy);
        player.StickySlime_PoaerList.Add(OwnPower / 10f * 3f);
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
