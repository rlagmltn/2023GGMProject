using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_StateIdle : StateIdle
{
    private bool canUsePassive = true;
    private int skillCool = 6;


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
            Transform target = stateMachineClass.SearchAr();
            if(!target)
            {
                Debug.LogWarning("None Target!");
                return;
            }
        
            if(enemy.stat.HP <= 2 && canUsePassive)
            {
                skillCool = 0;
                canUsePassive = false;
                stateMachine.ChangeState<BanditWarrior_StatePassive>();
            }
            else if(skillCool == 6)
            {
                skillCool = 0;
                stateMachine.ChangeState<BanditWarrior_StateSkill>();
            }
            else
            {
                skillCool = Mathf.Min(6, skillCool + 1);
                stateMachine.ChangeState<BanditWarrior_StateMove>();
            }
        }
    }
}
