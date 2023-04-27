using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickySlime_AfterCrash : ItemInfo
{
    private float OwnPower;
    private float DecreasePower;
    Transform nearEnemy;

    public override void Passive() //������ ũ������ ������
    {
        GetNearEnemy();
        StickySlime_Play();
    }

    void StickySlime_Play()
    {
        //�Ͼ� ���ʹ����� �Ŀ� ���߱�
        //�׸��� �Ͼ� �ֳʹ̸� player�� transform ����Ʈ�� �־���ٰ� �ӵ��� ������� �����ָ� ����Ʈ Ŭ����

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
