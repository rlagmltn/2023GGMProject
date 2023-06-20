using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mjolnir : ItemInfo
{
    [SerializeField] private float Distance = 0f;

    public override void Passive()
    {
        GetNearEnemyDamage();
    }

    void GetNearEnemyDamage()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(player.transform.position, Distance);

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Enemy>()) continue;

            player.cameraMove.Shake();
            var effect = EffectManager.Instance.InstantiateEffect_P(Effect.Mjolnir, hit[num].transform.position);
            effect.transform.SetParent(hit[num].transform);
            hit[num].GetComponent<Enemy>().Hit(3, player);
        }
    }
}
