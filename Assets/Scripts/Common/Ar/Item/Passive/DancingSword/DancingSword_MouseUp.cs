using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingSword_MouseUp : ItemInfo
{
    public override void Passive()
    {
        player._isMove = true;
    }
}
