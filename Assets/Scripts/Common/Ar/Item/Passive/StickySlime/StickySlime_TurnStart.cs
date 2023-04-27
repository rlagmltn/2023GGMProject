using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickySlime_TurnStart : ItemInfo
{
    private int OwnSpeed;
    private int DecreaseSpeed;

    public override void Passive()
    {
        StickySlime_Play();
    }

    void StickySlime_Play()
    {
        for(int num = 0; num < player.StickySlimeList.Count; num++)
        {
            player.StickySlimeList[num].GetComponent<Ar>().pushPower += player.StickySlime_PoaerList[num];
        }
        player.StickySlimeList.Clear();
        player.StickySlime_PoaerList.Clear();
    }
}
