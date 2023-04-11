using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher_StateIdle : StateIdle
{
    private BanditArcher enemy;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<BanditArcher>();
    }

    public override void OnStart()
    {
        if(!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
        {
            Transform target = stateMachineClass.SearchAr();
            float distance = Vector2.Distance(stateMachineClass.transform.position, target.position);

            if (distance <= enemy.AtkRange)
            {
                stateMachine.ChangeState<BanditArcher_StateAtk>();
            }
            else
            {
                stateMachine.ChangeState<BanditArcher_StateMove>();
            }

        }
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }

    public override void OnEnd()
    {
        
    }
}
