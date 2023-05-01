using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadnessNecklace : ItemInfo
{
    public override void Passive()
    {
        player.Hit(1);
    }
}
