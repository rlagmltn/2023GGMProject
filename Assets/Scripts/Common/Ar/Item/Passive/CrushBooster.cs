using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushBooster : ItemInfo
{
    public override void Passive()
    {
        Debug.Log("�忡!");
        player.Push(((player.rigid.velocity.normalized * player.Power) * player.PushPower));
    }
}
