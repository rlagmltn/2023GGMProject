using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBird_AfterCrash : ItemInfo
{
    private Transform nearEnemy;

    public override void Passive()
    {
        GetNearEnemy();
        StartCoroutine(PaperBird_AfterCrash_Play());
    }

    IEnumerator PaperBird_AfterCrash_Play()
    {
        player.isPaperBirdPlay = true;
        yield return new WaitForSeconds(0.5f);
        nearEnemy.GetComponent<Ar>().Hit(2);
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
