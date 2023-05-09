using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FangtianHuaji_MouseUp : ItemInfo
{
    public override void Passive()
    {
        player.tag = "Untagged";
    }
}
