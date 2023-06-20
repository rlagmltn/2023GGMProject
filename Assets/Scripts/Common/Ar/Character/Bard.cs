using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : Player
{
    public override void StatReset()
    {
        isRangeCharacter = false;
        Healed.AddListener(Passive);
        base.StatReset();
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        animationManager.Cast();
    }

    public override IEnumerator AnimTimingSkill()
    {
        while (isSkill) yield return null;
        var cols = Physics2D.OverlapCircleAll(transform.position, 2);
        foreach(Collider2D col in cols)
        {
            var player = col.GetComponent<Player>();
            if (player.gameObject != gameObject)
            {
                player.Heal(stat.SATK, this);
                var effect = EffectManager.Instance.InstantiateEffect_P(Effect.Heal, player.transform.position);
                effect.transform.SetParent(player.transform);
            }
        }
    }

    protected override void Passive()
    {
        Heal(1, null);
        var effect = EffectManager.Instance.InstantiateEffect_P(Effect.Heal, transform.position);
        effect.transform.SetParent(transform);
    }
}
