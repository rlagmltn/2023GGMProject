using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher_StateAtk : StateAtk
{
    private BanditArcher archer;

    public override void OnAwake()
    {
        archer = stateMachineClass.GetComponent<BanditArcher>();
    }

    public override void OnStart()
    {

    }

    public override void OnUpdate(float deltaTime)
    {
        
    }
}
