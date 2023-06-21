using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Player
{
    private bool isUsingSkill;

    protected override void Start()
    {
        base.Start();
        AfterMove.AddListener(() => { isUsingSkill = false; });
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
        base.StatReset();
    }
    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        RayCastTargets(moveDrag);
        var target = FindNearObject();

        if (target.magnitude != 0)
        {
            skillRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
        }
        else
        {
            skillRange.size = new Vector2(moveDrag / 2, 1);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.collider.CompareTag("Player")&&isUsingSkill)
        {
            var healTarget = collision.gameObject.GetComponent<Player>();

            healTarget.Heal(stat.SATK, this);
            var effect = EffectManager.Instance.InstantiateEffect_P(Effect.Heal, healTarget.transform.position);
            effect.transform.SetParent(healTarget.transform);
            Passive();

            isUsingSkill = false;
        }
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        isUsingSkill = true;
        rigid.velocity = -(angle * power) * pushPower / (1 + stat.WEIGHT * 0.1f);
        EffectManager.Instance.InstantiateEffect_P(Effect.DASH, transform.position, new Vector2(-angle.x, angle.y));
        if (angle.x < 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (angle.x > 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    protected override void Passive()
    {
        Heal(stat.ATK, this);
        var effect = EffectManager.Instance.InstantiateEffect_P(Effect.Heal, transform.position);
        effect.transform.SetParent(transform);
    }
}
