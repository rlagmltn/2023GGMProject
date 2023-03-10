using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State<ArFSM>
{
    private bool turnFlag = false;

    public override void OnAwake()
    {

    }

    public override void OnStart()
    {
        Transform target = stateMachineClass.SearchAr();
    }

    public override void OnUpdate(float deltaTime)
    {
        //자기턴이면
        if(turnFlag)
        {
            Transform target = stateMachineClass.SearchAr();
            if(target)
            {
                turnFlag = !turnFlag;
                stateMachine.ChangeState<StateMove>();
            }


        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            turnFlag = true;
        }


    }

    public override void OnEnd()
    {

    }
}
