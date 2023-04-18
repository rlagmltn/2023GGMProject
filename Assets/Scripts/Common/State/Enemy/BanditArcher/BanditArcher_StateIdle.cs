using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher_StateIdle : StateIdle
{
    private BanditArcher enemy;
    private int skillCool = 11;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<BanditArcher>();
    }

    public override void OnStart()
    {
        if(!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
        {
            Transform target = stateMachineClass.SearchAr();
            if(target == null)
            {
                return;
            }

            float distance = Vector2.Distance(stateMachineClass.transform.position, target.position);

            if(skillCool == 11)
            {
                skillCool = 0;
                stateMachine.ChangeState<BanditArcher_StateSkill>();
            }
            else if (distance <= enemy.AtkRange)
            {
                skillCool++;
                stateMachine.ChangeState<BanditArcher_StateAtk>();
            }
            else
            {
                skillCool++;
                stateMachine.ChangeState<BanditArcher_StateMove>();
            }

        }
    }
}
