using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEdgeAx_AfterMove : ItemInfo
{
    public override void Passive()
    {
        DoubleEdgeAx_Play();
    }

    void DoubleEdgeAx_Play()
    {
        for(int num = 0; num < player.AxList.Count; num++)
        {
            player.AxList[num].GetComponent<Enemy>().stat.WEIGHT *= 10f/9f;
        }
        player.AxList.Clear();
    }
}
