using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBoss_StateMove : StateMove
{
    public override void OnStart()
    {
        base.OnStart();
        stateMachine.ChangeState<BanditBoss_StateIdle>();
    }
}
