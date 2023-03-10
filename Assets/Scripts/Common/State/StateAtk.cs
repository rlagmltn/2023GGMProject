using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAtk : State<ArFSM>
{
    public override void OnAwake()
    {
        
    }

    public override void OnStart()
    {
        Vector2 angle = stateMachineClass.SearchAr().position - stateMachineClass.transform.position;


    }

    public override void OnUpdate(float deltaTime)
    {
        
    }

    public override void OnEnd()
    {
        
    }
}
