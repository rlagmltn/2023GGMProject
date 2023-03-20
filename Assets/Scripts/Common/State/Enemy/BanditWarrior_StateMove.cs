using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_StateMove : StateMove
{
    private CONEntity conEntity;
    private Ar ar;
    private Rigidbody2D rigid;
    private Enemy enemy;

    public override void OnAwake()
    {
        conEntity = stateMachineClass.GetComponent<CONEntity>();
        ar = stateMachineClass.GetComponent<Ar>();
        rigid = stateMachineClass.GetComponent<Rigidbody2D>();
        enemy = stateMachineClass.GetComponent<Enemy>();
    }

    public override void OnStart()
    {
        Vector2 angle = Vector3.Normalize(stateMachineClass.SearchAr().position - stateMachineClass.transform.position);
        //벽체크 부분 해결해야함(임시로 그냥풀어둠)
        //if (!stateMachineClass.CheckWall())
        //{
        rigid.velocity = angle * enemy.GetPower();
        //}
        //power를 Enemy class에서 가지고 오기
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
