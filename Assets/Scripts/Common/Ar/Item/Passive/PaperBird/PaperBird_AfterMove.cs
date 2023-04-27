using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBird_AfterMove : ItemInfo
{
    [SerializeField] private int Can_Lengh;

    public override void Passive()
    {
        if (AfterCrash_PlayCheck()) return;
        PaperBird_AfterMove_Play();
    }

    bool AfterCrash_PlayCheck()
    {
        if (player.isPaperBirdPlay)
        {
            player.isPaperBirdPlay = false;
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
            hit[num].GetComponent<Ar>().Hit(2);
            EffectManager.Instance.InstantiateEffect(Effect.HIT, hit[num].gameObject.transform.position, new Vector3(0, 0, 0));//³ªÁß¿¡ ¹Ù²ãÁà¾ßÇÔ
        }
    }
}
