using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Player
{
    private Vector2 goalPoint;
    private Vector2 target;

    protected override void Start()
    {
        base.Start();
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
        Dealed.AddListener(Passive);
        base.StatReset();
    }
    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        RayCastTargets(moveDrag);
        target = FindNearWall();

        if (target.magnitude != 0)
        {
            skillRange.size = new Vector2(Vector2.Distance(transform.position, target) / 2, 1);
            range = Vector2.Distance(transform.position, target);
        }
        else
        {
            range = moveDrag;
            skillRange.size = new Vector2(moveDrag / 2, 1);
        }
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);

        if (target.magnitude != 0)
            goalPoint = (Vector2)transform.position - target;
        else
            goalPoint = angle * moveDrag;

        Debug.DrawRay(transform.position, angle, Color.red, 3f);
        RaycastHit2D[] colliders = Physics2D.RaycastAll(attackRange.transform.position, -angle, range);

        foreach (RaycastHit2D collider in colliders)
        {
            var enemy = collider.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Push(-angle.normalized * 3);
                BattleManager.Instance.SettingAr(enemy, this);
                EffectManager.Instance.InstantiateEffect_P(Effect.CristalGuntlet, enemy.transform.position);
            }
        }

        transform.position -= (Vector3)goalPoint;
        EffectManager.Instance.InstantiateEffect_P(Effect.DASH, transform.position, new Vector2(-angle.x, angle.y));
        if (angle.x < 0) character.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (angle.x > 0) character.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    protected override void Passive()
    {
        if(lastDealed?.stat.SP>0)
        {
            lastDealed.Hit(1, null);
            stat.SP++;

            lastDealed = null;
            DeadCheck();
        }
    }
}
