using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickySlime : ItemInfo
{
    private int OwnSpeed;
    private int DecreaseSpeed;

    public override void Passive() //������ ũ������ ������
    {
        StickySlime_Play();
    }

    void StickySlime_Play()
    {

    }
}
