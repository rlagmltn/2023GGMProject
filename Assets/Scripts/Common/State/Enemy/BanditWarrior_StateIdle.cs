using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_StateIdle : StateIdle
{
    public override void OnUpdate(float deltaTime)
    {
        //�ڱ����̸�
        if (!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
        {
            Debug.Log("����");
            Transform target = stateMachineClass.SearchAr();
            if (target)
            {
                stateMachine.ChangeState<BanditWarrior_StateMove>();
            }
        }
    }
}
