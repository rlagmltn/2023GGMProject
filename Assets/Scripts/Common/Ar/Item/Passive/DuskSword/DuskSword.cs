using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuskSword : ItemInfo
{
    [SerializeField] private int Percent;

    public override void Passive()
    {
        DuskSword_Passive();
    }

    void DuskSword_Passive()
    {
        int tempNum = Random.Range(1, 101);
        if (Percent > tempNum) return;
        //È®·ü ¸¸µé¾îÁà¾ßÇÔ
        var effect = EffectManager.Instance.InstantiateEffect_P(Effect.DuskSword, player.transform.position);
        effect.transform.SetParent(player.transform);
        player.currentCooltime = player.skillCooltime;
    }
}
