using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalBullet : Bullet
{
    protected override void SetSO()
    {
        summoner = FindObjectOfType<Elementalist>();
        base.SetSO();
    }

    protected override void AfterCrush()
    {
        EffectManager.Instance.InstantiateEffect_P(Effect.ElementExplode, transform.position, Vector2.zero);
        base.AfterCrush();
    }
}
