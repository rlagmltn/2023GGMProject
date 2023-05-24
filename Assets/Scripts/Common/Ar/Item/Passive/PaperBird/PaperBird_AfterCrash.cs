using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBird_AfterCrash : ItemInfo
{
    private Transform nearEnemy;
    private bool isFirst = true;
    private int ItemNum = -1;

    public override void Passive()
    {
        GetNearEnemy();
        StartCoroutine(PaperBird_AfterCrash_Play());
    }

    IEnumerator PaperBird_AfterCrash_Play()
    {
        if (isFirst)
        {
            isFirst = false;
            player.isPaperBird.Add(false);
            ItemNum = player.isPaperBird.Count - 1;
        }
        player.isPaperBird[ItemNum] = true;
        yield return new WaitForSeconds(0.1f);
        nearEnemy.GetComponent<Ar>().Hit(2);
        Debug.Log("2데미지 애프터 크러쉬");
        EffectManager.Instance.InstantiateEffect(Effect.HIT, nearEnemy.gameObject.transform.position, new Vector3(0, 0, 0)); //나중에 바꿔줘야함
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
