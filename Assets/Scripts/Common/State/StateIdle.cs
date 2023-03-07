using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State<ArFSM>
{
    private bool turnFlag = true;

    public override void OnAwake()
    {

    }

    public override void OnStart()
    {
        Transform target = stateMachineClass.SearchAr();
        if (target)
        {
            Debug.Log("Find");
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        Debug.Log("Running");
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
    }

    public override void OnEnd()
    {

    }
}
