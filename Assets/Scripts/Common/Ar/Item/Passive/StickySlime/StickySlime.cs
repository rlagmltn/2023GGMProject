using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickySlime : ItemInfo
{
    private int OwnSpeed;
    private int DecreaseSpeed;

    public override void Passive() //애프터 크러쉬에 넣을것
    {
        StickySlime_Play();
    }

    void StickySlime_Play()
    {

    }
}
