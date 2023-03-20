using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_StateIdle : StateIdle
{
    public override void OnUpdate(float deltaTime)
    {
        //자기턴이면
        if (!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
        {
            Debug.Log("적턴");
            Transform target = stateMachineClass.SearchAr();
            if (target)
            {
                stateMachine.ChangeState<BanditWarrior_StateMove>();
            }
        }
    }
}
