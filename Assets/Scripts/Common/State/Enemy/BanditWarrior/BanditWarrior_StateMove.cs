using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_StateMove : StateMove
{
    public override void OnStart()
    {
        base.OnStart();
        stateMachine.ChangeState<BanditWarrior_StateIdle>();
    }
}
