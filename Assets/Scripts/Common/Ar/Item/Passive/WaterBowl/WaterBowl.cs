using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBowl : ItemInfo
{
    public override void Passive()
    {
        WaterBowl_Play();
    }

    void WaterBowl_Play()
    {
        player.Hit(-2);
    }
}
