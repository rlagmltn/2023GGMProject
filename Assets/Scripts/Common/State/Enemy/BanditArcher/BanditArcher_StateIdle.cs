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

    public override void OnUpdate(float deltaTime)
    {
        if (!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
        {
            Transform target = stateMachineClass.SearchAr();
            if(target == null)
            {
                return;
            }
            Debug.Log("Barcher Start");
            float distance = Vector2.Distance(stateMachineClass.transform.position, target.position);

            if(skillCool == 11)
            {
                skillCool = 0;
                stateMachineClass.turnFlag = false;
                stateMachine.ChangeState<BanditArcher_StateSkill>();
            }
            else if (distance <= enemy.AtkRange)
            {
                skillCool++;
                stateMachineClass.turnFlag = false;
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
