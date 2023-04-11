using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher_StateMove : StateMove
{

    public override void OnStart()
    {
        base.OnStart();
        stateMachine.ChangeState<BanditWarrior_StateIdle>();
    }
}
