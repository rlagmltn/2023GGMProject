using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMiddleBoss_StateMove : StateMove
{
    public override void OnStart()
    {
        base.OnStart();
        stateMachine.ChangeState<BanditMiddleBoss_StateIdle>();
    }
}
