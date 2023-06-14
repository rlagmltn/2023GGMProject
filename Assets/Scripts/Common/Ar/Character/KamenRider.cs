using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamenRider : Player
{
    private int henshinCount = 0;
    private int MaxHenshinCount = 3;
    private bool henshin = false;

    protected override void Start()
    {
        base.Start();
        Passive();
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
        AfterAttack.AddListener(() => henshinCount++);
        base.StatReset();
    }

    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        skillRange.size = new Vector2(0, 1);
    }

    public override void DragEnd(JoystickType joystickType, float charge, Vector2 angle)
    {
        if (joystickType == JoystickType.Skill && henshinCount < MaxHenshinCount || henshin && joystickType == JoystickType.Skill) return;
        base.DragEnd(joystickType, charge, angle);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        stat.MaxHP *= 2;
        stat.MaxSP *= 2;
        stat.HP *= 2;
        stat.SP *= 2;
        stat.ATK *= 2;
        stat.SATK *= 2;
        stat.CriPer *= 2;
        stat.WEIGHT *= 2;
        pushPower *= 2;
        henshin = true;
        EffectManager.Instance.InstantiateEffect_P(Effect.Teleport, transform.position);
        TurnManager.Instance.NextPlayerTurn += 3;
        DeadCheck();
    }

    protected override void Passive()
    {
        EndTurn.AddListener(() => { if (henshin)
            {
                stat.SP = stat.MaxSP; DeadCheck();
            }
        });
    }
}
