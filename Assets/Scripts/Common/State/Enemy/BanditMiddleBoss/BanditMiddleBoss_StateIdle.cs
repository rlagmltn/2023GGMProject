using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMiddleBoss_StateIdle : StateIdle
{
    private BanditMiddleBoss enemy;

    private int skillCool1 = 7;
    private int skillCool2 = 10;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<BanditMiddleBoss>();
    }

    public override void OnUpdate(float deltaTime)
    {
        if(!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
        {
            Transform target = stateMachineClass.SearchAr();
            if(target == null)
            {
                return;
            }

            if(enemy.Passive())
            {
                stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
                enemy.isMove = true;
                TurnManager.Instance.UseTurn();
                Debug.Log("Phase1");
                return;
            }

            if(enemy.stat.HP <= enemy.stat.MaxHP / 2)
            {
                skillCool1++;
                skillCool2++;
                enemy.Howling();
                stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
                enemy.isMove = true;
                TurnManager.Instance.UseTurn();
            }
            else if(skillCool1 >= 7 && !stateMachineClass.CheckWall())
            {
                skillCool1 = 1;
                skillCool2++;
                enemy.AxeAttack();
                stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
                enemy.isMove = true;
                TurnManager.Instance.UseTurn();

            }
            else if(skillCool2 >= 10)
            {
                skillCool1++;
                skillCool2 = 1;
                enemy.ThrowAxe(target.transform.position - stateMachineClass.transform.position);
                stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
                enemy.isMove = true;
                TurnManager.Instance.UseTurn();
            }
            else
            {
                skillCool1++;
                skillCool2++;
                stateMachine.ChangeState<BanditMiddleBoss_StateMove>();
            }
        }
    }
}
