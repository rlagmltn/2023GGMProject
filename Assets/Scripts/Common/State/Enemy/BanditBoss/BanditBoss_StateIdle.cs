using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBoss_StateIdle : StateIdle
{

    private bool usedSkill3 = false;
    private int skilCool4 = 17;

    private Enemy enemy;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<Enemy>();
    }

    public override void OnUpdate(float deltaTime)
    {
        if(!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
        {
            if(enemy.stat.HP >= 20 && !usedSkill3)
            {
                //skill3
                stateMachine.ChangeState<BanditBoss_StateSkill>();
            }
            else if(enemy.stat.WEIGHT >= 50)
            {
                //skill2
            }
            else if(enemy.stat.WEIGHT >= 15)
            {
                //skill1
                
                
            }
        }
    }
}
