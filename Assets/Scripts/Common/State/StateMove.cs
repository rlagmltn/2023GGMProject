using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : State<ArFSM>
{
    private CONEntity conEntity;
    private Ar ar;
    private Rigidbody2D rigid;

    public override void OnAwake()
    {
        conEntity = stateMachineClass.GetComponent<CONEntity>();
        ar = stateMachineClass.GetComponent<Ar>();
        rigid = stateMachineClass.GetComponent<Rigidbody2D>();
    }

    public override void OnStart()
    {
        Vector2 angle = stateMachineClass.SearchAr().position - stateMachineClass.transform.position;
        if (!stateMachineClass.CheckWall())
        {
            rigid.velocity = (angle * 1.5f) * 5f;
        }
        //power를 Enemy class에서 가지고 오기
        stateMachine.ChangeState<StateIdle>();
    }

    public override void OnUpdate(float deltaTime)
    {

    }

    public override void OnEnd()
    {
        //TurnManager에게 turn종료알리기
        
    }
}
