using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher_StateIdle : StateIdle
{
    private Enemy enemy;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<Enemy>();
    }

    public override void OnStart()
    {
        
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }

    public override void OnEnd()
    {
        
    }
}
