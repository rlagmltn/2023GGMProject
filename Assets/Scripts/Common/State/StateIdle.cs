using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State<ArFSM>
{
    public override void OnAwake()
    {

    }

    public override void OnStart()
    {

    }

    public override void OnUpdate(float deltaTime)
    {
        //�ڱ����̸�
        if(false)
        {
            Transform target = stateMachineClass.SearchAr();
            if(target)
            {
                stateMachine.ChangeState<StateMove>();
            }
        }
    }

    public override void OnEnd()
    {

    }
}
