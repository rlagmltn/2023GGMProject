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
        EffectManager.Instance.InstantiateEffect_P(Effect.Heal, player.transform.position);
        player.Heal(2);
    }
}
