using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBird_AfterMove : ItemInfo
{
    [SerializeField] private int Can_Lengh;
    private bool isFirst = true;
    private int ItemNum = 0;

    public override void Passive()
    {
        if (AfterCrash_PlayCheck()) return;
        PaperBird_AfterMove_Play();
    }

    bool AfterCrash_PlayCheck()
    {
        if (isFirst)
        {
            isFirst = false;
            ItemNum = player.PaperBirdNum;
            player.PaperBirdNum++;
        }

        if (player.isPaperBird[ItemNum])
        {
            player.isPaperBird[ItemNum] = false;
            return true;
        }
        return false;
    }

    void PaperBird_AfterMove_Play()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(player.transform.position, Can_Lengh);

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Enemy>()) continue;
            hit[num].GetComponent<Ar>().Hit(2, player);
            Debug.Log("2데미지 애프터 무브");
            EffectManager.Instance.InstantiateEffect(Effect.HIT, hit[num].gameObject.transform.position, new Vector3(0, 0, 0));//나중에 바꿔줘야함
        }
    }
}
