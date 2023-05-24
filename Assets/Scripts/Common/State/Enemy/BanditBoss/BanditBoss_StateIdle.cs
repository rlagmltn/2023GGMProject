using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBoss_StateIdle : StateIdle
{

    private bool usedSkill3 = false;
    private int skilCool4 = 17;

    private BanditBoss enemy;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<BanditBoss>();
    }

    public override void OnUpdate(float deltaTime)
    {
        if(!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
        {
            if(!enemy.CanMove())
            {
                enemy.Passive();
                stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
                return;
            }

            enemy.Passive();
            skilCool4--;
            if(enemy.stat.HP <= 20 && !usedSkill3)
            {
                stateMachine.ChangeState<BanditBoss_StateSkill>();
            }
            else if(skilCool4 <= 0)
            {
                enemy.SkillShield();
                stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
            }
            else if(enemy.stat.WEIGHT == 50)
            {
                enemy.SkillOverload();
                stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
            }
            else if(enemy.stat.WEIGHT == 15)
            {
                enemy.SkillHeavyArmor();
                stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
            }
            else
            {
                stateMachine.ChangeState<BanditBoss_StateMove>();
            }
        }
    }
}
