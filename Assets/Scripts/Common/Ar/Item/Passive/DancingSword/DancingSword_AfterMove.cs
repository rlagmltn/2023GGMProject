using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingSword_AfterMove : ItemInfo
{
    public override void Passive()
    {
        player._isMove = false;
    }
}
