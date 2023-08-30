using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher_StateSkill : State<ArFSM>
{
    private BanditArcher archer;

    public override void OnAwake()
    {
        archer = stateMachineClass.GetComponent<BanditArcher>();
    }

    public override void OnStart()
    {
        Vector2 angle = Vector3.Normalize(stateMachineClass.SearchAr().position - stateMachineClass.transform.position);
        archer.ShootArrow(angle, false, true);
        stateMachineClass.enemy.isMove = true;
        TurnManager.Instance.UseTurn();
        stateMachine.ChangeState<BanditArcher_StateIdle>();
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }
}
