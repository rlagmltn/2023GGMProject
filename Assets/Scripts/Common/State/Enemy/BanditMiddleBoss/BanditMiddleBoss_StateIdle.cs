using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMiddleBoss_StateIdle : StateIdle
{
    private BanditMiddleBoss enemy;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<BanditMiddleBoss>();
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }
}
