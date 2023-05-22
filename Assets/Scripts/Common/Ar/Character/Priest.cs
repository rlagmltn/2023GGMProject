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

        if (targets.Length > 1)
        {
            var a = Vector2.Distance(transform.position, targets[1].point);
            skillRange.size = new Vector2(a / 2, 1);
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

            healTarget.Heal(stat.SATK);
            var effect = EffectManager.Instance.InstantiateEffect_P(Effect.Heal, healTarget.transform.position);
            effect.transform.SetParent(healTarget.transform);
            Passive();

            isUsingSkill = false;
        }
    }

    protected override void Attack(Vector2 angle)
    {
        base.Attack(angle);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        isUsingSkill = true;
        rigid.velocity = -(angle * power) * pushPower / (1 + stat.WEIGHT * 0.1f);
        cameraMove.Shake();
    }

    protected override void Passive()
    {
        Heal(stat.ATK);
        var effect = EffectManager.Instance.InstantiateEffect_P(Effect.Heal, transform.position);
        effect.transform.SetParent(transform);
    }
}
