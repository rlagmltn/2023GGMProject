using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FangtianHuaji_AfterMove : ItemInfo
{
    public override void Passive()
    {
        player.tag = "Player";
    }
}
