using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : Bullet
{
    protected override void SetSO()
    {
        summoner = FindObjectOfType<Assassin>();
        base.SetSO();
    }

    protected override void AfterCrush()
    {
        EffectManager.Instance.InstantiateEffect_P(Effect.AssassinSmoke, summoner.transform.position);
        summoner.transform.position = transform.position;
        summoner.Push(new Vector2(0.1f, 0.1f));
        EffectManager.Instance.InstantiateEffect_P(Effect.AssassinSmoke, summoner.transform.position);
        base.AfterCrush();
    }
}
