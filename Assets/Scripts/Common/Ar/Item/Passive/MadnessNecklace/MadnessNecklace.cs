using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadnessNecklace : ItemInfo
{
    public override void Passive()
    {
        var effect =  EffectManager.Instance.InstantiateEffect_P(Effect.Blood, transform.position);
        effect.transform.SetParent(player.transform);
        player.Hit(1);
    }
}
