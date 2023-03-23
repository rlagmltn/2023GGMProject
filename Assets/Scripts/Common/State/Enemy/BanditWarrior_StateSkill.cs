using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_StateSkill : StateAtk
{
    private Enemy enemy;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<Enemy>();
    }

    public override void OnStart()
    {
        Vector2 angle = Vector3.Normalize(stateMachineClass.SearchAr().position - stateMachineClass.transform.position);

        stateMachineClass.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        float rangeAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        stateMachineClass.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, rangeAngle));
        stateMachineClass.StartCoroutine("MoveAr", angle * enemy.GetPower());

        TurnManager.Instance.UseTurn();
        stateMachineClass.turnFlag = !stateMachineClass.turnFlag;



        stateMachine.ChangeState<BanditWarrior_StateIdle>();

    }

    public override void OnUpdate(float deltaTime)
    {

    }

    public override void OnEnd()
    {

    }
}
