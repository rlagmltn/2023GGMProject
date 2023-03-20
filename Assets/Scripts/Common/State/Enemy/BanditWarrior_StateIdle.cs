using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_StateIdle : StateIdle
{
    private bool canUsePassive = true;

    private Enemy enemy;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<Enemy>();
    }

    public override void OnUpdate(float deltaTime)
    {
        //자기턴이면
        if (!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
        {
            
            if(enemy.stat.HP <= 30 && canUsePassive)
            {
                canUsePassive = false;
                stateMachine.ChangeState<BanditWarrior_StatePassive>();
            }
            else
            {
                Transform target = stateMachineClass.SearchAr();
                if (target)
                {
                    stateMachine.ChangeState<BanditWarrior_StateMove>();
                }
            }
        }
    }
}
